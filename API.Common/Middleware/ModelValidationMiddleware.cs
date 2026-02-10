namespace Skyline.DataMiner.SDM.Registration.Middleware
{
	using System;
	using System.Collections.Generic;

	using Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM.Registration.Validation;

	using SLDataGateway.API.Types.Querying;

	internal class ModelValidationMiddleware : IBulkRepositoryMiddleware<ModelRegistration>
	{
		public long OnCount(FilterElement<ModelRegistration> filter, Func<FilterElement<ModelRegistration>, long> next)
		{
			if (filter == null)
			{
				throw new ArgumentNullException(nameof(filter), "Filter cannot be null.");
			}

			return next(filter);
		}

		public long OnCount(IQuery<ModelRegistration> query, Func<IQuery<ModelRegistration>, long> next)
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query), "Query cannot be null.");
			}

			return next(query);
		}

		public IReadOnlyCollection<ModelRegistration> OnCreate(IEnumerable<ModelRegistration> oToCreate, Func<IEnumerable<ModelRegistration>, IReadOnlyCollection<ModelRegistration>> next)
		{
			var builder = new ValidationResult.Builder();
			foreach (var modelRegistration in oToCreate)
			{
				var entry = Validate(modelRegistration);
				builder.Add(entry);
			}

			var result = builder.Build();
			if (!result.IsValid)
			{
				throw result.ToException();
			}

			return next(oToCreate);
		}

		public ModelRegistration OnCreate(ModelRegistration oToCreate, Func<ModelRegistration, ModelRegistration> next)
		{
			var result = Validate(oToCreate);
			if (!result.IsValid)
			{
				throw result.ToException();
			}

			return next(oToCreate);
		}

		public IReadOnlyCollection<ModelRegistration> OnCreateOrUpdate(IEnumerable<ModelRegistration> oToCreateOrUpdate, Func<IEnumerable<ModelRegistration>, IReadOnlyCollection<ModelRegistration>> next)
		{
			var builder = new ValidationResult.Builder();
			foreach (var modelRegistration in oToCreateOrUpdate)
			{
				var entry = Validate(modelRegistration);
				builder.Add(entry);
			}

			var result = builder.Build();
			if (!result.IsValid)
			{
				throw result.ToException();
			}

			return next(oToCreateOrUpdate);
		}

		public void OnDelete(IEnumerable<ModelRegistration> oToDelete, Action<IEnumerable<ModelRegistration>> next)
		{
			if (oToDelete is null)
			{
				throw new ArgumentNullException(nameof(oToDelete), "The collection of model registrations to delete cannot be null.");
			}

			next(oToDelete);
		}

		public void OnDelete(ModelRegistration oToDelete, Action<ModelRegistration> next)
		{
			if (oToDelete is null)
			{
				throw new ArgumentNullException(nameof(oToDelete), "The model registration to delete cannot be null.");
			}

			next(oToDelete);
		}

		public IEnumerable<ModelRegistration> OnRead(FilterElement<ModelRegistration> filter, Func<FilterElement<ModelRegistration>, IEnumerable<ModelRegistration>> next)
		{
			if (filter == null)
			{
				throw new ArgumentNullException(nameof(filter), "Filter cannot be null.");
			}

			return next(filter);
		}

		public IEnumerable<ModelRegistration> OnRead(IQuery<ModelRegistration> query, Func<IQuery<ModelRegistration>, IEnumerable<ModelRegistration>> next)
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query), "Query cannot be null.");
			}

			return next(query);
		}

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(FilterElement<ModelRegistration> filter, Func<FilterElement<ModelRegistration>, IEnumerable<IPagedResult<ModelRegistration>>> next)
		{
			if (filter == null)
			{
				throw new ArgumentNullException(nameof(filter), "Filter cannot be null.");
			}

			return next(filter);
		}

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(IQuery<ModelRegistration> query, Func<IQuery<ModelRegistration>, IEnumerable<IPagedResult<ModelRegistration>>> next)
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query), "Query cannot be null.");
			}

			return next(query);
		}

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(FilterElement<ModelRegistration> filter, int pageSize, Func<FilterElement<ModelRegistration>, int, IEnumerable<IPagedResult<ModelRegistration>>> next)
		{
			if (filter == null)
			{
				throw new ArgumentNullException(nameof(filter), "Filter cannot be null.");
			}

			if (pageSize <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
			}

			return next(filter, pageSize);
		}

		public IEnumerable<IPagedResult<ModelRegistration>> OnReadPaged(IQuery<ModelRegistration> query, int pageSize, Func<IQuery<ModelRegistration>, int, IEnumerable<IPagedResult<ModelRegistration>>> next)
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query), "Query cannot be null.");
			}

			if (pageSize <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
			}

			return next(query, pageSize);
		}

		public IReadOnlyCollection<ModelRegistration> OnUpdate(IEnumerable<ModelRegistration> oToUpdate, Func<IEnumerable<ModelRegistration>, IReadOnlyCollection<ModelRegistration>> next)
		{
			var builder = new ValidationResult.Builder();
			foreach (var link in oToUpdate)
			{
				var entry = Validate(link);
				builder.Add(entry);
			}

			var result = builder.Build();
			if (!result.IsValid)
			{
				throw result.ToException();
			}

			return next(oToUpdate);
		}

		public ModelRegistration OnUpdate(ModelRegistration oToUpdate, Func<ModelRegistration, ModelRegistration> next)
		{
			var result = Validate(oToUpdate);
			if (!result.IsValid)
			{
				throw result.ToException();
			}

			return next(oToUpdate);
		}

		private static ValidationEntry Validate(ModelRegistration modelRegistration)
		{
			var entry = new ValidationEntry();
			if (modelRegistration is null)
			{
				entry.Exceptions.Add(new ArgumentNullException(nameof(modelRegistration), "ModelRegistration object cannot be null."));
				return entry;
			}

			if (String.IsNullOrEmpty(modelRegistration.Name))
			{
				entry.Exceptions.Add(new ArgumentException("ModelRegistration Name cannot be null or empty.", nameof(modelRegistration)));
			}

			if (String.IsNullOrEmpty(modelRegistration.DisplayName))
			{
				entry.Exceptions.Add(new ArgumentException("ModelRegistration Display Name cannot be null or empty.", nameof(modelRegistration)));
			}

			if (String.IsNullOrEmpty(modelRegistration.Version))
			{
				entry.Exceptions.Add(new ArgumentException("ModelRegistration Version cannot be null or empty.", nameof(modelRegistration)));
			}

			if (modelRegistration.Solution == default)
			{
				entry.Exceptions.Add(new ArgumentException("ModelRegistration Solution cannot be null or empty.", nameof(modelRegistration)));
			}

			try
			{
				SdmVersion.FromString(modelRegistration.Version);
			}
			catch (Exception ex)
			{
				entry.Exceptions.Add(ex);
			}

			return entry;
		}
	}
}
