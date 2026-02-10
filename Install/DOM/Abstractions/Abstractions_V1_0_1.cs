namespace Skyline.DataMiner.SDM.Registration.Install.DOM.Registration
{
	using System;

	using Shared;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Utils.DOM.Builders;

	internal class Abstractions_V1_0_1 : DomMigration
	{
		public Abstractions_V1_0_1(IConnection connection, Action<string> logMethod = null)
			: base(connection, ModelRegistrationDomMapper.ModuleId, logMethod)
		{
		}

		public override void Migrate()
		{
			var solutionGuid = Guid.Parse(AbstractionsInstaller.Guid);
			var solutionInstance = new DomInstanceBuilder()
				.WithID(new DomInstanceId(solutionGuid)
				{
					ModuleId = SolutionRegistrationDomMapper.ModuleId,
				})
				.WithDefinition(SolutionRegistrationDomMapper.DomDefinitionId)
				.AddSection(new DomSectionBuilder()
					.WithID(new Net.Sections.SectionID(Guid.NewGuid()))
					.WithSectionDefinitionID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.ID, AbstractionsInstaller.ID)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.DisplayName, AbstractionsInstaller.DisplayName)
					.WithFieldValue(SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version, new SdmVersion(1, 0, 1).ToString()))
				.Build();

			Import(DomInstanceExposers.Id.Equal(solutionGuid), solutionInstance);
			Log("Registered solution for " + AbstractionsInstaller.DisplayName);
		}
	}
}
