namespace Tatami.Tests.Models
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Xml.XPath;
    using Tatami.Extensions;
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Tatami.Models.Assertions.Fakes;
    using Tatami.Parsers.Documents;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;

    [TestClass]
    public class TestCaseTests
    {
        private static readonly Fixture fixture = new Fixture();

        [TestMethod]
        public void TestAssert()
        {
            // Arrange
            var testCase = new TestCase
            {
                Assertions = new AssertionList
                {
                    new StubAssertionBase
                    {
                        Success = true
                    }
                }
            };

            // Act
            testCase.Assert(new Arrange(), new Arrange());

            // Assert
            Assert.IsTrue(testCase.Success);
            Assert.AreEqual(0, testCase.FailedAssertions.Count);
        }

        [TestMethod]
        public void TestAssertWithFailedAssertion()
        {
            // Arrange
            var testCase = new TestCase
            {
                Assertions = new AssertionList
                {
                    new StubAssertionBase
                    {
                        Success = false
                    }
                }
            };

            // Act
            testCase.Assert(new Arrange(), new Arrange());

            // Assert
            Assert.IsFalse(testCase.Success);
            Assert.AreEqual(1, testCase.FailedAssertions.Count);
        }

        [TestMethod]
        public void TestAssertWithException()
        {
            // Arrange
            var exception = fixture.Create<Exception>();
            var testCase = new TestCase
            {
                Assertions = new AssertionList
                {
                    new StubAssertionBase
                    {
                        AssertArrangeArrange = (e, a) => { throw exception; }
                    }
                }
            };

            // Act
            testCase.Assert(new Arrange(), new Arrange());

            // Assert
            Assert.IsFalse(testCase.Success);
            Assert.AreEqual(exception, testCase.FailedAssertions[0].Exception);
        }

        [TestMethod]
        public void TestGetDocumentValue()
        {
            // Arrange
            var r = new HttpResponse
            {
                ContentType = "text/xml",
                Contents = "<root><child>a</child></root>",
            };

            // Act
            var result = r.GetDocumentValue("root/child", null);

            // Assert
            Assert.AreEqual("a", result);
        }

        [TestMethod]
        [ExpectedException(typeof(XPathException))]
        public void TestGetDocumentValueWithInvalidXPath()
        {
            // Arrange
            var r = new HttpResponse
            {
                ContentType = "text/xml",
                Contents = "<root><child>a</child></root>",
            };

            // Act & Assert
            r.GetDocumentValue("//(root", null);
        }

        [TestMethod]
        public void TestGetDocumentValues()
        {
            // Arrange
            var r = new HttpResponse
            {
                ContentType = "text/xml",
                Contents = "<root><child>a</child><child>b</child></root>",
            };

            // Act
            var result = r.GetDocumentValues("root/child", null).GetString();

            // Assert
            Assert.AreEqual("{a, b}", result);
        }

        [TestMethod]
        [ExpectedException(typeof(XPathException))]
        public void TestGetDocumentValuesWithInvalidXPath()
        {
            // Arrange
            var r = new HttpResponse
            {
                ContentType = "text/xml",
                Contents = "<root><child>a</child><child>b</child></root>",
            };

            // Act & Assert
            r.GetDocumentValues("//(root", null).GetString();
        }

        [TestMethod]
        public void TestDocumentParserPropertyWithXml()
        {
            var r = new HttpResponse
            {
                ContentType = "text/xml",
                Contents = "<root/>",
            };

            Assert.IsTrue(r.DocumentParser is XmlParser);
        }

        [TestMethod]
        public void TestDocumentParserPropertyWithHtml()
        {
            var r = new HttpResponse
            {
                ContentType = "text/html",
                Contents = string.Empty,
            };

            Assert.IsTrue(r.DocumentParser is HtmlParser);
        }

        [TestMethod]
        public void TestDocumentParserPropertyWithJson()
        {
            var r = new HttpResponse
            {
                ContentType = "javascript",
                Contents = string.Empty,
            };

            Assert.IsTrue(r.DocumentParser is JsonParser);
        }

        [TestMethod]
        public void TestDocumentParserPropertyWithText()
        {
            var r = new HttpResponse
            {
                ContentType = "text/text",
                Contents = string.Empty,
            };

            Assert.IsTrue(r.DocumentParser is TextParser);
        }

        [TestMethod]
        public void TestToString()
        {
            // Arrange
            var uri = fixture.Create<Uri>();
            var statusCode= fixture.Create<HttpStatusCode>();
            var lastModified = fixture.Create<DateTime>();
            var contentType = fixture.Create<string>();
            var headers = fixture.Create<NameValueCollection>();
            var cookies = fixture.Create<NameValueCollection>();
            var exception = fixture.Create<Exception>();

            // Act
            var result = new HttpResponse
            {
                Uri = uri,
                StatusCode = statusCode,
                LastModified = lastModified,
                ContentType = contentType,
                Headers = headers,
                Cookies = cookies,
                Exception = exception,
            }.ToString();

            // Assert
            Assert.AreEqual(
                string.Format("Uri={0}, StatusCode={1}, LastModified={2}, ContentType={3}, Headers={4}, Cookies={5}, Exception={6}",
                    uri,
                    statusCode,
                    lastModified,
                    contentType,
                    headers.GetString(),
                    cookies.GetString(),
                    exception),
                result);
        }
    }
}
