using System;

namespace FunctionsDesigner.Exceptions
{
	[Serializable]
	public class CustomException : ApplicationException
	{
		public CustomException()
		{
		}

		public CustomException(string message)
			: base(message)
		{
		}

		public CustomException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected CustomException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
