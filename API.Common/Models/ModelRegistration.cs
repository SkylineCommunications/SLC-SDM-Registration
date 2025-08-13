// Ignore Spelling: SDM Api

namespace Skyline.DataMiner.SDM.Registration
{
	[DataMinerObject]
	[SdmDomStorage(Constants.ModuleId)]
	public class ModelRegistration : SdmObject<ModelRegistration>
	{
		public string Name { get; set; }

		public string DisplayName { get; set; }

		public string Version { get; set; }

		public string ApiScriptName { get; set; }

		public string ApiEndpoint { get; set; }

		public string VisualizationEndpoint { get; set; }

		public SdmObjectReference<SolutionRegistration> Solution { get; internal set; }
	}
}
