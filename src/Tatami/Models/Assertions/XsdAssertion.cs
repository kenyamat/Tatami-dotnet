namespace Tatami.Models.Assertions
{
    using System;
    using Tatami.Constants;

    /// <summary>
    /// XsdAssertion class
    /// </summary>
    public class XsdAssertion : AssertionBase
	{
        /// <summary>
        /// Gets Name
        /// </summary>
		public override string Name 
		{
			get
			{
				return HeaderName.Xsd;
			}
		}

        /// <summary>
        /// Gets or sets Xsd
        /// </summary>
		public string Xsd { get; set; }

        /// <summary>
        /// Asserts
        /// </summary>
        /// <param name="expected">expected parameter</param>
        /// <param name="actual">actual parameter</param>
		public override void Assert(Arrange expected, Arrange actual)
		{
			string text;
			try
			{
				actual.HttpResponse.DocumentParser.TestSchemaWithXsd(this.Xsd);
				text = null;
			}
			catch (Exception ex)
			{
				text = ex.ToString();
			}

			this.ExpectedValue = "True";
			this.ActualValue = ((text == null) ? "True" : string.Format("Exception=<{0}>", text));
			this.Success = (this.ExpectedValue == this.ActualValue);
		}
	}
}
