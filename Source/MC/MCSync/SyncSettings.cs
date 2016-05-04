namespace CarbonCore.Applications.MCSync
{
    using CarbonCore.ContentServices.Logic.Attributes;
    using CarbonCore.ContentServices.Logic.DataEntryLogic;

    [DataEntry]
    public class SyncSettings : SmartDataEntry
    {
        public SyncSettings()
        {
            this.Version = 1;
        }

        [DataElement]
        public int Version { get; set; }
    }
}
