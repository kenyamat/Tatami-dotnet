namespace Tatami.Tests.Parsers
{
    using System;
    using Tatami.Parsers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WrongFileFormatExceptionTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            new WrongFileFormatException();
            new WrongFileFormatException("file");
            new WrongFileFormatException("file", new Exception("inner exception"));
        }
    }
}
