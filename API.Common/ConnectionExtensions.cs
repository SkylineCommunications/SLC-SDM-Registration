// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Net;

	/// <summary>
	/// Provides extension methods for <see cref="IConnection"/> to support SDM registration operations.
	/// </summary>
	public static class ConnectionExtensions
	{
		/// <summary>
		/// Gets an <see cref="SdmRegistrar"/> instance for the specified <see cref="IConnection"/>.
		/// </summary>
		/// <param name="connection">The connection to use for SDM registration operations.</param>
		/// <returns>An <see cref="SdmRegistrar"/> instance associated with the specified connection.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="connection"/> is <c>null</c>.</exception>
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
