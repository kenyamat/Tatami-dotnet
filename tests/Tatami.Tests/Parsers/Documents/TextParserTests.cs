namespace Tatami.Tests.Parsers.Documents
{
    using System;
    using Tatami.Parsers.Documents;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TextParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestExistsNode()
        {
            // Arrange
            const string contents = @"test";
            var doc = new TextParser(contents);
            
            // Act
            doc.ExistsNode("", null);
        }

        [TestMethod]
        public void TestGetDocumentValue()
        {
            // Arrange
            const string contents = @"test";
            var doc = new TextParser(contents);

            // Act
            var result = doc.GetDocumentValue("", null);

            // Arrange
            Assert.AreEqual(result, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestGetDocumentValues()
        {
            // Arrange
            const string contents = @"test";
            var doc = new TextParser(contents);

            // Act
            doc.GetDocumentValues("//name", null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestTestSchemaWithXsd()
        {
            // Arrange
            const string contents = @"test";
            var doc = new TextParser(contents);

            // Act
            doc.TestSchemaWithXsd(null);
        }
    }
}
