using System;
using System.Collections.Generic;
using JobLogger.BusinessComponent;

namespace JobLogger.Console
{
    internal class Program
    {
        public static void Main()
        {
            var jobLogger = CreateJobLoggerNone();
            var messages = new Dictionary<string, LogSeverity>
                {
                    { "MessageLog", LogSeverity.Message}
                    ,{ "ErrorLog", LogSeverity.Error}
                    ,{ "WarningLog", LogSeverity.Warning}

                };
            foreach (var m in messages)
            {
                Log(jobLogger, m.Key, m.Value);
            }
            System.Console.ReadLine();
        }

        private static BusinessComponent.JobLogger CreateJobLoggerNone()
        {
            var logWriterConfiguration = new LogWriterConfiguration();
            logWriterConfiguration.IgnoreAll();
            var logSeverityConfiguration = new LogSeverityConfiguration();
            var jobLogger = new BusinessComponent.JobLogger(logWriterConfiguration, logSeverityConfiguration);
            return jobLogger;
        }


        private static void Log(BusinessComponent.JobLogger jobLogger, string message, LogSeverity logSeverity)
        {
            jobLogger.LogMessage(message, logSeverity);
            //Console is kind of a Presentation Layer, JobLogger does not need to know where is it invoked from in order to log
            //Also a Console.WriteLine is not usefull from outside a Console Application
            LogInConsole(message, logSeverity);
        }

        private static void LogInConsole(string message, LogSeverity logSeverity)
        {
            var originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = GetColorFromLogSeverity(logSeverity);
            System.Console.WriteLine(DateTime.Now.ToShortDateString() + message);
            System.Console.ForegroundColor = originalColor;
        }

        private static ConsoleColor GetColorFromLogSeverity(LogSeverity logSeverity)
        {
            switch (logSeverity)
            {
                case LogSeverity.Message:
                    return ConsoleColor.White;
                case LogSeverity.Error:
                    return ConsoleColor.Red;
                case LogSeverity.Warning:
                    return ConsoleColor.Yellow;
                default: return System.Console.ForegroundColor;
            }
        }
    }
}