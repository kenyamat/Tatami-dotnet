namespace SampleTest.Wikipedia
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tatami;

    [TestClass]
    public class WikipediaTests
    {
        /// <summary>
        /// Test United States page
        /// http://en.wikipedia.org/wiki/United_States
        /// </summary>
        [TestMethod]
        public async Task TestWikipediaWithUnitedStatesPage()
        {
            // Arrange
            var testCasesCsv = await new HttpClient().GetStringAsync(
                "https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/export?format=csv&id=1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE&gid=0");
            var baseUriMappingXml = File.ReadAllText(@"Wikipedia\BaseUriMapping.xml");
            var userAgentMappingXml = File.ReadAllText(@"UserAgentMapping.xml");
            var testExecutor = new TestExecutor(testCasesCsv, baseUriMappingXml, userAgentMappingXml);

            // Act
            var result = await testExecutor.TestAsync();

            // Assert
            if (!string.IsNullOrWhiteSpace(result.FailedMessage))
            {
                Assert.Fail(result.FailedMessage);
            }
        }
    }
}
