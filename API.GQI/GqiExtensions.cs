// Ignore Spelling: SDM GQI DMS

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Analytics.GenericInterface;

	/// <summary>
	/// Provides extension methods for the <see cref="GQIDMS"/> related to SDM registration.
	/// </summary>
	public static class GqiExtensions
	{
		/// <summary>
		/// Creates a new <see cref="SdmRegistrar"/> instance using the specified <see cref="GQIDMS"/>.
		/// </summary>
		/// <param name="dms">The GQI DataMiner System (DMS) instance.</param>
		/// <returns>
		/// A new <see cref="SdmRegistrar"/> initialized with the DMS connection.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="dms"/> is <c>null</c>.
		/// </exception>
		public static SdmRegistrar GetSdmRegistrar(GQIDMS dms)
		{
			if (dms is null)
			{
				throw new ArgumentNullException(nameof(dms), "dms cannot be null.");
			}

			return new SdmRegistrar(dms.GetConnection());
		}

		/// <summary>
		/// Creates a new <see cref="SdmRegistrar"/> instance using the <see cref="GQIDMS"/> from the specified <see cref="OnInitInputArgs"/>.
		/// </summary>
		/// <param name="args">The initialization arguments containing the DMS instance.</param>
		/// <returns>
		/// A new <see cref="SdmRegistrar"/> initialized with the DMS connection from <paramref name="args"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="args"/> is <c>null</c>.
		/// </exception>
		public static SdmRegistrar GetSdmRegistrar(this OnInitInputArgs args)
		{
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args), "OnInitInputArgs cannot be null.");
			}

			return GetSdmRegistrar(args.DMS);
		}
	}
}