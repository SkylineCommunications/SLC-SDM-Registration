// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Represents errors that occur during SDM registration operations.
	/// </summary>
	[Serializable]
	public class SdmRegistrarException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SdmRegistrarException"/> class.
		/// </summary>
		public SdmRegistrarException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="SdmRegistrarException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public SdmRegistrarException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="SdmRegistrarException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public SdmRegistrarException(string message, Exception innerException) : base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="SdmRegistrarException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected SdmRegistrarException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
