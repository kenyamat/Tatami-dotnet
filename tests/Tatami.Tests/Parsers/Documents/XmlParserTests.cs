namespace Tatami.Tests.Parsers.Documents
{
    using System.Linq;
    using Tatami.Parsers.Documents;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XmlParserTests
    {
        [TestMethod]
        public void TestExistsNode()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name>test</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);
            
            // Act
            var result = doc.ExistsNode("//name", null);

            // Arrange
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExistsNodeWithInvalidXPath()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name>test</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);

            // Act
            var result = doc.ExistsNode("//value", null);

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExistsNodeWithAttribute()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name class='blue'>test</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);

            // Act
            var result = doc.ExistsNode("//name", "class");

            // Arrange
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExistsNodeWithInvalidAttribute()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name class='blue'>test</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);

            // Act
            var result = doc.ExistsNode("//name", "id");

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestGetDocumentValue()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name class='blue'>test</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);

            // Act
            var result = doc.GetDocumentValue("//name", null);

            // Arrange
            Assert.AreEqual(result, "test");
        }

        [TestMethod]
        public void TestGetDocumentValueWithAttribute()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name class='blue'>test</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);

            // Act
            var result = doc.GetDocumentValue("//name", "class");

            // Arrange
            Assert.AreEqual(result, "blue");
        }

        [TestMethod]
        public void TestGetDocumentValues()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name>test1</name>
                        <name>test2</name>
                        <name>test3</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);

            // Act
            var result = doc.GetDocumentValues("//name", null);

            // Arrange
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("test1", result.ElementAt(0));
            Assert.AreEqual("test2", result.ElementAt(1));
            Assert.AreEqual("test3", result.ElementAt(2));
        }

        [TestMethod]
        public void TestGetDocumentValuesWithAttribute()
        {
            // Arrange
            const string contents =
                @"<root>
                    <item>
                        <name col='red'>test1</name>
                        <name col='yellow'>test2</name>
                        <name col='blue'>test3</name>
                    </item>
                  </root>";
            var doc = new XmlParser(contents);

            // Act
            var result = doc.GetDocumentValues("//name", "col");

            // Arrange
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("red", result.ElementAt(0));
            Assert.AreEqual("yellow", result.ElementAt(1));
            Assert.AreEqual("blue", result.ElementAt(2));
        }

        [TestMethod]
        public void TestTestSchemaWithXsd()
        {
            // Arrange
            const string contents =
                @"<Shop>
                    <Item />
                    <Item />
                    <Item />
                  </Shop>";

            var doc = new XmlParser(contents);
            const string xsd = @"<?xml version='1.0' encoding='utf-8'?>
                        <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'>
                          <xs:element name='Shop'>
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element name='Item' minOccurs='1' maxOccurs='unbounded' />
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                        </xs:schema>";

            // Act
            doc.TestSchemaWithXsd(xsd);
        }
    }
}
