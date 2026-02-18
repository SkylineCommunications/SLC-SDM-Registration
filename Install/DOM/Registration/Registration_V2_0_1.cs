namespace Skyline.DataMiner.SDM.Registration.Install.DOM.Registration
{
	using System;

	using Shared;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Utils.DOM.Builders;

	internal class Registration_V2_0_1 : DomMigration
	{
		public Registration_V2_0_1(IConnection connection, Action<string> logMethod = null)
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
					SolutionRegistrationDomMapper.SolutionRegistrationProperties.ID,
					"52173e49-9185-4772-9b60-c186ee365a81")
				.WithFieldValue(
					SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId,
					SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version,
					new SdmVersion(2, 0, 1).ToString())
				.Build();

			Import(solutionEquality, updatedSolutionInstance);
		}
	}
}
