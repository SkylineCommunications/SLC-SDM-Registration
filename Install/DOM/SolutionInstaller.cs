// Ignore Spelling: SDM Un

namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel.Concatenation;
	using Skyline.DataMiner.Net.Sections;
	using Skyline.DataMiner.Utils.DOM.Builders;

	public partial class DomInstaller
	{
		private void InstallSolution(DomHelper helper)
		{
			var solutionProperties = new SectionDefinitionBuilder()
				.WithName(nameof(SolutionRegistrationDomMapper.SolutionRegistrationProperties))
				.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId)
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.ID)
					.WithName("ID")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The unique identifier of the solution."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.DisplayName)
					.WithName("Display Name")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The display name of the solution."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version)
					.WithName("Version")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The version of the solution."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiScriptName)
					.WithName("Default API Script Name")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip(""))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiEndpoint)
					.WithName("Default API Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The base endpoint to interact with the solution using the REST API."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationEndpoint)
					.WithName("Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint where a user should navigate to, to interact with the solution using an UI."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationCreateEndpoint)
					.WithName("Create Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to inreract with the solution to create a new visualization using the REST API."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationUpdateEndpoint)
					.WithName("Update Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to inreract with the solution to update a visualization using the REST API."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationDeleteEndpoint)
					.WithName("Delete Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to inreract with the solution to delete a visualization using the REST API."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.UninstallScript)
					.WithName("Un-Install Script")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The script to trigger when un-installing the solution."))
				.AddFieldDescriptor(new DomInstanceFieldDescriptorBuilder()
					.WithModule(ModelRegistrationDomMapper.ModuleId)
					.AddDomDefinition(ModelRegistrationDomMapper.DomDefinitionId)
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.Models)
					.WithName("Models")
					.WithAllowMultiple(true)
					.WithIsOptional(true)
					.WithTooltip("Links to all of the models the solution exposes."))
				.Build();

			Import(helper.SectionDefinitions, SectionDefinitionExposers.ID.Equal(SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId), solutionProperties);
			Log("Installed section definition for Solution Registration");

			var solutionDefinition = new DomDefinitionBuilder()
				.WithID(SolutionRegistrationDomMapper.DomDefinitionId)
				.WithName("Solution Registration")
				.AddSectionDefinitionLink(new Skyline.DataMiner.Net.Apps.Sections.SectionDefinitions.SectionDefinitionLink
				{
					SectionDefinitionID = SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId,
					AllowMultipleSections = false,
					IsOptional = false,
				})
				.Build();
			solutionDefinition.ModuleSettingsOverrides = new ModuleSettingsOverrides
			{
				NameDefinition = new DomInstanceNameDefinition
				{
					ConcatenationItems =
					{
						new FieldValueConcatenationItem(SolutionRegistrationDomMapper.SolutionRegistrationProperties.ID),
					},
				},
			};

			Import(helper.DomDefinitions, DomDefinitionExposers.Id.Equal(SolutionRegistrationDomMapper.DomDefinitionId), solutionDefinition);
			Log("Installed DOM definition for Solution Registration");
		}
	}
}
