namespace Tatami.Tests.Parsers.Documents
{
    using System;
    using System.Linq;
    using Tatami.Parsers.Documents;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HtmlParserTests
    {
        [TestMethod]
        public void TestExistsNode()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span>test</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);
            
            // Act
            var result = doc.ExistsNode("//span", null);

            // Arrange
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExistsNodeWithInvalidXPath()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span>test</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            var result = doc.ExistsNode("//div", null);

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExistsNodeWithAttribute()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span class='col'>test</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            var result = doc.ExistsNode("//span", "class");

            // Arrange
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExistsNodeWithInvalidAttribute()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span class='col'>test</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            var result = doc.ExistsNode("//span", "id");

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestGetDocumentValue()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span>test</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            var result = doc.GetDocumentValue("//span", null);

            // Arrange
            Assert.AreEqual(result, "test");
        }

        [TestMethod]
        public void TestGetDocumentValueWithAttribute()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span class='col'>test</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            var result = doc.GetDocumentValue("//span", "class");

            // Arrange
            Assert.AreEqual(result, "col");
        }

        [TestMethod]
        public void TestGetDocumentValues()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span>test1</span>
                        <span>test2</span>
                        <span>test3</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            var result = doc.GetDocumentValues("//span", null);

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
                @"<html>
                    <body>
                        <span col='red'>test1</span>
                        <span col='yellow'>test2</span>
                        <span col='blue'>test3</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            var result = doc.GetDocumentValues("//span", "col");

            // Arrange
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("red", result.ElementAt(0));
            Assert.AreEqual("yellow", result.ElementAt(1));
            Assert.AreEqual("blue", result.ElementAt(2));
        }

        
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestTestSchemaWithXsd()
        {
            // Arrange
            const string contents =
                @"<html>
                    <body>
                        <span col='red'>test1</span>
                        <span col='yellow'>test2</span>
                        <span col='blue'>test3</span>
                    </body>
                  </html>";
            var doc = new HtmlParser(contents);

            // Act
            doc.TestSchemaWithXsd(null);
        }
    }
}
