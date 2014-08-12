namespace Tatami.Parsers.Csv.Assertions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Tatami.Constants;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;

    /// <summary>
    /// ContentAssertionItemParser class
    /// </summary>
    public static class ContentAssertionItemParser
    {
        /// <summary>
        /// Parses csv to Assertions
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        /// <returns>Assertions list</returns>
        public static ContentAssertionItem Parse(Header assertionHeader, List<string> row)
        {
            if (assertionHeader == null)
            {
                return null;
            }

            Validate(assertionHeader, row);

            return new ContentAssertionItem
            {
                Key = Header.GetString(assertionHeader, HeaderName.Key, row),
                Value = Header.GetString(assertionHeader, HeaderName.Value, row),
                Query = Header.GetString(assertionHeader, HeaderName.Query, row),
                Attribute = Header.GetString(assertionHeader, HeaderName.Attribute, row),
                Exists = Header.GetNullableBoolean(assertionHeader, HeaderName.Exists, row),
                Pattern = Header.GetString(assertionHeader, HeaderName.Pattern, row),
                Format = Header.GetString(assertionHeader, HeaderName.Format, row),
                FormatCulture = Header.GetString(assertionHeader, HeaderName.FormatCulture, row),
                UrlDecode = Header.GetBoolean(assertionHeader, HeaderName.UrlDecode, row),
            };
        }

        /// <summary>
        /// Validates row
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        private static void Validate(Header assertionHeader, List<string> row)
        {
            // FormatCulture attribute
            var formatCulture = Header.GetString(assertionHeader, "FormatCulture", row);
            if (formatCulture != null)
            {
                try
                {
                    // ReSharper disable UnusedVariable
                    var cultureInfo = new CultureInfo(formatCulture);
                    // ReSharper restore UnusedVariable
                }
                catch (Exception)
                {
                    throw new WrongFileFormatException(
                        string.Format("Invalid Data Format. <FormatCulture> is unknown CultureInfo. value={0}",
                            formatCulture));

                }
            }
        }
    }
}