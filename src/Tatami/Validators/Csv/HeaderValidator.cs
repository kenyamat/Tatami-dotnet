namespace Tatami.Validators.Csv
{
    using System;
    using System.Linq;
    using Tatami.Constants;
    using Tatami.Models.Csv;
    using Tatami.Parsers;

    /// <summary>
    /// HeaderValidator class
    /// </summary>
    public static class HeaderValidator
    {
        /// <summary>
        /// Validates Root header
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void Validate(Header header)
        {
            // header
            ValidateNotNull(header, HeaderName.Root);
            ValidateName(header, HeaderName.Root);
            ValidateDepthFromTo(header, -1);
            ValidateHavingChildren(header, 2);
            ValidateHavingChild(header, HeaderName.Arrange);
            ValidateHavingChild(header, HeaderName.Assertion);

            // Arrange
            ValidateArrange(header.Children[0]);

            // Assertion
            ValidateAssertion(header.Children[1]);
        }

        /// <summary>
        /// Validates Arrange header
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateArrange(Header header)
        {
            ValidateNotNull(header, HeaderName.Arrange);
            ValidateName(header, HeaderName.Arrange);
            ValidateHavingChildren(header);
            ValidateHavingChild(header, HeaderName.HttpRequestActual);

            foreach (var child in header.Children)
            {
                var name = child.Name;
                if (name == HeaderName.HttpRequestExpected
                    || name == HeaderName.HttpRequestActual)
                {
                    ValidateHttpRequest(child);
                }
                else
                {
                    throw new WrongFileFormatException(
                        string.Format("Invalid Header Format. <{0}> has unknown child. Name={1}", 
                            header.Name,
                            name));
                }
            }
        }

        /// <summary>
        /// Validates HttpRequest header
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateHttpRequest(Header header)
        {
            ValidateNotNull(header, "HttpRequest Expected".Replace("Expected", string.Empty));
            ValidateHavingChild(header, "BaseUri");

            foreach (var child in header.Children)
            {
                var name = child.Name;
                if (name == HeaderName.BaseUri
                    || name == HeaderName.Method
                    || name == HeaderName.UserAgent
                    || name == HeaderName.PathInfos
                    || name == HeaderName.Fragment
                    || name == HeaderName.Content)
                {
                    ValidateNotHavingChildren(child);
                }
                else if (name == HeaderName.Headers
                         || name == HeaderName.Cookies
                         || name == HeaderName.QueryStrings)
                {
                    ValidateHavingChildren(child);
                    ValidateNotHavingGrandChildren(child);
                }
                else
                {
                    throw new WrongFileFormatException(
                        String.Format("Invalid Header Format. <{0}> a has unknown child. Name={1}",
                            header.Name,
                            name));
                }
            }
        }

        /// <summary>
        /// Validates Assertion header
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateAssertion(Header header)
        {
            ValidateNotNull(header, HeaderName.Assertion);
            ValidateName(header, HeaderName.Assertion);
            ValidateHavingChildren(header);

            foreach (var child in header.Children)
            {
                var name = child.Name;
                if (name == HeaderName.Uri
                    || name == HeaderName.StatusCode
                    || name == HeaderName.Xsd)
                {
                    ValidateNotHavingChildren(child);
                }
                else if (name == HeaderName.Headers
                         || name == HeaderName.Cookies)
                {
                    ValidateHavingChildren(child);
                    ValidateNotHavingGrandChildren(child);
                }
                else if (name == HeaderName.Contents)
                {
                    ValidateContents(child);
                }
                else
                {
                    throw new WrongFileFormatException(
                        String.Format("Invalid Header Format. <{0}> has a unknown child. Name={1}",
                            header.Name,
                            name));
                }
            }
        }

        /// <summary>
        /// Validates Contents header
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateContents(Header header)
        {
            ValidateNotNull(header, HeaderName.Contents);
            ValidateHavingChildren(header);
            ValidateHavingChild(header, HeaderName.Name);
            ValidateHavingChild(header, HeaderName.Expected);
            ValidateHavingChild(header, HeaderName.Actual);

            foreach (var child in header.Children)
            {
                var name = child.Name;
                if (name == HeaderName.Name
                    || name == HeaderName.IsList
                    || name == HeaderName.IsDateTime
                    || name == HeaderName.IsTime)
                {
                    ValidateNotHavingChildren(child);
                }
                else if (name == HeaderName.Expected
                         || name == HeaderName.Actual)
                {
                    ValidateContentsItem(child);
                }
                else
                {
                    throw new WrongFileFormatException(
                        String.Format("Invalid Header Format. <{0}> has a unknown child. Name={1}",
                            header.Name,
                            name));
                }
            }
        }

        /// <summary>
        /// Validates ContentsItem header
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateContentsItem(Header header)
        {
            ValidateHavingChildren(header);

            foreach (var child in header.Children)
            {
                var name = child.Name;

                if (name == HeaderName.Value
                    || name == HeaderName.Query
                    || name == HeaderName.Attribute
                    || name == HeaderName.Exists
                    || name == HeaderName.Pattern
                    || name == HeaderName.Format
                    || name == HeaderName.FormatCulture
                    || name == HeaderName.UrlDecode)
                {
                    ValidateNotHavingChildren(child);
                }
                else
                {
                    throw new WrongFileFormatException(
                        String.Format("Invalid Header Format. <{0}> has a unknown child. Name={1}",
                            header.Name,
                            name));
                }
            }
        }

        /// <summary>
        /// Validates if header is not null
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="name">name parameter</param>
        // ReSharper disable UnusedParameter.Local
        public static void ValidateNotNull(Header header, string name)
            // ReSharper restore UnusedParameter.Local
        {
            if (header == null)
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. Header <{0}> is not found.",
                        name));
            }
        }

        /// <summary>
        /// Validates Depth / From / To of the header
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="depth">depth parameter</param>
        public static void ValidateDepthFromTo(Header header, int depth)
        {
            if (header.From > header.To)
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. <{0}>'s From/To is invalid.",
                        header.Name));
            }

            if (header.Depth != depth)
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. <{0}>'s Depth is invalid. Expected={1}, Actual={2}",
                        header.Name,
                        depth,
                        header.Depth));
            }

            foreach (var child in header.Children)
            {
                if (header.From > child.From)
                {
                    throw new WrongFileFormatException(
                        String.Format("Invalid Header Format. <{0}>'s From is invalid. <{0}>'s From={1}, {0}'s parent's From={2}",
                            header.Name,
                            child.From,
                            header.From));
                }

                if (header.To < child.To)
                {
                    throw new WrongFileFormatException(
                        String.Format("Invalid Header Format. <{0}>'s To is invalid. <{0}>'s To={1}, {0}'s parent's To={2}",
                            header.Name,
                            child.To,
                            header.To));
                }

                ValidateDepthFromTo(child, depth + 1);
            }
        }

        /// <summary>
        /// Validates header's name
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="expected">expected parameter</param>
        public static void ValidateName(Header header, string expected)
        {
            if (header.Name != expected)
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. Invalid Header Name. Expected={0}, Actual={1}",
                        expected,
                        header.Name));
            }
        }

        /// <summary>
        /// Validates if header has the child
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="childName">childName parameter</param>
        public static void ValidateHavingChild(Header header, string childName)
        {
            if (header.Children.All(child => child.Name != childName))
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. <{0}> should have <{1}> as his child.",
                        header.Name,
                        childName));
            }
        }

        /// <summary>
        /// Validates if header has children
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateHavingChildren(Header header)
        {
            if (header.Children == null || !header.Children.Any())
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. <0>'s children are not found. <{0}> should have children.",
                        header.Name));
            }
        }

        /// <summary>
        /// Validates if header has no children
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateNotHavingChildren(Header header)
        {
            if (header.Children != null && header.Children.Any())
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. <{0}>'s children are found. <{0}> should have no children.",
                        header.Name));
            }
        }

        /// <summary>
        /// Validates if header has children
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="count">count parameter</param>
        public static void ValidateHavingChildren(Header header, int count)
        {
            ValidateHavingChildren(header);
            if (header.Children.Count != count)
            {
                throw new WrongFileFormatException(
                    String.Format("Invalid Header Format. Count of <{0}>'s children is invalid. Expected={1}, Actual={2}",
                        header.Name,
                        count,
                        header.Children.Count));
            }
        }

        /// <summary>
        /// Validates if header has no grand children
        /// </summary>
        /// <param name="header">header parameter</param>
        public static void ValidateNotHavingGrandChildren(Header header)
        {
            foreach (var child in header.Children)
            {
                if (child.Children.Any())
                {
                    throw new WrongFileFormatException(
                        String.Format("Invalid Header Format. <{0}> should have no grand children.",
                            header.Name));
                }
            }
        }
    }
}
