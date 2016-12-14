using JobLogger.BusinessComponent.LogMessageSeverity;
using JobLogger.BusinessComponent.LogMessageWriter;

namespace JobLogger.BusinessComponent
{
    public class JobLogger
    {
        private readonly LogSeverityConfiguration _logSeverityConfiguration;
        private LogWriter[] _logWriters;

        public JobLogger(LogWriterConfiguration logWriterConfiguration, LogSeverityConfiguration logSeverityConfiguration)
        {
            _logSeverityConfiguration = logSeverityConfiguration;
            AddWriters(logWriterConfiguration);
        }

        public LogWriter[] GetWriters() => _logWriters;

        private void AddWriters(LogWriterConfiguration logWriterConfiguration)
        {
            _logWriters = logWriterConfiguration.GetWriters();
        }

        public void LogMessage(string message, LogSeverity logSeverity)
        {
            message = message?.Trim();
            if (string.IsNullOrEmpty(message))
                return;

            if (!_logSeverityConfiguration.IsSeverityIncluded(logSeverity))
                return;

            foreach (var l in _logWriters)
            {
                l.Log(message, logSeverity);
            }
        }
    }
}