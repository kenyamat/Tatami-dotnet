namespace Tatami.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using Tatami.Constants;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;

    /// <summary>
    /// DateTimeAssertionParser class
    /// </summary>
    public static class DateTimeAssertionParser
    {
        /// <summary>
        /// Parses csv to Assertions
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        /// <returns>Assertions list</returns>
        public static AssertionList Parse(Header assertionHeader, List<string> row)
        {
            Validate(assertionHeader, row);

            return new AssertionList
            {
                new DateTimeAssertion
                {
                    Name = Header.GetString(assertionHeader, HeaderName.Name, row),
                    IsList = Header.GetBoolean(assertionHeader, HeaderName.IsList, row),
                    IsTime = Header.GetBoolean(assertionHeader, HeaderName.IsTime, row),
                    Expected = ContentAssertionItemParser.Parse(Header.Search(assertionHeader, HeaderName.Expected), row),
                    Actual = ContentAssertionItemParser.Parse(Header.Search(assertionHeader, HeaderName.Actual), row),
                }
            };
        }

        /// <summary>
        /// Validates row
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        private static void Validate(Header assertionHeader, List<string> row)
        {
            // Name
            var name = Header.GetString(assertionHeader, HeaderName.Name, row);
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new WrongFileFormatException("Invalid Data Format. Value of <Name> has no value.");
            }
        }
    }
}