namespace Tatami.Tests.Models.Assertions
{
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeAssertionTests
    {
        [TestMethod]
        public void TestAssert()
        {
            // Arrange
            const string ExpectedXml =
                @"<Expected>2013/08/02 16:48:10</Expected>";
            const string ActualHtml =
                @"<html>
                    <body><span>2013/08/02 16:48:10</span></body>
                </html>";

            var item = new DateTimeAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Expected",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
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
        public void TestAssertWithOnlyTime()
        {
            // Arrange
            const string ExpectedXml =
                @"<Expected>2012/08/02 16:48:10</Expected>";
            const string ActualHtml =
                @"<html>
                    <body><span>16:48:10</span></body>
                </html>";

            var item = new DateTimeAssertion
            {
                IsList = false,
                IsTime = true,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Expected",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                    Format = "HH:mm:ss"
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
        public void TestAssertWithStandardDateTimeFormat()
        {
            // Arrange
            const string ExpectedXml =
                @"<Expected>2013-08-02T17:54:00</Expected>";
            const string ActualHtml =
                @"<html>
                    <body><span>17:54</span></body>
                </html>";

            var item = new DateTimeAssertion
            {
                IsList = false,
                IsTime = true,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Expected",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy-MM-ddTHH:mm:ss"
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                    Format = "t", // Standard DateTime format
                    FormatCulture = "en-US"
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
            const string ExpectedXml =
                @"<Expected>2013/08/02 16:48:10</Expected>";
            const string ActualHtml =
                @"<html>
                    <body><span>2013/08/02 12:00:00</span></body>
                </html>";

            var item = new DateTimeAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Expected",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
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
        public void TestAssertWithList()
        {
            // Arrange
            const string ExpectedXml =
                @"<Expected>
                    <Item Name='2013/08/02 16:00:00' />
                    <Item Name='2013/08/02 17:00:00' />
                    <Item Name='2013/08/02 18:00:00' />
                    <Item Name='2013/08/02 19:00:00' />
                    <Item Name='2013/08/02 20:00:00' />
                </Expected>"; 
            const string ActualHtml =
                @"<html>
                    <head></head>
                    <body>
                        <ul>
                            <li>2013/08/02 16:00:00</li>
                            <li>2013/08/02 17:00:00</li>
                            <li>2013/08/02 18:00:00</li>
                            <li>2013/08/02 19:00:00</li>
                            <li>2013/08/02 20:00:00</li>
                        </ul>
                    </body>
                </html>";

            var item = new DateTimeAssertion
            {
                IsList = true,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Expected/Item",
                    Attribute = "Name",
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/ul/li",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
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
        [ExpectedException(typeof(System.FormatException))]
        public void TestAssertWithInvalidFormat()
        {
            // Arrange
            const string ExpectedXml =
                @"<Expected>2013/08/02 16:48:10</Expected>";
            const string ActualHtml =
                @"<html>
                    <body><span>2013/08/02 12:00:00</span></body>
                </html>";

            var item = new DateTimeAssertion
            {
                IsList = false,
                Expected = new ContentAssertionItem
                {
                    Key = "Expected",
                    Value = null,
                    Query = "//Expected",
                    Attribute = null,
                    Pattern = null,
                    Format = "xxxxxxxxxxxx"
                },
                Actual = new ContentAssertionItem
                {
                    Key = "Actual",
                    Value = null,
                    Query = "//body/span",
                    Attribute = null,
                    Pattern = null,
                    Format = "yyyy/MM/dd HH:mm:ss"
                },
            };

            var expected = new Arrange { HttpResponse = new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" } };
            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/html" } };

            // Act
            item.Assert(expected, actual);
        }
    }
}
