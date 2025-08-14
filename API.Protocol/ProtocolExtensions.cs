// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	using Skyline.DataMiner.Scripting;

	/// <summary>
	/// Provides extension methods for the <see cref="SLProtocol"/> class to support SDM registration operations.
	/// </summary>
	public static class ProtocolExtensions
	{
		/// <summary>
		/// Gets an <see cref="SdmRegistrar"/> instance for the specified <see cref="SLProtocol"/>.
		/// </summary>
		/// <param name="protocol">The protocol instance to extend.</param>
		/// <returns>
		/// An <see cref="SdmRegistrar"/> that provides registration and retrieval operations for SDM solutions and models.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="protocol"/> is <c>null</c>.
		/// </exception>
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