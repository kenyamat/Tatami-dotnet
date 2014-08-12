namespace Tatami.Models.Assertions
{
    using System.Globalization;
    using Tatami.Constants;

    /// <summary>
    /// StatusCodeAssertion class
    /// </summary>
    public class StatusCodeAssertion : AssertionBase
    {
        /// <summary>
        /// Gets Name
        /// </summary>
        public override string Name
        {
            get { return HeaderName.StatusCode; }
        }

        /// <summary>
        /// Gets or sets Value (200)
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Asserts the case
        /// </summary>
        /// <param name="expected">expected Arrange</param>
        /// <param name="actual">actual Arrange</param>
        public override void Assert(Arrange expected, Arrange actual)
        {
            this.ExpectedValue = this.Value;
            this.ActualValue = ((int)actual.HttpResponse.StatusCode).ToString(CultureInfo.InvariantCulture);
            this.Success = this.ExpectedValue == this.ActualValue;
        }
    }
}