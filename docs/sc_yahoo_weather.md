# Showcase: Yahoo Weather page test

### Scenario : Test a HTML structure of "New York" page using values from Web API (Yahoo Weather RSS).
Recent Web applications consume data created by Web API instead of database. This scenario will cover to test values between HTML and XML from Web API.     

* Test target : [http://weather.yahoo.com/united-states/new-york/new-york-2459115/](http://weather.yahoo.com/united-states/new-york/new-york-2459115/)
* Test expected value : [http://weather.yahooapis.com/forecastrss?w=2459115](http://weather.yahooapis.com/forecastrss?w=2459115)
* Test project : [samples/SampleTest/SampleTest.csproj](../samples/SampleTest/SampleTest.csproj) 
* Test CSV file : [samples/SampleTest/YahooWeather/Resources/Test_NewYork.xlsx](../samples/SampleTest/YahooWeather/Resources/Test_NewYork.xlsx)

### Test details
1. Get a expected XML document from Web API (Yahoo Weather RSS)
1. Get a actual html document from Web App (Yahoo Weather New youk page) 
1. Assert HTTP response header and/or document values using expected values in XML or CSV file.

![sample2](imgs/sample2.png)

```
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
```