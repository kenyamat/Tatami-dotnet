namespace Tatami
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using CsvParser;
    using Tatami.Models;
    using Tatami.Parsers.Csv;
    using Tatami.Parsers.Mappings;
    using Tatami.Services;

    /// <summary>
    /// TestExecutor class
    /// </summary>
    public class TestExecutor
    {
        private string testCasesCsv;
        private string baseUriMappingXml;
        private string userAgentMappingXml;
        private string proxyUri = null;

        /// <summary>
        /// Hook before requesting expected document
        /// </summary>
        public Action<HttpClient> ExpectedRequestHook { get; set; }

        /// <summary>
        /// Hook before requesting actual document
        /// </summary>
        public Action<HttpClient> ActualRequestHook { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="testCasesCsv">testCasesCsv parameter</param>
        /// <param name="baseUriMappingXml">baseUriMappingXml parameter</param>
        /// <param name="userAgentMappingXml">userAgentMappingXml parameter</param>
        /// <param name="proxyUri">proxyUri parameter</param>
        public TestExecutor(string testCasesCsv, string baseUriMappingXml, string userAgentMappingXml = null, string proxyUri = null)
        {
            if (string.IsNullOrWhiteSpace(testCasesCsv)) throw new ArgumentNullException("testCasesCsv");
            if (string.IsNullOrWhiteSpace(baseUriMappingXml)) throw new ArgumentNullException("baseUriMappingXml");

            this.testCasesCsv = testCasesCsv;
            this.baseUriMappingXml = baseUriMappingXml;
            this.userAgentMappingXml = userAgentMappingXml;
            this.proxyUri = proxyUri;
        }

        /// <summary>
        /// Executes tests
        /// </summary>
        /// <returns>failed message. it returns null if tests are succeed.</returns>
        public TestCases Test()
        {
            var testCases = TestCasesParser.Parse(new Parser().Parse(this.testCasesCsv));
            testCases.Test(new HttpRequestService(BaseUriMappingParser.Parse(XElement.Parse(baseUriMappingXml)),
                    userAgentMappingXml != null ? UserAgentMappingParser.Parse(XElement.Parse(userAgentMappingXml)) : null, proxyUri), ActualRequestHook, ExpectedRequestHook);
            return testCases;
        }

        /// <summary>
        /// Executes async tests
        /// </summary>
        public async Task<TestCases> TestAsync()
        {
            var testCases = TestCasesParser.Parse(new Parser().Parse(this.testCasesCsv));
            await testCases.TestAsync(new HttpRequestService(BaseUriMappingParser.Parse(XElement.Parse(baseUriMappingXml)),
                    userAgentMappingXml != null ? UserAgentMappingParser.Parse(XElement.Parse(userAgentMappingXml)) : null, proxyUri), ActualRequestHook, ExpectedRequestHook);
            return testCases;
        }
    }
}