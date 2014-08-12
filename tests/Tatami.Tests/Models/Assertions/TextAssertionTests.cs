namespace Tatami.Tests.Models.Assertions
{
    using System;
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TextAssertionTests
    {
        [TestMethod]
        public void TestAssert()
        {
            // Arrange
            const string ExpectedXml = @"<Shop>a</Shop>";
            const string ActualHtml =
                @"<html>
                    <body>
                        <span>a</span>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Shop",
                    Attribute = null,
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        public void TestAssertWithExists()
        {
            // Arrange
            const string ActualHtml =
                @"<html>
                    <body>
                        <span>a</span>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = null,
                    Attribute = null,
                    Pattern = null,
                    Exists = true
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                },
            };

            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(null, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        public void TestAssertWithUrlDecode()
        {
            // Arrange
            const string ExpectedXml = @"<Shop>http://www.test.com/?query=a+b</Shop>";
            const string ActualHtml =
                @"<html>
                    <body>
                        <a href='http://www.test.com/?query=a+b'>link</a>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Shop",
                    Attribute = null,
                    Pattern = null,
                    UrlDecode = true,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/a",
                    Attribute = "href",
                    Pattern = null,
                    UrlDecode = true,
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        public void TestAssertWithFail()
        {
            // Arrange
            const string ExpectedXml = @"<Shop>a</Shop>";
            const string ActualHtml =
                @"<html>
                    <body>
                        <span>b</span>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Shop",
                    Attribute = null,
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);

            // Assert
            Assert.IsFalse(item.Success);            
        }

        [TestMethod]
        public void TestAssertWithQueryAttribute()
        {
            // Arrange
            const string ExpectedXml =
                @"<Shop src='a' />";
            const string ActualHtml =
                @"<html>
                    <body>
                        <span class='a'>b</span>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Shop",
                    Attribute = "src",
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = "class",
                    Pattern = null,
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        public void TestAssertWithList()
        {
            // Arrange
            const string ExpectedXml =
                @"<Shop>
                    <Item Name='a' />
                    <Item Name='b' />
                    <Item Name='c' />
                    <Item Name='d' />
                    <Item Name='e' />
                </Shop>"; 
            const string ActualHtml =
                @"<html>
                    <body>
                        <ul>
                            <li>a</li>
                            <li>b</li>
                            <li>c</li>
                            <li>d</li>
                            <li>e</li>
                        </ul>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = true,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Shop/Item",
                    Attribute = "Name",
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/ul/li",
                    Attribute = null,
                    Pattern = null,
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }
        
        [TestMethod]
        public void TestAssertWithPattern()
        {
            // Arrange
            const string ActualHtml =
                @"<html>
                    <body>
                        <span>aaa:bbb</span>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = "bbb",
                    Query = null,
                    Attribute = null,
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = @":(.*)",
                },
            };

            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(null, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        public void TestAssertWithListAndPattern()
        {
            // Arrange
            const string ExpectedXml =
                @"<Shop>
                    <Item>30 in</Item>
                    <Item>61°</Item>
                    <Item>66%</Item>
                    <Item>10 miles</Item>
                    <Item>0 mph</Item>
                </Shop>"; 
            const string ActualHtml =
                @"<html>
                    <body>
                        <ul>
                            <li>Barometer: 30 in</li>
                            <li>Dewpoint: 61°</li>
                            <li>Humidity: 66%</li>
                            <li>Visibility: 10 miles</li>
                            <li>Wind: 0 mph</li>
                        </ul>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = true,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Shop/Item",
                    Attribute = null,
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/ul/li",
                    Attribute = null,
                    Pattern = @"[^:]:\s(.*)",
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAssertWithFailedPattern()
        {
            // Arrange
            const string ExpectedXml =
                @"<Shop>
                    <Item>30 in</Item>
                </Shop>";
            const string ActualHtml =
                @"<html>
                    <body>
                        <ul><li>Barometer 30 in</li></ul>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Shop/Item",
                    Attribute = null,
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/ul/li",
                    Attribute = null,
                    Pattern = @"[^:]:\s(.*)",
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);
        }

        [TestMethod]
        public void TestAssertWithStaticValue()
        {
            // Arrange
            const string ActualHtml =
                @"<html>
                    <body>
                        <span>aaa</span>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = null,
                    Value = "aaa",
                    Query = null,
                    Attribute = null,
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                },
            };

            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(null, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestAssertWithListAndStaticValue()
        {
            // Arrange
            const string ActualHtml =
                @"<html>
                    <body>
                        <span>aaa</span>
                        <span>aaa</span>
                    </body>
                </html>";

            var item = new TextAssertion
            {
                IsList = true,
                Expected = new ContentAssertionItem
                {
                    Key = null,
                    Value = "aaa",
                    Query = null,
                    Attribute = null,
                    Pattern = null,
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                },
            };

            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(null, actual);
        }
    }
}
