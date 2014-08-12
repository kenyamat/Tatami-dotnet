namespace Tatami.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Xml.XPath;
    using Tatami.Constants;
    using Tatami.Extensions;
    using Tatami.Parsers.Documents;

    /// <summary>
    /// HttpResponse class
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// documentParser field
        /// </summary>
        private IDocumentParser documentParser;

        /// <summary>
        /// Gets or sets Uri
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets StatusCode
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets ContentType
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets LastModifies
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets Headers
        /// </summary>
        public NameValueCollection Headers { get; set; }

        /// <summary>
        /// Gets or sets Cookies
        /// </summary>
        public NameValueCollection Cookies { get; set; }

        /// <summary>
        /// Gets or sets Contents
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// Gets or sets Exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets Document parser
        /// </summary>
        public IDocumentParser DocumentParser
        {
            get
            {
                if (this.documentParser == null)
                {
                    switch (this.GetParserType())
                    {
                        case ParserType.Xml:
                            this.documentParser = new XmlParser(this.Contents);
                            break;
                        case ParserType.Html:
                            this.documentParser = new HtmlParser(this.Contents);
                            break;
                        case ParserType.JavaScript:
                            this.documentParser = new JsonParser(this.Contents);
                            break;
                        default:
                            this.documentParser = new TextParser(this.Contents);
                            break;
                    }
                }

                return this.documentParser;
            }
        }

        /// <summary>
        /// Exists node
        /// </summary>
        /// <param name="xpath">the xpath</param>
        /// <param name="attribute">the attribute</param>
        /// <returns>true: exists</returns>
        public bool ExistsNode(string xpath, string attribute)
        {
            try
            {
                return this.DocumentParser.ExistsNode(xpath, attribute);
            }
            catch (Exception innerException)
            {
                throw new XPathException(
                    string.Format("Failed to get value from document. ParserType={0}, xpath={1}, attribute={2}", this.GetParserType(), xpath, attribute),
                    innerException);
            }
        }

        /// <summary>
        /// Gets Document value
        /// </summary>
        /// <param name="xpath">the xpath</param>
        /// <param name="attribute">the attribute</param>
        /// <returns>document value</returns>
        public string GetDocumentValue(string xpath, string attribute)
        {
            try
            {
                return this.DocumentParser.GetDocumentValue(xpath, attribute);
            }
            catch (Exception innerException)
            {
                throw new XPathException(
                    string.Format("Failed to get value from document. ParserType={0}, xpath={1}, attribute={2}", this.GetParserType(), xpath, attribute),
                    innerException);
            }
        }

        /// <summary>
        /// Gets document values
        /// </summary>
        /// <param name="xpath">the xpath</param>
        /// <param name="attribute">the attribute</param>
        /// <returns>document values</returns>
        public IEnumerable<string> GetDocumentValues(string xpath, string attribute)
        {
            try
            {
                return this.DocumentParser.GetDocumentValues(xpath, attribute);
            }
            catch (Exception innerException)
            {
                throw new XPathException(
                    string.Format("Failed to get value from document. ParserType={0}, xpath={1}, attribute={2}", this.GetParserType(), xpath, attribute),
                    innerException);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Uri={0}, StatusCode={1}, LastModified={2}, ContentType={3}, Headers={4}, Cookies={5}, Exception={6}",
                this.Uri,
                this.StatusCode,
                this.LastModified,
                this.ContentType,
                this.Headers.GetString(),
                this.Cookies.GetString(),
                this.Exception);
        }

        /// <summary>
        /// Gets parser type
        /// </summary>
        /// <returns>parser type</returns>
        private ParserType GetParserType()
        {
            var contentType = this.ContentType;

            if (contentType.IndexOf("text/html", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                return ParserType.Html;
            }

            if (contentType.IndexOf("text/xml", StringComparison.InvariantCultureIgnoreCase) >= 0
                || contentType.IndexOf("rss+xml", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                return ParserType.Xml;
            }

            if (contentType.IndexOf("javascript", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                return ParserType.JavaScript;
            }

            return ParserType.Text;
        }
    }
}
