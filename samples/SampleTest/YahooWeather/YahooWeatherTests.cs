namespace SampleTest.YahooWeather
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tatami;

    [TestClass]
    public class YahooWeatherTests
    {
        private static readonly string baseUriMappingXml = File.ReadAllText(@"YahooWeather\Resources\BaseUriMapping.xml");
        private static readonly string userAgentMappingXml = File.ReadAllText(@"YahooWeather\Resources\UserAgentMapping.xml");

        /// <summary>
        /// Test New york page
        /// https://weather.yahoo.com/us/ny/new-york-2459115/
        /// </summary>
        [TestMethod]
        public async Task TestNewYork()
        {
            // Arrange
            var testCasesCsv = File.ReadAllText(@"YahooWeather\Resources\Test_NewYork.csv");

            // Act
            var result = await TestExecutor.Test(testCasesCsv, baseUriMappingXml, userAgentMappingXml, null);
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
