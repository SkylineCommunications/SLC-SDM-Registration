// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Automation;

	/// <summary>
	/// Provides extension methods for the <see cref="IEngine"/> interface related to SDM registration.
	/// </summary>
	public static class EngineExtensions
	{
		/// <summary>
		/// Gets an <see cref="SdmRegistrar"/> instance for the specified <see cref="IEngine"/>.
		/// </summary>
		/// <param name="engine">The engine to retrieve the SDM registrar for.</param>
		/// <returns>An <see cref="SdmRegistrar"/> instance associated with the engine's user connection.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="engine"/> is <c>null</c>.</exception>
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