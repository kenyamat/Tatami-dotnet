namespace Tatami.Parsers.Csv.Assertions
{
    using System.Collections.Generic;
    using System.Resources;
    using Tatami.Constants;
    using Tatami.Models.Assertions;
    using Tatami.Models.Csv;

    /// <summary>
    /// AssertionsParser class
    /// </summary>
    public static class AssertionsParser
    {
        /// <summary>
        /// Parses csv to Assertions
        /// </summary>
        /// <param name="assertionHeader">assertionHeader parameter</param>
        /// <param name="data">data parameter</param>
        /// <param name="resources">resources parameter</param>
        /// <returns>Assertions object</returns>
        public static AssertionList Parse(Header assertionHeader, List<List<string>> data, ResourceManager resources = null)
        {
            var list = new AssertionList();
            
            foreach (var row in data)
            {
                for (var j = assertionHeader.From; j < row.Count; j++)
                {
                    var value = row[j];
                    if (value == null)
                    {
                        continue;
                    }

                    var assertionItemHeader = Header.GetHeader(assertionHeader, j, 1);
                    var name = assertionItemHeader.Name;
                    AssertionList assertions;

                    if (name == HeaderName.Uri)
                    {
                        assertions = UriAssertionParser.Parse(assertionHeader, row);
                    }
                    else if (name == HeaderName.StatusCode)
                    {
                        assertions = StatusCodeAssertionParser.Parse(assertionHeader, row);
                    }
                    else if (name == HeaderName.Headers)
                    {
                        assertions = HeadersAssertionParser.Parse(assertionHeader, row);
                    }
                    else if (name == HeaderName.Cookies)
                    {
                        assertions = CookiesAssertionParser.Parse(assertionHeader, row);
                    }
                    else if (name == HeaderName.Xsd)
                    {
                        assertions = XsdAssertionParser.Parse(assertionHeader, row, resources);
                    }
                    else if (name == HeaderName.Contents)
                    {
                        if (Header.GetBoolean(assertionHeader, HeaderName.IsDateTime, row)
                            || Header.GetBoolean(assertionHeader, HeaderName.IsTime, row))
                        {
                            assertions = DateTimeAssertionParser.Parse(assertionHeader, row);
                        }
                        else
                        {
                            assertions = TextAssertionParser.Parse(assertionHeader, row);
                        }
                    }
                    else
                    {
                        throw new WrongFileFormatException("Invalid Assertion name. name=" + name);
                    }

                    if (assertions != null)
                    {
                        list.AddRange(assertions);
                    }

                    j = assertionHeader.To;
                }
            }

            return list;
        }
    }
}