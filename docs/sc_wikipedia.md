# Showcase: Wikipedia page test

### Scenario : Test a HTML structure of "United States" page using static expected values.
This scenario covers to test between html values and static values in CSV. 

* Test target : [http://en.wikipedia.org/wiki/United_States](http://en.wikipedia.org/wiki/United_States)
* Test project : [samples/SampleTest/SampleTest.csproj](../samples/SampleTest/SampleTest.csproj) 
* Test spreadsheet : [https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing](https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing)
* CSV download link: [https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/export?format=csv&id=1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE&gid=0](https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/export?format=csv&id=1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE&gid=0)

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
	* csv file : [https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing](https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing)
1. Create a test method.
	* [samples/SampleTest/Wikipedia/WikipediaTests.cs](../samples/SampleTest/Wikipedia/WikipediaTests.cs)
1. Run test.

```
[TestMethod]
public async Task TestWikipediaWithUnitedStatesPage()
{
    // Arrange
    var testCasesCsv = await new HttpClient().GetStringAsync(
        "https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/export?format=csv&id=1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE&gid=0");
    var baseUriMappingXml = File.ReadAllText(@"Wikipedia\BaseUriMapping.xml");
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