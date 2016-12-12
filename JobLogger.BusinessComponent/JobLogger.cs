using System;
using JobLogger.BusinessComponent.LogMessageWriter;

namespace JobLogger.BusinessComponent
{
    public class JobLogger
    {
        private readonly LogSeverityConfiguration _logSeverityConfiguration;
        private LogWriter[] _logWriters;
        public JobLogger()
        {
            _logSeverityConfiguration = new LogSeverityConfiguration();
            AddWriters();
            ValidateSeverityConfiguration();
        }

        public JobLogger(LogWriterConfiguration logWriterConfiguration, LogSeverityConfiguration logSeverityConfiguration)
        {
            _logSeverityConfiguration = logSeverityConfiguration;
            AddWriters(logWriterConfiguration);
            ValidateSeverityConfiguration();
        }

        private void AddWriters()
        {
            _logWriters = new LogWriterConfiguration().GetWriters();
        }

        private void AddWriters(LogWriterConfiguration logWriterConfiguration)
        {
            _logWriters = logWriterConfiguration.GetWriters();
        }

        private void ValidateSeverityConfiguration()
        {
            if (!_logSeverityConfiguration.IsValid())
                throw new Exception("Error or Warning or Message must be specified");
        }

        public void LogMessage(string message, LogSeverity logSeverity)
        {
            message = message.Trim();
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