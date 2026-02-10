namespace Skyline.DataMiner.SDM.Registration.Install.DOM.Registration
{
	using System;

	using Shared;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Utils.DOM.Builders;

	internal class Registration_V1_0_2 : DomMigration
	{
		public Registration_V1_0_2(IConnection connection, Action<string> logMethod = null)
			: base(connection, ModelRegistrationDomMapper.ModuleId, logMethod)
		{
		}

		public override void Migrate()
		{
			// Register the solution
			var solutionEquality = DomInstanceExposers.Id.Equal(Constants.Solution.Guid);
			var existingSolutionInstance = Get(solutionEquality);
			var updatedSolutionInstance = new DomInstanceBuilder(existingSolutionInstance)
				.WithFieldValue(
					SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId,
					SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version,
					new SdmVersion(1, 0, 2).ToString())
				.Build();

			Import(solutionEquality, updatedSolutionInstance);

			// Register the ModelRegistration model
			var modelRegistrationEquality = DomInstanceExposers.Id.Equal(Constants.Models.ModelRegistration.Guid);
			var existingModelRegistrationInstance = Get(modelRegistrationEquality);
			var updatedModelRegistrationInstance = new DomInstanceBuilder(existingModelRegistrationInstance)
				.WithFieldValue(
					ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
					ModelRegistrationDomMapper.ModelRegistrationProperties.Version,
					new SdmVersion(1, 0, 2).ToString())
				.Build();

			Import(modelRegistrationEquality, updatedModelRegistrationInstance);

			// Register the SolutionRegistration model
			var solutionRegistrationEquality = DomInstanceExposers.Id.Equal(Constants.Models.SolutionRegistration.Guid);
			var existingSolutionRegistrationInstance = Get(solutionRegistrationEquality);
			var updatedSolutionRegistrationInstance = new DomInstanceBuilder(existingSolutionRegistrationInstance)
				.WithFieldValue(
					ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
					ModelRegistrationDomMapper.ModelRegistrationProperties.Version,
					new SdmVersion(1, 0, 2).ToString())
				.Build();

			Import(solutionRegistrationEquality, updatedSolutionRegistrationInstance);
		}
	}
}
