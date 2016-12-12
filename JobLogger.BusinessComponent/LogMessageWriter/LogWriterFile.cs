using System;

namespace JobLogger.BusinessComponent.LogMessageWriter
{
    public class LogWriterFile : LogWriter
    {
        public void Log(string message, LogSeverity logSeverity)
        {
            var l = string.Empty;
            if (!System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
                l = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");

            l = l + DateTime.Now.ToShortDateString() + message;

            System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l);
        }
    }
}