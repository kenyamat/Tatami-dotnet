namespace Tatami.Models.Assertions
{
    using System.Text.RegularExpressions;
    using Tatami.Constants;

    /// <summary>
    /// UriAssertion class
    /// </summary>
    public class UriAssertion : AssertionBase
    {
        /// <summary>
        /// Gets Name
        /// </summary>
        public override string Name
        {
            get { return HeaderName.Uri; }
        }

        /// <summary>
        /// Gets or sets Value ("/locations/us/ny?category=bag")
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
            var match = Regex.Match(actual.HttpResponse.Uri.OriginalString, ".*://[^/]*(.*)", RegexOptions.None);
            if (match.Success)
            {
                ActualValue = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(ActualValue))
                {
                    ActualValue = "/";
                }
            }

            this.Success = this.ExpectedValue == this.ActualValue;
        }
    }
}