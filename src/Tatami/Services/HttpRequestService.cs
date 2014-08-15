namespace Tatami.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;
    using Tatami.Models.Mappings;
    using HttpRequest = Tatami.Models.HttpRequest;
    using HttpResponse = Tatami.Models.HttpResponse;

    /// <summary>
    /// HttpRequestService class
    /// </summary>
    public class HttpRequestService : IHttpRequestService
    {
        /// <summary>
        /// baseUriMapping field
        /// </summary>
        private readonly BaseUriMapping baseUriMapping;

        /// <summary>
        /// userAgentMapping field
        /// </summary>
        private readonly UserAgentMapping userAgentMapping;
        
        /// <summary>
        /// proxyUri field
        /// </summary>
        private readonly string proxyUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestService" /> class.
        /// </summary>
        /// <param name="baseUriMapping">baseUriMapping parameter</param>
        /// <param name="userAgentMapping">userAgentMapping parameter</param>
        /// <param name="proxyUri">proxyUri parameter</param>
		public HttpRequestService(BaseUriMapping baseUriMapping, UserAgentMapping userAgentMapping, string proxyUri)
        {
            this.baseUriMapping = baseUriMapping;
            this.userAgentMapping = userAgentMapping;
            this.proxyUri = proxyUri;
        }

        /// <summary>
        /// Create Uri
        /// </summary>
        /// <param name="baseUri">base uri</param>
        /// <param name="pathInfos">path infos</param>
        /// <param name="queryStrings">query strings</param>
        /// <param name="fragment">fragment string</param>
        /// <returns>request uri</returns>
        public static Uri CreateUri(
            Uri baseUri,
            IEnumerable<string> pathInfos,
            NameValueCollection queryStrings,
            string fragment)
        {
            UriBuilder uriBuilder;

            // build pathInfo
            if (pathInfos != null && pathInfos.Any())
            {
                uriBuilder = new UriBuilder(new Uri(baseUri, new Uri(ToPathInfo(pathInfos), UriKind.Relative)));
            }
            else
            {
                uriBuilder = new UriBuilder(baseUri);
            }

            // build queryString
            if (queryStrings != null && queryStrings.HasKeys())
            {
                var query = HttpUtility.ParseQueryString(baseUri.Query);
                foreach (string key in queryStrings)
                {
                    query[key] = queryStrings[key];
                }

                uriBuilder.Query = ToQueryString(query);
            }

            // append fragment
            if (!string.IsNullOrWhiteSpace(fragment))
            {
                uriBuilder.Fragment = HttpUtility.UrlEncode(fragment);
            }

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Gets response
        /// </summary>
        /// <param name="request">request information</param>
        /// <param name="hook">Hook before requesting</param>
        /// <returns>response information</returns>
        public HttpResponse GetResponse(HttpRequest request, Action<HttpClient> hook = null)
        {
            return GetResponseAsync(request, false, hook).Result;
        }

        /// <summary>
        /// Gets response
        /// </summary>
        /// <param name="request">request information</param>
        /// <param name="hook">Hook before requesting</param>
        /// <returns>response information</returns>
        public async Task<HttpResponse> GetResponseAsync(HttpRequest request, Action<HttpClient> hook = null)
        {
            return await GetResponseAsync(request, true, hook);
        }

        /// <summary>
        /// Gets response
        /// </summary>
        /// <param name="request">request information</param>
        /// <param name="isAsync">is async</param>
        /// <param name="hook">Hook before requesting</param>
        /// <returns>response information</returns>
        public async Task<HttpResponse> GetResponseAsync(HttpRequest request, bool isAsync, Action<HttpClient> hook = null)
        {
            var baseUri = new Uri(this.baseUriMapping[request.BaseUri]);
            var requestUri = CreateUri(baseUri, request.PathInfos, request.QueryStrings, request.Fragment);
            request.Uri = requestUri.ToString();
            var handler = new HttpClientHandler();

            if (!string.IsNullOrWhiteSpace(this.proxyUri))
            {
                handler.Proxy = new System.Net.WebProxy(this.proxyUri);
                handler.UseProxy = true;
            }

            if (request.Cookies != null && request.Cookies.HasKeys())
            {
                handler.CookieContainer = new CookieContainer();
                handler.CookieContainer.Add(baseUri, CreateCookieCollection(request.Cookies));
                handler.UseCookies = true;
            }

            var httpClient = new HttpClient(handler);

            if (request.Headers != null && request.Headers.HasKeys())
            {
                foreach (string key in request.Headers)
                {
                    httpClient.DefaultRequestHeaders.Add(key, request.Headers[key]);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.UserAgent))
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(this.userAgentMapping[request.UserAgent]);
            }

            if (hook != null)
            {
                hook(httpClient);
            }

            var message = isAsync
                ? await RequestAsync(httpClient, request.Method, requestUri, request.Content)
                : RequestAsync(httpClient, request.Method, requestUri, request.Content).Result;


            var httpResponse = new HttpResponse
            {
                Headers = CreateNameValueCollection(message.Headers),
                Cookies = CreateNameValueCollection(handler.CookieContainer.GetCookies(requestUri)),
                StatusCode = message.StatusCode,
                ContentType = message.Content.Headers.ContentType.ToString()
            };

            var lastModified = message.Content.Headers.LastModified;
            if (lastModified != null)
            {
                httpResponse.LastModified = lastModified.Value.UtcDateTime;
            }

            httpResponse.Uri = message.RequestMessage.RequestUri;
            httpResponse.Contents = isAsync ? await message.Content.ReadAsStringAsync() : message.Content.ReadAsStringAsync().Result;

            return httpResponse;
        }

        private async Task<HttpResponseMessage> RequestAsync(HttpClient httpClient, string method, Uri requestUri, string content = null)
        {
            if (method == "POST")
            {
                return await httpClient.PostAsync(requestUri, content != null ? new StringContent(content) : null);
            }

            if (method == "DELETE")
            {
                return await httpClient.DeleteAsync(requestUri);
            }

            if (method == "PUT")
            {
                return await httpClient.PutAsync(requestUri, content != null ? new StringContent(content) : null);
            }

            return await httpClient.GetAsync(requestUri);
        }

        /// <summary>
        /// convert from NameValueCollection to query string
        /// </summary>
        /// <param name="queryStrings">queryStrings parameter</param>
        /// <returns>query string</returns>
        private static string ToQueryString(NameValueCollection queryStrings)
        {
            var array = (from key in queryStrings.AllKeys
                         from value in queryStrings.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();
            return string.Join("&", array);
        }

        /// <summary>
        /// convert from List to path info string
        /// </summary>
        /// <param name="pathInfos">pathInfos parameter</param>
        /// <returns>pathInfo string</returns>
        private static string ToPathInfo(IEnumerable<string> pathInfos)
        {
            var array = (from value in pathInfos
                         select HttpUtility.UrlEncode(value)).ToArray();
            if (pathInfos.Last().Contains("."))
            {
                return string.Join("/", array);
            }

            return string.Join("/", array);
        }

        /// <summary>
        /// Create NameValueCollection from HttpResponseHeaders
        /// </summary>
        /// <param name="headers">header parameter</param>
        /// <returns>the NameValueCollection</returns>
        private static NameValueCollection CreateNameValueCollection(HttpResponseHeaders headers)
        {
            var nvc = new NameValueCollection();

            foreach (var header in headers)
            {
                nvc.Add(header.Key, string.Join(",", header.Value));
            }

            return nvc;
        }

        /// <summary>
        /// Create CookiContainer
        /// </summary>
        /// <param name="cookies">cookies parameter</param>
        /// <returns>the CookieContainer</returns>
        private static CookieCollection CreateCookieCollection(NameValueCollection cookies)
        {
            var cookieCollection = new CookieCollection();

            foreach (string key in cookies)
            {
                cookieCollection.Add(new Cookie(key, cookies[key]));
            }

            return cookieCollection;
        }

        /// <summary>
        /// Create NameValueCollection from CookieCollection
        /// </summary>
        /// <param name="cookieCollection">cookies parameter</param>
        /// <returns>the CookieContainer</returns>
        private static NameValueCollection CreateNameValueCollection(CookieCollection cookieCollection)
        {
            var collection = new NameValueCollection();

            foreach (Cookie cookie in cookieCollection)
            {
                collection.Add(cookie.Name, cookie.Value);
            }

            return collection;
        }
    }
}