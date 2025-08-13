// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class SdmRegistrarException : Exception
	{
		public SdmRegistrarException() { }

		public SdmRegistrarException(string message) : base(message) { }

		public SdmRegistrarException(string message, Exception innerException) : base(message, innerException) { }

		protected SdmRegistrarException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
