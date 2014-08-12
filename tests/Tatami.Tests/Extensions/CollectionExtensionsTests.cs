namespace Tatami.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Tatami.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void TestGetStringForNameValueCollection()
        {
            Assert.AreEqual("{a=b}", new NameValueCollection { { "a", "b" } }.GetString());
            Assert.AreEqual("{a=b, c=d}", new NameValueCollection { { "a", "b" }, { "c", "d" } }.GetString());
        }

        [TestMethod]
        public void TestGetStringForNameValueCollectionWithNullCollection()
        {
            Assert.IsNull(((NameValueCollection)null).GetString());
        }

        [TestMethod]
        public void TestGetStringForIEnumerable()
        {
            Assert.AreEqual("{a, b}", new List<string> { "a", "b" }.GetString());
        }

        [TestMethod]
        public void TestGetStringForIEnumerableWithNullCollection()
        {
            Assert.IsNull(((List<string>)null).GetString());
        }
    }
}
