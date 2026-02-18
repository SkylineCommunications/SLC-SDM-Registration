namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using System;
	using System.Linq;

	using Shared;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Apps.Modules;
	using Skyline.DataMiner.Net.ManagerStore;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Net.Sections;
	using Skyline.DataMiner.SDM.Registration.Install.DOM.Module;
	using Skyline.DataMiner.SDM.Registration.Install.DOM.Registration;
	using Skyline.DataMiner.Utils.DOM.Builders;
	using Skyline.DataMiner.Utils.DOM.Extensions;

	public class DomInstaller
	{
		private static readonly SdmVersion[] _versions = new[]
		{
			new SdmVersion(1, 0, 0),
			new SdmVersion(1, 0, 1),
			new SdmVersion(1, 0, 2),
			new SdmVersion(1, 0, 3),
			new SdmVersion(2, 0, 0, null, "rc1"),
			new SdmVersion(2, 0, 0),
			new SdmVersion(2, 0, 1),
		};

		private readonly IConnection _connection;
		private readonly Action<string> _logMethod;

		public DomInstaller(IConnection connection, Action<string> logMethod = null)
		{
			_connection = connection;
			_logMethod = logMethod;
		}

		public void InstallDefaultContent()
		{
			Log("Installation for SDM Registration started...");

			var moduleHelper = new ModuleSettingsHelper(_connection.HandleMessages);
			var moduleComparer = new ModuleSettingsComparer();
			var moduleSettings = moduleHelper.ModuleSettings.Read(ModuleSettingsExposers.ModuleId.Equal(SolutionRegistrationDomMapper.ModuleId)).SingleOrDefault();
			var module = new DomModuleBuilder()
					.WithModuleId(SolutionRegistrationDomMapper.ModuleId)
					.WithInformationEvents(false)
					.WithHistory(true)
					.Build();

			// If the module settings differ import it
			// The comparer is not exhaustive it only checks for the properties we care about
			if (!moduleComparer.Equals(moduleSettings, module))
			{
				Log("Installing Module Settings...");
				Import(moduleHelper.ModuleSettings, ModuleSettingsExposers.ModuleId.Equal(SolutionRegistrationDomMapper.ModuleId), module);
				Log("Installed Module Settings");
			}

			var domHelper = new DomHelper(_connection.HandleMessages, SolutionRegistrationDomMapper.ModuleId);
			var currentVersion = new SdmVersion(0, 0, 0);
			if (SdmVersion.TryParse(
					domHelper.DomInstances
					.Read(DomInstanceExposers.Id.Equal(Constants.Solution.Guid))
					.FirstOrDefault()?
					.GetFieldValue<string>(
						SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId,
						SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version)
					.GetValue(), out var v))
			{
				currentVersion = v;
			}

			var solutionInstaller = new SolutionInstaller(_connection, _logMethod);
			var modelInstaller = new ModelInstaller(_connection, _logMethod);
			var registrationInstaller = new RegistrationInstaller(_connection, _logMethod);
			foreach (var version in _versions)
			{
				if (currentVersion >= version)
				{
					continue;
				}

				solutionInstaller.RunMigration(version);
				modelInstaller.RunMigration(version);
				registrationInstaller.RunMigration(version);

				currentVersion = version;
			}

			// Check the version of the abstractions devpack solution, if it's not present it will be installed, if it's an older version it will be updated
			var currentAbstractionsVersion = new SdmVersion(0, 0, 0);
			if (SdmVersion.TryParse(
					domHelper.DomInstances
					.Read(DomInstanceExposers.Id.Equal(Guid.Parse(AbstractionsInstaller.Guid)))
					.FirstOrDefault()?
					.GetFieldValue<string>(
						SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId,
						SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version)
					.GetValue(), out var abstractionVersion))
			{
				currentAbstractionsVersion = abstractionVersion;
			}

			// Register the abstractions devpack solution
			var abstractionInstallers = new AbstractionsInstaller(_connection, _logMethod);
			foreach (var version in abstractionInstallers.Versions)
			{
				// Currently there is only 1 version
				////if (currentAbstractionsVersion >= version)
				////{
				////	continue;
				////}

				abstractionInstallers.RunMigration(version);

				currentAbstractionsVersion = version;
			}
		}

		internal void Log(string message)
		{
			_logMethod?.Invoke($"SDM.Registration.Installer: {message}");
		}

		private static void Import<T>(ICrudHelperComponent<T> crudHelperComponent, FilterElement<T> equalityFilter, T dataType)
			where T : DataType
		{
			bool exists = crudHelperComponent.Read(equalityFilter).Any();

			if (exists)
			{
				crudHelperComponent.Update(dataType);
			}
			else
			{
				crudHelperComponent.Create(dataType);
			}
		}
	}
}
