namespace Tatami.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using System.Resources;
    using Tatami.Constants;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;

    /// <summary>
    /// XsdAssertionParser class
    /// </summary>
	public static class XsdAssertionParser
	{
        /// <summary>
        /// Parses csv to Assertions
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        /// <param name="resources">resource parameter</param>
        /// <returns></returns>
		public static AssertionList Parse(Header assertionHeader, List<string> row, ResourceManager resources)
		{
            var value = Header.GetString(assertionHeader, HeaderName.Xsd, row);
			if (value == null)
			{
				return null;
			}

			string string2 = resources.GetString(value);
			if (string.IsNullOrWhiteSpace(string2))
			{
				throw new WrongFileFormatException(
                    string.Format("Invalid Data Format. Value of <Xsd> is not exist in Resources. key={0}", value));
			}

			return new AssertionList 
			{
				new XsdAssertion
				{
					Xsd = string2
				}
			};
		}
	}
}
