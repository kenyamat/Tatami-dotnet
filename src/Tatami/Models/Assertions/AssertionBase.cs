namespace Tatami.Models.Assertions
{
    using System;

    /// <summary>
    /// AssertionBase class
    /// </summary>
    public abstract class AssertionBase
    {
        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets ExpectedValue
        /// </summary>
        public string ExpectedValue { get; set; }

        /// <summary>
        /// Gets or sets ActualValue
        /// </summary>
        public string ActualValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether result is success or fail
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets Exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Assert the case
        /// </summary>
        /// <param name="expected">expected Arrange</param>
        /// <param name="actual">actual Arrange</param>
        public abstract void Assert(Arrange expected, Arrange actual);
    }
}