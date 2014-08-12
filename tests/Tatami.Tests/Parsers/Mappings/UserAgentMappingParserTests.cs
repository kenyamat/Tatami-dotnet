namespace Tatami.Tests.Parsers.Mappings
{
    using System;
    using System.Xml.Linq;
    using Tatami.Parsers.Mappings;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserAgentMappingParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            const string Xml =
                @"<UserAgentMapping>
                    <Item Key='IE11'>Mozilla IE11</Item>
                    <Item Key='IE10'>Mozilla IE10</Item>
                </UserAgentMapping>";

            // Act
            var result = UserAgentMappingParser.Parse(XDocument.Parse(Xml).Root);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Mozilla IE11", result["IE11"]);
            Assert.AreEqual("Mozilla IE10", result["IE10"]);
        }

        [TestMethod]
        public void TestParseWithNoItem()
        {
            // Arrange
            const string Xml = @"<UserAgentMapping />";

            // Act
            var result = UserAgentMappingParser.Parse(XDocument.Parse(Xml).Root);

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
            UserAgentMappingParser.Parse(XDocument.Parse(Xml).Root);
        }
    }
}
