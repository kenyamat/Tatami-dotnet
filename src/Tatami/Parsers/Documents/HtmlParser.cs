namespace Tatami.Parsers.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using HtmlAgilityPack;

    /// <summary>
    /// HtmlParser class
    /// </summary>
	public class HtmlParser : IDocumentParser
	{
        /// <summary>
        /// Gets or sets Contents
        /// </summary>
		protected string Contents { get; set; }

        /// <summary>
        /// Gets or sets Document
        /// </summary>
        protected HtmlDocument Document { get; set; }

        /// <summary>
        /// HtmlParser class
        /// </summary>
        /// <param name="contents">contents parameter</param>
		public HtmlParser(string contents)
		{
			this.Contents = contents;
			this.Document = new HtmlDocument();
			this.Document.LoadHtml(this.Contents);
		}

        /// <summary>
        /// Exists Node
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns>true if node exists</returns>
		public bool ExistsNode(string xpath, string attribute)
		{
			var htmlNode = this.Document.DocumentNode.SelectSingleNode(xpath);
			if (htmlNode == null)
			{
                return false;
			}

			if (!string.IsNullOrWhiteSpace(attribute))
			{
			    var attributeNode = htmlNode.Attributes[attribute];
                if (attributeNode == null)
				{
					return false;
				}
			}

			return true;
		}

        /// <summary>
        /// Gets Document value
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns>document value</returns>
		public string GetDocumentValue(string xpath, string attribute)
		{
			var htmlNode = this.Document.DocumentNode.SelectSingleNode(xpath);
			var text = (attribute == null) ? htmlNode.InnerText : htmlNode.GetAttributeValue(attribute, string.Empty);
			if (text != null)
			{
				text = HttpUtility.HtmlDecode(text);
			}

			return text;
		}

        /// <summary>
        /// Gets document values
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns></returns>
		public IEnumerable<string> GetDocumentValues(string xpath, string attribute)
		{
			var list = new List<string>();
			var htmlNodeCollection = this.Document.DocumentNode.SelectNodes(xpath);

			foreach (var current in htmlNodeCollection)
			{
				var text = (attribute == null) ? current.InnerText : current.GetAttributeValue(attribute, string.Empty);
				if (text != null)
				{
					text = HttpUtility.HtmlDecode(text);
				}

				list.Add(text);
			}

			return list;
		}

        /// <summary>
        /// Tests schema with xsd
        /// </summary>
        /// <param name="xsd">xsd parameter</param>
		public void TestSchemaWithXsd(string xsd)
		{
			throw new NotImplementedException("HtmlParser.TestSchemaWithXsd is not supported.");
		}
	}
}
