namespace Tatami.Tests.Parsers.Csv
{
    using System.Linq;
    using CsvParser;
    using Tatami.Parsers.Csv;
    using Tatami.Validators.Csv;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HeaderParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            const string Csv =
                ",Arrange,,,,,,,,,,,Assertion,,,,,,,,,,,,,,,,,,,,\r\n" +
                ",HttpRequest Expected,,HttpRequest Actual,,,,,,,,,Uri,StatusCode,Headers,,Cookies,,Contents,,,,,,,,,,,,,,\r\n" +
                ",BaseUri,PathInfos,BaseUri,Headers,Cookies,,PathInfos,,QueryStrings,,Fragment,,,Content-Type,Last-Modified,Location,Degree,Name,IsList,IsDateTime,IsTime,Expected,,,,,,Actual,,,,\r\n" +
                ",,,,User-Agent,Location,Degree,,,locations,type,,,,,,,,,,,,Value,Query,Attribute,Pattern,Format,FormatCulture,Query,Attribute,Pattern,Format,FormatCulture";

            // Act
            var root = HeaderParser.Parse(new Parser().Parse(Csv));
            HeaderValidator.Validate(root);

            // Assert

            // depth=-1 (root)
            Assert.AreEqual("Root", root.Name);
            Assert.AreEqual(2, root.Children.Count);
            Assert.AreEqual(-1, root.Depth);
            Assert.AreEqual(0, root.From);
            Assert.AreEqual(32, root.To);

            // depth=0 (Arrange)
            var arrange = root.Children.ElementAt(0);
            Assert.AreEqual("Arrange", arrange.Name);
            Assert.AreEqual(0, arrange.Depth);
            Assert.AreEqual(1, arrange.From);
            Assert.AreEqual(11, arrange.To);
            Assert.AreEqual(2, arrange.Children.Count);

            // depth=0 (Assertion)
            var assertion = root.Children.ElementAt(1);
            Assert.AreEqual("Assertion", assertion.Name);
            Assert.AreEqual(0, assertion.Depth);
            Assert.AreEqual(12, assertion.From);
            Assert.AreEqual(32, assertion.To);
            Assert.AreEqual(5, assertion.Children.Count);

            // depth=1 (HttpRequest expected)
            var expected = arrange.Children.ElementAt(0);
            Assert.AreEqual("HttpRequest Expected", expected.Name);
            Assert.AreEqual(1, expected.Depth);
            Assert.AreEqual(1, expected.From);
            Assert.AreEqual(2, expected.To);
            Assert.AreEqual(2, expected.Children.Count);

            // depth=1 (HttpRequest actual)
            var actual = arrange.Children.ElementAt(1);
            Assert.AreEqual("HttpRequest Actual", actual.Name);
            Assert.AreEqual(1, actual.Depth);
            Assert.AreEqual(3, actual.From);
            Assert.AreEqual(11, actual.To);
            Assert.AreEqual(6, actual.Children.Count);

            // depth=1 (Uri)
            var uri = assertion.Children.ElementAt(0);
            Assert.AreEqual("Uri", uri.Name);
            Assert.AreEqual(1, uri.Depth);
            Assert.AreEqual(12, uri.From);
            Assert.AreEqual(12, uri.To);
            Assert.AreEqual(0, uri.Children.Count);

            // depth=1 (StatusCode)
            var statusCode = assertion.Children.ElementAt(1);
            Assert.AreEqual("StatusCode", statusCode.Name);
            Assert.AreEqual(1, statusCode.Depth);
            Assert.AreEqual(13, statusCode.From);
            Assert.AreEqual(13, statusCode.To);
            Assert.AreEqual(0, uri.Children.Count);

            // depth=1 (Headers)
            var headersA = assertion.Children.ElementAt(2);
            Assert.AreEqual("Headers", headersA.Name);
            Assert.AreEqual(1, headersA.Depth);
            Assert.AreEqual(14, headersA.From);
            Assert.AreEqual(15, headersA.To);
            Assert.AreEqual(2, headersA.Children.Count);

            // depth=1 (Cookies)
            var cookiesA = assertion.Children.ElementAt(3);
            Assert.AreEqual("Cookies", cookiesA.Name);
            Assert.AreEqual(1, cookiesA.Depth);
            Assert.AreEqual(16, cookiesA.From);
            Assert.AreEqual(17, cookiesA.To);
            Assert.AreEqual(2, cookiesA.Children.Count);

            // depth=1 (Contents)
            var contents = assertion.Children.ElementAt(4);
            Assert.AreEqual("Contents", contents.Name);
            Assert.AreEqual(1, contents.Depth);
            Assert.AreEqual(18, contents.From);
            Assert.AreEqual(32, contents.To);
            Assert.AreEqual(6, contents.Children.Count);

            // depth=2 (expected BaseUri)
            var baseUriArrange = expected.Children.ElementAt(0);
            Assert.AreEqual("BaseUri", baseUriArrange.Name);
            Assert.AreEqual(2, baseUriArrange.Depth);
            Assert.AreEqual(1, baseUriArrange.From);
            Assert.AreEqual(1, baseUriArrange.To);
            Assert.AreEqual(0, baseUriArrange.Children.Count);
        }
    }
}
