namespace Skyline.DataMiner.SDM.Registration.Middleware
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Net.Helper;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM.Registration.Exceptions;

	using SLDataGateway.API.Types.Querying;

	internal class ModelSolutionSyncingMiddleware : IBulkRepositoryMiddleware<ModelRegistration>
	{
		private readonly IBulkRepository<SolutionRegistration> _solutionProvider;

		internal ModelSolutionSyncingMiddleware(IBulkRepository<SolutionRegistration> solutionProvider)
		{
			_solutionProvider = solutionProvider ?? throw new ArgumentNullException(nameof(solutionProvider));
		}

		public void OnCreate(IEnumerable<ModelRegistration> oToCreate, Action<IEnumerable<ModelRegistration>> next)
		{
			HandleModels(oToCreate, next);
		}

		public void OnCreate(ModelRegistration oToCreate, Action<ModelRegistration> next)
		{
			HandleModel(oToCreate, next);
		}

		public void OnCreateOrUpdate(IEnumerable<ModelRegistration> oToCreateOrUpdate, Action<IEnumerable<ModelRegistration>> next)
		{
			HandleModels(oToCreateOrUpdate, next);
		}

		public void OnDelete(IEnumerable<ModelRegistration> oToDelete, Action<IEnumerable<ModelRegistration>> next)
		{
			HandleModels(oToDelete, next, addition: false);
		}

		public void OnDelete(ModelRegistration oToDelete, Action<ModelRegistration> next)
		{
			HandleModel(oToDelete, next, addition: false);
		}

		public void OnUpdate(IEnumerable<ModelRegistration> oToUpdate, Action<IEnumerable<ModelRegistration>> next)
		{
			HandleModels(oToUpdate, next);
		}

		public void OnUpdate(ModelRegistration oToUpdate, Action<ModelRegistration> next)
		{
			HandleModel(oToUpdate, next);
		}

		#region Ignored
		public long OnCount(FilterElement<ModelRegistration> filter, Func<FilterElement<ModelRegistration>, long> next) => next(filter);

		public long OnCount(IQuery<ModelRegistration> query, Func<IQuery<ModelRegistration>, long> next) => next(query);

		public IEnumerable<ModelRegistration> OnRead(FilterElement<ModelRegistration> filter, Func<FilterElement<ModelRegistration>, IEnumerable<ModelRegistration>> next) => next(filter);

		public IEnumerable<ModelRegistration> OnRead(IQuery<ModelRegistration> query, Func<IQuery<ModelRegistration>, IEnumerable<ModelRegistration>> next) => next(query);

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(FilterElement<ModelRegistration> filter, Func<FilterElement<ModelRegistration>, IEnumerable<IPagedResult<ModelRegistration>>> next) => next(filter);

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(IQuery<ModelRegistration> query, Func<IQuery<ModelRegistration>, IEnumerable<IPagedResult<ModelRegistration>>> next) => next(query);

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(FilterElement<ModelRegistration> filter, int pageSize, Func<FilterElement<ModelRegistration>, int, IEnumerable<IPagedResult<ModelRegistration>>> next) => next(filter, pageSize);

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(IQuery<ModelRegistration> query, int pageSize, Func<IQuery<ModelRegistration>, int, IEnumerable<IPagedResult<ModelRegistration>>> next) => next(query, pageSize);
		#endregion

		/// <summary>
		/// Handles the synchronization of a single <see cref="ModelRegistration"/> with its associated <see cref="SolutionRegistration"/>.
		/// </summary>
		/// <param name="model">The model to process.</param>
		/// <param name="next">The next action to invoke for processing the model.</param>
		/// <param name="addition">
		/// Indicates whether the model should be added (<c>true</c>) or removed (<c>false</c>) from the solution's models collection.
		/// </param>
		/// <exception cref="RegistrationNotFoundException">
		/// Thrown when the associated solution registration cannot be found.
		/// </exception>
		private void HandleModel(ModelRegistration model, Action<ModelRegistration> next, bool addition = true)
		{
			// Fetch the solution
			var solution = _solutionProvider
				.Read(SolutionRegistrationExposers.Identifier.Equal(model.Solution))
				.FirstOrDefault();
			if (solution is null)
			{
				throw new RegistrationNotFoundException($"Solution Registration with identifier: {model.Solution} was not found.");
			}

			// Try to create the model
			next(model);

			// Add/remove the model to/from the solution
			var solutionModels = solution.Models.ToList();
			if (addition)
			{
				solutionModels.Add(model);
				solution.Models = solutionModels.Distinct().ToList();
			}
			else
			{
				solutionModels.Remove(model);
				solution.Models = solutionModels.Distinct().ToList();
			}

			_solutionProvider.Update(solution);
		}

		/// <summary>
		/// Handles the synchronization of multiple <see cref="ModelRegistration"/> instances with their associated <see cref="SolutionRegistration"/> objects.
		/// </summary>
		/// <param name="models">The collection of models to process.</param>
		/// <param name="next">The next action to invoke for processing the models.</param>
		/// <param name="addition">
		/// Indicates whether the models should be added (<c>true</c>) or removed (<c>false</c>) from their respective solutions' models collections.
		/// </param>
		/// <exception cref="SdmBulkCrudException{ModelRegistration}">
		/// Thrown when one or more models could not be processed due to missing solutions or update failures.
		/// </exception>
		private void HandleModels(IEnumerable<ModelRegistration> models, Action<IEnumerable<ModelRegistration>> next, bool addition = true)
		{
			// Fetch all the needed solutions
			var solutionIds = models.Select(m => m.Solution).Distinct().ToList();
			var solutions = new Dictionary<string, SolutionRegistration>();
			foreach (var batch in solutionIds.Batch(100))
			{
				var filters = batch.Select(id => SolutionRegistrationExposers.Identifier.Equal(id)).ToArray();
				_solutionProvider
					.Read(new ORFilterElement<SolutionRegistration>(filters))
					.ForEach(s => solutions[s.Identifier] = s);
			}

			// Check if there are any models that do not have a valid solution
			var invalidModels = new List<ModelRegistration>();
			var exceptionBuilder = new SdmBulkCrudException<ModelRegistration>.Builder();
			foreach (var model in models)
			{
				if (solutions.ContainsKey(model.Solution))
				{
					continue;
				}

				invalidModels.Add(model);
				exceptionBuilder.AddFailed(model, new RegistrationNotFoundException($"Solution Registration with identifier: {model.Solution} was not found."));
			}

			// Create the valid models
			var validModels = models.Except(invalidModels).ToList();
			try
			{
				next(validModels);
			}
			catch (SdmBulkCrudException<ModelRegistration> ex)
			{
				// Add the failed items to the invalid list
				foreach (var failure in ex.FailedItems)
				{
					exceptionBuilder.AddFailed(failure.Item, failure.Exception);
				}
			}

			// Add the valid models to their solutions
			var groupedModels = validModels.GroupBy(m => m.Solution).ToDictionary(g => g.Key, g => g.ToList());
			foreach (var group in groupedModels)
			{
				if (!solutions.TryGetValue(group.Key, out var solution))
				{
					continue;
				}

				var solutionModels = solution.Models.ToList();
				if (addition)
				{
					solutionModels.AddRange(group.Value.Select(m => SdmObjectReference<ModelRegistration>.Convert(m)));
					solution.Models = solutionModels.Distinct().ToList();
				}
				else
				{
					solutionModels.RemoveAll(modelRef => group.Value.Any(m => SdmObjectReference<ModelRegistration>.Convert(m) == modelRef));
					solution.Models = solutionModels.Distinct().ToList();
				}
			}

			try
			{
				// Update the solutions
				_solutionProvider.Update(solutions.Values);
			}
			catch (SdmBulkCrudException<SolutionRegistration> ex)
			{
				// If the solution could not be updated, add the models for that solution to the failed list
				foreach (var failure in ex.FailedItems)
				{
					var failedModels = groupedModels[failure.Item];
					failedModels.ForEach(m => exceptionBuilder.AddFailed(m, failure.Exception));
				}
			}

			// Throw exception if there are any invalid models
			if (exceptionBuilder.HasFailure)
			{
				throw exceptionBuilder.Build();
			}
		}
	}
}
