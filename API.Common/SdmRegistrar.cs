// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM.Middleware;
	using Skyline.DataMiner.SDM.Registration.Exceptions;
	using Skyline.DataMiner.SDM.Registration.Middleware;

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
			Solutions = Sdm.CreateProviderBuilder(new SolutionRegistrationDomStorageProvider(connection))
				.AddMiddleware(new SolutionValidationMiddleware())
				//.AddMiddleware(new SdmTracingMiddleware<SolutionRegistration>())
				.Build();

			Models = Sdm.CreateProviderBuilder(new ModelRegistrationDomStorageProvider(connection))
				.AddMiddleware(new ModelSolutionSyncingMiddleware(Solutions))
				.AddMiddleware(new ModelValidationMiddleware())
				//.AddMiddleware(new SdmTracingMiddleware<ModelRegistration>())
				.Build();
		}

		/// <summary>
		/// Gets the storage provider for solution registrations.
		/// </summary>
		public IObservableBulkStorageProvider<SolutionRegistration> Solutions { get; }

		/// <summary>
		/// Gets the storage provider for model registrations.
		/// </summary>
		public IObservableBulkStorageProvider<ModelRegistration> Models { get; }

		#region Solutions

		/// <summary>
		/// Retrieves a solution registration by its unique <see cref="Guid"/> identifier.
		/// </summary>
		/// <param name="identifier">The unique identifier of the solution.</param>
		/// <returns>The matching <see cref="SolutionRegistration"/>.</returns>
		/// <exception cref="RegistrationNotFoundException">Thrown if no solution is found with the specified identifier.</exception>
		public SolutionRegistration GetSolutionByGuid(Guid identifier)
		{
			var solution = Solutions.Read(SolutionRegistrationExposers.Guid.Equal(identifier)).FirstOrDefault();
			if (solution is null)
			{
				throw new RegistrationNotFoundException($"Solution Registration with identifier: {identifier} was not found.");
			}

			return solution;
		}

		/// <summary>
		/// Retrieves a solution registration by its string identifier.
		/// </summary>
		/// <param name="identifier">The string identifier of the solution.</param>
		/// <returns>The matching <see cref="SolutionRegistration"/>.</returns>
		/// <exception cref="RegistrationNotFoundException">Thrown if no solution is found with the specified identifier.</exception>
		public SolutionRegistration GetSolutionById(string identifier)
		{
			var solution = Solutions.Read(SolutionRegistrationExposers.ID.Equal(identifier)).FirstOrDefault();
			if (solution is null)
			{
				throw new RegistrationNotFoundException($"Solution Registration with identifier: {identifier} was not found.");
			}

			return solution;
		}

		/// <summary>
		/// Registers a solution and its associated models.
		/// </summary>
		/// <param name="solution">The solution registration to add or update.</param>
		/// <param name="models">The models to associate with the solution. Optional.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="solution"/> is null.</exception>
		public void RegisterSolution(SolutionRegistration solution, IEnumerable<ModelRegistration> models = null)
		{
			if (solution is null)
			{
				throw new ArgumentNullException(nameof(solution), "Solution Registration cannot be null.");
			}

			if (models is null)
			{
				models = Enumerable.Empty<ModelRegistration>();
			}

			Solutions.CreateOrUpdate(new[] { solution });

			foreach (var model in models)
			{
				model.Solution = solution;
			}

			RegisterModels(models);
		}
		#endregion

		#region Models

		/// <summary>
		/// Retrieves a model registration by its unique <see cref="Guid"/> identifier.
		/// </summary>
		/// <param name="identifier">The unique identifier of the model.</param>
		/// <returns>The matching <see cref="ModelRegistration"/>.</returns>
		/// <exception cref="RegistrationNotFoundException">Thrown if no model is found with the specified identifier.</exception>
		public ModelRegistration GetModelByGuid(Guid identifier)
		{
			var model = Models.Read(ModelRegistrationExposers.Guid.Equal(identifier)).FirstOrDefault();
			if (model is null)
			{
				throw new RegistrationNotFoundException($"Model Registration with identifier: {identifier} was not found.");
			}

			return model;
		}

		/// <summary>
		/// Retrieves a model registration by its name.
		/// </summary>
		/// <param name="name">The name of the model.</param>
		/// <returns>The matching <see cref="ModelRegistration"/>.</returns>
		/// <exception cref="RegistrationNotFoundException">Thrown if no model is found with the specified name.</exception>
		/// <exception cref="SdmRegistrarException">Thrown if multiple models are found with the specified name.</exception>
		public ModelRegistration GetModelByName(string name)
		{
			var models = Models.Read(ModelRegistrationExposers.Name.Equal(name)).ToList();
			if (!models.Any())
			{
				throw new RegistrationNotFoundException($"Model Registration with name: {name} was not found.");
			}

			if (models.Count > 1)
			{
				throw new SdmRegistrarException($"Multiple Model Registrations found for name: {name}. Please use the GetModelsByName call if this is intended.");
			}

			return models[0];
		}

		/// <summary>
		/// Retrieves all model registrations with the specified name.
		/// </summary>
		/// <param name="name">The name of the models.</param>
		/// <returns>A collection of <see cref="ModelRegistration"/> objects.</returns>
		/// <exception cref="RegistrationNotFoundException">Thrown if no models are found with the specified name.</exception>
		public IEnumerable<ModelRegistration> GetModelsByName(string name)
		{
			var models = Models.Read(ModelRegistrationExposers.Name.Equal(name)).ToList();
			if (!models.Any())
			{
				throw new RegistrationNotFoundException($"Model Registration with name: {name} was not found.");
			}

			return models;
		}

		/// <summary>
		/// Retrieves all model registrations associated with the specified solution.
		/// </summary>
		/// <param name="solution">The solution registration.</param>
		/// <returns>A collection of <see cref="ModelRegistration"/> objects.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="solution"/> is null.</exception>
		public IEnumerable<ModelRegistration> GetModelsBySolution(SolutionRegistration solution)
		{
			if (solution is null)
			{
				throw new ArgumentNullException(nameof(solution), "Solution Registration cannot be null.");
			}

			return GetModelsBySolution(solution.Reference);
		}

		/// <summary>
		/// Retrieves all model registrations associated with the specified solution reference.
		/// </summary>
		/// <param name="solutionReference">The reference to the solution.</param>
		/// <returns>A collection of <see cref="ModelRegistration"/> objects.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="solutionReference"/> is null.</exception>
		public IEnumerable<ModelRegistration> GetModelsBySolution(SdmObjectReference<SolutionRegistration> solutionReference)
		{
			if (solutionReference == default)
			{
				throw new ArgumentNullException(nameof(solutionReference), "Solution Reference cannot be null.");
			}

			return Models.Read(ModelRegistrationExposers.Solution.Equal(solutionReference));
		}

		/// <summary>
		/// Registers one or more model registrations.
		/// </summary>
		/// <param name="models">The models to add or update.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="models"/> is null.</exception>
		public void RegisterModels(params ModelRegistration[] models)
		{
			if (models is null)
			{
				throw new ArgumentNullException(nameof(models), "Model Registrations cannot be null.");
			}

			if (!models.Any())
			{
				return;
			}

			Models.CreateOrUpdate(models);
		}

		/// <summary>
		/// Registers a collection of model registrations.
		/// </summary>
		/// <param name="models">The models to add or update.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="models"/> is null.</exception>
		public void RegisterModels(IEnumerable<ModelRegistration> models)
		{
			if (models is null)
			{
				throw new ArgumentNullException(nameof(models), "Model Registrations cannot be null.");
			}

			if (!models.Any())
			{
				return;
			}

			Models.CreateOrUpdate(models);
		}
		#endregion
	}
}
