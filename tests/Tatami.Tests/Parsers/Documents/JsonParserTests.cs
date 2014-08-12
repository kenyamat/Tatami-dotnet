namespace Tatami.Tests.Parsers.Documents
{
    using Tatami.Parsers.Documents;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JsonParserTests
    {
        [TestMethod]
        public void TestCtor()
        {
            // Arrange
            const string contents = @"{ 'name' : 'test' }";
            new JsonParser(contents);
        }
    }
}
