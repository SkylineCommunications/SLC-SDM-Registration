// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Automation;

	public static class EngineExtensions
	{
		public static SdmRegistrar GetSdmRegistrar(this IEngine engine)
		{
			if (engine is null)
			{
				throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
			}

			return new SdmRegistrar(engine.GetUserConnection());
		}
	}
}