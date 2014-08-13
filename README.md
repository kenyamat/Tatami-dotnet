# Tatami.NET
* Tatami is a C# test library for web apps (HTML, XML, JSON and Other formats). 
* Tatami manages request information (Uri, Query string, Header, Cookie), Test cases and Assert conditions with CSV file.
* Tatami enables you to reduce test codes and improve maintainability of test cases and help review for test cases and assert. conditions.
* Tatami can be integrated in the major C# test frameworks (NUnit, MS Test, etc.)

## Scope 
Tatami library covers
* Integration Test
* Acceptance Test
* Smoke Test
* (not for Unit Test).

## Functions
Tatami provides the following test functions.
* Manage HTTP request information, test cases and assert conditions in CSV.
* Get expected and actual documents (such as HTML, XML, JSON and Other formats).
* Test HTTP response information (Uri, Status Code, Header, Cookie, Format using XSD).
* Test response documents using expected values, XPath and regular expression.
* Get failed cases information.  

## Sample cases
### 1. Tests web pages using static expected values
1. Gets a actual html document from Web App (Wikipedia: [http://en.wikipedia.org/wiki/United_States](http://en.wikipedia.org/wiki/United_States)).      
1. Asserts HTTP response header and/or document values using expected values in CSV file.

![sample1](docs/imgs/sample1.png)

### 2. Test web pages using expected values from Web API
1. Gets a expected document from Web API (Yahoo RSS: [http://weather.yahooapis.com/forecastrss?w=2459115](http://weather.yahooapis.com/forecastrss?w=2459115)). 
1. Gets a actual document from Web App (Yahoo Weather: [http://weather.yahoo.com/united-states/new-york/new-york-2459115/](http://weather.yahoo.com/united-states/new-york/new-york-2459115/)). 
1. Asserts HTTP response header and/or document values using expected values in web service or CSV file.

![sample1](docs/imgs/sample2.png)

## Set up steps
1. Creates a unit test project.
1. Gets Tatami library from nuget.org then sets this into the project.
	* [https://www.nuget.org/packages/Tatami.NET/](https://www.nuget.org/packages/Tatami.NET/)
1. Creates a [BaseUriMapping.xml](samples/SampleTest/Wikipedia/Resources/BaseUriMapping.xml)
1. Creates a [UserAgentMapping.xml](samples/SampleTest/Wikipedia/Resources/BaseUriMapping.xml) if you need.
1. Creates a CSV file for test.
	* See [CSV implementation][] for details.
1. Creates a test method like the following.

```Test.cs
[TestMethod]
public async Task TestWikipediaWithUnitedStatesPage()
{
    // Arrange
    var testCasesCsv = File.ReadAllText(@"Wikipedia\Resources\Test_United_States.csv");
    var baseUriMappingXml = File.ReadAllText(@"Wikipedia\Resources\BaseUriMapping.xml");
    var userAgentMappingXml = File.ReadAllText(@"Wikipedia\Resources\UserAgentMapping.xml");

    // Act
    var result = await TestExecutor.Test(testCasesCsv, baseUriMappingXml, userAgentMappingXml);

    // Assert
    if (!string.IsNullOrWhiteSpace(result.FailedMessage))
    {
        Assert.Fail(result.FailedMessage);
    }
}
```

## CSV implementation
See [CSV implementation][] for details.

## Dependencies
* [HtmlAgilityPack](http://htmlagilitypack.codeplex.com/)
* [Newtonsoft.Json](http://james.newtonking.com/json)

## Copyright
Copyright (c) 2014 kenyamat. Licensed under MIT.

[CSV implementation]: docs/csv_implementation.md
