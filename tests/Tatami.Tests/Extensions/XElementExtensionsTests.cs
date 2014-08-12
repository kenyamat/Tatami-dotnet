namespace Tatami.Tests.Extensions
{
    using System.Xml.Linq;
    using Tatami.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XElementExtensionsTests
    {
        [TestMethod]
        public void TestGetAttributeValue()
        {
            Assert.AreEqual("value", XElement.Parse("<Item attr='value' />").GetAttributeValue("attr"));
            Assert.AreEqual(string.Empty, XElement.Parse("<Item attr='' />").GetAttributeValue("attr"));
            Assert.AreEqual(null, XElement.Parse("<Item />").GetAttributeValue("attr"));
        }

        [TestMethod]
        public void TestGetAttributeValueAsBoolean()
        {
            Assert.AreEqual(true, XElement.Parse("<Item attr='true' />").GetAttributeValueAsBoolean("attr"));
            Assert.AreEqual(true, XElement.Parse("<Item attr='True' />").GetAttributeValueAsBoolean("attr"));
            Assert.AreEqual(true, XElement.Parse("<Item attr='TRUE' />").GetAttributeValueAsBoolean("attr"));
            Assert.AreEqual(false, XElement.Parse("<Item attr='false' />").GetAttributeValueAsBoolean("attr"));
            Assert.AreEqual(false, XElement.Parse("<Item attr='False' />").GetAttributeValueAsBoolean("attr"));
            Assert.AreEqual(false, XElement.Parse("<Item attr='FALSE' />").GetAttributeValueAsBoolean("attr"));
            Assert.AreEqual(false, XElement.Parse("<Item attr='' />").GetAttributeValueAsBoolean("attr"));
            Assert.AreEqual(false, XElement.Parse("<Item />").GetAttributeValueAsBoolean("attr"));
        }
    }
}
