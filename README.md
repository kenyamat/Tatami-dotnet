#![tatami](docs/imgs/tatami_s.png) Tatami.NET
* Tatami is a C# test library for web apps. 
* Tatami verifies document structure. (HTML, XML, JSON and Other formats)
* Tatami manages request information (Uri, Query string, Header, Cookie), Test cases and Assert conditions with CSV file.
* Tatami can be integrated in major C# test frameworks and CI. (NUnit, MS Test, Jenkins, TeamCity, etc.)
* Tatami enables you to reduce test codes and improve maintainability of test cases and help review for test cases and assert. conditions.

## Contents
* [Scope](#Scope)
* [Functions](#Functions)
* [Nuget package](#Nuget_package)
* [Showcase](#Showcase)
* [Setup steps](#Setup_steps)
* [CSV implementation](#CSV_implementation)
* [BaseUriMapping.xml settings](#BaseUriMapping.xml_settings)
* [Dependencies](#Dependencies)
* [Copyright](#Copyright)

## <a name="Scope">Scope</a> 
This library covers the following test types.
* Integration Test
* Acceptance Test
* Smoke Test
* (not for Unit Test).

## <a name="Functions">Functions</a>
Tatami provides the following test functions.
* Manage HTTP request information, test cases and assert conditions in CSV.
* Get expected and actual documents (such as HTML, XML, JSON and Other formats).
* Test HTTP response information (Uri, Status Code, Header, Cookie, Format using XSD).
* Test response documents using expected values, XPath and regular expression.
* Provide failed information.  

## <a name="Nuget_package">Nuget_package</a>
[https://www.nuget.org/packages/Tatami.NET/](https://www.nuget.org/packages/Tatami.NET/)

## <a name="Showcase">Showcase</a>
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

## <a name="Setup_steps">Setup steps</a>
1. Create a test project. 
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

## <a name="CSV_implementation">CSV implementation</a>
See [CSV implementation][] for details.

## <a name="BaseUriMapping.xml_settings">BaseUriMapping.xml settings</a>
See [BaseUriMapping.xml settings][] for details.

## <a name="UserAgentMapping.xml_settings">UserAgentMapping.xml settings</a>
See [UserAgentMapping.xml settings][] for details.

## <a name="Dependencies">Dependencies</a>
* [HtmlAgilityPack](http://htmlagilitypack.codeplex.com/)
* [Newtonsoft.Json](http://james.newtonking.com/json)

## <a name="Copyright">Copyright</a>
Copyright (c) 2014 kenyamat. Licensed under MIT.

[CSV implementation]: docs/csv_implementation.md
[BaseUriMapping.xml settings]: docs/BaseUriMapping.md
[UserAgentMapping.xml settings]: docs/UserAgentMapping.md