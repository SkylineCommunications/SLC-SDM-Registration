namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using System;
	using System.Collections.Generic;

	using Shared;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.SDM.Registration.Install.DOM.SolutionDefinition;

	internal class SolutionInstaller : BaseMigrator
	{
		public SolutionInstaller(IConnection connection, Action<string> logMethod = null)
			: base(
				  connection,
				  ModelRegistrationDomMapper.ModuleId,
				  new Dictionary<SdmVersion, DomMigration>
				  {
					  [new SdmVersion(1, 0, 1)] = new SolutionRegistration_V1_0_1(connection, logMethod),
					  [new SdmVersion(1, 0, 2)] = new SolutionRegistration_V1_0_2(connection, logMethod),
				  },
				  logMethod)
		{
		}
	}
}
