// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Net;

	public static class ConnectionExtensions
	{
		public static SdmRegistrar GetSdmRegistrar(this IConnection connection)
		{
			if (connection is null)
			{
				throw new ArgumentNullException(nameof(connection), "IConnection cannot be null.");
			}

			return new SdmRegistrar(connection);
		}
	}
}
