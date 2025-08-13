// Ignore Spelling: SDM Api Uninstall

namespace Skyline.DataMiner.SDM.Registration
{
	using System.Collections.Generic;

	[DataMinerObject]
	[SdmDomStorage(Constants.ModuleId)]
	public class SolutionRegistration : SdmObject<SolutionRegistration>
	{
		public string ID { get; set; }

		public string DisplayName { get; set; }

		public string Version { get; set; }

		public string DefaultApiScriptName { get; set; }

		public string DefaultApiEndpoint { get; set; }

		public string VisualizationEndpoint { get; set; }

		public string UninstallScript { get; set; }

		public IReadOnlyCollection<SdmObjectReference<ModelRegistration>> Models { get; internal set; } = new List<SdmObjectReference<ModelRegistration>>();
	}
}
