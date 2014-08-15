namespace Tatami.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Tatami.Models;

    /// <summary>
    /// IHttpRequestService interface
    /// </summary>
    public interface IHttpRequestService
    {
        /// <summary>
        /// Gets response
        /// </summary>
        /// <param name="request">request information</param>
        /// <param name="hook">Hook before requesting</param>
        /// <returns>response information</returns>
        HttpResponse GetResponse(HttpRequest request, Action<HttpClient> hook = null);

        /// <summary>
        /// Gets response async
        /// </summary>
        /// <param name="request">request information</param>
        /// <param name="hook">Hook before requesting</param>
        /// <returns>response information</returns>
        Task<HttpResponse> GetResponseAsync(HttpRequest request, Action<HttpClient> hook = null);
    }
}