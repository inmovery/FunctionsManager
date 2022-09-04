using System;
using System.Runtime.Serialization;

namespace FunctionsDesigner.Exceptions
{
	/// <summary>
	/// Exception generated when using an invalid data format
	/// </summary>
	[Serializable]
	public class InvalidDataFormatException : CustomException
	{
		public InvalidDataFormatException(string message)
			: base(message)
		{
		}

		public InvalidDataFormatException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected InvalidDataFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
