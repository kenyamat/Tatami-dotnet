namespace Tatami.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using Tatami.Constants;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;

    /// <summary>
    /// UriAssertionParser class
    /// </summary>
    public static class UriAssertionParser
    {
        /// <summary>
        /// Parses csv to Assertions
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        /// <returns>Assertions list</returns>
        public static AssertionList Parse(Header assertionHeader, List<string> row)
        {
            var value = Header.GetString(assertionHeader, HeaderName.Uri, row);
            if (value == null)
            {
                return null;
            }

            if (!value.StartsWith("/"))
            {
                throw new WrongFileFormatException(
                    string.Format("Invalid Data Format. Value of <Uri> doesn't start with '/'. value={0}",
                        value));
            }

            return new AssertionList
            {
                new UriAssertion { Value = value }
            };
        }
    }
}