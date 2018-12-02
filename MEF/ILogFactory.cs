namespace MEF
{
    public interface ILogFactory
    {
        void AddLogger(ILogger logger);
        void RemoveLogger(ILogger logger);
        void Log(MessageStructure message, LogCategoryEnum level = LogCategoryEnum.Information);

    }
}
