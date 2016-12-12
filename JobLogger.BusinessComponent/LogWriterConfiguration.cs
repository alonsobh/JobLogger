using System.Collections.Generic;
using System.Linq;
using JobLogger.BusinessComponent.LogMessageWriter;

namespace JobLogger.BusinessComponent
{
    public class LogWriterConfiguration
    {
        public bool LogToFile { get; set; } = true;
        public bool LogToDataBase { get; set; } = true;

        public void IgnoreAll()
        {
            LogToFile = false;
            LogToDataBase = false;
        }

        private readonly List<LogWriter> _logWriters = new List<LogWriter>();

        public void AddWriter(LogWriter logWriter) => AddIfNotExists(_logWriters, logWriter);

        public LogWriter[] GetWriters()
        {
            var logWriters = new List<LogWriter>();
            if (LogToDataBase)
                AddIfNotExists(logWriters,new LogWriterDataBase());
            if (LogToFile)
                AddIfNotExists(logWriters, new LogWriterFile());
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