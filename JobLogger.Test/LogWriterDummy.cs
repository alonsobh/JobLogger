using System.Collections.Generic;
using System.Linq;
using JobLogger.BusinessComponent.LogMessageSeverity;
using JobLogger.BusinessComponent.LogMessageWriter;

namespace JobLogger.Test
{
    internal class LogWriterDummy : LogWriter
    {
        private readonly Dictionary<string, LogSeverity> _messages = new Dictionary<string, LogSeverity>();

        public void Log(string message, LogSeverity logSeverity)
        {
            _messages.Add(message, logSeverity);
        }

        public int GetMessageCount() => _messages.Count;

        public string GetMessage(int index) => _messages.Keys.AsEnumerable().Skip(index).First();

        public LogSeverity GetSeverity(int index) => _messages.Values.AsEnumerable().Skip(index).First();
    }
}