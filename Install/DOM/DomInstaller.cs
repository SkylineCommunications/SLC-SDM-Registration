namespace Skyline.DataMiner.SDM.Registration.Install.DOM
{
	using System;
	using System.Linq;

	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Apps.Modules;
	using Skyline.DataMiner.Net.ManagerStore;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Net.Sections;
	using Skyline.DataMiner.Utils.DOM.Builders;
	using Skyline.DataMiner.Utils.DOM.Extensions;

	public partial class DomInstaller
	{
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
			var moduleSettings = moduleHelper.ModuleSettings.Read(ModuleSettingsExposers.ModuleId.Equal(SolutionRegistrationDomMapper.ModuleId));
			var moduleExist = moduleSettings.Count > 0;
			if (!moduleExist)
			{
				// If something needs to change here then we need to make sure to handle upgrades as well.
				Log("Installing Module Settings...");
				var module = new DomModuleBuilder()
					.WithModuleId(SolutionRegistrationDomMapper.ModuleId)
					.WithInformationEvents(false)
					.WithHistory(false)
					.Build();

				Import(moduleHelper.ModuleSettings, ModuleSettingsExposers.ModuleId.Equal(SolutionRegistrationDomMapper.ModuleId), module);
				Log("Installed Module Settings");
			}

			var domHelper = new DomHelper(_connection.HandleMessages, SolutionRegistrationDomMapper.ModuleId);
			var solutionVersion = Shared.Version.FromString(
				domHelper.DomInstances
				.Read(DomInstanceExposers.Id.Equal(Constants.Solution.Guid))
				.FirstOrDefault()?
				.GetFieldValue<string>(
					SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId,
					SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version)
				.GetValue() ?? "0.0.0");
			InstallSolution(domHelper, solutionVersion);
			InstallModel(domHelper, solutionVersion);
			RegisterSolution(domHelper);
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

		private static void Import<T>(ICrudHelperComponent<T> crudHelperComponent, FilterElement<T> equalityFilter, T dataType, Func<T, Shared.Version> versionSelector)
			where T : DataType
		{
			var existing = crudHelperComponent.Read(equalityFilter);
			if (existing.Count > 1)
			{
				throw new InvalidOperationException("Multiple instances found when only one was expected.");
			}

			if (existing.Count == 0)
			{
				crudHelperComponent.Create(dataType);
				return;
			}

			var existingVersion = versionSelector(existing[0]);
			var newVersion = versionSelector(dataType);
			if (newVersion > existingVersion)
			{
				crudHelperComponent.Update(dataType);
			}
		}

		private static void Import<T>(ICrudHelperComponent<T> crudHelperComponent, FilterElement<T> equalityFilter, T dataType, Shared.Version existingVersion, Shared.Version newVersion)
			where T : DataType
		{
			var existing = crudHelperComponent.Read(equalityFilter);
			if (existing.Count > 1)
			{
				throw new InvalidOperationException("Multiple instances found when only one was expected.");
			}

			if (existing.Count == 0)
			{
				crudHelperComponent.Create(dataType);
				return;
			}

			if (newVersion > existingVersion)
			{
				crudHelperComponent.Update(dataType);
			}
		}
	}
}
