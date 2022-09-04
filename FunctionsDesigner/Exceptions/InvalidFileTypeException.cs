using System;
using System.Runtime.Serialization;

namespace FunctionsDesigner.Exceptions
{
	/// <summary>
	/// Exception generated when using an invalid file type
	/// </summary>
	[Serializable]
	public class InvalidFileTypeException : CustomException
	{
		public InvalidFileTypeException(string message)
			: base(message)
		{
		}

		public InvalidFileTypeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected InvalidFileTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
