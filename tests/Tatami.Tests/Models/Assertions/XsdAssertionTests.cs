namespace Tatami.Tests.Models.Assertions
{
    using Tatami.Models;
    using Tatami.Models.Assertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XsdAssertionTests
    {
        [TestMethod]
        public void TestName()
        {
            // Arrange
            var item = new XsdAssertion();

            // Act & Assert
            Assert.AreEqual("Xsd", item.Name);
        }

        [TestMethod]
        public void TestAssert()
        {
            // Arrange
            const string ActualHtml =
              @"<Shop>
                    <Item>a</Item>
                    <Item>b</Item>
                    <Item>c</Item>
                </Shop>"; 

            var item = new XsdAssertion
            {
                Xsd =
                    @"<?xml version='1.0' encoding='utf-8'?>
                        <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'>
                          <xs:element name='Shop'>
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element name='Item' minOccurs='1' maxOccurs='unbounded' />
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                        </xs:schema>"
            };

            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/xml" } };

            // Act
            item.Assert(null, actual);

            // Assert
            Assert.IsTrue(item.Success);
        }

        [TestMethod]
        public void TestAssertWithInvalidXsd()
        {
            // Arrange
            const string ActualHtml =
              @"<Shop>
                    <Item>a</Item>
                    <Item>b</Item>
                    <Item>c</Item>
                </Shop>";

            var item = new XsdAssertion
            {
                Xsd =
                    @"<?xml version='1.0' encoding='utf-8'?>
                        <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'>
                          <xs:element name='Shop'>
                            <xs:complexType>
                              <xs:sequence>"
            };

            var actual = new Arrange { HttpResponse = new HttpResponse { Contents = ActualHtml, ContentType = "text/xml" } };

            // Act
            item.Assert(null, actual);

            // Assert
            Assert.IsFalse(item.Success);
        }
    }
}
