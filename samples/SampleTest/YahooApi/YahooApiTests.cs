namespace SampleTest.YahooApi
{
    using System.IO;
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
            var baseUriMappingXml = File.ReadAllText(@"YahooApi\Resources\BaseUriMapping.xml");
            var testCasesCsv = File.ReadAllText(@"YahooApi\Resources\Test_NewYork.csv");

            // Act
            var result = await TestExecutor.TestAsync(testCasesCsv, baseUriMappingXml, null);
            var failedMessage = result.FailedMessage;

            // Assert
            // var debug = result.ResultMessage;
            if (!string.IsNullOrWhiteSpace(failedMessage))
            {
                Assert.Fail(failedMessage);
            }
        }
    }
}
