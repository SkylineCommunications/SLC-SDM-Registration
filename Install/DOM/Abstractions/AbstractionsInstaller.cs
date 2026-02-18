namespace Skyline.DataMiner.SDM.Registration.Install.DOM.Registration
{
	using System;
	using System.Collections.Generic;

	using Shared;

	using Skyline.DataMiner.Net;

	internal class AbstractionsInstaller : BaseMigrator
	{
		internal const string Guid = "e41ec7db-088f-4b2a-9ac1-b1c6694ab33b";
		internal const string ID = "52173e49-9185-4772-9b60-c186ee365a81.Abstraction";
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
