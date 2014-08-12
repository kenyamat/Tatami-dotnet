namespace Tatami.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using Tatami.Constants;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;

    /// <summary>
    /// CookiesAssertionParser class
    /// </summary>
    public static class CookiesAssertionParser
    {
        /// <summary>
        /// Parses csv to Assertions
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="row">row parameter</param>
        /// <returns>Assertions list</returns>
        public static AssertionList Parse(Header assertionHeader, List<string> row)
        {
            var collection = Header.GetNameValueCollection(assertionHeader, HeaderName.Cookies, row);
            if (collection.Count == 0)
            {
                return null;
            }

            var list = new AssertionList();

            foreach (string key in collection.Keys)
            {
                list.Add(new CookieAssertion
                        {
                            Key = key,
                            Value = collection[key]
                        });
            }

            return list;
        }
    }
}