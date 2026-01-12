namespace Skyline.DataMiner.SDM.Registration
{
	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.SDM.Middleware;
	using Skyline.DataMiner.SDM.Registration.Middleware;

#if NETSTANDARD2_0_OR_GREATER
	using Skyline.DataMiner.SDM.Telemetry;
#endif

	/// <summary>
	/// Provides registration and retrieval operations for SDM solutions and models.
	/// </summary>
	/// <remarks>
	/// This class manages the registration, validation, and querying of <see cref="SolutionRegistration"/> and <see cref="ModelRegistration"/> objects.
	/// It uses middleware for validation, synchronization, and tracing, and interacts with storage providers for persistence.
	/// </remarks>
	public class SdmRegistrar
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SdmRegistrar"/> class using the specified connection.
		/// </summary>
		/// <param name="connection">The connection to use for storage providers.</param>
		public SdmRegistrar(IConnection connection)
		{
			Solutions = new SolutionRegistrationDomRepository(connection)
				.WithMiddleware(new SolutionValidationMiddleware())
#if NETSTANDARD2_0_OR_GREATER
				.WithMiddleware(new TracingMiddleware<SolutionRegistration>());
#else
;
#endif

			Models = new ModelRegistrationDomRepository(connection)
				.WithMiddleware(new ModelSolutionSyncingMiddleware(Solutions))
				.WithMiddleware(new ModelValidationMiddleware())
#if NETSTANDARD2_0_OR_GREATER
				.WithMiddleware(new TracingMiddleware<ModelRegistration>());
#else
;
#endif

		}

		/// <summary>
		/// Gets the repository for solution registrations.
		/// </summary>
		public ISolutionRepository Solutions { get; }

		/// <summary>
		/// Gets the repository for model registrations.
		/// </summary>
		public IModelRepository Models { get; }
	}
}
