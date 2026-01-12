namespace Skyline.DataMiner.SDM.Registration.Install.DOM.ModelDefinition
{
	using System;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Sections;
	using Skyline.DataMiner.Utils.DOM.Builders;

	internal class ModelRegistration_V1_0_2 : DomMigration
	{
		public ModelRegistration_V1_0_2(IConnection connection, Action<string> logMethod = null) : base(connection, ModelRegistrationDomMapper.ModuleId, logMethod)
		{
		}

		public override void Migrate()
		{
			var equality = SectionDefinitionExposers.ID.Equal(ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId);
			var existingSectionDefinition = Get(equality);

			var updatedSectionDefinition = new SectionDefinitionBuilder(existingSectionDefinition)
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationCreateEndpoint)
					.WithName("Create Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the model to create a visualization using the UI."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationUpdateEndpoint)
					.WithName("Update Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the model to update a visualization using the UI."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationDeleteEndpoint)
					.WithName("Delete Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the model to delete a visualization using the UI."))
				.Build();

			Import(equality, updatedSectionDefinition);
		}
	}
}
