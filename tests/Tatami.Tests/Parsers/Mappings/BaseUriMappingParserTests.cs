namespace Tatami.Tests.Parsers.Mappings
{
    using System;
    using System.Xml.Linq;
    using Tatami.Parsers.Mappings;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseUriMappingParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            const string Xml =
                @"<BaseUriMapping>
                    <Item Key='TargetSite'>yahoo.com</Item>
                    <Item Key='ExpectedApi'>api.yahoo.com</Item>
                </BaseUriMapping>";

            // Act
            var result = BaseUriMappingParser.Parse(XDocument.Parse(Xml).Root);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("yahoo.com", result["TargetSite"]);
            Assert.AreEqual("api.yahoo.com", result["ExpectedApi"]);
        }

        [TestMethod]
        public void TestParseWithNoItem()
        {
            // Arrange
            const string Xml = @"<BaseUriMapping />";

            // Act
            var result = BaseUriMappingParser.Parse(XDocument.Parse(Xml).Root);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseWithInvalidName()
        {
            // Arrange
            const string Xml = @"<XXX />";

            // Act
            BaseUriMappingParser.Parse(XDocument.Parse(Xml).Root);
        }
    }
}
