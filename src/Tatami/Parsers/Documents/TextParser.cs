namespace Tatami.Parsers.Documents
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TextParser class
    /// </summary>
	public class TextParser : IDocumentParser
	{
        /// <summary>
        /// Gets or sets Contents
        /// </summary>
        protected string Contents { get; set; }

        /// <summary>
        /// TestParser
        /// </summary>
        /// <param name="contents"></param>
		public TextParser(string contents)
		{
			this.Contents = contents;
		}

        /// <summary>
        /// Exists Node
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns></returns>
		public bool ExistsNode(string xpath, string attribute)
		{
			throw new NotImplementedException("TextParser.ExistsNode is not supported.");
		}

        /// <summary>
        /// Gets Document value
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns>document value</returns>
		public string GetDocumentValue(string xpath, string attribute)
		{
			return this.Contents;
		}

        /// <summary>
        /// Gets document values
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns></returns>
		public IEnumerable<string> GetDocumentValues(string xpath, string attribute)
		{
			throw new NotImplementedException("TextParser.GetDocumentValues is not supported.");
		}

        /// <summary>
        /// Tests schema with xsd
        /// </summary>
        /// <param name="xsd">xsd parameter</param>
		public void TestSchemaWithXsd(string xsd)
		{
			throw new NotImplementedException("TextParser.TestSchemaWithXsd is not supported.");
		}
	}
}
