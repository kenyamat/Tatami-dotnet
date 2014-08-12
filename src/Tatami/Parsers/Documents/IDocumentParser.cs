namespace Tatami.Parsers.Documents
{
    using System.Collections.Generic;

    /// <summary>
    /// DocumentParser interface
    /// </summary>
	public interface IDocumentParser
	{
        /// <summary>
        /// Exists Node
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns></returns>
		bool ExistsNode(string xpath, string attribute);

        /// <summary>
        /// Gets Document value
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns>document value</returns>
		string GetDocumentValue(string xpath, string attribute);

        /// <summary>
        /// Gets document values
        /// </summary>
        /// <param name="xpath">xpath parameter</param>
        /// <param name="attribute">attribute parameter</param>
        /// <returns></returns>
		IEnumerable<string> GetDocumentValues(string xpath, string attribute);

        /// <summary>
        /// Tests schema with xsd
        /// </summary>
        /// <param name="xsd">xsd parameter</param>
		void TestSchemaWithXsd(string xsd);
	}
}
