namespace Tatami.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using Tatami.Constants;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;

    /// <summary>
    /// StatusCodeAssertionParser class
    /// </summary>
    public static class StatusCodeAssertionParser
    {
        /// <summary>
        /// Parses csv to Assertions
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        /// <returns>Assertions list</returns>
        public static AssertionList Parse(Header assertionHeader, List<string> row)
        {
            var value = Header.GetString(assertionHeader, HeaderName.StatusCode, row);
            if (value == null)
            {
                return null;
            }

            int result;
            if (!int.TryParse(value, out result))
            {
                throw new WrongFileFormatException(
                    string.Format("Invalid Data Format. Value of <StatusCode> is not numeric. value={0}",
                        value));
            }

            return new AssertionList
            {
                new StatusCodeAssertion { Value = value }
            };
        }
    }
}