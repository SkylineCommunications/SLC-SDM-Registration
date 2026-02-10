namespace Skyline.DataMiner.SDM.Registration.Install.DOM.Registration
{
	using System;
	using System.Collections.Generic;

	using Shared;

	using Skyline.DataMiner.Net;

	internal class AbstractionsInstaller : BaseMigrator
	{
		internal const string Guid = "e41ec7db-088f-4b2a-9ac1-b1c6694ab33b";
		internal const string ID = "standard_data_model_abstractions";
		internal const string DisplayName = "SDM Abstractions";

		public AbstractionsInstaller(IConnection connection, Action<string> logMethod = null)
			: base(
				  connection,
				  ModelRegistrationDomMapper.ModuleId,
				  new Dictionary<SdmVersion, DomMigration>
				  {
					  [new SdmVersion(1, 0, 1)] = new Abstractions_V1_0_1(connection, logMethod),
				  },
				  logMethod)
		{
		}
	}
}
