namespace Tatami.Models.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Tatami.Parsers;

    /// <summary>
    /// Header class
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Header" /> class.
        /// </summary>
        public Header()
        {
            this.Children = new List<Header>();
        }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Depth
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// Gets or sets From
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Gets or sets To
        /// </summary>
        public int To { get; set; }

        /// <summary>
        /// Gets or sets Parent
        /// </summary>
        public Header Parent { get; set; }

        /// <summary>
        /// Gets or sets Children
        /// </summary>
        public IList<Header> Children { get; set; }

        /// <summary>
        /// Gets parent
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="depth">depth parameter</param>
        /// <param name="index">index parameter</param>
        /// <returns>Parent header</returns>
        public static Header GetParent(Header header, int depth, int index)
        {
            if (depth <= header.Depth)
            {
                throw new ArgumentException("Invalid Header Format. depth <= header.Depth", "depth");
            }

            if (depth == 0)
            {
                return header;
            }

            var parent = header;
            while (parent.Children.Any())
            {
                var h = parent;
                foreach (var child in parent.Children)
                {
                    if (child.From <= index && child.To >= index)
                    {
                        if (child.Depth == depth - 1)
                        {
                            return child;
                        }

                        parent = child;
                        break;
                    }
                }

                if (h == parent)
                {
                    break;
                }
            }

            throw new WrongFileFormatException("Invalid Header structure: name=" + parent.Name);
        }

        /// <summary>
        /// Gets header
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="index">index parameter</param>
        /// <returns>header selected by index</returns>
        public static Header GetHeader(Header header, int index)
        {
            while (true)
            {
                if (!header.Children.Any())
                {
                    return header;
                }

                foreach (var child in header.Children)
                {
                    if (child.From <= index && child.To >= index)
                    {
                        return GetHeader(child, index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets header
        /// </summary>
        /// <param name="header">header parameter</param>
        /// <param name="index">index parameter</param>
        /// <param name="depth">depth parameter</param>
        /// <returns>header selected by index</returns>
        public static Header GetHeader(Header header, int index, int depth)
        {
            while (true)
            {
                if (header.Depth == depth)
                {
                    return header;
                }

                if (!header.Children.Any())
                {
                    return null;
                }

                foreach (var child in header.Children)
                {
                    if (child.From <= index && child.To >= index)
                    {
                        return GetHeader(child, index, depth);
                    }
                }
            }
        }

        /// <summary>
        /// Searches by header name
        /// </summary>
        /// <param name="header">target source</param>
        /// <param name="headerName">headerName parameter</param>
        /// <returns>searched header</returns>
        public static Header Search(Header header, string headerName)
        {
            if (header.Name == headerName)
            {
                return header;
            }

            foreach (var child in header.Children)
            {
                var result = Search(child, headerName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets string
        /// </summary>
        /// <param name="header">target source</param>
        /// <param name="headerName">header name</param>
        /// <param name="row">row parameter</param>
        /// <returns>value string</returns>
        public static string GetString(Header header, string headerName, List<string> row)
        {
            var searchedHeader = Search(header, headerName);
            if (searchedHeader == null)
            {
                return null;
            }

            return row[searchedHeader.From];
        }

        /// <summary>
        /// Gets boolean
        /// </summary>
        /// <param name="header">target source</param>
        /// <param name="headerName">header name</param>
        /// <param name="row">row parameter</param>
        /// <returns>boolean value</returns>
        public static bool GetBoolean(Header header, string headerName, List<string> row)
        {
            var value = GetString(header, headerName, row);
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            bool result;
            if (!bool.TryParse(value, out result))
            {
                throw new ArgumentException(
                    string.Format("Invalid Data Format. <{0}>'s value should be boolean type. value={1}", 
                        headerName,
                        value));
            }

            return result;
        }

        /// <summary>
        /// Gets boolean?
        /// </summary>
        /// <param name="header">target source</param>
        /// <param name="headerName">header name</param>
        /// <param name="row">row parameter</param>
        /// <returns>boolean value</returns>
        public static bool? GetNullableBoolean(Header header, string headerName, List<string> row)
        {
            var value = Header.GetString(header, headerName, row);
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            bool flg;
            if (!bool.TryParse(value, out flg))
            {
                throw new ArgumentException(string.Format("Invalid Data Format. <{0}>'s value should be boolean type. value={1}", headerName, value));
            }

            return flg;
        }

        /// <summary>
        /// Gets string list
        /// </summary>
        /// <param name="header">target source</param>
        /// <param name="headerName">header name</param>
        /// <param name="row">row parameter</param>
        /// <returns>value string</returns>        
        public static IEnumerable<string> GetStringList(Header header, string headerName, List<string> row)
        {
            var searchedHeader = Search(header, headerName);
            if (searchedHeader == null)
            {
                return new List<string>();
            }

            var result = new List<string>();
            for (var i = searchedHeader.From; i <= searchedHeader.To; i++)
            {
                if (row[i] != null)
                {
                    result.Add(row[i]);
                }
            }

            if (result.Any())
            {
                return result;
            }

            return new List<string>();
        }

        /// <summary>
        /// Gets NameValueCollection
        /// </summary>
        /// <param name="header">target source</param>
        /// <param name="headerName">header name</param>
        /// <param name="row">row parameter</param>
        /// <returns>value string</returns>
        public static NameValueCollection GetNameValueCollection(Header header, string headerName, List<string> row)
        {
            var searchedHeader = Search(header, headerName);
            if (searchedHeader == null)
            {
                return new NameValueCollection();
            }

            var result = new NameValueCollection();
            for (var i = searchedHeader.From; i <= searchedHeader.To; i++)
            {
                if (row[i] != null)
                {
                    var child = GetHeader(header, i);
                    result.Add(child.Name, row[i]);
                }
            }

            if (result.HasKeys())
            {
                return result;
            }

            return new NameValueCollection();
        }
    }
}
