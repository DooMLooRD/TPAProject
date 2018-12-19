using System;
using System.IO;
using DBData;
using MEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DBUnitTests
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestLoggingWorking()
        {
            ILogger logger = new DatabaseLogger();
            logger.Log(new MessageStructure("test", "testOrigin", "testFilename", 3));

        }
    }
}
