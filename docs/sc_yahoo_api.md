# Showcase: Yahoo API test

### Scenario : Test Web API which returns XML/JSON using static values.(Yahoo API)

* Test target : [Yahoo API](http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid=%222459115%22and%20u=%22f%22&format=xml)
* Test project : [samples/SampleTest/SampleTest.csproj](../samples/SampleTest/SampleTest.csproj)
* Test spreadsheet : [https://docs.google.com/spreadsheets/d/1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg/edit?usp=sharing](https://docs.google.com/spreadsheets/d/1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg/edit?usp=sharing)
* CSV download link : [https://docs.google.com/spreadsheets/d/1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg/export?format=csv&id=1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg&gid=0](https://docs.google.com/spreadsheets/d/1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg/export?format=csv&id=1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg&gid=0) 

### Test details
1. Get a actual JSON document from Web App (Yahoo API) 
1. Assert HTTP response header and/or document values using static values in CSV file.

![sample3](imgs/sample3.png)

```
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
```