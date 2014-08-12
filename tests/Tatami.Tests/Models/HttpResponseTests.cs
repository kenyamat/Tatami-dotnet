namespace Tatami.Tests.Models
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Xml.XPath;
    using Tatami.Extensions;
    using Tatami.Models;
    using Tatami.Parsers.Documents;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HttpResponseTests
    {
        [TestMethod]
        public void TestExistsNode()
        {
            // Arrange
            var r = new HttpResponse
            {
                ContentType = "text/xml",
                Contents = "<root><child /></root>",
            };

            // Act
            var result= r.ExistsNode("root/child", null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(XPathException))]
        public void TestExistsNodeWithInvalidXPath()
        {
            // Arrange
            var r = new HttpResponse
            {
                ContentType = "text/xml",
                Contents = "<root><child/></root>",
            };

            // Act/Assert
            r.ExistsNode("//(root", null);
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
            var uri = new Uri("http://yahoo.com");
            const HttpStatusCode statusCode = HttpStatusCode.OK;
            var lastModified = DateTime.Now;
            const string contentType = "text/xml";
            var headers = new NameValueCollection { { "a", "b" } };
            var cookies = new NameValueCollection { { "c", "d" } };
            var exception= new Exception("message");

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
