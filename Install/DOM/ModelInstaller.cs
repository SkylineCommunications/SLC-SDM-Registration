namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using System;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel.Concatenation;
	using Skyline.DataMiner.Net.Sections;
	using Skyline.DataMiner.Utils.DOM.Builders;

	public partial class DomInstaller
	{
		private void InstallModel(DomHelper helper, Shared.Version existingVersion)
		{
			var modelProperties = new SectionDefinitionBuilder()
				.WithName(nameof(ModelRegistrationDomMapper.ModelRegistrationProperties))
				.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId)
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.Name)
					.WithName("ID")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The unique name for them model."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.DisplayName)
					.WithName("Display Name")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The display name of the model."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.Version)
					.WithName("Version")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The version of the model."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.ApiScriptName)
					.WithName("API Script Name")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip(String.Empty))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.ApiEndpoint)
					.WithName("API Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The base endpoint to interact with the model using the REST API."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationEndpoint)
					.WithName("Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint where a user should navigate to, to interact with the model using an UI."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationCreateEndpoint)
					.WithName("Create Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the model to create a visualization using the REST API."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationUpdateEndpoint)
					.WithName("Update Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the model to update a visualization using the REST API."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationDeleteEndpoint)
					.WithName("Delete Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the model to delete a visualization using the REST API."))
				.AddFieldDescriptor(new DomInstanceFieldDescriptorBuilder()
					.WithModule(SolutionRegistrationDomMapper.ModuleId)
					.AddDomDefinition(SolutionRegistrationDomMapper.DomDefinitionId)
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.Solution)
					.WithName("Solution")
					.WithIsOptional(true)
					.WithAllowMultiple(false)
					.WithTooltip("The solution this model belongs to."))
				.Build();

			Import(
				helper.SectionDefinitions,
				SectionDefinitionExposers.ID.Equal(ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId),
				modelProperties,
				existingVersion,
				Constants.Solution.Version);
			Log("Installed section definition for Model Registration");

			var modelDefinition = new DomDefinitionBuilder()
				.WithID(ModelRegistrationDomMapper.DomDefinitionId)
				.WithName("Model Registration")
				.AddSectionDefinitionLink(new Skyline.DataMiner.Net.Apps.Sections.SectionDefinitions.SectionDefinitionLink
				{
					SectionDefinitionID = ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
					AllowMultipleSections = false,
					IsOptional = false,
				})
				.Build();
			modelDefinition.ModuleSettingsOverrides = new ModuleSettingsOverrides
			{
				NameDefinition = new DomInstanceNameDefinition
				{
					ConcatenationItems =
					{
						new FieldValueConcatenationItem(ModelRegistrationDomMapper.ModelRegistrationProperties.Name),
					},
				},
			};

			Import(
				helper.DomDefinitions,
				DomDefinitionExposers.Id.Equal(ModelRegistrationDomMapper.DomDefinitionId),
				modelDefinition,
				existingVersion,
				Constants.Solution.Version);
			Log("Installed DOM definition for Model Registration");
		}
	}
}
