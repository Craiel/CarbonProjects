namespace SMC.SourceLib.Contracts
{
    public interface IProcessLineData
    {
        string Name { get; set; }

        TX GetLineData<TX>() where TX : class, ILine;
    }
}
