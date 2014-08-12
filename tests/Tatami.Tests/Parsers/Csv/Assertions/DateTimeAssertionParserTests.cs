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
    public class DateTimeAssertionParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", "true", "true", "true", "a", "b"
            };

            // Arrange
            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 6, Children = new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "IsDateTime", Depth = 3, From = 3, To = 3, },
                    new Header { Name = "IsTime", Depth = 3, From = 4, To = 4, },
                    new Header { Name = "Expected", Depth = 3, From = 5, To = 5, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 5, To = 5, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 6, To = 6, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 6, To = 6, }
                        }
                    }   
                }
            };

            // Act
            var result = DateTimeAssertionParser.Parse(header, csvRow);
            var assertion = result.ElementAt(0) as DateTimeAssertion;

            // Assert
            Assert.AreEqual("name", assertion.Name);
            Assert.AreEqual(true, assertion.IsList);
            Assert.AreEqual(true, assertion.IsTime);
            Assert.IsNotNull(assertion.Expected);
            Assert.IsNotNull(assertion.Actual);
        }

        [TestMethod]
        public void TestParseWIthIsListIsFalseAndIsTimeIsFalse()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", "false", "true", "false", "a", "b"
            };

            // Arrange
            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 6, Children = new List<Header> { 
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "IsDateTime", Depth = 3, From = 3, To = 3, },
                    new Header { Name = "IsTime", Depth = 3, From = 4, To = 4, },
                    new Header { Name = "Expected", Depth = 3, From = 5, To = 5, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 5, To = 5, }, 
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 6, To = 6, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 6, To = 6, }
                        }
                    }
                }
            };

            // Act
            var result = DateTimeAssertionParser.Parse(header, csvRow);
            var assertion = result.ElementAt(0) as DateTimeAssertion;

            // Assert
            Assert.AreEqual("name", assertion.Name);
            Assert.AreEqual(false, assertion.IsList);
            Assert.AreEqual(false, assertion.IsTime);
            Assert.IsNotNull(assertion.Expected);
            Assert.IsNotNull(assertion.Actual);
        }

        [TestMethod]
        public void TestParseWIthIsListIsNullAndIsTimeIsNull()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", null, "true", null, "a", "b"
            };

            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 6, Children = new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "IsDateTime", Depth = 3, From = 3, To = 3, },
                    new Header { Name = "IsTime", Depth = 3, From = 4, To = 4, },
                    new Header { Name = "Expected", Depth = 3, From = 5, To = 5, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 5, To = 5, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 6, To = 6, Children = new List<Header> {
                        new Header { Name = "Value",Depth = 4, From = 6, To = 6, }
                        }
                    }
                }
            };

            // Act
            var result = DateTimeAssertionParser.Parse(header, csvRow);
            var assertion = result.ElementAt(0) as DateTimeAssertion;

            // Assert
            Assert.AreEqual("name", assertion.Name);
            Assert.AreEqual(false, assertion.IsList);
            Assert.AreEqual(false, assertion.IsTime);
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
                null, "name", "aa", "true", null, "a", "b"
            };

            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 6, Children = new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "IsDateTime", Depth = 3, From = 3, To = 3, },
                    new Header { Name = "IsTime", Depth = 3, From = 4, To = 4, },
                    new Header { Name = "Expected", Depth = 3, From = 5, To = 5, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 5, To = 5 },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 6, To = 6, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 6, To = 6, }
                        }
                    }
                }
            };

            // Act
            DateTimeAssertionParser.Parse(header, csvRow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseWIthIsTimeIsInvalid()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", null, "true", "aa", "a", "b"
            };

            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 6, Children = new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "IsDateTime", Depth = 3, From = 3, To = 3, },
                    new Header { Name = "IsTime", Depth = 3, From = 4, To = 4, },
                    new Header { Name = "Expected", Depth = 3, From = 5, To = 5, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 5, To = 5, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 6, To = 6, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 6, To = 6, }
                        }
                    }
                }
            };

            // Act
            DateTimeAssertionParser.Parse(header, csvRow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseWIthNoItems()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", null, "true", "aa", null, null
            };

            var header = new Header { Name = "Contents", Depth = 2, From = 0, To = 6, Children = new List<Header> {
                    new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "IsDateTime", Depth = 3, From = 3, To = 3, },
                    new Header { Name = "IsTime", Depth = 3, From = 4, To = 4, },
                    new Header { Name = "Expected", Depth = 3, From = 5, To = 5, Children = new List<Header> {
                        new Header { Name = "Value", Depth = 4, From = 5, To = 5, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 6, To = 6, Children = new List<Header> {
                        new Header { Name = "Value", Depth = 4, From = 6, To = 6, }
                        }
                    }
                }
            };

            // Act
            DateTimeAssertionParser.Parse(header, csvRow);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestParseWithNoName()
        {
            // Arrange
            var csvRow = new List<string>
            {
                null, "name", "true", "true", "true", "a", "b"
            };

            // Arrange
            var header = new Header
            {
                Name = "Contents", Depth = 2, From = 0, To = 6, Children = new List<Header> {
                    //new Header { Name = "Name", Depth = 3, From = 1, To = 1, },
                    new Header { Name = "IsList", Depth = 3, From = 2, To = 2, },
                    new Header { Name = "IsDateTime", Depth = 3, From = 3, To = 3, },
                    new Header { Name = "IsTime", Depth = 3, From = 4, To = 4, },
                    new Header { Name = "Expected", Depth = 3, From = 5, To = 5, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 5, To = 5, },
                        }
                    },
                    new Header { Name = "Actual", Depth = 3, From = 6, To = 6, Children = new List<Header> {
                            new Header { Name = "Value", Depth = 4, From = 6, To = 6, }
                        }
                    }   
                }
            };

            // Act
            DateTimeAssertionParser.Parse(header, csvRow);
        }
    }
}
