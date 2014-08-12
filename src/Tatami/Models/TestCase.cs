namespace Tatami.Models
{
    using System;
    using System.Linq;
    using Tatami.Models.Assertions;

    /// <summary>
    /// TestCase class
    /// </summary>
    public class TestCase
    {
        /// <summary>
        /// Gets or sets Test case name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Arranges
        /// </summary>
        public Arranges Arranges { get; set; }

        /// <summary>
        /// Gets or sets Assertions
        /// </summary>
        public AssertionList Assertions { get; set; }

        /// <summary>
        /// Gets a value indicating whether result is success or fail
        /// </summary>
        public bool Success
        {
            get { return this.Assertions.All(assertion => assertion.Success); }
        }

        /// <summary>
        /// Gets or sets failed assertions
        /// </summary>
        public AssertionList FailedAssertions
        {
            get
			{
				var assertions = new AssertionList();
				if (this.Success)
				{
                    return assertions;
				}

				foreach (var current in this.Assertions)
				{
					if (!current.Success)
					{
						assertions.Add(current);
					}
				}

				return assertions;
			}
        }

        /// <summary>
        /// Assert all assertion
        /// </summary>
        /// <param name="expected">expected Arrange</param>
        /// <param name="actual">actual Arrange</param>
        public void Assert(Arrange expected, Arrange actual)
        {
            foreach (var current in this.Assertions)
            {
                try
                {
                    current.Assert(expected, actual);
                }
                catch (Exception exception)
                {
                    current.Exception = exception;
                    current.Success = false;
                }
            }
        }
    }
}