namespace Tatami.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using Tatami.Models;
    using Tatami.Validators.Csv;

    /// <summary>
    /// TestCasesParser class
    /// </summary>
    public static class TestCasesParser
    {
        /// <summary>
        /// Parses csv to TestCases
        /// </summary>
        /// <param name="data">data parameter</param>
        /// <param name="resources">resources parameter</param>
        /// <returns>TestCase list</returns>
        public static TestCases Parse(List<List<string>> data, ResourceManager resources = null)
        {
            if (data == null || !data.Any()) { throw new ArgumentNullException("data"); }

            var rootHeader = HeaderParser.Parse(data);
            HeaderValidator.Validate(rootHeader);

            var cases = new TestCases();
            var lastStartIndex = 0;

            for (var i = HeaderParser.HeaderRowCount; i < data.Count; i++)
            {
                var row = data[i];
                if (!string.IsNullOrWhiteSpace(row[0]))
                {
                    if (i != HeaderParser.HeaderRowCount)
                    {
                        cases.Add(TestCaseParser.Parse(rootHeader, data.GetRange(lastStartIndex, i - lastStartIndex), resources));
                    }

                    lastStartIndex = i;
                }
            }

            cases.Add(TestCaseParser.Parse(rootHeader, data.GetRange(lastStartIndex, data.Count - lastStartIndex), resources));

            return cases;
        }
    }
}