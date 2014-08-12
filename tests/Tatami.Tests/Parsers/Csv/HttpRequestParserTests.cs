namespace Tatami.Tests.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Parsers.Csv;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HttpRequestParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var data = new List<string>
            {
                null, "TargetSite", "Mozilla/5.0", "cookie0value", "cookie1value", "path", "london united kingdom", "00000", "bob", "japan"
            };

            var header = new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 9, Children = new List<Header> {
                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                    new Header { Name = "Headers", Depth = 2, From = 2, To = 2, Children = new List<Header> {
                            new Header { Name = "User-Agent", Depth = 3, From = 2, To = 2, },
                        }
                    },
                    new Header { Name = "Cookies", Depth = 2, From = 3, To = 4, Children = new List<Header> {
                            new Header { Name = "cookie0", Depth = 3, From = 3, To = 3, },
                            new Header { Name = "cookie1", Depth = 3, From = 4, To = 4, },
                        }
                    },
                    new Header { Name = "PathInfos", Depth = 2, From = 5, To = 6, },
                    new Header { Name = "QueryStrings", Depth = 2, From = 7, To = 8, Children = new List<Header> {
                            new Header { Name = "id", Depth = 3, From = 7, To = 7, },
                            new Header { Name = "name", Depth = 3, From = 8, To = 8, },
                        }
                    },
                    new Header { Name = "Fragment", Depth = 2, From = 9, To = 9, },
                }
            };

            // Act
            var result = HttpRequestParser.Parse(header, data, "Actual");

            // Assert
            Assert.AreEqual("Actual", result.Name);
            Assert.AreEqual("TargetSite", result.BaseUri);
            Assert.AreEqual(1, result.Headers.Count);
            Assert.AreEqual("Mozilla/5.0", result.Headers["User-Agent"]);
            Assert.AreEqual(2, result.Cookies.Count);
            Assert.AreEqual("cookie0value", result.Cookies["cookie0"]);
            Assert.AreEqual("cookie1value", result.Cookies["cookie1"]);
            Assert.AreEqual(2, result.PathInfos.Count());
            Assert.AreEqual("path", result.PathInfos.ElementAt(0));
            Assert.AreEqual(2, result.QueryStrings.Count);
            Assert.AreEqual("00000", result.QueryStrings["id"]);
            Assert.AreEqual("bob", result.QueryStrings["name"]);
            Assert.AreEqual("japan", result.Fragment);
        }

        [TestMethod]
        public void TestParseWithNoItem()
        {
            // Arrange
            var data = new List<string>
            {
                null, "TargetSite", 
            };

            var header = new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 1, Children = new List<Header> {
                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                }
            };

            // Act
            var result = HttpRequestParser.Parse(header, data, "Actual");

            // Assert
            Assert.AreEqual("Actual", result.Name);
            Assert.AreEqual("TargetSite", result.BaseUri);
            Assert.AreEqual(0, result.Headers.Count);
            Assert.AreEqual(0, result.Cookies.Count);
            Assert.AreEqual(0, result.PathInfos.Count());
            Assert.AreEqual(0, result.QueryStrings.Count);
            Assert.AreEqual(null, result.Fragment);
        }

        [TestMethod]
        public void TestParseWithData()
        {
            // Arrange
            var data = new List<string> { null, null, null };
            var header = new Header
            {
                Name = "HttpRequest Actual",
                Depth = 1,
                From = 1,
                To = 2,
                Children = new List<Header> {
                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                    new Header { Name = "Headers", Depth = 2, From = 2, To = 2, Children = new List<Header> {
                            new Header { Name = "User-Agent", Depth = 3, From = 2, To = 2, },
                        }
                    },
                }
            };

            // Act
            var result = HttpRequestParser.Parse(header, data, "Name");

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullHeader()
        {
            // Arrange
            var data = new List<string> { null, "TargetSite" };

            // Act
            HttpRequestParser.Parse(null, data, "Actual");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullRow()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest Actual", Depth = 1, From = 1, To = 2, Children = new List<Header> {
                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                    new Header { Name = "Headers", Depth = 2, From = 2, To = 2, Children = new List<Header> {
                            new Header { Name = "User-Agent", Depth = 3, From = 2, To = 2, },
                        }
                    },
                }
            };


            // Act
            HttpRequestParser.Parse(header, null, "Actual");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullName()
        {
            // Arrange
            var data = new List<string> { null, "TargetSite" };
            var header = new Header
            {
                Name = "HttpRequest Actual", Depth = 1, From = 1, To = 2, Children = new List<Header> {
                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                    new Header { Name = "Headers", Depth = 2, From = 2, To = 2, Children = new List<Header> {
                            new Header { Name = "User-Agent", Depth = 3, From = 2, To = 2, },
                        }
                    },
                }
            };

            // Act
            HttpRequestParser.Parse(header, data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithNoBaseUri()
        {
            // Arrange
            var data = new List<string> { null, "TargetSite" };
            var header = new Header
            {
                Name = "HttpRequest Actual", Depth = 1, From = 1, To = 1, Children = new List<Header> {
                    new Header { Name = "Headers", Depth = 2, From = 1, To = 1, Children = new List<Header> {
                            new Header { Name = "User-Agent", Depth = 3, From = 1, To = 1, },
                        }
                    },
                }
            };

            // Act
            HttpRequestParser.Parse(header, data, "Name");
        }
    }
}
