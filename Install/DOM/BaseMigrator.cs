namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using System;
	using System.Collections.Generic;

	using Shared;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	internal abstract class BaseMigrator
	{
		private readonly DomHelper _helper;
		private readonly Action<string> _logMethod;
		private readonly IDictionary<SdmVersion, DomMigration> _migrations;

		protected BaseMigrator(
			IConnection connection,
			string moduleId,
			IDictionary<SdmVersion, DomMigration> migrations = null,
			Action<string> logMethod = null)
		{
			_helper = new DomHelper(connection.HandleMessages, moduleId);
			_logMethod = logMethod;
			_migrations = migrations ?? new Dictionary<SdmVersion, DomMigration>();
		}

		public DomHelper Helper { get => _helper; }

		public IReadOnlyList<SdmVersion> Versions { get => _migrations.Keys as IReadOnlyList<SdmVersion> ?? new List<SdmVersion>(_migrations.Keys); }

		public void RunMigration(SdmVersion version)
		{
			if (!_migrations.ContainsKey(version))
			{
				return;
			}

			var migration = _migrations[version];
			migration.Migrate();
		}

		////protected SdmVersion GetCurrentVersion()
		////{
		////	var currentVersion = SdmVersion.FromString(
		////		Helper.DomInstances
		////		.Read(DomInstanceExposers.Id.Equal(Constants.Solution.Guid))
		////		.FirstOrDefault()?
		////		.GetFieldValue<string>(
		////			SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId,
		////			SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version)
		////		.GetValue() ?? "0.0.0");

		////	return currentVersion;
		////}

		protected void Log(string message)
		{
			_logMethod?.Invoke($"SDM.Registration.Installer: {message}");
		}
	}
}
