namespace Tatami.Models.Assertions
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// TextAssertion class
    /// </summary>
    public class TextAssertion : AssertionBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether list or not
        /// </summary>
        public bool IsList { get; set; }

        /// <summary>
        /// Gets or sets Expected
        /// </summary>
        public ContentAssertionItem Expected { get; set; }
        
        /// <summary>
        /// Gets or sets Actual
        /// </summary>
        public ContentAssertionItem Actual { get; set; }

        /// <summary>
        /// Matched by regular expression
        /// </summary>
        /// <param name="value">value parameter</param>
        /// <param name="pattern">pattern parameter</param>
        /// <returns>mattched value</returns>
        protected static string Match(string value, string pattern)
        {
            var match = Regex.Match(value, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new ArgumentException("Regex failed to match: value:" + value + ", pattern:" + pattern);
        }

        /// <summary>
        /// Asserts the case
        /// </summary>
        /// <param name="expected">expected Arrange</param>
        /// <param name="actual">actual Arrange</param>
        public override void Assert(Arrange expected, Arrange actual)
        {
            if (this.Expected.Exists.HasValue)
            {
                this.ExpectedValue = "Node exists: " + this.Expected.Exists;
                this.ActualValue = "Node exists: " + actual.HttpResponse.ExistsNode(this.Actual.Query, this.Actual.Attribute);
            }
            else
            {
                if (!this.IsList)
                {
                    this.ExpectedValue = this.GetValue(expected, this.Expected);
                    this.ActualValue = this.GetValue(actual, this.Actual);
                }
                else
                {
                    this.ExpectedValue = string.Join(", ", this.GetValues(expected, this.Expected));
                    this.ActualValue = string.Join(", ", this.GetValues(actual, this.Actual));
                }
            }

            if (this.Expected.UrlDecode)
            {
                this.ExpectedValue = HttpUtility.UrlDecode(this.ExpectedValue);
            }

            if (this.Actual.UrlDecode)
            {
                this.ActualValue = HttpUtility.UrlDecode(this.ActualValue);
            }

            this.Success = (this.ExpectedValue == this.ActualValue);
        }

        /// <summary>
        /// Gets Value
        /// </summary>
        /// <param name="arrange">arrange parameter</param>
        /// <param name="assertionItem">assertionItem parameter</param>
        /// <returns>value from arrange</returns>
        protected virtual string GetValue(Arrange arrange, ContentAssertionItem assertionItem)
        {
            if (assertionItem.Value != null)
            {
                return assertionItem.Value;
            }

            var value = arrange.HttpResponse.GetDocumentValue(assertionItem.Query, assertionItem.Attribute);
            if (assertionItem.Pattern != null)
            {
                return TextAssertion.Match(value, assertionItem.Pattern);
            }

            return value;
        }

        /// <summary>
        /// Gets Values
        /// </summary>
        /// <param name="arrange">arrange parameter</param>
        /// <param name="assertionItem">assertionItem parameter</param>
        /// <returns>value from arrange</returns>
        protected virtual IEnumerable<string> GetValues(Arrange arrange, ContentAssertionItem assertionItem)
        {
            if (assertionItem.Value != null)
            {
                throw new NotImplementedException("Static value test for list is not supported.");
            }

            var values = arrange.HttpResponse.GetDocumentValues(assertionItem.Query, assertionItem.Attribute);

            if (assertionItem.Pattern != null)
            {
                var list = new List<string>();
                foreach (var value in values)
                {
                    list.Add(Match(value, assertionItem.Pattern));
                }

                return list;
            }

            return values;
        }
    }
}