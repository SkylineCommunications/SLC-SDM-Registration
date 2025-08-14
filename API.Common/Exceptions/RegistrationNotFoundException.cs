// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// The exception that is thrown when a registration cannot be found in the SDM registrar.
	/// </summary>
	[Serializable]
	public class RegistrationNotFoundException : SdmRegistrarException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> class.
		/// </summary>
		public RegistrationNotFoundException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public RegistrationNotFoundException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public RegistrationNotFoundException(string message, Exception innerException) : base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected RegistrationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
