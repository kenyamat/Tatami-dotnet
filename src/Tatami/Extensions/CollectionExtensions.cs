namespace Tatami.Extensions
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// CollectionExtensions class
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Converts collection to string
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <returns>comma joined string</returns>
        public static string GetString(this NameValueCollection collection)
        {
            if (collection == null)
            {
                return null;
            }

            var array = (from key in collection.AllKeys
                         from value in collection.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray<string>();

            return "{" + string.Join(", ", array) + "}";
        }

        /// <summary>
        /// Converts collection to string
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <returns>comma joined string</returns>
        public static string GetString(this IEnumerable<string> collection)
        {
            if (collection == null)
            {
                return null;
            }

            return "{" + string.Join(", ", collection) + "}";
        }
    }
}