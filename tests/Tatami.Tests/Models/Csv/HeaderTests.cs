namespace Tatami.Tests.Models.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HeaderTests
    {
        [TestMethod]
        public void TestGetParent()
        {
            // Arrange
            var root = new Header { Name = "Root", Depth = -1, From = 0, To = 30, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 11, },
                    new Header { Name = "Assertion", Depth = 0, From = 12, To = 30, }
                }
            };

            // Act & Assert
            Assert.AreEqual("Arrange", Header.GetParent(root, 1, 3).Name);
            Assert.AreEqual("Arrange", Header.GetParent(root, 1, 11).Name);
            Assert.AreEqual("Assertion", Header.GetParent(root, 1, 12).Name);
            Assert.AreEqual("Assertion", Header.GetParent(root, 1, 30).Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestGetParentWithInvalidIndex()
        {
            // Arrange
            var root = new Header
            {
                Name = "Root",
                Depth = -1,
                From = 0,
                To = 30,
                Children =
                    new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 11, },
                    new Header { Name = "Assertion", Depth = 0, From = 12, To = 30, }
                }
            };

            // Act & Assert
            Assert.AreEqual(null, Header.GetParent(root, 1, 31));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetParentWithInvalidValue()
        {
            // Arrange
            var root = new Header { Depth = 1, };

            // Act & Assert
            Header.GetParent(root, 0, 1);
        }

        [TestMethod]
        public void TestGetHeader()
        {
            // Arrange
            var root = new Header { Name = "Root", Depth = -1, From = 0, To = 30, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 11, Children =
                        new List<Header> {
                            new Header { Name = "HttpRequest Expected", Depth = 1, From = 1, To = 5, },
                            new Header { Name = "HttpRequest Actual", Depth = 1, From = 5, To = 11, },
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 12, To = 12, Children =
                        new List<Header> {
                            new Header { Name = "Uri", Depth = 1, From = 12, To = 12, },
                        }
                    }
                }
            };

            // Act & Assert
            Assert.AreEqual("HttpRequest Expected", Header.GetHeader(root, 1).Name);
            Assert.AreEqual("HttpRequest Actual", Header.GetHeader(root, 11).Name);
            Assert.AreEqual("Uri", Header.GetHeader(root, 12).Name);            
        }

        [TestMethod]
        public void TestGetHeaderWithDepth()
        {
            // Arrange
            var root = new Header { Name = "Root", Depth = -1, From = 0, To = 30, Children =
                new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 1, To = 11, Children =
                        new List<Header> {
                            new Header { Name = "HttpRequest Expected", Depth = 1, From = 1, To = 5, },
                            new Header { Name = "HttpRequest Actual", Depth = 1, From = 5, To = 11, },
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 12, To = 30, }
                }
            };

            // Act & Assert
            Assert.AreEqual("Arrange", Header.GetHeader(root, 1, 0).Name);
            Assert.AreEqual("HttpRequest Expected", Header.GetHeader(root, 1, 1).Name);
            Assert.AreEqual("Arrange", Header.GetHeader(root, 11, 0).Name);
            Assert.AreEqual("HttpRequest Actual", Header.GetHeader(root, 11, 1).Name);
            Assert.AreEqual("Assertion", Header.GetHeader(root, 12, 0).Name);
            Assert.AreEqual(null, Header.GetHeader(root, 30, 1));
        }

        [TestMethod]
        public void TestSearch()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 3, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "PathInfo", Depth = 3, From = 2, To = 3, },
                }
            };

            // Act & Assert
            Assert.AreEqual("BaseUri", Header.Search(httpRequestHeader, "BaseUri").Name);
            Assert.AreEqual(null, Header.Search(httpRequestHeader, "QueryStrings"));
        }

        [TestMethod]
        public void TestGetString()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 2, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "Fragment", Depth = 3, From = 2, To = 2, },
                }
            };
            var data = new List<string> { "case name", "ExpectedSite", null };

            // Act & Assert
            Assert.AreEqual("ExpectedSite", Header.GetString(httpRequestHeader, "BaseUri", data));
            Assert.AreEqual(null, Header.GetString(httpRequestHeader, "Fragment", data));
        }

        [TestMethod]
        public void TestGetBoolean()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 2, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "Fragment", Depth = 3, From = 2, To = 2, },
                }
            };

            var data = new List<string> { "case name", "ExpectedSite", "true" };

            // Act & Assert
            Assert.AreEqual(true, Header.GetBoolean(httpRequestHeader, "Fragment", data));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetBooleanWithInvalidValue()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 2, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "Fragment", Depth = 3, From = 2, To = 2, },
                }
            };

            var data = new List<string> { "case name", "ExpectedSite", "0" };

            // Act & Assert
            Assert.AreEqual(true, Header.GetBoolean(httpRequestHeader, "Fragment", data));
        }

        [TestMethod]
        public void TestGetNullableBoolean()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 3, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "PathInfo", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "Fragment", Depth = 3, From = 3, To = 3, },
                }
            };

            var data = new List<string> { "case name", "ExpectedSite", "true", null };

            // Act & Assert
            Assert.AreEqual(true, Header.GetNullableBoolean(httpRequestHeader, "PathInfo", data));
            Assert.AreEqual(null, Header.GetNullableBoolean(httpRequestHeader, "Fragment", data));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetNullableBooleanWithInvalidValue()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 2, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "Fragment", Depth = 3, From = 2, To = 2, },
                }
            };

            var data = new List<string> { "case name", "ExpectedSite", "0", null };

            // Act & Assert
            Header.GetNullableBoolean(httpRequestHeader, "Fragment", data);
        }

        [TestMethod]
        public void TestGetStringList()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 4, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "PathInfo", Depth = 3, From = 2, To = 3, },
                    new Header { Name = "Fragment", Depth = 3, From = 4, To = 4, },
                }
            };

            var data = new List<string> { "case name", "ExpectedSite", "pathInfo1", "pathInfo2", null };

            // Act & Assert
            var pathInfos = Header.GetStringList(httpRequestHeader, "PathInfo", data);
            Assert.AreEqual(2, pathInfos.Count());
            Assert.AreEqual("pathInfo1", pathInfos.ElementAt(0));
            Assert.AreEqual("pathInfo2", pathInfos.ElementAt(1));
            Assert.AreEqual(null, Header.GetString(httpRequestHeader, "Fragment", data));
            Assert.AreEqual(0, Header.GetStringList(httpRequestHeader, "NotFound", data).Count());
        }

        [TestMethod]
        public void TestGetStringListWithEmptyList()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 4, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "PathInfo", Depth = 3, From = 2, To = 3, },
                    new Header { Name = "Fragment", Depth = 3, From = 4, To = 4, },
                }
            };
            
            var data = new List<string> { "case name", "ExpectedSite", null, null, null };

            // Act & Assert
            Assert.AreEqual(0, Header.GetStringList(httpRequestHeader, "PathInfo", data).Count());
        }
        
        [TestMethod]
        public void TestGetNameValueCollection()
        {
            // Arrange
            var httpRequestHeader = new Header { Name = "HttpRequest Expected", Depth = 2, From = 1, To = 4, Children =
                new List<Header> {
                    new Header { Name = "BaseUri", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "Cookies", Depth = 3, From = 2, To = 3, Children =
                        new List<Header> {
                            new Header { Name = "Cookie1", Depth = 4, From = 2, To = 2, },
                            new Header { Name = "Cookie2", Depth = 4, From = 3, To = 3, }
                        }
                    },
                    new Header { Name = "Fragment", Depth = 3, From = 4, To = 4, },
                }
            };

            var data = new List<string> { "case name", "ExpectedSite", "Cookie1Value", "Cookie2Value", null };

            // Act & Assert
            var collection = Header.GetNameValueCollection(httpRequestHeader, "Cookies", data);
            Assert.AreEqual(2, collection.Count);
            Assert.AreEqual("Cookie1", collection.GetKey(0));
            Assert.AreEqual("Cookie1Value", collection.Get(0));
            Assert.AreEqual("Cookie2", collection.GetKey(1));
            Assert.AreEqual("Cookie2Value", collection.Get(1));
            Assert.AreEqual(0, Header.GetNameValueCollection(httpRequestHeader, "NotFound", data).Count);
            Assert.AreEqual(0, Header.GetNameValueCollection(httpRequestHeader, "Fragment", data).Count);
        }
    }
}
