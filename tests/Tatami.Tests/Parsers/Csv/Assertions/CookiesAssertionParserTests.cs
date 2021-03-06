namespace Tatami.Tests.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;
    using Tatami.Parsers.Csv.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CookiesAssertionParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var csvRow = new List<string> { null, "001", "002" };

            // Arrange
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 2, Children = new List<Header>
                {
                    new Header { Name = "Cookies", Depth = 2, From = 1, To = 2, Children = new List<Header>
                        {
                            new Header { Name = "Location", Depth = 3, From = 1, To = 1, },
                            new Header { Name = "Degree", Depth = 3, From = 2, To = 2, }
                        }
                    }
                }
            };

            // Act
            var result = CookiesAssertionParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Location", ((CookieAssertion)result.ElementAt(0)).Key);
            Assert.AreEqual("001", ((CookieAssertion)result.ElementAt(0)).Value);
            Assert.AreEqual("Degree", ((CookieAssertion)result.ElementAt(1)).Key);
            Assert.AreEqual("002", ((CookieAssertion)result.ElementAt(1)).Value);
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
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 2, Children = new List<Header>
                {
                    new Header { Name = "Cookies", Depth = 2, From = 1, To = 2, Children = new List<Header>
                        {
                            new Header { Name = "Location", Depth = 3, From = 1, To = 1, },
                            new Header { Name = "Degree", Depth = 3, From = 2, To = 2, }
                        }
                    }
                }
            };

            // Act
            var result = CookiesAssertionParser.Parse(header, csvRow);

            Assert.AreEqual(null, result);
        }
    }
}
