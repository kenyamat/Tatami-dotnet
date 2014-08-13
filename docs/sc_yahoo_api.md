# Show case: Yahoo API test

### Scenario : Test Web API which returns XML/JSON using static values.(Yahoo API)

Test target : [http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid=%222459115%22and%20u=%22f%22&format=xml](http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid=%222459115%22and%20u=%22f%22&format=xml)
Test project : [samples/SampleTest/SampleTest.csproj](../samples/SampleTest/SampleTest.csproj) 
Test CSV file : [samples/SampleTest/YahooApi/Resources/Test_NewYork.xlsx](../samples/SampleTest/YahooApi/Resources/Test_NewYork.xlsx)

### Test details
1. Get a actual JSON document from Web App (Yahoo API) 
1. Assert HTTP response header and/or document values using static values in CSV file.

![sample3](imgs/sample3.png)

```
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
```