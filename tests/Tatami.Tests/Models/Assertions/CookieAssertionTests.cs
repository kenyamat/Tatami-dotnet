namespace Tatami.Tests.Models.Assertions
{
    using System.Collections.Specialized;
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;

    [TestClass]
    public class CookieAssertionTests
    {
        private static readonly Fixture fixture = new Fixture();

        [TestMethod]
        public void TestName()
        {
            // Arrange
            var key = fixture.Create<string>();
            var item = new CookieAssertion { Key = key };

            // Act & Assert
            Assert.AreEqual("Cookies(" + key + ")", item.Name);
        }

        [TestMethod]
        public void TestAssert()
        {
            // Arrange
            var key = fixture.Create<string>();
            var value = fixture.Create<string>();
            var item = new CookieAssertion { Key = key, Value = value, };

            var arrange = new Arrange
            {
                HttpResponse = new HttpResponse
                {
                    Cookies = new NameValueCollection
                    {
                        { key, value },
                        { "b", "b value" },
                    }
                },
            };

            // Act
            item.Assert(null, arrange);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        public void TestAssertWithFail()
        {
            // Arrange
            var key = fixture.Create<string>();
            var value = fixture.Create<string>();
            var item = new CookieAssertion
            {
                Key = key,
                Value = value,
            };

            var arrange = new Arrange
            {
                HttpResponse = new HttpResponse
                {
                    Cookies = new NameValueCollection
                    {
                        { key, "a value" },
                        { "b", "b value" },
                    }
                },
            };

            // Act
            item.Assert(null, arrange);

            // Assert
            Assert.IsFalse(item.Success);
        }

        [TestMethod]
        public void TestAssertWithNoItem()
        {
            // Arrange
            var key = fixture.Create<string>();
            var value = fixture.Create<string>();
            var item = new CookieAssertion { Key = key, Value = value, };

            var arrange = new Arrange
            {
                HttpResponse = new HttpResponse
                {
                    Cookies = new NameValueCollection
                    {
                        { "a", "a value" },
                        { "b", "b value" },
                    }
                },
            };

            // Act
            item.Assert(null, arrange);

            // Assert
            Assert.IsFalse(item.Success);
        }
    }
}
