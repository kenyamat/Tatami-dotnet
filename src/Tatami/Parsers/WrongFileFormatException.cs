namespace Tatami.Parsers
{
    using System;

    /// <summary>
    /// WrongFileFormatException class
    /// </summary>
	[Serializable]
	public class WrongFileFormatException : Exception
	{
		public WrongFileFormatException()
		{
		}

		public WrongFileFormatException(string message) : base(message)
		{
		}

		public WrongFileFormatException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
