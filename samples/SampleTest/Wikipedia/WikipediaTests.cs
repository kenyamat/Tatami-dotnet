namespace SampleTest.Wikipedia
{
    using System.IO;
    using System.Threading.Tasks;
    using Tatami;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var testCasesCsv = File.ReadAllText(@"Wikipedia\Resources\Test_United_States.csv");
            var baseUriMappingXml = File.ReadAllText(@"Wikipedia\Resources\BaseUriMapping.xml");
            var userAgentMappingXml = File.ReadAllText(@"Wikipedia\Resources\UserAgentMapping.xml");

            // Act
            var result = await TestExecutor.TestAsync(testCasesCsv, baseUriMappingXml, userAgentMappingXml);

            // Assert
            if (!string.IsNullOrWhiteSpace(result.FailedMessage))
            {
                Assert.Fail(result.FailedMessage);
            }
        }
    }
}
