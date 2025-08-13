// Ignore Spelling: SDM GQI DMS

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Analytics.GenericInterface;

	public static class GqiExtensions
	{
		public static SdmRegistrar GetSdmRegistrar(GQIDMS dms)
		{
			if (dms is null)
			{
				throw new ArgumentNullException(nameof(dms), "dms cannot be null.");
			}

			return new SdmRegistrar(dms.GetConnection());
		}
	}
}