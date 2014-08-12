namespace Tatami.Models.Assertions
{
    using Tatami.Constants;

    /// <summary>
    /// HeaderAssertion class
    /// </summary>
    public class HeaderAssertion : AssertionBase
    {
        /// <summary>
        /// Gets Name
        /// </summary>
        public override string Name
        {
            get { return string.Format("{0}({1})", HeaderName.Headers, this.Key); }
        }

        /// <summary>
        /// Gets or sets Key ("Content-Type")
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets Value ("text/html; charset=utf-8")
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
            this.ActualValue = actual.HttpResponse.Headers[this.Key];
            this.Success = this.ExpectedValue == this.ActualValue;
        }
    }
}