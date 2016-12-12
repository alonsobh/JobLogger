using System.Collections.Generic;
using System.Linq;

namespace JobLogger.BusinessComponent
{
    public class LogSeverityConfiguration
    {
        public void ExcludeAll()
        {
            _includedSeverities = new List<LogSeverity>();
        }

        public void ExcludeMessages() => ExcludeSeverity(LogSeverity.Message);
        public void ExcludeWarnings() => ExcludeSeverity(LogSeverity.Warning);
        public void ExcludeErrors() => ExcludeSeverity(LogSeverity.Error);
        private void ExcludeSeverity(LogSeverity logSeverity) { _includedSeverities = _includedSeverities.Where(l => l != logSeverity).ToList(); }

        public bool IsSeverityIncluded(LogSeverity logSeverity) => _includedSeverities.Any(s => s == logSeverity);

        private List<LogSeverity> _includedSeverities;

        public bool IsValid() => _includedSeverities.Any();

        public LogSeverityConfiguration()
        {
            _includedSeverities = new List<LogSeverity> { LogSeverity.Error, LogSeverity.Message, LogSeverity.Warning };
        }
    }
}