# Tatami CSV settings

* Sample CSVs 
* [Arrange section](#Arrange_section)
	* [CSV structure](#Arrage_CSV_structure)
	* [Column details](#Arrage_Column_details) 
* [Assert section](#Assert_section)
	* [CSV structure](#Assert_CSV_structure)
	* [Column details](#Assert_Column_details) 
* [Contents section in Assert section](#Contents_section)
	* [CSV structure](#Contents_CSV_structure)
	* [Column details](#Contents_Column_details) 
	* [Column details for Expected/Actual section](#Contents_Column_details2)

## <a name="Sample_CSVs">Sample CSVs</a>
You can check sample csvs from the following links. These files are excel format, you need to convert from excel to csv before test. 
* [Wikipedia Test](../samples/SampleTest/Wikipedia/Resources/Test_United_States.xlsx)
* [Yahoo Weather Test](../samples/YahooWeather/Resources/Test_United_States.xlsx)

## <a name="Arrange_section">Arrange section</a>
Arrange section describes how to get actual document and expected document.
This section specifies arrange settings, such as the data sources for expected and actual contents

### <a name="Arrage_CSV_structure">CSV structure</a>
|Local Page|Arrange|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|:---|---|---|---|---|---|---|---|---|---|---|---|---|
|&nbsp;|HttpRequest Expected|&nbsp;|&nbsp;|&nbsp;|HttpRequest Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|BaseUri|Method|UserAgent|PathInfos|BaseUri|Headers|Cookies|&nbsp;|PathInfo|QueryStrings|&nbsp;|Fragment|Content|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;&nbsp;|X-My-Header|myCookie1|myCookie2|&nbsp;|culture|query|&nbsp;|&nbsp;|

* row[0] : Arrange
* row[1] : HttpRequest Expected, HttpRequest Actual
* row[2] : BaseUri, Method, UserAgent, PathInfos, Headers, Cookies, QueryStrings, Fragment, Content
* row[3] : Headers' Key Names, Cookies' Key Names, QueryStrings' Key Names

### <a name="Arrage_Column_details">Column details</a>
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

## <a name="Assert_section">Assert section</a>
Assertion section describes expected value and where is expected value in XML/HTML.
This section specifies assertion settings how to assert HTTP Response and/or Actual document.

### <a name="Assert_CSV_structure">CSV structure</a>
|Assertion|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|---|---|---|---|---|---|---|
|Uri|StatusCode|Headers|&nbsp;|Cookies|&nbsp;|Xsd|Contents|
|&nbsp;|&nbsp;|X-B3-TraceId|X-B3-SpanId|myCookie1|myCookie2|&nbsp;|__See Below__|

### <a name="Assert_Column_details">Column details</a>
Section name|Detailed|Required
:-----|:-----|:-----
Uri|The section specifies response URI for assertion. __/local/data.aspx__ |optional
StatusCode|The section specifies response status code for assertion. It accepts only text node. __200__,__404__|optional
Headers|The section specifies HTTP response header for assertion.|optional|
Cookies|The section specifies HTTP response cookies for assertion.|optional
Xsd|The section specifies XSD assertion.|optional 
Contents|The section specifies document assertion.|optional

## <a name="Contents_section">Contents section in Assert section</a>
### <a name="Contents_CSV_structure">CSV structure</a>
|Contents|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|:----------------|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
|Name|IsList|IsDateTime|IsTime|Expected|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Actual|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|Value|Query|Exists|Attribute|Pattern|Format|FormatCulture|Query|Attrribute|Pattern|Format|FormatCulture|

### <a name="Contents_Column_details">Column details</a>
Section name|Detailed|Required
:-------|:-----|:-----
Name|The section specifies assertion name. |required
IsList|The section specifies whether node getting XPath is list or not.It accepts "true" or "false". Default value is "false".|optional
IsDateTime|The section specifies whether the value is date time. It accepts "true" or "false". Default value is "false".|optional
IsTime|The section specifies whether the value is time. It accepts "true" or "false". Default value is "false".|optional
Expected|The section specifies what is the expected value or how to get the expected value from expected document. see next slide|required
Actual|The section specifies how to get the test target value from actual document.|required

### <a name="Contents_Column_details2">Column details for Expected/Actual section</a>
Section name|Detailed|Required
:-------|:-----|:-----
Value|The section specifies expected value.|optional
Query|The section specifies XPath getting target element. ex. "//section[@id='hf']/h2"|optional
Attribute|The section specifies attribute of target element getting by XPath. ex. "href"|optional
Exists|The section specifies whether node exists. It accepts "true", "false" or "". Default value is "".|optional
Pattern|The section specifies regular expression pattern to capture target value. ex. [^:]+: (.*)|optional
Format|The section specifies DateTime format. ex. "yyyy/MM/dd", "t"|optional
FormatCulture|The section specifies formatting culture to convert DateTime to string. ex. "en-US"|optional