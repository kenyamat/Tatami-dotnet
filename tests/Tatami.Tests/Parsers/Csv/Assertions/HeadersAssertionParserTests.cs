namespace Tatami.Tests.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;
    using Tatami.Parsers.Csv.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HeadersAssertionParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "text/html", "15:29:20",
            };

            // Arrange
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "Headers", Depth = 2, From = 1, To = 2, Children =
                        new List<Header> { 
                            new Header { Name = "Content-Type", Depth = 3, From = 1, To = 1, },
                            new Header { Name = "Last-Modified", Depth = 3, From = 2, To = 2, }
                        }
                    }
                }
            };

            // Act
            var result = HeadersAssertionParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Content-Type", ((HeaderAssertion)result.ElementAt(0)).Key);
            Assert.AreEqual("text/html", ((HeaderAssertion)result.ElementAt(0)).Value);
            Assert.AreEqual("Last-Modified", ((HeaderAssertion)result.ElementAt(1)).Key);
            Assert.AreEqual("15:29:20", ((HeaderAssertion)result.ElementAt(1)).Value);
        }

        [TestMethod]
        public void TestParseWithNoValue()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, null, null
            };

            // Arrange
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "Headers", Depth = 2, From = 1, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Content-Type", Depth = 3, From = 1, To = 1 },
                            new Header { Name = "Last-Modified", Depth = 3, From = 2, To = 2, }
                        }
                    }
                }
            };

            // Act
            var result = HeadersAssertionParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual(null, result);
        }
    }
}
