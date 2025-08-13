// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class RegistrationNotFoundException : SdmRegistrarException
	{
		public RegistrationNotFoundException() { }

		public RegistrationNotFoundException(string message) : base(message) { }

		public RegistrationNotFoundException(string message, Exception innerException) : base(message, innerException) { }

		protected RegistrationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
