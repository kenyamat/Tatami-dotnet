namespace Tatami.Tests.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources.Fakes;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Parsers.Csv.Assertions;
    using Microsoft.QualityTools.Testing.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AssertionsParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var csvRow = new List<List<string>>
            {
                new List<string> { null, "/local", null, null, null, null, null, null, null, null },
                new List<string> { null, null, "200", null, null, null, null, null, null, null },
                new List<string> { null, null, null, "text/html", "15:29:20", null, null, null, null, null },
                new List<string> { null, null, null, null, null, "001", "C", null, null, null },
                new List<string> { null, null, null, null, null, null, null, "XsdFile", null, null },
                new List<string> { null, null, null, null, null, null, null, null, "DateTime Test", "true", null },
                new List<string> { null, null, null, null, null, null, null, null, "Time Test", null, "true" },
            };

            var header = new Header
            {
                Name = "Assertion",
                Depth = 0,
                From = 0,
                To = 10,
                Children = new List<Header>
                {
                    new Header { Name = "Uri", Depth = 1, From = 1, To = 1, },
                    new Header { Name = "StatusCode", Depth = 1, From = 2, To = 2, },
                    new Header { Name = "Headers", Depth = 1, From = 3, To = 4, Children = new List<Header>
                        {
                            new Header { Name = "Content-Type", Depth = 2, From = 3, To = 3, },
                            new Header { Name = "Last-Modified", Depth = 2, From = 4, To = 4, },
                        }
                    },
                    new Header { Name = "Cookies", Depth = 1, From = 5, To = 6, Children = new List<Header>
                        {
                            new Header { Name = "Location", Depth = 2, From = 5, To = 5,},
                            new Header { Name = "Degree", Depth = 2,From = 6, To = 6, },
                        }
                    },
                    new Header { Name = "Xsd", Depth = 1, From = 7, To = 7 },
                    new Header { Name = "Contents", Depth = 1, From = 8, To = 8, Children = new List<Header>
                        {
                            new Header { Name = "Name", Depth = 2, From = 8, To = 8, },
                            new Header { Name = "IsDateTime", Depth = 2, From = 9, To = 9, },
                            new Header { Name = "IsTime", Depth = 2, From = 10, To = 10, },
                        }
                    
                    },
                }
            };

            using (ShimsContext.Create())
            {
                ShimResourceManager.AllInstances.GetStringString = (s, v) => "XsdFileValue";
                var resource = new ShimResourceManager();

                // Act
                var result = AssertionsParser.Parse(header, csvRow, resource);

                // Assert

                // Uri
                Assert.AreEqual("/local", (result.ElementAt(0) as UriAssertion).Value);

                // StatusCode
                Assert.AreEqual("200", (result.ElementAt(1) as StatusCodeAssertion).Value);

                // Headers
                Assert.AreEqual("Content-Type", (result.ElementAt(2) as HeaderAssertion).Key);
                Assert.AreEqual("text/html", (result.ElementAt(2) as HeaderAssertion).Value);
                Assert.AreEqual("Last-Modified", (result.ElementAt(3) as HeaderAssertion).Key);
                Assert.AreEqual("15:29:20", (result.ElementAt(3) as HeaderAssertion).Value);

                // Cookies
                Assert.AreEqual("Location", (result.ElementAt(4) as CookieAssertion).Key);
                Assert.AreEqual("001", (result.ElementAt(4) as CookieAssertion).Value);
                Assert.AreEqual("Degree", (result.ElementAt(5) as CookieAssertion).Key);
                Assert.AreEqual("C", (result.ElementAt(5) as CookieAssertion).Value);

                // Xsd
                Assert.AreEqual("XsdFileValue", (result.ElementAt(6) as XsdAssertion).Xsd);

                // IsDateTime
                Assert.AreEqual(false, (result.ElementAt(7) as DateTimeAssertion).IsTime);

                // IsTime
                Assert.AreEqual(true, (result.ElementAt(8) as DateTimeAssertion).IsTime);
            }
        }

        [TestMethod]
        public void TestParseWithNoItem()
        {
            // Arrange
            var csvRow = new List<List<string>>
            {
                new List<string> { null, null, null, null, null, null, null },
            };

            // Arrange
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 6, Children = new List<Header>
                {
                    new Header { Name = "Uri", Depth = 2, From = 1, To = 1, },
                    new Header { Name = "StatusCode", Depth = 2, From = 2, To = 2, },
                    new Header { Name = "Headers", Depth = 2, From = 3, To = 4, Children = new List<Header>
                        {
                            new Header { Name = "Content-Type", Depth = 3, From = 3, To = 3, },
                            new Header { Name = "Last-Modified", Depth = 3, From = 4, To = 4, },
                        }
                    },
                    new Header { Name = "Cookies", Depth = 2, From = 5, To = 6, Children = new List<Header>
                        {
                            new Header { Name = "Location", Depth = 3, From = 5, To = 5, },
                            new Header { Name = "Degree", Depth = 3, From = 6, To = 6, },
                        }
                    },
                }
            };

            // Act
            var result = AssertionsParser.Parse(header, csvRow);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithInvalidAssertionName()
        {
            // Arrange
            var csvRow = new List<List<string>>
            {
                new List<string> { null, "/local" },
            };

            var header = new Header { Name = "Assertion", Depth = 0, From = 0, To = 9, Children = new List<Header>
                {
                    new Header { Name = "XXX", Depth = 1, From = 1, To = 1, },
                }
            };

            // Act
            AssertionsParser.Parse(header, csvRow);
        }
    }
}
