namespace Skyline.DataMiner.SDM.Registration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM.Registration.Exceptions;

	[AllowSdmMiddleware]
	public interface IModelRepository : IBulkRepository<ModelRegistration>
	{
		/// <summary>
		/// Retrieves a model registration by its name.
		/// </summary>
		/// <param name="name">The name of the model.</param>
		/// <returns>The matching <see cref="ModelRegistration"/>.</returns>
		/// <exception cref="RegistrationNotFoundException">Thrown if no model is found with the specified name.</exception>
		/// <exception cref="SdmRegistrarException">Thrown if multiple models are found with the specified name.</exception>
		ModelRegistration GetModelByName(string name);

		/// <summary>
		/// Retrieves all model registrations associated with the specified solution reference.
		/// </summary>
		/// <param name="solutionReference">The reference to the solution.</param>
		/// <returns>A collection of <see cref="ModelRegistration"/> objects.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="solutionReference"/> is null.</exception>
		IEnumerable<ModelRegistration> GetModelsBySolution(SdmObjectReference<SolutionRegistration> solutionReference);
	}

	internal partial class ModelRegistrationDomRepository : IModelRepository
	{
		public ModelRegistration GetModelByName(string name)
		{
			var models = Read(ModelRegistrationExposers.Name.Equal(name)).ToList();
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

		public IEnumerable<ModelRegistration> GetModelsBySolution(SdmObjectReference<SolutionRegistration> solutionReference)
		{
			if (solutionReference == default)
			{
				throw new ArgumentNullException(nameof(solutionReference), "Solution Reference cannot be null.");
			}

			return Read(ModelRegistrationExposers.Solution.Equal(solutionReference));
		}
	}
}
