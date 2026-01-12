namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.SDM.Registration.Install.DOM.ModelDefinition;

	internal class ModelInstaller : BaseMigrator
	{
		public ModelInstaller(IConnection connection, Action<string> logMethod = null)
			: base(
				connection,
				ModelRegistrationDomMapper.ModuleId,
				new Dictionary<Shared.Version, DomMigration>
				{
					[new Shared.Version(1, 0, 1)] = new ModelRegistration_V1_0_1(connection, logMethod),
					[new Shared.Version(1, 0, 2)] = new ModelRegistration_V1_0_2(connection, logMethod),
				},
				logMethod)
		{
		}
	}
}
