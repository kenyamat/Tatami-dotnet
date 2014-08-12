namespace Tatami.Models
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Tatami.Extensions;

    /// <summary>
    /// HttpRequest class
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets BaseUri
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        /// Gets or sets Method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets UseAgent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets Uri
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets Headers
        /// </summary>
        public NameValueCollection Headers { get; set; }

        /// <summary>
        /// Gets or sets Cookies
        /// </summary>
        public NameValueCollection Cookies { get; set; }

        /// <summary>
        /// Gets or sets Cookies
        /// </summary>
        public IEnumerable<string> PathInfos { get; set; }

        /// <summary>
        /// Gets or sets Cookies
        /// </summary>
        public NameValueCollection QueryStrings { get; set; }

        /// <summary>
        /// Gets or sets Fragment (http://yahoo.com/national/#Temperature)
        /// </summary>
        public string Fragment { get; set; }

        /// <summary>
        /// Gets or sets Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>string </returns>
        public override string ToString()
        {
            return
                string.Format("Name={0}, BaseUri={1}, Method={2}, UserAgent={3}, Uri={4}, Headers={5}, Cookies={6}, PathInfos={7}, QueryStrings={8}, Fragment={9}, Content={10}", 
                    new object[]
			        {
				        this.Name,
				        this.BaseUri,
				        this.Method,
				        this.UserAgent,
				        this.Uri,
				        this.Headers.GetString(),
				        this.Cookies.GetString(),
				        this.PathInfos.GetString(),
				        this.QueryStrings.GetString(),
				        this.Fragment,
                        this.Content
			        });
        }
    }
}