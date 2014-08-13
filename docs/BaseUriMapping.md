# BaseUriMapping.xml settings

When you execute smoke test, you need to set Base URIs of test target each environment.
Tatami separates Base URIs settings from test case CSV. You need to create BaseUriMapping.xml and assign key name into test case CSV.    

Sample file : [samples/SampleTest/Wikipedia/Resources/BaseUriMapping.xml](../samples/SampleTest/Wikipedia/Resources/BaseUriMapping.xml)
```
<BaseUriMapping>
  <Item Key="en-US">http://en.wikipedia.org</Item>
  <Item Key="ja-JP">http://ja.wikipedia.org</Item>
</BaseUriMapping>
```

Sample test case CSV : [samples/SampleTest/Wikipedia/Resources/Test_United_States.xlsx](../samples/SampleTest/Wikipedia/Resources/Test_United_States.xlsx) 

&nbsp;|Arrange|&nbsp;|&nbsp;|
:-----|:-----|:-----|:-----|
&nbsp;|HttpRequest Actual|&nbsp;|&nbsp;|
&nbsp;|BaseUri|UserAgent|PathInfos|
Test United States|__en-US__|IE11|wiki|
