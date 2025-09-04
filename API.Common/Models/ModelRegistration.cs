// Ignore Spelling: SDM Api

namespace Skyline.DataMiner.SDM.Registration
{
	/// <summary>
	/// Represents the registration details of a data model.
	/// </summary>
	[DataMinerObject]
	[SdmDomStorage(Constants.ModuleId)]
	public class ModelRegistration : SdmObject<ModelRegistration>
	{
		/// <summary>
		/// Gets or sets the unique name of the model.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the display name of the model.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the version of the model.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Gets or sets the name of the automation script associated with the model.
		/// This script should be able to do CRUD operation on a model.
		/// </summary>
		public string ApiScriptName { get; set; }

		/// <summary>
		/// Gets or sets the CRUD API endpoint for the model.
		/// </summary>
		public string ApiEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the model's visualization.
		/// </summary>
		public string VisualizationEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the reference to the solution registration this model belongs to.
		/// </summary>
		public SdmObjectReference<SolutionRegistration> Solution { get; set; }
	}
}
