namespace Skyline.DataMiner.SDM.Registration.Install.DOM.Registration
{
	using System;
	using System.Collections.Generic;

	using Shared;

	using Skyline.DataMiner.Net;

	internal class RegistrationInstaller : BaseMigrator
	{
		public RegistrationInstaller(IConnection connection, Action<string> logMethod = null)
			: base(
				  connection,
				  ModelRegistrationDomMapper.ModuleId,
				  new Dictionary<SdmVersion, DomMigration>
				  {
					  [new SdmVersion(1, 0, 1)] = new Registration_V1_0_1(connection, logMethod),
					  [new SdmVersion(1, 0, 2)] = new Registration_V1_0_2(connection, logMethod),
					  [new SdmVersion(1, 0, 3)] = new Registration_V1_0_3(connection, logMethod),
					  [new SdmVersion(1, 1, 1)] = new Registration_V1_1_1(connection, logMethod),
				  },
				  logMethod)
		{
		}
	}
}
