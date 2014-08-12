namespace Tatami.Models.Assertions
{
    using Tatami.Constants;

    /// <summary>
    /// CookieAssertion class
    /// </summary>
    public class CookieAssertion : AssertionBase
    {
        /// <summary>
        /// Gets Name
        /// </summary>
        public override string Name
        {
            get { return string.Format("{0}({1})", HeaderName.Cookies, this.Key); }
        }

        /// <summary>
        /// Gets or sets Key ("setMyLocation")
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// Gets or sets Value ("wc:001/wc:003/wc:004")
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Assert the case
        /// </summary>
        /// <param name="expected">expected Arrange</param>
        /// <param name="actual">actual Arrange</param>
        public override void Assert(Arrange expected, Arrange actual)
        {
            this.ExpectedValue = this.Value;
            this.ActualValue = actual.HttpResponse.Cookies[this.Key];
            this.Success = this.ExpectedValue == this.ActualValue;
        }
    }
}