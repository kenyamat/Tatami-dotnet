namespace SampleTest.YahooApi
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tatami;

    [TestClass]
    public class YahooApiTests
    {
        /// <summary>
        /// Test New york page
        /// http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid=%222459115%22and%20u=%22f%22&format=xml
        /// </summary>
        [TestMethod]
        public async Task TestApiWithNewYork()
        {
            // Arrange
            var testCasesCsv = await new HttpClient().GetStringAsync(
                "https://docs.google.com/spreadsheets/d/1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg/export?format=csv&id=1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg&gid=0");
            var baseUriMappingXml = File.ReadAllText(@"YahooApi\BaseUriMapping.xml");

            // Act
            var result = await TestExecutor.TestAsync(testCasesCsv, baseUriMappingXml, null);

            // Assert
            if (!string.IsNullOrWhiteSpace(result.FailedMessage))
            {
                Assert.Fail(result.FailedMessage);
            }
        }
    }
}
