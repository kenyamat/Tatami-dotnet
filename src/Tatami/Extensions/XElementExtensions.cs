namespace Tatami.Extensions
{
    using System.Xml.Linq;

    /// <summary>
    /// XElementExtensions class
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Gets Attribute value
        /// it will return null when attribute is null or empty string.
        /// </summary>
        /// <param name="element">element parameter</param>
        /// <param name="name">name parameter</param>
        /// <returns>attribute value</returns>
        public static string GetAttributeValue(this XElement element, string name)
        {
            return element.Attribute(name) == null ? null : element.Attribute(name).Value;
        }

        /// <summary>
        /// Gets Attribute value as boolean type
        /// it will return false when attribute is null or empty string.
        /// </summary>
        /// <param name="element">element parameter</param>
        /// <param name="name">name parameter</param>
        /// <returns>attribute value</returns>
        public static bool GetAttributeValueAsBoolean(this XElement element, string name)
        {
            return element.Attribute(name) != null
                && !string.IsNullOrWhiteSpace(element.Attribute(name).Value)
                && bool.Parse(element.Attribute(name).Value);
        }
    }
}