namespace Tatami.Parsers.Mappings
{
    using System;
    using System.Xml.Linq;
    using Tatami.Models.Mappings;

    /// <summary>
    /// UserAgentMappingParser class
    /// </summary>
	public static class UserAgentMappingParser
	{
		public static UserAgentMapping Parse(XElement element)
		{
			if (element.Name != "UserAgentMapping")
			{
				throw new ArgumentException("Invalid element", "element");
			}

			var userAgentMapping = new UserAgentMapping();
			foreach (var current in element.Elements("Item"))
			{
				userAgentMapping.Add(current.Attribute("Key").Value, current.Value);
			}

			return userAgentMapping;
		}
	}
}
