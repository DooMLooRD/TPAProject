using System;
using System.IO;
using FileData;
using MEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XMLUnitTests
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestLoggingWorking()
        {
            if (File.Exists("logger.log"))
                File.Delete("logger.log");

            ILogger logger = new FileLogger();
            logger.Log(new MessageStructure("test", "testOrigin", "testFilename", 3));

            Assert.IsTrue(File.Exists("logger.log"));
        }
    }
}
