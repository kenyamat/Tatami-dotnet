#![tatami](docs/imgs/tatami_s.png) Tatami.NET
Tatami is a C# library for web application testing.

* Manages test cases in CSV file like [this](https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing).
	* HTTP request conditions to get test documents (Uri, Method, UseAgent, QueryString, PathInfos, Header, Cookie)
	* Test cases and Assert conditions
* Verifies HTTP response information.
	* HTTP response header, cookies, status code, URL
	* Document structure (HTML, XML, JSON and Other text format)
* Can be integrated in major C# test frameworks and CI.
	* NUnit, MS Test, Jenkins, TeamCity, etc.

Tatami enables you to reduce test codes and help review for test cases.

## Contents
* [Scope](#Scope)
* [Functions](#Functions)
* [Showcase](#Showcase)
* [Implementation](#Implementation)
	* [How to create a test case CSV](#CSV_implementation)
	* [How to create BaseUriMapping.xml](#BaseUriMapping.xml_settings)
	* [How to create UserAgentMapping.xml](#UserAgentMapping.xml_settings)
* [nuget package](#nuget_package)
* [Setup steps](#Setup_steps)
* [Dependencies](#Dependencies)
* [Copyright](#Copyright)

## <a name="Scope">Scope</a>
This library covers the following test types.
* Integration Test
* Acceptance Test
* Smoke Test
* (not for Unit Test)

## <a name="Functions">Functions</a>
Tatami provides the following test functions.
* Manage HTTP request information, test cases and assert conditions in CSV.
* Get expected and actual documents (such as HTML, XML, JSON and Other formats).
* Test HTTP response information (Uri, Status Code, Header, Cookie, Format using XSD).
* Test response documents using expected values, XPath and regular expression.
* Provide failed information.

## <a name="Showcase">Showcase</a>
### 1. HTML page test (Wikipedia)
Test the HTML structure of the "United States" Wikipedia article by expecting static values.
* This scenario covers to test between html values and static values in CSV.
* See [Wikipedia page test](docs/sc_wikipedia.md) for details.
![sample1](docs/imgs/sample1.png)


### 2. HTML page test using Web API (Yahoo Weather)
Test the HTML structure of the "New York" Wikipedia article by expecting values from Web APIs (Yahoo Weather RSS).
* Recent Web applications consume data created by Web APIs instead of databases. This scenario will test that particular values that are present in an XML Web API exist in a separate HTML interface.
* See [Yahoo Weather page test](docs/sc_yahoo_weather.md) for details.
![sample2](docs/imgs/sample2.png)


### 3. Web API test (XML/JSON)
* Test Web API which returns XML/JSON using static values.
* See [Yahoo API test](docs/sc_yahoo_api.md) for details.
![sample3](docs/imgs/sample3.png)

## <a name="Implementation">Implementation</a>
### <a name="CSV_implementation">How to create a test case CSV</a>
See [CSV implementation][] for details.

### <a name="BaseUriMapping.xml_settings">How to create BaseUriMapping.xml</a>
See [BaseUriMapping.xml settings][] for details.

### <a name="UserAgentMapping.xml_settings">How to create UserAgentMapping.xml</a>
See [UserAgentMapping.xml settings][] for details.

## <a name="nuget_package">nuget package</a>
[https://www.nuget.org/packages/Tatami.NET/](https://www.nuget.org/packages/Tatami.NET/)

## <a name="Setup_steps">Setup steps</a>
1. Create a C# test project.
1. Install Tatami library from nuget.org then sets this into the project.
	* [https://www.nuget.org/packages/Tatami.NET/](https://www.nuget.org/packages/Tatami.NET/)
1. Create a BaseUriMapping.xml
	* See more details : [BaseUriMapping.xml settings][]
1. Create a UserAgentMapping.xml
	* See more details : [UserAgentMapping.xml settings][]
1. Create a CSV file including test cases.
	* Csv file : [https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing](https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing)
1. Create a test method.
	* [samples/SampleTest/Wikipedia/WikipediaTests.cs](samples/SampleTest/Wikipedia/WikipediaTests.cs)
1. Run test.

## <a name="Dependencies">Dependencies</a>
* [CsvParser.NET](https://github.com/kenyamat/CsvParser)
* [HtmlAgilityPack](http://htmlagilitypack.codeplex.com/)
* [Newtonsoft.Json](http://james.newtonking.com/json)

## <a name="Copyright">Copyright</a>
Copyright (c) 2014 kenyamat. Licensed under MIT.

[CSV implementation]: docs/csv_implementation.md
[BaseUriMapping.xml settings]: docs/BaseUriMapping.md
[UserAgentMapping.xml settings]: docs/UserAgentMapping.md
