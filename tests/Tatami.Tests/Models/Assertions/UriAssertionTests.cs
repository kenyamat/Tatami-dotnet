namespace Tatami.Tests.Models.Assertions
{
    using System;
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UriAssertionTests
    {
        [TestMethod]
        public void TestName()
        {
            // Arrange
            var item = new UriAssertion();

            // Act & Assert
            Assert.AreEqual("Uri", item.Name);
        }

        [TestMethod]
        public void TestAssert()
        {
            Assert.AreEqual(true, this.TestAssert("/home", "http://yahoo.com/home"));
            Assert.AreEqual(false, this.TestAssert("/next", "http://yahoo.com/home"));
            Assert.AreEqual(true, this.TestAssert("/", "http://yahoo.com/"));
            Assert.AreEqual(true, this.TestAssert("/", "http://yahoo.com"));
            Assert.AreEqual(true, this.TestAssert("/home?a=b", "http://yahoo.com/home?a=b"));
            Assert.AreEqual(true, this.TestAssert("/home?a=b#Temp", "http://yahoo.com/home?a=b#Temp"));
        }

        private bool TestAssert(string expected, string actual)
        {
            // Arrange
            var item = new UriAssertion { Value = expected };
            var arrange = new Arrange { HttpResponse = new HttpResponse { Uri = new Uri(actual) }, };

            // Act
            item.Assert(null, arrange);

            return item.Success;
        }
    }
}
