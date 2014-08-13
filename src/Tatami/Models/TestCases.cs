namespace Tatami.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tatami.Services;

    /// <summary>
    /// TestCases class
    /// </summary>
    public class TestCases : List<TestCase>
    {
        /// <summary>
        /// Gets or sets Test case name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets a value indicating whether result is success or fail
        /// </summary>
        public bool Success
        {
            get { return this.All(testCase => testCase.Success); }
        }

        /// <summary>
        /// Gets or sets failed cases
        /// </summary>
        public IList<TestCase> FailedCases
        { 
			get
			{
				var list = new List<TestCase>();
				if (this.Success)
				{
					return list;
				}

				foreach (TestCase current in this)
				{
					if (!current.Success)
					{
						list.Add(current);
					}
				}

				return list;
			}
        }

        /// <summary>
        /// Gets Result Message.
        /// </summary>
        public string ResultMessage
        {
            get
            {
                var buffer = new StringBuilder();
                buffer.AppendFormat("Test Cases Count: {0}/{1}", this.FailedCases.Count, Count).AppendLine();

                foreach (var testCase in this)
                {
                    buffer.AppendFormat("  Test Case Name: {0}", testCase.Name).AppendLine();
                    buffer.AppendFormat("    Success: {0}", testCase.Success).AppendLine();
                    buffer.AppendFormat("    Arranges", new object[0]).AppendLine();
                    buffer.AppendFormat("      Expected", new object[0]).AppendLine();

                    if (testCase.Arranges.Expected != null)
                    {
                        buffer.AppendFormat("        HttRequest: {0}", testCase.Arranges.Expected.HttpRequest).AppendLine();
                        buffer.AppendFormat("        HttResponse: {0}", testCase.Arranges.Expected.HttpResponse).AppendLine();
                    }

                    buffer.AppendFormat("      Actual", new object[0]).AppendLine();

                    if (testCase.Arranges.Expected != null)
                    {
                        buffer.AppendFormat("        HttRequest: {0}", testCase.Arranges.Actual.HttpRequest).AppendLine();
                        buffer.AppendFormat("        HttResponse: {0}", testCase.Arranges.Actual.HttpResponse).AppendLine();
                    }

                    buffer.AppendFormat("    Assertions Count: {0}/{1}", testCase.FailedAssertions.Count, testCase.Assertions.Count).AppendLine();

                    foreach (var assertion in testCase.Assertions)
                    {
                        buffer.AppendFormat("      Assertion Name: {0}", assertion.Name).AppendLine();
                        buffer.AppendFormat("        Success: {0}", assertion.Success).AppendLine();

                        if (assertion.Exception != null)
                        {
                            buffer.AppendFormat("        Result StackTrace: {0}", assertion.Exception).AppendLine();
                        }
                        else
                        {
                            buffer.AppendFormat("        Result Message: Expected:<{0}>. Actual:<{1}>.", assertion.ExpectedValue, assertion.ActualValue).AppendLine();
                        }
                    }
                }

                return buffer.ToString();
            }
        }

        /// <summary>
        /// Gets FailedMessage
        /// </summary>
        public string FailedMessage
        {
            get
            {
                if (this.Success)
                {
                    return null;
                }

                var buffer = new StringBuilder();
                buffer.AppendFormat("Failed Test Cases Count: {0}/{1}", this.FailedCases.Count, Count).AppendLine();

                foreach (var failedCase in this.FailedCases)
                {
                    buffer.AppendFormat("  Failed Test Case Name: {0}", failedCase.Name).AppendLine();
                    buffer.AppendFormat("    Arranges", new object[0]).AppendLine();
                    buffer.AppendFormat("      Expected", new object[0]).AppendLine();

                    if (failedCase.Arranges.Expected != null)
                    {
                        buffer.AppendFormat("        HttRequest: {0}", failedCase.Arranges.Expected.HttpRequest).AppendLine();
                        buffer.AppendFormat("        HttResponse: {0}", failedCase.Arranges.Expected.HttpResponse).AppendLine();
                    }

                    buffer.AppendFormat("      Actual", new object[0]).AppendLine();
                    if (failedCase.Arranges.Expected != null)
                    {
                        buffer.AppendFormat("        HttRequest: {0}", failedCase.Arranges.Actual.HttpRequest).AppendLine();
                        buffer.AppendFormat("        HttResponse: {0}", failedCase.Arranges.Actual.HttpResponse).AppendLine();
                    }

                    buffer.AppendFormat("    Failed Assertions Count: {0}/{1}", failedCase.FailedAssertions.Count, failedCase.Assertions.Count).AppendLine();
                    foreach (var failedAssertion in failedCase.FailedAssertions)
                    {
                        buffer.AppendFormat("      Failed Assertion Name: {0}", failedAssertion.Name).AppendLine();
                        if (failedAssertion.Exception != null)
                        {
                            buffer.AppendFormat("        Result StackTrace: {0}", failedAssertion.Exception).AppendLine();
                        }
                        else
                        {
                            buffer.AppendFormat("        Result Message: Expected:<{0}>. Actual:<{1}>.", failedAssertion.ExpectedValue, failedAssertion.ActualValue).AppendLine();
                        }
                    }
                }

                return buffer.ToString();
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="httpRequestService">the httpRequestService</param>
        public void Test(IHttpRequestService httpRequestService)
        {
            foreach (var testCase in this)
            {
                if (testCase.Arranges.Expected != null && testCase.Arranges.Expected.HttpRequest != null)
                {
                    testCase.Arranges.Expected.HttpResponse = httpRequestService.GetResponse(testCase.Arranges.Expected.HttpRequest);
                }

                if (testCase.Arranges.Actual != null)
                {
                    testCase.Arranges.Actual.HttpResponse = httpRequestService.GetResponse(testCase.Arranges.Actual.HttpRequest);
                }

                testCase.Assert(testCase.Arranges.Expected, testCase.Arranges.Actual);
            }
        }

        /// <summary>
        /// Test async
        /// </summary>
        /// <param name="httpRequestService">the httpRequestService</param>
        public async Task TestAsync(IHttpRequestService httpRequestService)
        {
            foreach (var testCase in this)
            {
                if (testCase.Arranges.Expected != null && testCase.Arranges.Expected.HttpRequest != null)
                {
                    testCase.Arranges.Expected.HttpResponse = await httpRequestService.GetResponseAsync(testCase.Arranges.Expected.HttpRequest);
                }

                if (testCase.Arranges.Actual != null)
                {
                    testCase.Arranges.Actual.HttpResponse = await httpRequestService.GetResponseAsync(testCase.Arranges.Actual.HttpRequest);
                }

                testCase.Assert(testCase.Arranges.Expected, testCase.Arranges.Actual);
            }
        }
    }
}