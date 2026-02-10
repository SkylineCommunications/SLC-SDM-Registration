namespace Skyline.DataMiner.SDM.Registration.Install.DOM.Registration
{
	using System;
	using System.Collections.Generic;

	using Shared;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Utils.DOM.Builders;

	internal class Registration_V1_0_1 : DomMigration
	{
		public Registration_V1_0_1(IConnection connection, Action<string> logMethod = null)
			: base(connection, ModelRegistrationDomMapper.ModuleId, logMethod)
		{
		}

		public override void Migrate()
		{
			var solutionInstance = new DomInstanceBuilder()
				.WithID(new DomInstanceId(Constants.Solution.Guid)
				{
					ModuleId = SolutionRegistrationDomMapper.ModuleId,
				})
				.WithDefinition(SolutionRegistrationDomMapper.DomDefinitionId)
				.AddSection(new DomSectionBuilder()
					.WithID(new Net.Sections.SectionID(Guid.NewGuid()))
					.WithSectionDefinitionID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.ID, Constants.Solution.ID)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.DisplayName, Constants.Solution.DisplayName)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version, new SdmVersion(1, 0, 1).ToString())
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.Models, new List<Guid>
					{
						Constants.Models.ModelRegistration.Guid,
						Constants.Models.SolutionRegistration.Guid,
					}))
				.Build();

			Import(DomInstanceExposers.Id.Equal(Constants.Solution.Guid), solutionInstance);
			Log("Registered solution for " + Constants.Solution.DisplayName);

			var modelRegistrationInstance = new DomInstanceBuilder()
				.WithID(new DomInstanceId(Constants.Models.ModelRegistration.Guid)
				{
					ModuleId = ModelRegistrationDomMapper.ModuleId,
				})
				.WithDefinition(ModelRegistrationDomMapper.DomDefinitionId)
				.AddSection(new DomSectionBuilder()
					.WithID(new Net.Sections.SectionID(Guid.NewGuid()))
					.WithSectionDefinitionID(ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Name, Constants.Models.ModelRegistration.Name)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.DisplayName, Constants.Models.ModelRegistration.DisplayName)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Version, new SdmVersion(1, 0, 1).ToString())
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Solution, Constants.Solution.Guid))
				.Build();

			Import(DomInstanceExposers.Id.Equal(Constants.Models.ModelRegistration.Guid), modelRegistrationInstance);
			Log("Registered model for " + Constants.Models.ModelRegistration.DisplayName);

			var solutionRegistrationInstance = new DomInstanceBuilder()
				.WithID(new DomInstanceId(Constants.Models.SolutionRegistration.Guid)
				{
					ModuleId = ModelRegistrationDomMapper.ModuleId,
				})
				.WithDefinition(ModelRegistrationDomMapper.DomDefinitionId)
				.AddSection(new DomSectionBuilder()
					.WithID(new Net.Sections.SectionID(Guid.NewGuid()))
					.WithSectionDefinitionID(ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Name, Constants.Models.SolutionRegistration.Name)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.DisplayName, Constants.Models.SolutionRegistration.DisplayName)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Version, new SdmVersion(1, 0, 1).ToString())
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Solution, Constants.Solution.Guid))
				.Build();

			Import(DomInstanceExposers.Id.Equal(Constants.Models.SolutionRegistration.Guid), solutionRegistrationInstance);
			Log("Registered model for " + Constants.Models.SolutionRegistration.DisplayName);
		}
	}
}
