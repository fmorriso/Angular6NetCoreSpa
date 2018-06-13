using System;

namespace Angular6NetCoreSpa.CustomExceptions
{
	public class CustomExceptionExample : Exception
	{
		public CustomExceptionExample()
		{ }

		public CustomExceptionExample(string message)
			: base(message)
		{ }

		public CustomExceptionExample(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
