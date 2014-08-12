namespace Tatami.Tests.Validators
{
    using System.Collections.Generic;
    using Tatami.Models.Csv;
    using Tatami.Parsers;
    using Tatami.Validators.Csv;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HeaderValidatorTests
    {
        [TestMethod]
        public void TestValidate()
        {
            // Arrange
            var header = new Header { Name = "Root", Depth = -1, From = 0, To = 1, Children = new List<Header> {
                    new Header { Name = "Arrange", Depth = 0, From = 0, To = 0, Children = new List<Header> {
                        new Header { Name = "HttpRequest Actual", Depth = 1, From = 0, To = 0, Children = new List<Header> {
                            new Header { Depth = 2, From = 0, To = 0, Name = "BaseUri" }
                                }
                            }
                        }
                    },
                    new Header { Name = "Assertion", Depth = 0, From = 1, To = 1, Children = new List<Header> {
                        new Header { Name = "Uri", Depth = 1, From = 1, To = 1, }
                        }
                    }
                }
            }; 

            // Act
            HeaderValidator.Validate(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateWithInvalidName()
        {
            // Arrange
            var header = new Header 
            {
                Name = "AA",
            };

            // Act
            HeaderValidator.Validate(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateWithInvalidNumberOfChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Root",
                Children = new List<Header>
                {
                    new Header(),
                    new Header(),
                    new Header(),
                }
            };

            // Act
            HeaderValidator.Validate(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateArrangeWithInvalidName()
        {
            // Arrange
            var header = new Header { Name = "Arrange", Depth = 0, From = 1, To = 2, Children = new List<Header> {
                        new Header { Name = "XXXX", Depth = 1, From = 1, To = 1, Children = new List<Header> {
                            new Header { Depth = 2, From = 0, To = 0, Name = "BaseUri" }
                                }
                        },
                        new Header { Name = "HttpRequest Actual", Depth = 1, From =2, To = 2, Children = new List<Header> {
                            new Header { Depth = 2, From = 2, To = 2, Name = "BaseUri" }
                                }
                            }
                        },
                    };

            // Act
            HeaderValidator.ValidateArrange(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateArrangeWithNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Arrange",
            };

            // Act
            HeaderValidator.ValidateArrange(header);
        }

        [TestMethod]
        public void TestValidateHttpRequest()
        {
            // Arrange
            var header = new Header { Name = "HttpRequest Actual", Depth = 1, From = 1, To = 7, Children =
                new List<Header> {
                    new Header { Depth = 2, From = 1, To = 1, Name = "BaseUri" },
                    new Header { Depth = 2, From = 2, To = 2, Name = "UserAgent" },
                    new Header { Depth = 2, From = 3, To = 3, Name = "PathInfos" },
                    new Header { Depth = 2, From = 4, To = 4, Name = "Fragment" },
                    new Header { Depth = 2, From = 5, To = 5, Name = "Headers", Children =
                        new List<Header> {
                            new Header { Depth = 3, From = 5, To = 5, Name = "Cache" }
                        }
                    },
                    new Header { Depth = 2, From = 6, To = 6, Name = "Cookies", Children = 
                        new List<Header> {
                            new Header { Depth = 3, From = 6, To = 6, Name = "TestCookie" }
                        }
                    },
                    new Header { Depth = 2, From = 7, To = 7, Name = "QueryStrings", Children =
                        new List<Header> {
                            new Header { Depth = 3, From = 7, To = 7, Name = "TestQuery" }
                        }
                    },
               },
            };

            // Act
            HeaderValidator.ValidateHavingChildren(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHttpRequestWithInvalidName()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest aa",
            };

            // Act
            HeaderValidator.ValidateHttpRequest(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHttpRequestWithNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest Expected",
            };

            // Act
            HeaderValidator.ValidateHttpRequest(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHttpRequestWithNotHavingBaseUri()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest Expected",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "PathInfos",
                    }
                }
            };

            // Act
            HeaderValidator.ValidateHttpRequest(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHttpRequestInvalidChildName()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest Actual",
                Depth = 1,
                From = 1,
                To = 2,
                Children =
                    new List<Header> {
                    new Header { Depth = 2, From = 1, To = 1, Name = "BaseUri" },
                    new Header { Depth = 2, From = 1, To = 1, Name = "XXXX" },
                }
            };

            // Act
            HeaderValidator.ValidateHttpRequest(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHttpRequestWithBaseUriHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest Expected",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "BaseUri",
                        Children = new List<Header>
                        {
                            new Header
                            {
                                Name = "User-Agent"
                            }
                        }
                    }
                }
            };

            // Act
            HeaderValidator.ValidateHttpRequest(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHttpRequestWithHeadersNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest Expected",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "BaseUri",
                    },
                    new Header
                    {
                        Name = "Headers",
                    }
                }
            };

            // Act
            HeaderValidator.ValidateHttpRequest(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHttpRequestWithFragmentHavingTwoChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "HttpRequest Expected",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "BaseUri",
                    },
                    new Header
                    {
                        Name = "Fragment",
                        Children = new List<Header>
                        {
                            new Header(),
                            new Header(),
                        }
                    }
                }
            };

            // Act
            HeaderValidator.ValidateHttpRequest(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateAssertionWithInvalidName()
        {
            // Arrange
            var header = new Header
            {
                Name = "AAA",
            };

            // Act
            HeaderValidator.ValidateAssertion(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateAssertionWithNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Assertion",
            };

            // Act
            HeaderValidator.ValidateAssertion(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateAssertionWithUriHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Assertion",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Uri",
                        Children = new List<Header>
                        {
                            new Header()
                        }
                    }
                }
            };

            // Act
            HeaderValidator.ValidateAssertion(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateAssertionWithHeadersNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Assertion",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Headers",
                    }
                }
            };

            // Act
            HeaderValidator.ValidateAssertion(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateAssertionWithInvalidChildName()
        {
            // Arrange
            var header = new Header
            {
                Name = "Assertion",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "XXX",
                    }
                }
            };

            // Act
            HeaderValidator.ValidateAssertion(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsWithNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Contents",
            };

            // Act
            HeaderValidator.ValidateContents(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsWithHavingEmptyChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Contents",
                Children = new List<Header>(),
            };

            // Act
            HeaderValidator.ValidateContents(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsWithHavingInvalidChildren()
        {
            // Arrange
            var header = new Header { Name = "Contents", Children =
                new List<Header> {
                    new Header { Name = "Name", },
                    new Header { Name = "Expected", Children =
                        new List<Header> {
                            new Header { Name = "Value", },
                        }
                    },
                    new Header { Name = "Actual", Children =
                        new List<Header> {
                            new Header { Name = "Query", },
                        }
                    },
                    new Header { Name = "AAA", }
                }
            };

            // Act
            HeaderValidator.ValidateContents(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsWithNameHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Contents",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Name",
                        Children = new List<Header>
                        {
                            new Header()
                        }
                    }
                }
            };

            // Act
            HeaderValidator.ValidateContents(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsWithExpectedNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Contents",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Expected"
                    }
                }
            };

            // Act
            HeaderValidator.ValidateContents(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsItemWithNotHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Expected"
            };

            // Act
            HeaderValidator.ValidateContentsItem(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsItemWithHavingInvalidChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Expected",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "AAA"
                    }
                }
            };

            // Act
            HeaderValidator.ValidateContentsItem(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateContentsItemWithValueHavingChildren()
        {
            // Arrange
            var header = new Header
            {
                Name = "Value",
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Value",
                        Children = new List<Header>
                        {
                            new Header()
                        }
                    }
                }
            };

            // Act
            HeaderValidator.ValidateContentsItem(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void ValidateNotNullWithNull()
        {
            HeaderValidator.ValidateNotNull(null, "test");
        }

        [TestMethod]
        public void TestValidateDepthFromTo()
        {
            // Arrange
            var header = new Header
            {
                Name = "Parent",
                Depth = -1,
                From = 0,
                To = 4,
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Child",
                        Depth = 0,
                        From = 0,
                        To = 2,
                    },
                    new Header
                    {
                        Name = "Child",
                        Depth = 0,
                        From = 2,
                        To = 4,
                    }
                }
            };

            // Act
            HeaderValidator.ValidateDepthFromTo(header, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateDepthFromToWithInvalidFromTo()
        {
            // Arrange
            var header = new Header
            {
                Name = "Parent",
                From = 1,
                To = 0,
            };

            // Act
            HeaderValidator.ValidateDepthFromTo(header, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateDepthFromToWithInvalidDepth()
        {
            // Arrange
            var header = new Header
            {
                Name = "Parent",
                Depth = -1,
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Child",
                        Depth = -1,
                    }
                }
            };

            // Act
            HeaderValidator.ValidateDepthFromTo(header, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateDepthFromToWithInvalidFrom()
        {
            // Arrange
            var header = new Header
            {
                Name = "Parent",
                Depth = -1,
                From = 1,
                To = 4,
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Child",
                        Depth = 0,
                        From = 0,
                        To = 2,
                    },
                    new Header
                    {
                        Name = "Child",
                        Depth = 0,
                        From = 2,
                        To = 4,
                    }
                }
            };

            // Act
            HeaderValidator.ValidateDepthFromTo(header, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateDepthFromToWithInvalidTo()
        {
            // Arrange
            var header = new Header
            {
                Name = "Parent",
                Depth = -1,
                From = 0,
                To = 4,
                Children = new List<Header>
                {
                    new Header
                    {
                        Name = "Child",
                        Depth = 0,
                        From = 0,
                        To = 2,
                    },
                    new Header
                    {
                        Name = "Child",
                        Depth = 0,
                        From = 2,
                        To = 5,
                    }
                }
            };

            // Act
            HeaderValidator.ValidateDepthFromTo(header, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHavingChildrenWithNullChildren()
        {
            // Arrange
            var header = new Header { Name = "Parent", Depth = -1, From = 0, To = 4, Children = null };

            // Act
            HeaderValidator.ValidateHavingChildren(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHavingChildrenWithEmptyChildren()
        {
            // Arrange
            var header = new Header { Name = "Parent", Depth = -1, From = 0, To = 4, Children = new List<Header>() };

            // Act
            HeaderValidator.ValidateHavingChildren(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateHavingChildrenWithInvalidChildCount()
        {
            // Arrange
            var header = new Header { Name = "Parent", Depth = -1, From = 0, To = 4, Children =
                new List<Header> {
                    new Header(),
                }
            };

            // Act
            HeaderValidator.ValidateHavingChildren(header, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateNotHavingChildrenWithChild()
        {
            // Arrange
            var header = new Header { Name = "Parent", Depth = -1, From = 0, To = 4, Children =
                new List<Header> {
                    new Header(),
                }
            };

            // Act
            HeaderValidator.ValidateNotHavingChildren(header);
        }

        [TestMethod]
        public void TestValidateNotHavingChildren()
        {
            // Arrange
            var header = new Header { Name = "Parent", Depth = -1, From = 0, To = 4, Children = null };

            // Act
            HeaderValidator.ValidateNotHavingChildren(header);
        }

        [TestMethod]
        public void TestValidateNotHavingChildrenWithEmptyChildren()
        {
            // Arrange
            var header = new Header { Name = "Parent", Depth = -1, From = 0, To = 4, Children = new List<Header>() };

            // Act
            HeaderValidator.ValidateNotHavingChildren(header);
        }

        [TestMethod]
        public void TestValidateNotHavingGrandChildren()
        {
            // Arrange
            var header = new Header { Depth = 2, From = 5, To = 5, Name = "Headers", Children =
                new List<Header> {
                    new Header { Depth = 3, From = 5, To = 5, Name = "Cache" }
                }
            };

            // Act
            HeaderValidator.ValidateNotHavingGrandChildren(header);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestValidateNotHavingGrandChildrenWithHavingChild()
        {
            // Arrange
            var header = new Header { Depth = 2, From = 5, To = 5, Name = "Headers", Children =
                new List<Header> {
                    new Header { Depth = 3, From = 5, To = 5, Name = "Cache", Children =
                        new List<Header> {
                            new Header { Depth = 4, From = 5, To = 5, Name = "XXXX" }
                        }
                    }
                }
            };

            // Act
            HeaderValidator.ValidateNotHavingGrandChildren(header);
        }
    }
}
