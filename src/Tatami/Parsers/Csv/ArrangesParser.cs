namespace Tatami.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Constants;
    using Tatami.Models;
    using Tatami.Models.Csv;

    /// <summary>
    /// ArrangesParser class
    /// </summary>
    public static class ArrangesParser
    {
        /// <summary>
        /// Parses csv to Arranges
        /// </summary>
        /// <param name="arrangeHeader">arrangeHeader parameter</param>
        /// <param name="data">TestCase data</param>
        /// <returns>Arranges object</returns>
        public static Arranges Parse(Header arrangeHeader, List<string> data)
        {
            if (arrangeHeader == null) { throw new ArgumentNullException("arrangeHeader"); }
            if (arrangeHeader.Children == null) { throw new ArgumentException("arrangeHeader.Children must not be null."); }
            if (!arrangeHeader.Children.Any()) { throw new ArgumentException("arrangeHeader.Children must no be empty."); }
            if (data == null) { throw new ArgumentNullException("data"); }

            var arranges = new Arranges();

            foreach (var httpRequestHeader in arrangeHeader.Children)
            {
                if (httpRequestHeader.Name == HeaderName.HttpRequestExpected)
                {
                    arranges.Expected = new Arrange
                    {
                        Name = "Expected",
                        HttpRequest = HttpRequestParser.Parse(httpRequestHeader, data, HeaderName.Expected),
                    };
                }
                else if (httpRequestHeader.Name == HeaderName.HttpRequestActual)
                {
                    arranges.Actual = new Arrange
                    {
                        Name = "Actual",
                        HttpRequest = HttpRequestParser.Parse(httpRequestHeader, data, HeaderName.Actual),
                    };
                }
                else
                {
                    throw new WrongFileFormatException(
                        "Invalid HttpRequest name. Expected is 'HttpRequest Expected' or 'HttpRequest Actual'. name=" + httpRequestHeader.Name);
                }
            }

            if (arranges.Actual == null)
            {
                throw new WrongFileFormatException("HttpRequest Actual is empty.");
            }

            return arranges;
        }
    }
}