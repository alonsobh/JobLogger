namespace JobLogger.BusinessComponent.LogMessageWriter
{
    public interface LogWriter
    {
        void Log(string message, LogSeverity logSeverity);
    }
}