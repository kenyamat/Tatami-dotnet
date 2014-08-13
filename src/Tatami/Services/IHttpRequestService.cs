namespace Tatami.Services
{
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
        /// <returns>response information</returns>
        HttpResponse GetResponse(HttpRequest request);

        /// <summary>
        /// Gets response async
        /// </summary>
        /// <param name="request">request information</param>
        /// <returns>response information</returns>
        Task<HttpResponse> GetResponseAsync(HttpRequest request);
    }
}