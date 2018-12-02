namespace MEF
{
    public interface ILogger
    {
        void Log(MessageStructure message, LogCategoryEnum level);
    }
}
