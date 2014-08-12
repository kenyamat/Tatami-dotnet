namespace Tatami.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Resources;
    using Tatami.Models;
    using Tatami.Models.Csv;
    using Tatami.Parsers.Csv.Assertions;

    /// <summary>
    /// TestCaseParser class
    /// </summary>
    public static class TestCaseParser
    {
        /// <summary>
        /// Parses xml to TestCase
        /// </summary>
        /// <param name="rootHeader">rootHeader parameter</param>
        /// <param name="data">TestCase data</param>
        /// <param name="resources">resources parameter</param>
        /// <returns>TestCase object</returns>
        public static TestCase Parse(Header rootHeader, List<List<string>> data, ResourceManager resources)
        {
            if (rootHeader == null) { throw new ArgumentNullException("rootHeader"); }
            if (data == null) { throw new ArgumentNullException("data"); }
            if (data.Count == 0 || data[0].Count == 0 || string.IsNullOrWhiteSpace(data[0][0]))
            {
                throw new WrongFileFormatException(
                    "Test case's format is invalid. data.Count == 0 || data[0].Count == 0 || string.IsNullOrWhiteSpace(data[0][0])");
            }

            return new TestCase
            {
                Name = data[0][0],
                Arranges = ArrangesParser.Parse(rootHeader.Children[0], data[0]),
                Assertions = AssertionsParser.Parse(rootHeader.Children[1], data, resources)
            };
        }
    }
}