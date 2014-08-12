namespace Tatami.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using Tatami.Constants;
    using Tatami.Models;
    using Tatami.Models.Csv;

    /// <summary>
    /// HttpRequestParser class
    /// </summary>
    public static class HttpRequestParser
    {
        /// <summary>
        /// Parses csv to HttpRequest
        /// </summary>
        /// <param name="httpRequestHeader">httpRequestHeader parameter</param>
        /// <param name="row">TestCase row data</param>
        /// <param name="name">name parameter</param>
        /// <returns>HttpRequest object</returns>
        public static HttpRequest Parse(Header httpRequestHeader, List<string> row, string name)
        {
            if (httpRequestHeader == null) { throw new ArgumentNullException("httpRequestHeader"); }
            if (row == null) { throw new ArgumentNullException("row"); }
            if (name == null) { throw new ArgumentNullException("name"); }

			var flag = true;
			for (var i = httpRequestHeader.From; i <= httpRequestHeader.To; i++)
			{
				if (!string.IsNullOrWhiteSpace(row[i]))
				{
					flag = false;
					break;
				}
			}

			if (flag)
			{
				return null;
			}

            Validate(httpRequestHeader, row, name);

            var method = Header.GetString(httpRequestHeader, HeaderName.Method, row);
            method = !string.IsNullOrWhiteSpace(method) ? method.ToUpperInvariant() : null;

            return new HttpRequest
            {
                Name = name,
                BaseUri = Header.GetString(httpRequestHeader, HeaderName.BaseUri, row),
                Method = method,
                UserAgent = Header.GetString(httpRequestHeader, HeaderName.UserAgent, row),
                Headers = Header.GetNameValueCollection(httpRequestHeader, HeaderName.Headers, row),
                Cookies = Header.GetNameValueCollection(httpRequestHeader, HeaderName.Cookies, row),
                PathInfos = Header.GetStringList(httpRequestHeader, HeaderName.PathInfos, row),
                QueryStrings = Header.GetNameValueCollection(httpRequestHeader, HeaderName.QueryStrings, row),
                Fragment = Header.GetString(httpRequestHeader, HeaderName.Fragment, row),
                Content = Header.GetString(httpRequestHeader, HeaderName.Content, row),
            };
        }

        /// <summary>
        /// Validates row
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="row">row parameter</param>
        /// <param name="name">name parameter</param>
        private static void Validate(Header header, List<string> row, string name)
        {
            var baseUri = Header.GetString(header, "BaseUri", row);
            if (baseUri == null)
            {
                throw new WrongFileFormatException(string.Format("<BaseUri> should be not null. name={0}", name));
            }
        }
    }
}