namespace Tatami.Tests.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Parsers.Csv.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StatusCodeAssertionParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var csvRow = new List<string> { null, "200" };

            // Arrange
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 1, Children =
                new List<Header> {
                    new Header { Name = "StatusCode", Depth = 2, From = 1, To = 1, }
                }
            };

            // Act
            var result = StatusCodeAssertionParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual("200", ((StatusCodeAssertion)result.ElementAt(0)).Value);
        }

        [TestMethod]
        public void TestParseWithNoValue()
        {
            // Arrange
            var csvRow = new List<string> { null, null };

            // Arrange
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 1, Children =
                new List<Header> {
                    new Header { Name = "StatusCode", Depth = 2, From = 1, To = 1, }
                }
            };

            // Act
            var result = StatusCodeAssertionParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithInvalidCode()
        {
            // Arrange
            var csvRow = new List<string> { null, "aa" };

            // Arrange
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 1, Children =
                new List<Header> {
                    new Header { Name = "StatusCode", Depth = 2, From = 1, To = 1, }
                }
            };

            // Act
            StatusCodeAssertionParser.Parse(header, csvRow);
        }
    }
}
