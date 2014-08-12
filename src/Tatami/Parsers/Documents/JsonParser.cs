namespace Tatami.Parsers.Documents
{
    using Tatami.Constants;
    using Newtonsoft.Json;

    /// <summary>
    /// JsonParser class
    /// </summary>
	public class JsonParser : XmlParser
	{
        /// <summary>
        /// JsonParser class
        /// </summary>
        /// <param name="contents">contents parameter</param>
		public JsonParser(string contents)
		{
			Contents = contents;
			Document = JsonConvert.DeserializeXNode(Contents, HeaderName.Root);
		}
	}
}
