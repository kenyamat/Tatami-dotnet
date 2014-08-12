namespace Tatami.Tests.Models
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Tatami.Extensions;
    using Tatami.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HttpRequestTests
    {
        [TestMethod]
        public void TestToString()
        {
            // Arrange
            const string name = "name";
            const string baseUri = "baseUri";
            const string method = "GET";
            const string userAgent = "useAgent";
            const string uri = "uri";
            var headers = new NameValueCollection { { "a", "b"} };
            var cookies = new NameValueCollection { { "c", "d" } };
            var pathInfos = new List<string> { "e", "f" };
            var queryStrings = new NameValueCollection { { "g", "h" } };
            const string fragment = "fragment";
            const string content = "content";

            // Act
            var result = new HttpRequest
            {
                Name = name,
                BaseUri = baseUri,
                Method = method,
                UserAgent = userAgent,
                Uri = uri,
                Headers = headers,
                Cookies = cookies,
                PathInfos = pathInfos,
                QueryStrings = queryStrings,
                Fragment = fragment,
                Content = content
            }.ToString();

            // Assert
            Assert.AreEqual(
                string.Format("Name={0}, BaseUri={1}, Method={2}, UserAgent={3}, Uri={4}, Headers={5}, Cookies={6}, PathInfos={7}, QueryStrings={8}, Fragment={9}, Content={10}",
                    name,
                    baseUri,
                    method,
                    userAgent,
                    uri,
                    headers.GetString(),
                    cookies.GetString(),
                    pathInfos.GetString(),
                    queryStrings.GetString(),
                    fragment,
                    content),
                result);
        }
    }
}
