namespace Tatami
{
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Csv;
    using Tatami.Models;
    using Tatami.Parsers.Csv;
    using Tatami.Parsers.Mappings;
    using Tatami.Services;

    /// <summary>
    /// TestExecutor class
    /// </summary>
    public static class TestExecutor
    {
        /// <summary>
        /// Executes tests
        /// </summary>
        /// <param name="testCasesCsv">testCasesCsv parameter</param>
        /// <param name="baseUriMappingXml">baseUriMappingXml parameter</param>
        /// <param name="userAgentMappingXml">userAgentMappingXml parameter</param>
        /// <param name="proxyUri">proxyUri parameter</param>
        /// <returns>failed message. it returns null if tests are succeed.</returns>
        public static TestCases Test(string testCasesCsv, string baseUriMappingXml, string userAgentMappingXml, string proxyUri = null)
        {
            var testCases = TestCasesParser.Parse(new CsvParser().Parse(testCasesCsv));
            testCases.Test(
                new HttpRequestService(BaseUriMappingParser.Parse(XElement.Parse(baseUriMappingXml)),
                    userAgentMappingXml != null ? UserAgentMappingParser.Parse(XElement.Parse(userAgentMappingXml)) : null, proxyUri));
            return testCases;
        }

        /// <summary>
        /// Executes async tests
        /// </summary>
        /// <param name="testCasesCsv">testCasesCsv parameter</param>
        /// <param name="baseUriMappingXml">baseUriMappingXml parameter</param>
        /// <param name="userAgentMappingXml">userAgentMappingXml parameter</param>
        /// <param name="proxyUri">proxyUri parameter</param>
        /// <returns>failed message. it returns null if tests are succeed.</returns>
        public static async Task<TestCases> TestAsync(string testCasesCsv, string baseUriMappingXml, string userAgentMappingXml, string proxyUri = null)
        {
            var testCases = TestCasesParser.Parse(new CsvParser().Parse(testCasesCsv));
            await testCases.TestAsync(
                new HttpRequestService(BaseUriMappingParser.Parse(XElement.Parse(baseUriMappingXml)),
                    userAgentMappingXml != null ? UserAgentMappingParser.Parse(XElement.Parse(userAgentMappingXml)) : null, proxyUri));
            return testCases;
        }
    }
}