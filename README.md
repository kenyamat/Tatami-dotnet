# Tatami.NET
* Tatami is a C# test library for web application (HTML, XML, JSON and Other formats).
* All request information, test cases and assert conditions are in CSV file.
* Tatami enables to reduce test code and review test cases easily, and also improve maintainability of test project.

## Overview
Tatami provides the following functions.
* Gets expected and actual documents(HTML, XML, JSON and Other formats)
* Tests HTTP Response (Headers, Cookies Status Code, Uri)
* Tests Response Documents using static value, XPath and Regular expression.

### 1. Tests a web page using static expected values
1. Gets a actual document from web application (Wikipedia). [http://en.wikipedia.org/wiki/United_States](http://en.wikipedia.org/wiki/United_States)
1. Asserts HTTP response header and/or document values using expected values in CSV file.

![sample1](docs/imgs/sample1.png)

### 2. Test a web page using expected values from web services
1. Gets a expected document from web service (Yahoo RSS). [http://weather.yahooapis.com/forecastrss?w=2459115](http://weather.yahooapis.com/forecastrss?w=2459115)
1. Gets a actual document from web application (Yahoo Weather). [http://weather.yahoo.com/united-states/new-york/new-york-2459115/](http://weather.yahoo.com/united-states/new-york/new-york-2459115/)
1. Asserts HTTP response header and/or document values using expected values in web service or CSV file.

![sample1](docs/imgs/sample2.png)

## Set up
1. Creates a unit test project.
1. Gets Tatami library from nuget.org then sets this into the project. [https://www.nuget.org/packages/Tatami.NET/](https://www.nuget.org/packages/Tatami.NET/)
1. Creates a [BaseUriMapping.xml](samples/SampleTest/Wikipedia/Resources/BaseUriMapping.xml)
1. Creates a [UserAgentMapping.xml](samples/SampleTest/Wikipedia/Resources/BaseUriMapping.xml) if you need.
1. Creates a CSV file for test. See [CSV implementation][] for details.
1. Creates a test method like the following.
```c#:WikipediaTests.cs
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
