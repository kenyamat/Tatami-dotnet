#![tatami](docs/imgs/tatami_s.png) Tatami.NET
* Tatami is a C# test library for web apps. 
* Tatami verify document structure. (HTML, XML, JSON and Other formats)
* Tatami manages request information (Uri, Query string, Header, Cookie), Test cases and Assert conditions with CSV file.
* Tatami can be integrated in the major C# test frameworks (NUnit, MS Test, etc.)
* Tatami enables you to reduce test codes and improve maintainability of test cases and help review for test cases and assert. conditions.

## Scope 
This library covers the following test types.
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
* Provide failed information.  

## Showcase
### 1. HTML page test (Wikipedia)
Test a HTML structure of "United States" page using static expected values.
* This scenario covers to test between html values and static values in CSV. 
![sample1](docs/imgs/sample1.png)

See [Wikipedia page test](docs/sc_wikipedia.md) for details.

### 2. HTML page test using Web API (Yahoo Weather)
Test a HTML structure of "New York" page using values from Web API (Yahoo Weather RSS).
* Recent Web applications consume data created by Web API instead of database. This scenario will cover to test values between HTML and XML from Web API.
![sample2](docs/imgs/sample2.png)

See [Yahoo Weather page test](docs/sc_yahoo_weather.md) for details.

### 3. Web API test (XML/JSON)
Test Web API which returns XML/JSON using static values.
![sample3](docs/imgs/sample3.png)

See [Yahoo API test](docs/sc_yahoo_api.md) for details.

## Setup steps
1. Create a test project. 
1. Install Tatami library from nuget.org then sets this into the project.
	* [https://www.nuget.org/packages/Tatami.NET/](https://www.nuget.org/packages/Tatami.NET/)
1. Create a BaseUriMapping.xml
	* See more details : [BaseUriMapping.xml settings][]
1. Create a UserAgentMapping.xml 
	* See more details : [UserAgentMapping.xml settings][]
1. Create a CSV file including test cases.
	* csv file : [samples/SampleTest/Wikipedia/Resources/Test_United_States.csv](samples/SampleTest/Wikipedia/Resources/Test_United_States.csv)
	* excel file : [samples/SampleTest/Wikipedia/Resources/Test_United_States.xls](samples/SampleTest/Wikipedia/Resources/Test_United_States.xlsx)
1. Create a test method.
	* [samples/SampleTest/Wikipedia/WikipediaTests.cs](samples/SampleTest/Wikipedia/WikipediaTests.cs)
1. Run test.

## CSV implementation
See [CSV implementation][] for details.

## BaseUriMapping.xml settings
See [BaseUriMapping.xml settings][] for details.

## UserAgentMapping.xml settings
See [UserAgentMapping.xml settings][] for details.

## Dependencies
* [HtmlAgilityPack](http://htmlagilitypack.codeplex.com/)
* [Newtonsoft.Json](http://james.newtonking.com/json)

## Copyright
Copyright (c) 2014 kenyamat. Licensed under MIT.

[CSV implementation]: docs/csv_implementation.md
[BaseUriMapping.xml settings]: docs/BaseUriMapping.md
[UserAgentMapping.xml settings]: docs/UserAgentMapping.md