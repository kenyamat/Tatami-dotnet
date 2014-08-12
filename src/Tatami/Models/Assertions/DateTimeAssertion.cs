namespace Tatami.Models.Assertions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// DateTimeAssertion class
    /// </summary>
    public class DateTimeAssertion : TextAssertion
    {
        /// <summary>
        /// Gets or sets a value indicating whether it is to test time only.
        /// </summary>
        public bool IsTime { get; set; }

        /// <summary>
        /// Asserts the case
        /// </summary>
        /// <param name="expected">expected Arrange</param>
        /// <param name="actual">actual Arrange</param>
        public override void Assert(Arrange expected, Arrange actual)
        {
            if (!this.IsList)
            {
                this.ExpectedValue = GetValue(expected, this.Expected);
                this.ActualValue = GetValue(actual, this.Actual);
            }
            else
            {
                this.ExpectedValue = string.Join(", ", GetValues(expected, this.Expected));
                this.ActualValue = string.Join(", ", GetValues(actual, this.Actual));
            }

            this.Success = this.ExpectedValue == this.ActualValue;
        }

        /// <summary>
        /// Gets Value
        /// </summary>
        /// <param name="arrange">arrange parameter</param>
        /// <param name="assertionItem">assertionItem parameter</param>
        /// <returns>value from arrange</returns>
        protected override string GetValue(Arrange arrange, ContentAssertionItem assertionItem)
        {
            var value = base.GetValue(arrange, assertionItem);
            return GetDateTimeString(value, assertionItem.Format, assertionItem.FormatCulture);
        }

        /// <summary>
        /// Gets Values
        /// </summary>
        /// <param name="arrange">arrange parameter</param>
        /// <param name="assertionItem">assertionItem parameter</param>
        /// <returns>value from arrange</returns>
        protected override IEnumerable<string> GetValues(Arrange arrange, ContentAssertionItem assertionItem)
        {
            var values = base.GetValues(arrange, assertionItem);

            var list = new List<string>();
            foreach (var value in values)
            {
                list.Add(GetDateTimeString(value, assertionItem.Format, assertionItem.FormatCulture));
            }

            return list;
        }

        /// <summary>
        /// Gets DateTime string
        /// </summary>
        /// <param name="value">value parameter</param>
        /// <param name="format">format parameter</param>
        /// <param name="formatCulture">formatCulture parameter</param>
        /// <returns>DateTime string(yyyyMMddHHmmss)</returns>
        private string GetDateTimeString(string value, string format, string formatCulture)
        {
            try
            {
                CultureInfo culture = null;
                if (formatCulture != null)
                {
                    culture = new CultureInfo(formatCulture);
                }

                if (this.IsTime)
                {
                    return DateTime.ParseExact(value, format, culture).ToString("HHmmss");
                }

                return DateTime.ParseExact(value, format, culture).ToString("yyyyMMddHHmmss");                
            }
            catch (Exception innerException)
            {
                throw new FormatException(
                    string.Format("Failed to parse DateTime. value={0}, format={1}, IsTime={2}, formatCulture={3}",
					    value,
					    format,
					    this.IsTime,
					    formatCulture),
				    innerException);
            }

        }
    }
}