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
        Task<HttpResponse> GetResponse(HttpRequest request);
    }
}