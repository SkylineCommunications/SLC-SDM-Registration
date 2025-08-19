// Ignore Spelling: SDM LCA

namespace Skyline.DataMiner.SDM.Registration.Install.LCA
{
	using System;
	using System.Linq;

	using Skyline.AppInstaller;
	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.AppPackages;

	public class LcaInstaller
	{
		private readonly AppInstaller _installer;
		private readonly Action<string> _logMethod;

		public LcaInstaller(IConnection connection, AppInstallContext context, Action<string> logMethod = null)
		{
			if (context is null)
			{
				throw new ArgumentNullException(nameof(context), "AppInstallContext cannot be null.");
			}

			var lca_context = new AppInstallContext(
				new AppInstaller(connection, context).GetSetupContentDirectory(),
				AppInfo.FromXml(context.AppInfo.ToXml()));

			_installer = new AppInstaller(connection, lca_context);
			_logMethod = logMethod;

			Log(context.AppContentPath);
			LogContext(context);
			Log(lca_context.AppContentPath);
			LogContext(lca_context);
		}

		public void InstallDefaultContent()
		{
			_installer.InstallDefaultContent();
		}

		private void Log(string message)
		{
			_logMethod?.Invoke($"Low Code App Installer: {message}");
		}

		private void LogContext(AppInstallContext context)
		{
			Log($"AppInfo.AppID: {context?.AppInfo?.AppID}");
			Log($"AppInfo.AllowMultipleInstalledVersions: {context?.AppInfo?.AllowMultipleInstalledVersions}");
			Log($"AppInfo.Description: {context?.AppInfo?.Description}");
			Log($"AppInfo.Name: {context?.AppInfo?.Name}");
			Log($"AppInfo.DisplayName: {context?.AppInfo?.DisplayName}");
			Log($"AppInfo.LastModifiedAt: {context?.AppInfo?.LastModifiedAt}");
			Log($"AppInfo.MinDmaVersion: {context?.AppInfo?.MinDmaVersion}");
			Log($"AppInfo.Version: {context?.AppInfo?.Version}");
			if (context?.AppInfo?.Configuration?.Entries is null || !context.AppInfo.Configuration.Entries.Any())
			{
				return;
			}

			for (int i = 0; i < context.AppInfo.Configuration.Entries.Count; i++)
			{
				var entry = context.AppInfo.Configuration.Entries[i];
				Log($"AppInfo.Configuration[0]: {entry.ID} {entry.Name}");
			}
		}
	}
}
