namespace Skyline.DataMiner.SDM.Registration.Install.DOM.SolutionDefinition
{
	using System;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Net.Sections;
	using Skyline.DataMiner.Utils.DOM.Builders;

	internal class SolutionRegistration_V1_0_2 : DomMigration
	{
		public SolutionRegistration_V1_0_2(IConnection connection, Action<string> logMethod = null)
			: base(connection, ModelRegistrationDomMapper.ModuleId, logMethod)
		{
		}

		public override void Migrate()
		{
			var equality = SectionDefinitionExposers.ID.Equal(SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId);
			var existingSectionDefinition = Get(equality);

			var updatedSectionDefinition = new SectionDefinitionBuilder(existingSectionDefinition)
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationCreateEndpoint)
					.WithName("Create Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the solution to create a new visualization using the UI."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationUpdateEndpoint)
					.WithName("Update Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the solution to update a visualization using the UI."))
				.AddFieldDescriptor(new FieldDescriptorBuilder()
					.WithID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationDeleteEndpoint)
					.WithName("Delete Visualization Endpoint")
					.WithType(typeof(string))
					.WithIsOptional(true)
					.WithTooltip("The endpoint to interact with the solution to delete a visualization using the UI."))
				.Build();

			Import(equality, updatedSectionDefinition);
		}
	}
}
