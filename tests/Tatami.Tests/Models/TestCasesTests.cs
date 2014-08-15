namespace Tatami.Tests.Models
{
    using System;
    using System.Threading.Tasks;
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Tatami.Models.Assertions.Fakes;
    using Tatami.Services.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;

    [TestClass]
    public class TestCasesTests
    {
        private static readonly Fixture fixture = new Fixture();

        [TestMethod]
        public async Task TestTest()
        {
            // Arrange
            var testCases = new TestCases
            {
                new TestCase
                {
                    Arranges = new Arranges
                    {
                        Expected = new Arrange { HttpRequest = new HttpRequest() },
                        Actual = new Arrange { HttpRequest = new HttpRequest() } 
                    },
                    Assertions = new AssertionList { new StubAssertionBase { Success = true } }
                },
            };

            var httpRequestService = new StubIHttpRequestService
            {
                GetResponseAsyncHttpRequestActionOfHttpClient = (r, h) => Task.FromResult(new HttpResponse())
            };

            // Act
            await testCases.TestAsync(httpRequestService);

            // Assert
            Assert.IsTrue(testCases.Success);
            Assert.AreEqual(0, testCases.FailedCases.Count);
            Assert.IsNotNull(testCases.ResultMessage);
            Assert.IsNull(testCases.FailedMessage);
        }

        [TestMethod]
        public async Task TestTestWithNullExpected()
        {
            // Arrange
            var testCases = new TestCases
            {
                new TestCase
                {
                    Arranges = new Arranges
                    {
                        Actual = new Arrange { HttpRequest = new HttpRequest() } 
                    },
                    Assertions = new AssertionList { new StubAssertionBase { Success = true } }
                },
            };
            var httpRequestService = new StubIHttpRequestService
            {
                GetResponseAsyncHttpRequestActionOfHttpClient = (r, h) => Task.FromResult(new HttpResponse())
            };

            // Act
            await testCases.TestAsync(httpRequestService);

            // Assert
            Assert.IsTrue(testCases.Success);
            Assert.AreEqual(0, testCases.FailedCases.Count);
            Assert.IsNotNull(testCases.ResultMessage);
            Assert.IsNull(testCases.FailedMessage);
        }

        [TestMethod]
        public async Task TestTestWithFailedCase()
        {
            // Arrange
            var testCases = new TestCases
            {
                new TestCase
                {
                    Arranges = new Arranges
                    {
                        Expected = new Arrange { HttpRequest = new HttpRequest() },
                        Actual = new Arrange { HttpRequest = new HttpRequest() } 
                    },
                    Assertions = new AssertionList { new StubAssertionBase { Success = false }},
                },
            };
            var httpRequestService = new StubIHttpRequestService
            {
                GetResponseAsyncHttpRequestActionOfHttpClient = (r, h) => Task.FromResult(new HttpResponse())
            };

            // Act
            await testCases.TestAsync(httpRequestService);

            // Assert
            Assert.IsFalse(testCases.Success);
            Assert.AreEqual(1, testCases.FailedCases.Count);
            Assert.IsNotNull(testCases.ResultMessage);
            Assert.IsNotNull(testCases.FailedMessage);
        }

        [TestMethod]
        public async Task TestTestWithFailedCaseHavingException()
        {
            // Arrange
            var exception = fixture.Create<Exception>();
            var testCases = new TestCases
            {
                new TestCase
                {
                    Arranges = new Arranges
                    {
                        Expected = new Arrange { HttpRequest = new HttpRequest() },
                        Actual = new Arrange { HttpRequest = new HttpRequest() } 
                    },
                    Assertions = new AssertionList { new StubAssertionBase { Success = false, Exception = exception }},
                },
            };
            var httpRequestService = new StubIHttpRequestService
            {
                GetResponseAsyncHttpRequestActionOfHttpClient = (r, h) => Task.FromResult(new HttpResponse())
            };

            // Act
            await testCases.TestAsync(httpRequestService);

            // Assert
            Assert.IsFalse(testCases.Success);
            Assert.AreEqual(1, testCases.FailedCases.Count);
            Assert.IsNotNull(testCases.ResultMessage);
            Assert.IsNotNull(testCases.FailedMessage);
        }
    }
}
