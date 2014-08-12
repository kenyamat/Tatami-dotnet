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
    public class XsdAssertionParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var csvRow = new List<string> { null, "XsdFile" };
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 1, Children = new List<Header> {
                    new Header { Name = "Xsd", Depth = 2, From = 1, To = 1, }
                }
            };

            using (ShimsContext.Create())
            {
                ShimResourceManager.AllInstances.GetStringString = (s, v) => "XsdFileValue";
                var resource = new ShimResourceManager();

                // Act
                var result = XsdAssertionParser.Parse(header, csvRow, resource);

                // Assert
                Assert.AreEqual("XsdFileValue", ((XsdAssertion)result.ElementAt(0)).Xsd);
            }
        }

        [TestMethod]
        public void TestParseWithNullValue()
        {
            // Arrange
            var csvRow = new List<string> { null, null };
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 1, Children =
                new List<Header> {
                    new Header { Name = "Xsd", Depth = 2, From = 1, To = 1, }
                }
            };

            using (ShimsContext.Create())
            {
                ShimResourceManager.AllInstances.GetStringString = (s, v) => "XsdFileValue";
                var resource = new ShimResourceManager();

                // Act
                var result = XsdAssertionParser.Parse(header, csvRow, resource);

                // Assert
                Assert.AreEqual(null, result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithNoResource()
        {
            // Arrange
            var csvRow = new List<string> { null, "XsdFile" };
            var header = new Header { Name = "Assertion", Depth = 1, From = 0, To = 1, Children =
                new List<Header> {
                    new Header { Name = "Xsd", Depth = 2, From = 1, To = 1, }
                }
            };

            using (ShimsContext.Create())
            {
                ShimResourceManager.AllInstances.GetStringString = (s, v) => null;
                var resource = new ShimResourceManager();

                // Act
                XsdAssertionParser.Parse(header, csvRow, resource);
            }
        }
    }
}
