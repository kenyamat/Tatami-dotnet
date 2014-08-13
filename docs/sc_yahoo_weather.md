# Showcase: Yahoo Weather page test

### Scenario : Test a HTML structure of "New York" page using values from Web API (Yahoo Weather RSS).
Recent Web applications consume data created by Web API instead of database. This scenario will cover to test values between HTML and XML from Web API.     

* Test target : [http://weather.yahoo.com/united-states/new-york/new-york-2459115/](http://weather.yahoo.com/united-states/new-york/new-york-2459115/)
* Test expected value : [http://weather.yahooapis.com/forecastrss?w=2459115](http://weather.yahooapis.com/forecastrss?w=2459115)
* Test project : [samples/SampleTest/SampleTest.csproj](../samples/SampleTest/SampleTest.csproj) 
* Test spreadsheet : [https://docs.google.com/spreadsheets/d/15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE/edit?usp=sharing](https://docs.google.com/spreadsheets/d/15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE/edit?usp=sharing)
* CSV download link: [https://docs.google.com/spreadsheets/d/15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE/export?format=csv&id=15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE&gid=0](https://docs.google.com/spreadsheets/d/15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE/export?format=csv&id=15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE&gid=0) 

### Test details
1. Get a expected XML document from Web API (Yahoo Weather RSS)
1. Get a actual html document from Web App (Yahoo Weather New youk page) 
1. Assert HTTP response header and/or document values using expected values in XML or CSV file.

![sample2](imgs/sample2.png)

```
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
```