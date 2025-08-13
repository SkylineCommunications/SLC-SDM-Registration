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

	public class SdmRegistrar
	{
		public SdmRegistrar(IConnection connection)
		{
			Solutions = Sdm.CreateProviderBuilder(new SolutionRegistrationDomStorageProvider(connection))
				.AddMiddleware(new SolutionValidationMiddleware())
				.AddMiddleware(new SdmTracingMiddleware<SolutionRegistration>())
				.Build();

			Models = Sdm.CreateProviderBuilder(new ModelRegistrationDomStorageProvider(connection))
				.AddMiddleware(new ModelSolutionSyncingMiddleware(Solutions))
				.AddMiddleware(new ModelValidationMiddleware())
				.AddMiddleware(new SdmTracingMiddleware<ModelRegistration>())
				.Build();
		}

		public IObservableBulkStorageProvider<SolutionRegistration> Solutions { get; }

		public IObservableBulkStorageProvider<ModelRegistration> Models { get; }

		#region Solutions
		public SolutionRegistration GetSolutionByGuid(Guid identifier)
		{
			var solution = Solutions.Read(SolutionRegistrationExposers.Guid.Equal(identifier)).FirstOrDefault();
			if (solution is null)
			{
				throw new RegistrationNotFoundException($"Solution Registration with identifier: {identifier} was not found.");
			}

			return solution;
		}

		public SolutionRegistration GetSolutionById(string identifier)
		{
			var solution = Solutions.Read(SolutionRegistrationExposers.ID.Equal(identifier)).FirstOrDefault();
			if (solution is null)
			{
				throw new RegistrationNotFoundException($"Solution Registration with identifier: {identifier} was not found.");
			}

			return solution;
		}

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
		public ModelRegistration GetModelByGuid(Guid identifier)
		{
			var model = Models.Read(ModelRegistrationExposers.Guid.Equal(identifier)).FirstOrDefault();
			if (model is null)
			{
				throw new RegistrationNotFoundException($"Model Registration with identifier: {identifier} was not found.");
			}

			return model;
		}

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

		public IEnumerable<ModelRegistration> GetModelsByName(string name)
		{
			var models = Models.Read(ModelRegistrationExposers.Name.Equal(name)).ToList();
			if (!models.Any())
			{
				throw new RegistrationNotFoundException($"Model Registration with name: {name} was not found.");
			}

			return models;
		}

		public IEnumerable<ModelRegistration> GetModelsBySolution(SolutionRegistration solution)
		{
			if (solution is null)
			{
				throw new ArgumentNullException(nameof(solution), "Solution Registration cannot be null.");
			}

			return GetModelsBySolution(solution.Reference);
		}

		public IEnumerable<ModelRegistration> GetModelsBySolution(SdmObjectReference<SolutionRegistration> solutionReference)
		{
			if (solutionReference == default)
			{
				throw new ArgumentNullException(nameof(solutionReference), "Solution Reference cannot be null.");
			}

			return Models.Read(ModelRegistrationExposers.Solution.Equal(solutionReference));
		}

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
