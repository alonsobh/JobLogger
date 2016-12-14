using System;
using JobLogger.BusinessComponent.LogMessageSeverity;

namespace JobLogger.BusinessComponent.LogMessageWriter
{
    public class LogWriterConsole : LogWriter
    {
        public void Log(string message, LogSeverity logSeverity)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = GetColorFromLogSeverity(logSeverity);
            Console.WriteLine(DateTime.Now.ToShortDateString() + message);
            Console.ForegroundColor = originalColor;
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
                default: return Console.ForegroundColor;
            }
        }
    }
}