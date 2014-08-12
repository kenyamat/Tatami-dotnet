namespace Tatami.Tests.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Parsers.Csv;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ArrangesParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var data = new List<string> { null, "ExpectedSite", "TargetSite" };
            var header = new Header { Name = "Arrange", Depth = 0, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "HttpRequest Expected", Depth = 1, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                        }
                    },
                    new Header { Name = "HttpRequest Actual", Depth = 1, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "BaseUri", Depth = 2, From = 2, To = 2, },
                        }
                    }
                }
            }; 

            // Act
            var result = ArrangesParser.Parse(header, data);

            // Assert
            Assert.IsTrue(result.Expected != null);
            Assert.IsTrue(result.Actual != null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullArrangeHeader()
        {
            // Arrange
            var data = new List<string> { null, "ExpectedSite", "TargetSite" };

            // Act
            ArrangesParser.Parse(null, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseWithNullChildren()
        {
            // Arrange
            var data = new List<string> { null, "ExpectedSite", "TargetSite" };
            var header = new Header { Name = "Arrange", Depth = 0, From = 0, To = 2, Children = null };

            // Act
            ArrangesParser.Parse(header, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseWithEmptyChildren()
        {
            // Arrange
            var data = new List<string> { null, "ExpectedSite", "TargetSite" };
            var header = new Header
            {
                Name = "Arrange",
                Depth = 0,
                From = 0,
                To = 2,
                Children = new List<Header>()
            };

            // Act
            ArrangesParser.Parse(header, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullData()
        {
            // Arrange
            var header = new Header { Name = "Arrange", Depth = 0, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "HttpRequest Expected", Depth = 1, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                        }
                    },
                    new Header { Name = "HttpRequest Actual", Depth = 1, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "BaseUri", Depth = 2, From = 2, To = 2, },
                        }
                    }
                }
            }; 

            // Act
            ArrangesParser.Parse(header, null);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithInvalidHeaderName()
        {
            // Arrange
            var data = new List<string> { null, "ExpectedSite", "TargetSite" };
            var header = new Header { Name = "Arrange", Depth = 0, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "XXXXXX", Depth = 1, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                        }
                    },
                }
            };

            // Act
            ArrangesParser.Parse(header, data);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithNoActualHeader()
        {
            // Arrange
            var data = new List<string> { null, "ExpectedSite", "TargetSite" };
            var header = new Header { Name = "Arrange", Depth = 0, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "HttpRequest Expected", Depth = 1, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                        }
                    },
                }
            };

            // Act
            ArrangesParser.Parse(header, data);
        }
    }
}
