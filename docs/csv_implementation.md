# How to create test case CSV

* [Sample test case CSVs](#Sample_CSVs) 
* [How to create a test case CSV](#How_to_create_a_test_case_CSV)
	[1. Define arrange header](#How1)
	[2. Write request information to get test target document](#How2)
	[3. Write Assertion header](#How3)
	[4. Write assert conditions](#How4)
* [CSV header details](#CSV_header_details)
	* [Arrange section](#Arrange_section)
		* [CSV structure](#Arrage_CSV_structure)
		* [Column details](#Arrage_Column_details) 
	* [Assertion section](#Assertion_section)
		* [CSV structure](#Assertion_CSV_structure)
		* [Column details](#Assertion_Column_details) 
	* [Contents section in Assert section](#Contents_section)
		* [CSV structure](#Contents_CSV_structure)
		* [Column details](#Contents_Column_details) 
		* [Column details for Expected/Actual section](#Contents_Column_details2)

## <a name="Sample_CSVs">Sample test case CSVs</a>
You can check sample Csvs from the following links. As these files are Google Docs Spreadsheet, you need to create a link to download CSV. Please see the [page](https://help.hootsuite.com/entries/21723778-Scheduling-in-Bulk-and-csv-Files#gdoc).
* [Wikipedia Test](https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/edit?usp=sharing) - [[CSV format](https://docs.google.com/spreadsheets/d/1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE/export?format=csv&id=1Gvnq2NlBXyrnsjBH0Xr-R8U0f9RLeCR9RH5eAdTL_XE&gid=0)]
* [Yahoo Weather Test](https://docs.google.com/spreadsheets/d/15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE/edit?usp=sharing) - [[CSV format](https://docs.google.com/spreadsheets/d/15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE/export?format=csv&id=15WbI7RpQZC-j--xsoYj7mfcapq96FsBi4ZVAEb_lroE&gid=0)] 
* [Yahoo API Test](https://docs.google.com/spreadsheets/d/1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg/edit?usp=sharing) - [[CSV format](https://docs.google.com/spreadsheets/d/1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg/export?format=csv&id=1h-8vkF-5jEHXDIBwUpA3_otRVa30Um6qm05ZYoSgbQg&gid=0)]

## <a name="How_to_create_a_test_case_CSV">How to create a test case CSV</a>
### <a name="How1">1.Define arrange header</a>
* You need to keep the following header structure. If CSV has invalid hierarchy, you will get a `WrongFileFormatException` before testing.
* You can set BaseUri, UserAgent, PathInfo, QueryString(uselang) if you create the following header.
* See [Arrange section](#Arrange_section) for details.

|Wikipedia US Test|Arrange|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|:---|---|---|---|---|---|
|&nbsp;|HttpRequest Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|BaseUri|UserAgent|PathInfo|&nbsp;|QueryStrings|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|uselang|

### <a name="How2">2.Write request information to get test target document</a>
* In this case, you can get test documents using the following requests.
	* http://en.wikipedia.org/wiki/United_States (UserAgent=IE11)
	* http://en.wikipedia.org/wiki/United_States?uselang=es (UserAgent=IE11)  
* Please see the following page about BaseUri and UserAgent.
	* [How to create BaseUriMapping.xml](#BaseUriMapping.xml_settings)
	* [How to create UserAgentMapping.xml](#UserAgentMapping.xml_settings)

|Wikipedia US Test|Arrange|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|:---|---|---|---|---|---|
|&nbsp;|HttpRequest Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|BaseUri|UserAgent|PathInfo|&nbsp;|QueryStrings|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|uselang|
|Test US|en-US|IE11|wiki|United_States|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|Test with uselang=es|en-US|IE11|wiki|United_States|es|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|

### <a name="How3">3.Write Assertion header</a>
* In the following case, you can assert Uri and values in HTML.
* See [Assertion section](#Assertion_section) for details.

|Wikipedia US Test|Arrange|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Assertion|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|
|&nbsp;|HttpRequest Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Uri|Contents|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|BaseUri|UserAgent|PathInfo|&nbsp;|QueryStrings|&nbsp;|Name|Expected|Actual|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|uselang|&nbsp;|&nbsp;|Value|Query|Pattern|
|Test US|en-US|IE11|wiki|United_States|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|Test with uselang=es|en-US|IE11|wiki|United_States|es|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|

### <a name="How4">4.Write assert conditions</a>
* __One assertion should put in one line__.
* In the following case, you can assert
	* Response URI should be '/wiki/United_States'.
	* Text node value of '//title' should be 'United States'.
	* Text node value of '//div[@id='p-views']//li[1]' should be 'Read'.

|Wikipedia US Test|Arrange|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Assertion|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|:---|
|&nbsp;|HttpRequest Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Uri|Contents|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|BaseUri|UserAgent|PathInfo|&nbsp;|QueryStrings|&nbsp;|Name|Expected|Actual|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|uselang|&nbsp;|&nbsp;|Value|Query|Pattern|
|Test US|en-US|IE11|wiki|United_States|&nbsp;|/wiki/United_States|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Title|United States|//title|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Read Tab|Read|//div[@id='p-views']//li[1]|(.*) -|
|Test with uselang=es|en-US|IE11|wiki|United_States|es|/wiki/United_States?uselang=es|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Title|United States|//title|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Read Tab|Leer|//div[@id='p-views']//li[1]|(.*) -|

## <a name="CSV_header_details">CSV header details</a>

### <a name="Arrange_section">Arrange section</a>
Arrange section describes how to get actual and expected document.
This section specifies arrange settings, such as the data sources for expected and actual contents.

#### <a name="Arrage_CSV_structure">CSV structure</a>
You need to keep the following hierarchy. If CSV has invalid hierarchy, you will get a `WrongFileFormatException` before testing. 

|Local Page|Arrange|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|:---|---|---|---|---|---|---|---|---|---|---|---|---|
|&nbsp;|HttpRequest Expected|&nbsp;|&nbsp;|&nbsp;|HttpRequest Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|BaseUri|Method|UserAgent|PathInfos|BaseUri|Headers|Cookies|&nbsp;|PathInfo|QueryStrings|&nbsp;|Fragment|Content|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;&nbsp;|X-My-Header|myCookie1|myCookie2|&nbsp;|culture|query|&nbsp;|&nbsp;|

* row[0] : Arrange
* row[1] : HttpRequest Expected, HttpRequest Actual
* row[2] : BaseUri, Method, UserAgent, PathInfos, Headers, Cookies, QueryStrings, Fragment, Content
* row[3] : Headers' Key Names, Cookies' Key Names, QueryStrings' Key Names

#### <a name="Arrage_Column_details">Column details</a>

Section name|Detailed|Required
:-----|:-----|:-----
HttpRequest Expected|The section specifies HTTP request settings for expected doc.|optional
HttpRequest Actual|The section specifies HTTP request settings for actual doc.|__required__
BaseUri|The section specifies BaseUriMapping key. __http://yahoo.com/__ local/london/?wc=112 |__required__
Method|The section specifies HTTP Method name. (GET, POST, PUT, DELETE)|optional
UserAgent|The section specifies UserAgentMapping key. |optional
Headers|The section specifies HTTP header settings. |optional
Cookies|The section specifies HTTP cookie settings. |optional
PathInfos|The section specifies path info settings. http://yahoo.com/ __local/london__ ?wc=112 |optional 
QueryStrings|The section specifies query string settings. http://yahoo.com/local/london? __wc=112__|optional
Fragment|The section specifies fragment settings. http://yahoo.com/local/london?wc=112 __#Temp__|optional
Content|The section specifies content string for POST/PUT. |optional

### <a name="Assertion_section">Assertion section</a>
Assertion section describes expected value and where is expected value in XML/HTML.
This section specifies assertion settings how to assert HTTP Response and/or Actual document.

#### <a name="Assertion_CSV_structure">CSV structure</a>
|Assertion|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|---|---|---|---|---|---|---|
|Uri|StatusCode|Headers|&nbsp;|Cookies|&nbsp;|Xsd|Contents|
|&nbsp;|&nbsp;|X-B3-TraceId|X-B3-SpanId|myCookie1|myCookie2|&nbsp;|__See Below__|

#### <a name="Assertion_Column_details">Column details</a>
Section name|Detailed|Required
:-----|:-----|:-----
Uri|The section specifies response URI for assertion. __/local/data.aspx__ |optional
StatusCode|The section specifies response status code for assertion. It accepts only text node. __200__,__404__|optional
Headers|The section specifies HTTP response header for assertion.|optional|
Cookies|The section specifies HTTP response cookies for assertion.|optional
Xsd|The section specifies XSD assertion.|optional 
Contents|The section specifies document assertion.|optional

### <a name="Contents_section">Contents section in Assertion section</a>
#### <a name="Contents_CSV_structure">CSV structure</a>
|Contents|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
|Name|IsList|IsDateTime|IsTime|Expected|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Value|Query|Exists|Attribute|Pattern|Format|FormatCulture|Query|Attrribute|Pattern|Format|FormatCulture|

#### <a name="Contents_Column_details">Column details</a>
Section name|Detailed|Required
:-------|:-----|:-----
Name|The section specifies assertion name. |__required__
IsList|The section specifies whether node getting XPath is list or not.It accepts "true" or "false". Default value is "false".|optional
IsDateTime|The section specifies whether the value is date time. It accepts "true" or "false". Default value is "false".|optional
IsTime|The section specifies whether the value is time. It accepts "true" or "false". Default value is "false".|optional
Expected|The section specifies what is the expected value or how to get the expected value from expected document. see next slide|__required__
Actual|The section specifies how to get the test target value from actual document.|__required__

#### <a name="Contents_Column_details2">Column details for Expected/Actual section</a>
Section name|Detailed|Required
:-------|:-----|:-----
Value|The section specifies expected value.|optional
Query|The section specifies XPath getting target element. ex. "//section[@id='hf']/h2"|optional
Attribute|The section specifies attribute of target element getting by XPath. ex. "href"|optional
Exists|The section specifies whether node exists. It accepts "true", "false" or "". Default value is "".|optional
Pattern|The section specifies regular expression pattern to capture target value. ex. [^:]+: (.*)|optional
Format|The section specifies DateTime format. ex. "yyyy/MM/dd", "t"|optional
FormatCulture|The section specifies formatting culture to convert DateTime to string. ex. "en-US"|optional