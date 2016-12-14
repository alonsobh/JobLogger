using System.Collections.Generic;
using System.Linq;

namespace JobLogger.BusinessComponent.LogMessageWriter
{
    public class LogWriterConfiguration
    {
        public bool LogToFile { get; set; } = true;
        public bool LogToDataBase { get; set; } = true;
        public bool LogToConsole { get; set; }

        public void IgnoreAll()
        {
            LogToFile = false;
            LogToDataBase = false;
        }

        public void IncludeConsole() => LogToConsole = true;

        public void AddWriter(LogWriter logWriter) => AddIfNotExists(_logWriters, logWriter);

        private readonly List<LogWriter> _logWriters = new List<LogWriter>();

        public LogWriter[] GetWriters()
        {
            var logWriters = new List<LogWriter>();

            if (LogToDataBase)
                AddIfNotExists(logWriters, new LogWriterDataBase());
            if (LogToFile)
                AddIfNotExists(logWriters, new LogWriterFile());
            if (LogToConsole)
                AddIfNotExists(logWriters, new LogWriterConsole());

            logWriters.AddRange(_logWriters);
            return logWriters.ToArray();
        }

        private void AddIfNotExists(ICollection<LogWriter> logWriters, LogWriter logWriter)
        {
            if (logWriters.All(l => l.GetType() != logWriter.GetType()))
                logWriters.Add(logWriter);
        }
    }
}