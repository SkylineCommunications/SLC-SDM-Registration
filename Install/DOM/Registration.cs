// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Utils.DOM.Builders;

	public partial class DomInstaller
	{
		private void RegisterSolution(DomHelper helper)
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
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version, Constants.Solution.Version)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiScriptName, Constants.Solution.DefaultApiScriptName)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiEndpoint, Constants.Solution.DefaultApiEndpoint)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationEndpoint, Constants.Solution.VisualizationEndpoint)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.UninstallScript, Constants.Solution.UninstallScript)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.Models, new List<Guid>
					{
						Constants.Models.ModelRegistration.Guid,
						Constants.Models.SolutionRegistration.Guid,
					}))
				.Build();

			Import(helper.DomInstances, DomInstanceExposers.Id.Equal(Constants.Solution.Guid), solutionInstance);
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
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Version, Constants.Models.ModelRegistration.Version)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.ApiScriptName, Constants.Models.ModelRegistration.ApiScriptName)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.ApiEndpoint, Constants.Models.ModelRegistration.ApiEndpoint)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationEndpoint, Constants.Models.ModelRegistration.VisualizationEndpoint)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Solution, Constants.Solution.Guid))
				.Build();

			Import(helper.DomInstances, DomInstanceExposers.Id.Equal(Constants.Models.ModelRegistration.Guid), modelRegistrationInstance);
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
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Version, Constants.Models.SolutionRegistration.Version)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.ApiScriptName, Constants.Models.SolutionRegistration.ApiScriptName)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.ApiEndpoint, Constants.Models.SolutionRegistration.ApiEndpoint)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationEndpoint, Constants.Models.SolutionRegistration.VisualizationEndpoint)
					.WithFieldValue(ModelRegistrationDomMapper.ModelRegistrationProperties.Solution, Constants.Solution.Guid))
				.Build();

			Import(helper.DomInstances, DomInstanceExposers.Id.Equal(Constants.Models.SolutionRegistration.Guid), solutionRegistrationInstance);
			Log("Registered model for " + Constants.Models.SolutionRegistration.DisplayName);
		}
	}
}
