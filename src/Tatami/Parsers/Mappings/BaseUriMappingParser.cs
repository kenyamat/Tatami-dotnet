namespace Tatami.Parsers.Mappings
{
    using System;
    using System.Xml.Linq;
    using Tatami.Models.Mappings;

    /// <summary>
    /// BaseUriMappingParser class
    /// </summary>
    public static class BaseUriMappingParser
    {
        /// <summary>
        /// Parses xml to BaseUriMapping
        /// </summary>
        /// <param name="element">TestCase element</param>
        /// <returns>TestCase object</returns>
        public static BaseUriMapping Parse(XElement element)
        {
            if (element.Name != "BaseUriMapping")
            {
                throw new ArgumentException("invalid element", "element");
            }

            var dictionary = new BaseUriMapping();

            foreach (var item in element.Elements("Item"))
            {
                dictionary.Add(item.Attribute("Key").Value, item.Value);
            }

            return dictionary;
        }
    }
}