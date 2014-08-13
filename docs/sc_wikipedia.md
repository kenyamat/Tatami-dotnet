# Show case: Wikipedia page test

### Scenario : Test a HTML structure of "United States" page using static expected values.
This scenario covers to test between html values and static values in CSV. 

Test target : [http://en.wikipedia.org/wiki/United_States](http://en.wikipedia.org/wiki/United_States)
Test project : [samples/SampleTest/SampleTest.csproj](../samples/SampleTest/SampleTest.csproj) 
Test CSV file : [samples/SampleTest/Wikipedia/Resources/Test_United_States.xlsx](../samples/SampleTest/Wikipedia/Resources/Test_United_States.xlsx)

### Test details
1. Get a actual html document from Web App.
1. Assert HTTP response header and/or document values using expected values in CSV file.

![sample1](imgs/sample1.png)

### Setup steps
1. Create a test project. 
1. Install Tatami library from nuget.org then sets this into the project.
	* [https://www.nuget.org/packages/Tatami.NET/](https://www.nuget.org/packages/Tatami.NET/)
1. Create a BaseUriMapping.xml
	* See more details : [BaseUriMapping.xml settings](BaseUriMapping.md)	
1. Create a UserAgentMapping.xml 
	* See more details : [UserAgentMapping.xml settings](UserAgentMapping.md)	
1. Create a CSV file including test cases.
	* csv file : [samples/SampleTest/Wikipedia/Resources/Test_United_States.csv](../samples/SampleTest/Wikipedia/Resources/Test_United_States.csv)
	* excel file : [samples/SampleTest/Wikipedia/Resources/Test_United_States.xls](../samples/SampleTest/Wikipedia/Resources/Test_United_States.xlsx)
1. Create a test method.
	* [samples/SampleTest/Wikipedia/WikipediaTests.cs](../samples/SampleTest/Wikipedia/WikipediaTests.cs)
1. Run test.

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