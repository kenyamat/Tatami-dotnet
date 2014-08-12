namespace Tatami.Tests.Models.Assertions
{
    using System.Globalization;
    using System.Net;
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StatusCodeAssertionTests
    {
        [TestMethod]
        public void TestName()
        {
            // Arrange
            var item = new StatusCodeAssertion();

            // Act & Assert
            Assert.AreEqual("StatusCode", item.Name);
        }

        [TestMethod]
        public void TestAssert()
        {
            Assert.AreEqual(true, this.TestAssert(200, 200));
            Assert.AreEqual(false, this.TestAssert(200, 404));
        }

        private bool TestAssert(int expected, int actual)
        {
            // Arrange
            var item = new StatusCodeAssertion
            {
                Value = expected.ToString(CultureInfo.InvariantCulture),
            };

            var arrange = new Arrange
            {
                HttpResponse = new HttpResponse
                {
                    StatusCode = (HttpStatusCode)actual
                },
            };

            // Act
            item.Assert(null, arrange);

            return item.Success;
        }
    }
}
