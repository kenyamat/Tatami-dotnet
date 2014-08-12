namespace Tatami.Parsers.Csv
{
    using System.Collections.Generic;
    using System.Linq;
    using Tatami.Models.Csv;

    /// <summary>
    /// Header Parser class
    /// </summary>
    public static class HeaderParser
    {
        /// <summary>
        /// Header Row Count
        /// </summary>
        public const int HeaderRowCount = 4;

        /// <summary>
        /// Parses csv
        /// </summary>
        /// <param name="data">csv data</param>
        /// <returns>Header object</returns>
        public static Header Parse(List<List<string>> data)
        {
            var root = new Header
            {
                Name = "Root",
                Depth = -1,
                From = 0,
                To = data.ElementAt(0).Count - 1,
            };

            for (var i = 0; i < HeaderRowCount; i++)
            {
                var row = data.ElementAt(i);
                Header current = null;
                for (var j = 1; j < row.Count; j++)
                {
                    var value = row[j];
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        current = new Header { Name = value, Depth = i, From = j, To = j, Parent = Header.GetParent(root, i, j) };
                        current.Parent.Children.Add(current);
                    }
                    else if (current != null)
                    {
                        var parentOfCurrent = Header.GetParent(root, current.Depth, current.From);
                        if (current.To < parentOfCurrent.To)
                        {
                            current.To++;
                        }
                    }
                }
            }

            return root;
        }
    }
}
