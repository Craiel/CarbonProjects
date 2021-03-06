﻿namespace ColladaMC
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using CarbonCore.Processing.Resource.Model;
    using CarbonCore.Processing.Source.Collada;
    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;

    using ColladaMC.Contracts;
    using ColladaMC.Logic;
    using ColladaMC.Logic.Enums;

    using Cyotek.Data.Nbt;

    using SharpDX;

    public class ColladaMinecraft : ConsoleApplicationBase, IColladaMinecraft
    {
        private const int MaxHeight = 240;
        
        private readonly ICommandLineArguments arguments;
        private readonly IDictionary<Block, Block> blocks;

        private CarbonFile sourceFile;

        private ColladaInfo modelInfo;
        private ModelResourceGroup model;
        private BoundingBox modelBoundingBox;
        private BoundingBox outputBoundingBox;
        private Vector3 boundingBoxSize;
        private Vector3 zeroOffset;
        private float scaleFactor = 10.0f;

        private ProcessingMode mode;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public ColladaMinecraft(IFactory factory)
            : base(factory)
        {
            this.arguments = factory.Resolve<ICommandLineArguments>();

            this.modelBoundingBox = new BoundingBox(new Vector3(0), new Vector3(0));

            this.blocks = new Dictionary<Block, Block>();

            this.mode = ProcessingMode.ColladaToSchematic;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override string Name => "ColladaMC";

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override void StartFinished()
        {
            if (!this.arguments.ParseCommandLineArguments())
            {
                this.arguments.PrintArgumentUse();
                return;
            }

            this.DoProcess();
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.arguments.Define("s", "sourceFile", x => this.sourceFile = new CarbonFile(x));
            definition.Required = true;
            definition.RequireArgument = true;
            definition.Description = "The source file to process";

            definition = this.arguments.Define("m", "mode", x => this.mode = (ProcessingMode)Enum.Parse(typeof(ProcessingMode), x));
            definition.RequireArgument = true;
            definition.Description = "The processing mode";

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoProcess()
        {
            if (this.sourceFile == null || !this.sourceFile.Exists)
            {
                this.arguments.PrintArgumentUse();
                return;
            }

            switch (this.mode)
            {
                    case ProcessingMode.ColladaToSchematic:
                    {
                        this.ConvertColladaToSchematic();
                        break;
                    }

                    case ProcessingMode.SchematicToJson:
                    {
                        this.ConvertSchematicToJson();
                        break;
                    }

                    case ProcessingMode.JsonToSchematic:
                    {
                        Diagnostic.Error("Json to Schematic is not implemented!");
                        break;
                    }
            }
        }

        private void ConvertSchematicToJson()
        {
            using (var stream = this.sourceFile.OpenRead())
            {
                var reader = new BinaryTagReader(stream, NbtOptions.None);
                var tag = reader.Read();
                Diagnostic.Info("Read: {0}", tag.ToString());
            }
        }

        private void ConvertColladaToSchematic()
        {
            CarbonFile targetFile = this.sourceFile.ChangeExtension(".schematic");

            using (new ProfileRegion("Load Model Info"))
            {
                this.modelInfo = new ColladaInfo(this.sourceFile);
                System.Diagnostics.Trace.TraceInformation("Model has {0} meshes", this.modelInfo.MeshInfos.Count);
            }

            using (new ProfileRegion("Process collada"))
            {
                this.model = ColladaProcessor.Process(this.modelInfo, null, null);
            }

            if (this.model.Groups == null)
            {
                Diagnostic.Error("Model has no group information");
                return;
            }

            using (new ProfileRegion("Updating bounding box"))
            {
                this.UpdateBoundingBox();
            }

            using (new ProfileRegion("Converting mesh"))
            {
                this.ConvertMesh();
            }

            this.WriteSchematic(targetFile);
        }
        
        private void WriteSchematic(CarbonFile targetFile)
        {
            var sizeVector = this.outputBoundingBox.Maximum - this.outputBoundingBox.Minimum + new Vector3(1);
            int maxAddress = (int)(sizeVector.Y * sizeVector.Z * sizeVector.X);
            using (var file = File.Open(targetFile.GetPath(), FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                var writer = new BinaryTagWriter(file);
                var compound = new TagCompound("Schematic");
                compound.Value.Add(new TagShort("Height", (short)sizeVector.Y));
                compound.Value.Add(new TagShort("Length", (short)sizeVector.Z));
                compound.Value.Add(new TagShort("Width", (short)sizeVector.X));
                compound.Value.Add(new TagList("Entities"));
                compound.Value.Add(new TagList("TileEntities"));
                compound.Value.Add(new TagString("Materials", "Alpha"));

                var blockArray = new byte[maxAddress];
                var biomeArray = new byte[(int)(sizeVector.Z * sizeVector.X)];
                var dataArray = new byte[maxAddress];

                foreach (Block block in this.blocks.Keys)
                {
                    // (Y×length + Z)×width + X
                    int address = this.GetInternalAddress(sizeVector, new Vector3(block.X, block.Y, block.Z));
                    blockArray[address] = 41; // Gold block for testing
                    dataArray[address] = 0;
                }

                /*int count = 0;
                for (var x = 0; x < sizeVector.X; x++)
                {
                    for (var y = 0; y < sizeVector.Y; y++)
                    {
                        for (var z = 0; z < sizeVector.Z; z++)
                        {
                            int test = this.GetInternalAddress(sizeVector, new Vector3(x, y, z));
                            blockArray[test] = 23;
                            count++;
                        }
                    }
                }*/

                /*for (int i = 0; i < sizeVector.Y; i++)
                {
                    //blockArray[i] = 1;
                    int test = this.GetInternalAddress(sizeVector, new Vector3(i, i, i));
                    blockArray[test] = 1;
                }*/

                compound.Value.Add(new TagByteArray("Data", dataArray));
                compound.Value.Add(new TagByteArray("Biomes", biomeArray));
                compound.Value.Add(new TagByteArray("Blocks", blockArray));
                writer.Write(compound);
            }
        }

        private int GetInternalAddress(Vector3 sizeVector, Vector3 address)
        {
            //Vector3 internalVector = address / sizeVector;
            return (int)((address.Y * (sizeVector.Z * sizeVector.X)) + (address.Z * sizeVector.X) + address.X);
            //return (int)((address.Y*sizeVector.Z+address.Z)*sizeVector.X+address.X);
            //return (int)(address.X + (address.Y * sizeVector.Y + address.Z) * sizeVector.X); //x + (y * Height + z) * Width
            //return (int)((address.Y * sizeVector.Z + address.Z) * sizeVector.X + address.X);
            //return (int)((((internalVector.Y * (int)sizeVector.Z) + internalVector.Z) * (int)sizeVector.X) + internalVector.X);
        }

        private void UpdateBoundingBox()
        {
            foreach (ModelResourceGroup @group in this.model.Groups)
            {
                foreach (ModelResource modelResource in @group.Models)
                {
                    if (modelResource.BoundingBox == null)
                    {
                        modelResource.CalculateBoundingBox();
                    }

                    this.modelBoundingBox = BoundingBox.Merge(
                        this.modelBoundingBox, modelResource.BoundingBox.Value);
                }
            }

            this.zeroOffset = new Vector3(0) - this.modelBoundingBox.Minimum;
            this.boundingBoxSize = this.modelBoundingBox.Maximum - this.modelBoundingBox.Minimum;
            if (this.boundingBoxSize.Y > MaxHeight)
            {
                this.scaleFactor = MaxHeight / this.boundingBoxSize.Y;
            }
        }

        private void ConvertMesh()
        {
            System.Diagnostics.Trace.TraceInformation("Converting mesh: ");
            foreach (ModelResourceGroup @group in this.model.Groups)
            {
                System.Diagnostics.Trace.TraceInformation("  # Group {0}", @group.Name);
                foreach (ModelResource modelResource in @group.Models)
                {
                    System.Diagnostics.Trace.TraceInformation("    - {0}", modelResource.Name);
                    foreach (ModelResourceElement element in modelResource.Elements)
                    {
                        this.AddBlock(modelResource.Name, element);
                    }
                }
            }
        }
        
        private void AddBlock(string modelName, ModelResourceElement element)
        {
            Vector3 zeroBasedPosition = (element.Position + this.zeroOffset) * this.scaleFactor;
            var block = new Block
                            {
                                X = (int)Math.Round(zeroBasedPosition.X),
                                Y = (int)Math.Round(zeroBasedPosition.Y),
                                Z = (int)Math.Round(zeroBasedPosition.Z)
                            };

            if (this.blocks.ContainsKey(block))
            {
                var existing = this.blocks[block];
                if (existing.Materials.Contains(modelName))
                {
                    return;
                }

                this.blocks[block].Materials.Add(modelName);
            }
            else
            {
                block.Materials.Add(modelName);
                this.blocks.Add(block, block);

                var pos = new Vector3(block.X, block.Y, block.Z);
                this.outputBoundingBox = BoundingBox.Merge(this.outputBoundingBox, new BoundingBox(pos, pos));
            }
        }
    }
}
