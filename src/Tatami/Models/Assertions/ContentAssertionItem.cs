namespace Tatami.Models.Assertions
{
    /// <summary>
    /// ContentAssertionItem class
    /// </summary>
    public class ContentAssertionItem
    {
        /// <summary>
        /// Gets or sets Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets Query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets Attribute
        /// </summary>
        public string Attribute { get; set; }

        /// <summary>
        /// Gets or sets Exists
        /// </summary>
        public bool? Exists { get; set; }

        /// <summary>
        /// Gets or sets Pattern
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets Format
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets FormatCulture
        /// </summary>
        public string FormatCulture { get; set; }

        /// <summary>
        /// Gets or sets UrlDecode
        /// </summary>
        public bool UrlDecode { get; set; }
    }
}