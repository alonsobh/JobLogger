using System.Collections.Generic;
using JobLogger.BusinessComponent.LogMessageSeverity;
using JobLogger.BusinessComponent.LogMessageWriter;

namespace JobLogger.Console
{
    internal class Program
    {
        public static void Main()
        {
            var messages = new Dictionary<string, LogSeverity>
                {
                    { "MessageLog", LogSeverity.Message},
                    { "ErrorLog", LogSeverity.Error},
                    { "WarningLog", LogSeverity.Warning}
                };
            foreach (var m in messages)
                CreateJobLoggerConsoleOnly().LogMessage(m.Key, m.Value);

            System.Console.ReadLine();
        }

        private static BusinessComponent.JobLogger CreateJobLoggerConsoleOnly()
        {
            var logWriterConfiguration = CreateLogWriterConfigurationForConsoleOnly();
            var logSeverityConfiguration = new LogSeverityConfiguration();
            return new BusinessComponent.JobLogger(logWriterConfiguration, logSeverityConfiguration);
        }

        private static LogWriterConfiguration CreateLogWriterConfigurationForConsoleOnly()
        {
            var logWriterConfiguration = new LogWriterConfiguration();
            logWriterConfiguration.IgnoreAll();
            logWriterConfiguration.IncludeConsole();
            return logWriterConfiguration;
        }
    }
}