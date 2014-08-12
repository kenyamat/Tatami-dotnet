namespace Tatami.Tests.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Parsers.Csv.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentAssertionItemParserTests
    {
        [TestMethod]
        public void TestParseWithHavingAllValues()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "key", "value", "query", "attribute", "pattern", "format", "en-US"
            };

            // Arrange
            var header = new Header { Name = "Expected", Depth = 1, From = 0, To = 7, Children =
                new List<Header> {
                    new Header { Name = "Key", Depth = 2, From = 1, To = 1, },
                    new Header { Name = "Value", Depth = 2, From = 2, To = 2, },
                    new Header { Name = "Query", Depth = 2, From = 3, To = 3, },
                    new Header { Name = "Attribute", Depth = 2, From = 4, To = 4, },
                    new Header { Name = "Pattern", Depth = 2, From = 5, To = 5, },
                    new Header { Name = "Format", Depth = 2, From = 6, To = 6, },
                    new Header { Name = "FormatCulture", Depth = 2, From = 7, To = 7, },
                }
            };

            // Act
            var result = ContentAssertionItemParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual("key", result.Key);
            Assert.AreEqual("value", result.Value);
            Assert.AreEqual("query", result.Query);
            Assert.AreEqual("attribute", result.Attribute);
            Assert.AreEqual("pattern", result.Pattern);
            Assert.AreEqual("format", result.Format);
            Assert.AreEqual("en-US", result.FormatCulture);
        }

        [TestMethod]
        public void TestParseWithNoAttributes()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, null, null, null, null, null, null, null, 
            };

            // Arrange
            var header = new Header { Name = "Expected", Depth = 1, From = 0, To = 7, Children =
                new List<Header> {
                    new Header { Name = "Key", Depth = 2, From = 1, To = 1, },
                    new Header { Name = "Value", Depth = 2, From = 2, To = 2, },
                    new Header { Name = "Query", Depth = 2, From = 3, To = 3, },
                    new Header { Name = "Attribute", Depth = 2, From = 4, To = 4, },
                    new Header { Name = "Pattern", Depth = 2, From = 5, To = 5, },
                    new Header { Name = "Format", Depth = 2, From = 6, To = 6, },
                    new Header { Name = "FormatCulture", Depth = 2, From = 7, To = 7, },
                }
            };

            // Act
            var result = ContentAssertionItemParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual(null, result.Key);
            Assert.AreEqual(null, result.Value);
            Assert.AreEqual(null, result.Query);
            Assert.AreEqual(null, result.Attribute);
            Assert.AreEqual(null, result.Pattern);
            Assert.AreEqual(null, result.Format);
            Assert.AreEqual(null, result.FormatCulture);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithInvalidFormatCulture()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "AA"
            };

            // Arrange
            var header = new Header { Name = "Expected", Depth = 1, From = 0, To = 6, Children =
                new List<Header> {
                    new Header { Name = "FormatCulture", Depth = 2, From = 1, To = 1,
                    },
                }
            };

            // Act
            ContentAssertionItemParser.Parse(header, csvRow);
        }
    }
}
