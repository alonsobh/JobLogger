using System;

namespace JobLogger.BusinessComponent.LogMessageWriter
{
    public class LogWriterDataBase : LogWriter
    {
        public void Log(string message, LogSeverity logSeverity)
        {
            var connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();
            var t = GetSqlTypeFromSeverity(logSeverity);
            var command = new System.Data.SqlClient.SqlCommand("Insert into Log Values('" + message + "', " + t + ")");
            command.ExecuteNonQuery();
        }

        private int GetSqlTypeFromSeverity(LogSeverity logSeverity)
        {
            switch (logSeverity)
            {
                case LogSeverity.Message:
                    return 1;
                case LogSeverity.Error:
                    return 2;
                case LogSeverity.Warning:
                    return 3;
                default: return 0;
            }
        }
    }
}