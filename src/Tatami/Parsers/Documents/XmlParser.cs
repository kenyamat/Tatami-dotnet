namespace Tatami.Parsers.Documents
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.XPath;

    /// <summary>
    /// XmlParser class
    /// </summary>
	public class XmlParser : IDocumentParser
	{
        private readonly XmlNamespaceManager nsmg;

        /// <summary>
        /// Gets or sets Contents
        /// </summary>
        protected string Contents { get; set; }

        /// <summary>
        /// Gets or sets Document
        /// </summary>
        protected XDocument Document { get; set; }

        /// <summary>
        /// XmlParser
        /// </summary>
        public XmlParser()
        {            
        }

        /// <summary>
        /// XmlParser
        /// </summary>
        /// <param name="contents">contents parameter</param>
		public XmlParser(string contents)
		{
			this.Contents = contents;
			this.Document = XDocument.Parse(this.Contents);

            // http://www.hanselman.com/blog/GetNamespacesFromAnXMLDocumentWithXPathDocumentAndLINQToXML.aspx
            var result = this.Document.Root.Attributes().
                Where(a => a.IsNamespaceDeclaration).
                GroupBy(a => a.Name.Namespace == XNamespace.None ? string.Empty : a.Name.LocalName,
                        a => XNamespace.Get(a.Value)).
                ToDictionary(g => g.Key,
                             g => g.First());

            nsmg = new XmlNamespaceManager(new NameTable());
            foreach (var item in result)
            {
                nsmg.AddNamespace(item.Key, item.Value.NamespaceName);
            }
		}

        /// <summary>
        /// Exists Node
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns></returns>
		public bool ExistsNode(string xpath, string attribute)
		{
			var element = this.Document.XPathSelectElement(xpath);
			if (element == null)
			{
				return false;
			}

			if (!string.IsNullOrWhiteSpace(attribute))
			{
				var attr = element.Attribute(attribute);
				if (attr == null)
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
			var element = this.Document.XPathSelectElement(xpath, nsmg);
			return (attribute == null) ? element.Value : element.Attribute(attribute).Value;
		}

        /// <summary>
        /// Gets document values
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns></returns>
		public IEnumerable<string> GetDocumentValues(string xpath, string attribute)
		{
			var values = new List<string>();
			foreach (var current in this.Document.XPathSelectElements(xpath, nsmg))
			{
				values.Add((attribute == null) ? current.Value : current.Attribute(attribute).Value);
			}

			return values;
		}

        /// <summary>
        /// Tests schema with xsd
        /// </summary>
        /// <param name="xsd">xsd parameter</param>
		public void TestSchemaWithXsd(string xsd)
		{
			var xmlSchemaSet = new XmlSchemaSet();
			using (var stringReader = new StringReader(xsd))
			{
				xmlSchemaSet.Add(null, XmlReader.Create(stringReader));
			}

			var settings = new XmlReaderSettings
			{
				ValidationType = ValidationType.Schema,
				Schemas = xmlSchemaSet
			};

			var xmlDocument = new XmlDocument();
			var input = new StringReader(this.Document.ToString());
			using (var xmlReader = XmlReader.Create(input, settings))
			{
				xmlDocument.Load(xmlReader);
			}
		}
	}
}
