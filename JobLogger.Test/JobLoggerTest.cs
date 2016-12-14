using System.Linq;
using JobLogger.BusinessComponent.LogMessageSeverity;
using JobLogger.BusinessComponent.LogMessageWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLogger.Test
{
    [TestClass]
    public class JobLoggerTest
    {
        private BusinessComponent.JobLogger ErrorLogger;
        private BusinessComponent.JobLogger WarningLogger;
        private BusinessComponent.JobLogger MessageLogger;
        private BusinessComponent.JobLogger JobLoggerAll;

        [TestInitialize]
        public void TestSetUp()
        {
            var logWriterConfiguration = GetDummyWriterConfiguration();
            ErrorLogger = new BusinessComponent.JobLogger(logWriterConfiguration, GetOnlyConfiguration(LogSeverity.Error));
            WarningLogger = new BusinessComponent.JobLogger(logWriterConfiguration, GetOnlyConfiguration(LogSeverity.Warning));
            MessageLogger = new BusinessComponent.JobLogger(logWriterConfiguration, GetOnlyConfiguration(LogSeverity.Message));
            JobLoggerAll = new BusinessComponent.JobLogger(logWriterConfiguration, new LogSeverityConfiguration());
        }

        private LogWriterConfiguration GetDummyWriterConfiguration()
        {
            var logWriterConfiguration = new LogWriterConfiguration();
            logWriterConfiguration.IgnoreAll();
            logWriterConfiguration.AddWriter(new LogWriterDummy());
            return logWriterConfiguration;
        }

        private LogSeverityConfiguration GetOnlyConfiguration(LogSeverity logSeverity)
        {
            var errorLogConfiguration = new LogSeverityConfiguration();
            errorLogConfiguration.ExcludeAll();
            errorLogConfiguration.Include(logSeverity);
            return errorLogConfiguration;
        }

        private static string ErrorText = "TestError";
        private static string MessageText => "TestMessage";
        private static string WarningText => "TestWarning";

        private int GetMessageCount(LogWriterDummy writer) => writer.GetMessageCount();
        private string GetMessage(LogWriterDummy writer, int index) => writer.GetMessage(index);
        private LogSeverity GetSeverity(LogWriterDummy writer, int index) => writer.GetSeverity(index);

        [TestMethod]
        public void LogEmptyOrNullMessage_LogShouldBeEmpty()
        {
            ErrorLogger.LogMessage(string.Empty, LogSeverity.Error);
            ErrorLogger.LogMessage(string.Empty, LogSeverity.Warning);
            ErrorLogger.LogMessage(string.Empty, LogSeverity.Message);
            ErrorLogger.LogMessage(null, LogSeverity.Error);
            ErrorLogger.LogMessage(null, LogSeverity.Warning);
            ErrorLogger.LogMessage(null, LogSeverity.Message);
            Assert.AreEqual(0, GetMessageCount(ErrorLogger.GetDummyWriter()));
        }

        [TestMethod]
        public void Add1ErrorWhenErrorLogOnly_ShouldLogError()
        {
            ErrorLogger.LogMessage(ErrorText, LogSeverity.Error);
            Assert.AreEqual(1, GetMessageCount(ErrorLogger.GetDummyWriter()));
            Assert.AreEqual(ErrorText, GetMessage(ErrorLogger.GetDummyWriter(), 0));
            Assert.AreEqual(LogSeverity.Error, GetSeverity(ErrorLogger.GetDummyWriter(), 0));
        }

        [TestMethod]
        public void Add2ErrorWhenErrorLogOnly_ShouldLogError()
        {
            ErrorLogger.LogMessage(ErrorText, LogSeverity.Error);
            ErrorLogger.LogMessage(ErrorText + "1", LogSeverity.Error);
            Assert.AreEqual(2, GetMessageCount(ErrorLogger.GetDummyWriter()));
            Assert.AreEqual(ErrorText, GetMessage(ErrorLogger.GetDummyWriter(), 0));
            Assert.AreEqual(LogSeverity.Error, GetSeverity(ErrorLogger.GetDummyWriter(), 0));
            Assert.AreEqual(ErrorText + "1", GetMessage(ErrorLogger.GetDummyWriter(), 1));
            Assert.AreEqual(LogSeverity.Error, GetSeverity(ErrorLogger.GetDummyWriter(), 1));
        }

        [TestMethod]
        public void AddMessageAndWarningWhenErrorLogOnly_LogShouldBeEmpty()
        {
            ErrorLogger.LogMessage(WarningText, LogSeverity.Warning);
            ErrorLogger.LogMessage(MessageText, LogSeverity.Message);
            Assert.AreEqual(0, GetMessageCount(ErrorLogger.GetDummyWriter()));
        }

        [TestMethod]
        public void Add1WarningWhenWarningLogOnly_ShouldLogWarning()
        {
            WarningLogger.LogMessage(WarningText, LogSeverity.Warning);
            Assert.AreEqual(1, GetMessageCount(WarningLogger.GetDummyWriter()));
            Assert.AreEqual(WarningText, GetMessage(WarningLogger.GetDummyWriter(), 0));
            Assert.AreEqual(LogSeverity.Warning, GetSeverity(WarningLogger.GetDummyWriter(), 0));
        }

        [TestMethod]
        public void Add2WarningWhenWarningLogOnly_ShouldLogWarning()
        {
            WarningLogger.LogMessage(WarningText, LogSeverity.Warning);
            WarningLogger.LogMessage(WarningText + "1", LogSeverity.Warning);
            Assert.AreEqual(2, GetMessageCount(WarningLogger.GetDummyWriter()));
            Assert.AreEqual(WarningText, GetMessage(WarningLogger.GetDummyWriter(), 0));
            Assert.AreEqual(LogSeverity.Warning, GetSeverity(WarningLogger.GetDummyWriter(), 0));
            Assert.AreEqual(WarningText + "1", GetMessage(WarningLogger.GetDummyWriter(), 1));
            Assert.AreEqual(LogSeverity.Warning, GetSeverity(WarningLogger.GetDummyWriter(), 1));
        }

        [TestMethod]
        public void AddMessageAndErrorWhenWarningLogOnly_LogShouldBeEmpty()
        {
            WarningLogger.LogMessage(WarningText, LogSeverity.Error);
            WarningLogger.LogMessage(MessageText, LogSeverity.Message);
            Assert.AreEqual(0, GetMessageCount(WarningLogger.GetDummyWriter()));
        }

        [TestMethod]
        public void Add1MessageWhenMessageLogOnly_ShouldLogMessage()
        {
            MessageLogger.LogMessage(MessageText, LogSeverity.Message);
            Assert.AreEqual(1, GetMessageCount(MessageLogger.GetDummyWriter()));
            Assert.AreEqual(MessageText, GetMessage(MessageLogger.GetDummyWriter(), 0));
            Assert.AreEqual(LogSeverity.Message, GetSeverity(MessageLogger.GetDummyWriter(), 0));
        }

        [TestMethod]
        public void Add2MessageWhenMessageLogOnly_ShouldLogMessage()
        {
            MessageLogger.LogMessage(MessageText, LogSeverity.Message);
            MessageLogger.LogMessage(MessageText + "1", LogSeverity.Message);
            Assert.AreEqual(2, GetMessageCount(MessageLogger.GetDummyWriter()));
            Assert.AreEqual(MessageText, GetMessage(MessageLogger.GetDummyWriter(), 0));
            Assert.AreEqual(LogSeverity.Message, GetSeverity(MessageLogger.GetDummyWriter(), 0));
            Assert.AreEqual(MessageText + "1", GetMessage(MessageLogger.GetDummyWriter(), 1));
            Assert.AreEqual(LogSeverity.Message, GetSeverity(MessageLogger.GetDummyWriter(), 1));
        }

        [TestMethod]
        public void AddErrorAndWarningWhenMessageLogOnly_LogShouldBeEmpty()
        {
            MessageLogger.LogMessage(WarningText, LogSeverity.Warning);
            MessageLogger.LogMessage(MessageText, LogSeverity.Error);
            Assert.AreEqual(0, GetMessageCount(MessageLogger.GetDummyWriter()));
        }

        [TestMethod]
        public void AddAllTypes_ShouldLog3()
        {
            JobLoggerAll.LogMessage(ErrorText, LogSeverity.Error);
            JobLoggerAll.LogMessage(MessageText, LogSeverity.Message);
            JobLoggerAll.LogMessage(WarningText, LogSeverity.Warning);
            Assert.AreEqual(3, GetMessageCount(JobLoggerAll.GetDummyWriter()));
        }

        //[TestMethod]
        //public void AddErrorAndWarningWhenMessageLogOnly_LogShouldBeEmpty()
        //{
        //    MessageLogger.LogMessage(WarningText, LogSeverity.Warning);
        //    MessageLogger.LogMessage(MessageText, LogSeverity.Error);
        //    Assert.AreEqual(0, GetMessageCount(MessageLogger.GetDummyWriter()));
        //}
    }

    internal static class TestExtentions
    {
        public static LogWriterDummy GetDummyWriter(this BusinessComponent.JobLogger jobLogger)
        {
            return (LogWriterDummy)jobLogger.GetWriters().First();
        }
    }
}