namespace Tatami.Tests.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Parsers.Csv;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestCaseParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var header = new Header { Name = "Root", Depth = -1, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 1, Children =
                                new List<Header> {
                                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                                }
                            }
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Uri", Depth = 1, From = 2, To = 2, },
                        }
                    }
                }
            };


            var data = new List<List<string>>
            {
                new List<string> { "test case 1", "BaseUri", "/local" }
            };

            // Act
            var result = TestCaseParser.Parse(header, data, null);

            // Assert
            Assert.AreEqual("test case 1", result.Name);
            Assert.AreEqual(1, result.Assertions.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullRootHeader()
        {
            // Arrange
            var data = new List<List<string>>
            {
                new List<string> { "test case 1", "BaseUri", "/local" }
            };

            // Act
            TestCaseParser.Parse(null, data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullData()
        {
            // Arrange
            var header = new Header { Name = "Root", Depth = -1, From = 0,To = 2, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 1, Children =
                                new List<Header> {
                                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                                }
                            }
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Uri", Depth = 1, From = 2, To = 2, },
                        }
                    }
                }
            };
            
            // Act
            TestCaseParser.Parse(header, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithEmptyData()
        {
            // Arrange
            var header = new Header { Name = "Root", Depth = -1, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 1, Children =
                                new List<Header> {
                                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                                }
                            }
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Uri", Depth = 1, From = 2, To = 2, },
                        }
                    }
                }
            };

            // Arrange
            var data = new List<List<string>>();

            // Act
            TestCaseParser.Parse(header, data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithEmptyDataRow()
        {
            // Arrange
            var header = new Header { Name = "Root", Depth = -1, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 1, Children =
                                new List<Header> {
                                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                                }
                            }
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Uri", Depth = 1, From = 2, To = 2, },
                        }
                    }
                }
            };

            // Arrange
            var data = new List<List<string>>
            {
                new List<string>(),
            };

            // Act
            TestCaseParser.Parse(header, data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithEmptyFirstData()
        {
            // Arrange
            var header = new Header { Name = "Root", Depth = -1, From = 0, To = 2, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 1, Children =
                        new List<Header> {
                            new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 1, Children =
                                new List<Header> {
                                    new Header { Name = "BaseUri", Depth = 2, From = 1, To = 1, },
                                }
                            }
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Uri", Depth = 1, From = 2, To = 2, },
                        }
                    }
                }
            };

            // Arrange
            var data = new List<List<string>>
            {
                new List<string> { string.Empty }
            };

            // Act
            TestCaseParser.Parse(header, data, null);
        }
    }
}
