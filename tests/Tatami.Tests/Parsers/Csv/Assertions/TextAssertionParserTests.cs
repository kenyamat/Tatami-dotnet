namespace Tatami.Tests.Parsers.Csv.Assertions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Parsers.Csv.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TextAssertionParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", "true", "a", "b"
            };

            // Arrange
            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 5, Children =
                new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "Expected", Depth = 3, From = 3, To = 3, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, }
                        }
                    }
                }
            };

            // Act
            var result = TextAssertionParser.Parse(header, csvRow);
            var assertion = result.ElementAt(0) as TextAssertion;

            // Assert
            Assert.AreEqual("name", assertion.Name);
            Assert.AreEqual(true, assertion.IsList);
            Assert.IsNotNull(assertion.Expected);
            Assert.IsNotNull(assertion.Actual);
        }

        [TestMethod]
        public void TestParseWIthIsListIsFalse()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", "false", "a", "b"
            };

            // Arrange
            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 5, Children =
                new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "Expected", Depth = 3, From = 3, To = 3, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, }
                        }
                    }
                }
            };

            // Act
            var result = TextAssertionParser.Parse(header, csvRow);
            var assertion = result.ElementAt(0) as TextAssertion;

            // Assert
            Assert.AreEqual("name", assertion.Name);
            Assert.AreEqual(false, assertion.IsList);
            Assert.IsNotNull(assertion.Expected);
            Assert.IsNotNull(assertion.Actual);
        }

        [TestMethod]
        public void TestParseWIthIsListIsNull()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", null, "a", "b"
            };

            // Arrange
            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 5, Children =
                new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "Expected", Depth = 3, From = 3, To = 3, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, }
                        }
                    }
                }
            };

            // Act
            var result = TextAssertionParser.Parse(header, csvRow);
            var assertion = result.ElementAt(0) as TextAssertion;

            // Assert
            Assert.AreEqual("name", assertion.Name);
            Assert.AreEqual(false, assertion.IsList);
            Assert.IsNotNull(assertion.Expected);
            Assert.IsNotNull(assertion.Actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseWIthIsListIsInvalid()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", "Aa", "a", "b"
            };

            // Arrange
            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 5, Children =
                new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "Expected", Depth = 3, From = 3, To = 3, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, }
                        }
                    }
                }
            }; 

            // Act
            TextAssertionParser.Parse(header, csvRow);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWIthNotHavingName()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, null, "true", null, null
            };

            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 5, Children =
                new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "Expected", Depth = 3, From = 3, To = 3, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 2, To = 2, Children =
                        new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 3, To = 3, }
                        }
                    }
                }
            }; 

            // Act
            TextAssertionParser.Parse(header, csvRow);
        }
    }
}
