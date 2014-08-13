namespace Tatami.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Fakes;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.QualityTools.Testing.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tatami.Models;
    using Tatami.Models.Mappings;
    using Tatami.Services;

    [TestClass]
    public class HttpRequestServiceTests
    {
        [TestMethod]
        public void TestGetResponseWithNullMethod()
        {
            TestGetResponse(null);
        }

        [TestMethod]
        public void TestGetResponseWithGet()
        {
            TestGetResponse("GET");
        }

        [TestMethod]
        public void TestGetResponseWithPost()
        {
            TestGetResponse("POST");
        }

        [TestMethod]
        public void TestGetResponseWithPut()
        {
            TestGetResponse("PUT");
        }

        [TestMethod]
        public void TestGetResponseWithDelete()
        {
            TestGetResponse("DELETE");
        }

        public void TestGetResponse(string method)
        {
            using (ShimsContext.Create())
            {
                // Arrange
                var baseUriMapping = new BaseUriMapping { { "TargetSite", "http://yahoo.com" } };
                var userAgentMapping = new UserAgentMapping { { "IE11", "Mozilla IE11" } };

                var httpRequest = new HttpRequest
                {
                    BaseUri = "TargetSite",
                    UserAgent = "IE11",
                    Method = method,
                    Headers = new NameValueCollection { { "test", "test-value" } },
                    PathInfos = new List<string> { "test.aspx" },
                    Cookies = new NameValueCollection { { "test", "test-value" } },
                };

                var httpContent = new StringContent("Hello World");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                httpContent.Headers.LastModified = DateTimeOffset.Parse("Tue, 16 Jul 2013 11:50:35 GMT");

                var httpResponseMessage = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = httpContent,
                    RequestMessage = new HttpRequestMessage { RequestUri = new Uri("http://yahoo.com/test.aspx") }
                };

                httpResponseMessage.Headers.Add("test", "test-value");

                var cookies = new CookieContainer();
                cookies.Add(new Cookie("test", "test-value", "", "yahoo.com"));

                ShimHttpClientHandler.AllInstances.CookieContainerGet = h => cookies;
                ShimHttpClient.AllInstances.GetAsyncUri = (client, task) => Task.FromResult(httpResponseMessage);
                ShimHttpClient.AllInstances.PostAsyncUriHttpContent = (client, task, c) => Task.FromResult(httpResponseMessage);
                ShimHttpClient.AllInstances.PutAsyncUriHttpContent = (client, task, c) => Task.FromResult(httpResponseMessage);
                ShimHttpClient.AllInstances.DeleteAsyncUri = (client, task) => Task.FromResult(httpResponseMessage);

                // Act
                var httpRequestService = new HttpRequestService(baseUriMapping, userAgentMapping, "http://proxy.com");
                var httpResponse = httpRequestService.GetResponseAsync(httpRequest).Result;

                // Assert
                Assert.AreEqual(1, httpResponse.Headers.Count);
                Assert.AreEqual(1, httpResponse.Cookies.Count);
                Assert.AreEqual(DateTimeOffset.Parse("Tue, 16 Jul 2013 11:50:35 GMT", CultureInfo.InvariantCulture), httpResponse.LastModified);
                Assert.AreEqual("Hello World", httpResponse.Contents);
                Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
                Assert.AreEqual(new Uri("http://yahoo.com/test.aspx"), httpResponse.Uri);
            }
        }

        [TestMethod]
        public void TestCreateUri()
        {
            // Arrange
            var baseUri = new Uri("http://a.com");
            IEnumerable<string> pathInfos = new List<string>
            {
                "a 1", "b 2"
            };
            var queryStrings = new NameValueCollection
            {
                { "c 3", "d 4" },
            };
            const string Fragment = "e 5";

            // Act
            var uri = HttpRequestService.CreateUri(baseUri, pathInfos, queryStrings, Fragment);

            // Assert
            Assert.AreEqual("http://a.com/a+1/b+2?c+3=d+4#e+5", uri.ToString());
        }

        [TestMethod]
        public void TestCreateUriWithParametersAreNull()
        {
            // Arrange
            var baseUri = new Uri("http://a.com");

            // Act & Assert
            Assert.AreEqual("http://a.com/",
                HttpRequestService.CreateUri(baseUri, null, null, null).ToString());
            Assert.AreEqual("http://a.com/",
                HttpRequestService.CreateUri(baseUri, Enumerable.Empty<string>(), new NameValueCollection(), string.Empty).ToString());
        }
    }
}
