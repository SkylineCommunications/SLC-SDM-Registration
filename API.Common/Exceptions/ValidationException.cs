// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class ValidationException : Exception
	{
		public ValidationException()
		{
		}

		public ValidationException(string message) : base(message)
		{
		}

		public ValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
