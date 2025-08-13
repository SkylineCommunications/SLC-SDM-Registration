// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Scripting;

	public static class ProtocolExtensions
	{
		public static SdmRegistrar GetSdmRegistrar(this SLProtocol protocol)
		{
			if (protocol is null)
			{
				throw new ArgumentNullException(nameof(protocol), "protocol cannot be null.");
			}

			return new SdmRegistrar(protocol.SLNet.RawConnection);
		}
	}
}