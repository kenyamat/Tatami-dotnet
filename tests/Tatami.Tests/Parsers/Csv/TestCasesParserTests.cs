namespace Tatami.Tests.Parsers.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tatami.Models;
    using Tatami.Parsers.Csv;
    using Tatami.Services.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestCasesParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            // Arrange
            var data = new List<List<string>>
            {
                new List<string> { null, "Arrange", null, "Assertion" },
                new List<string> { null, "HttpRequest Actual", null, "Uri" },
                new List<string> { null, "BaseUri", null, null },
                new List<string> { null, null, null, null },
                new List<string> { "test case 1", "TargetSite", null, "/local" },
                new List<string> { "test case 2", "TargetSite2", null, null },
            };

            // Act
            var result = TestCasesParser.Parse(data);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNoItem()
        {
            // Arrange
            var data = new List<List<string>>();

            // Act
            TestCasesParser.Parse(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseWithNullItem()
        {
            TestCasesParser.Parse(null);
        }

        [TestMethod]
        public async Task TestTest()
        {
            // Arrange
            var data = new List<List<string>>
            {
                new List<string> {          null,              "Arrange",                 null,   "Assertion",                 null,        null,                   null,        null },
                new List<string> {          null, "HttpRequest Expected", "HttpRequest Actual",    "Contents",                 null,        null,                   null,        null },
                new List<string> {          null,              "BaseUri",            "BaseUri",        "Name",           "Expected",        null,               "Actual",        null },
                new List<string> {          null,                   null,                 null,          null,              "Query", "Attribute",                "Query", "Attribute" },
                new List<string> { "test case 1",             "ExpectedSite",            "TargetSite", "text test 1", "//Expected/Item[1]",      "Name", "//html/body/ul/li[1]",        null },
                new List<string> {          null,                   null,                 null, "text test 2", "//Expected/Item[2]",      "Name", "//html/body/ul/li[2]",        null },
            };

            const string ExpectedXml =
                @"<Expected>
                    <Item Name='a' />
                    <Item Name='b' />
                </Expected>";
            const string ActualHtml =
                @"<html>
                    <body>
                        <ul>
                            <li>a</li>
                            <li>b</li>
                        </ul>
                    </body>
                </html>";
            var httpRequestServiceShim = new StubIHttpRequestService
            {
                GetResponseHttpRequest = request =>
                    request.Name == "Expected"
                        ? Task.FromResult(new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" })
                        : Task.FromResult(new HttpResponse { Contents = ActualHtml, ContentType = "text/html" })
            };

            // Act
            var testCases = TestCasesParser.Parse(data);
            await testCases.Test(httpRequestServiceShim);

            // Assert
            Assert.IsTrue(testCases.Success);
        }

        [TestMethod]
        public async Task TestTestWithHavingFailedCases()
        {
            // Arrange
            var data = new List<List<string>>
            {
                new List<string> {          null,              "Arrange",                 null,   "Assertion",                 null,        null,                   null,        null },
                new List<string> {          null, "HttpRequest Expected", "HttpRequest Actual",    "Contents",                 null,        null,                   null,        null },
                new List<string> {          null,              "BaseUri",            "BaseUri",        "Name",           "Expected",        null,               "Actual",        null },
                new List<string> {          null,                   null,                 null,          null,              "Query", "Attribute",                "Query", "Attribute" },
                new List<string> { "test case 1",             "ExpectedSite",            "TargetSite", "text test 1", "//Expected/Item[1]",      "Name", "//html/body/ul/li[1]",        null },
                new List<string> {          null,                   null,                 null, "text test 2", "//Expected/Item[2]",      "Name", "//html/body/ul/li[2]",        null },
            };
            const string ExpectedXml =
                @"<Expected>
                    <Item Name='a' />
                    <Item Name='b' />
                </Expected>";
            const string ActualHtml =
                @"<html>
                    <body>
                        <ul>
                            <li>AAAA</li>
                            <li>BBBB</li>
                        </ul>
                    </body>
                </html>";
            var httpRequestServiceShim = new StubIHttpRequestService
            {
                GetResponseHttpRequest = request =>
                    request.Name == "Expected"
                        ? Task.FromResult(new HttpResponse { Contents = ExpectedXml, ContentType = "text/xml" })
                        : Task.FromResult(new HttpResponse { Contents = ActualHtml, ContentType = "text/html" })
            };

            // Act
            var testCases = TestCasesParser.Parse(data);
            await testCases.Test(httpRequestServiceShim);

            // Assert
            Assert.IsTrue(!testCases.Success);
            Assert.AreEqual(1, testCases.FailedCases.Count);
            Assert.AreEqual(2, testCases.FailedCases[0].FailedAssertions.Count);
        }

        [TestMethod]
        public async Task TestTestWithOnlyStaticCases()
        {
            // Arrange
            var data = new List<List<string>>
            {
                new List<string> {          null,              "Arrange",                 null,   "Assertion",                 null,        null,                   null,        null },
                new List<string> {          null, "HttpRequest Expected", "HttpRequest Actual",    "Contents",                 null,        null,                   null,        null },
                new List<string> {          null,              "BaseUri",            "BaseUri",        "Name",           "Expected",        null,               "Actual",        null },
                new List<string> {          null,                   null,                 null,          null,              "Value", "Attribute",                "Query", "Attribute" },
                new List<string> { "test case 1",             "ExpectedSte",            "TargetSite", "text test 1",                  "a",        null, "//html/body/ul/li[1]",        null },
                new List<string> {          null,                   null,                 null, "text test 2",                  "b",        null, "//html/body/ul/li[2]",        null },
            };

            const string ActualHtml =
                @"<html>
                    <body>
                        <ul>
                            <li>a</li>
                            <li>b</li>
                        </ul>
                    </body>
                </html>";
            var httpRequestServiceShim = new StubIHttpRequestService
            {
                GetResponseHttpRequest = request => Task.FromResult(new HttpResponse { Contents = ActualHtml, ContentType = "text/html" })
            };

            // Act
            var testCases = TestCasesParser.Parse(data);
            await testCases.Test(httpRequestServiceShim);

            // Assert
            Assert.IsTrue(testCases.Success);
        }
    }
}
