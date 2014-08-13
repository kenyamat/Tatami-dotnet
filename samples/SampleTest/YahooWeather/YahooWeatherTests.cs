namespace SampleTest.YahooWeather
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tatami;

    [TestClass]
    public class YahooWeatherTests
    {
        /// <summary>
        /// Test New york page
        /// https://weather.yahoo.com/us/ny/new-york-2459115/
        /// </summary>
        [TestMethod]
        public async Task TestNewYork()
        {
            // Arrange
            var testCasesCsv = await new HttpClient().GetStringAsync(
                "https://docs.google.com/spreadsheets/d/15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE/export?format=csv&id=15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE&gid=0");
            var baseUriMappingXml = File.ReadAllText(@"YahooWeather\BaseUriMapping.xml");
            var userAgentMappingXml = File.ReadAllText(@"UserAgentMapping.xml");

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
