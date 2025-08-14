// Ignore Spelling: SDM Api Uninstall

namespace Skyline.DataMiner.SDM.Registration
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents the registration details of a solution.
	/// </summary>
	[DataMinerObject]
	[SdmDomStorage(Constants.ModuleId)]
	public class SolutionRegistration : SdmObject<SolutionRegistration>
	{
		/// <summary>
		/// Gets or sets the unique identifier of the solution.
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the display name of the solution.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the version of the solution.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Gets or sets the name of the default CRUD script associated with the solution.
		/// </summary>
		public string DefaultApiScriptName { get; set; }

		/// <summary>
		/// Gets or sets the default CRUD API endpoint for the solution.
		/// </summary>
		public string DefaultApiEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the solution's visualization.
		/// </summary>
		public string VisualizationEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the name of the script used to uninstall the solution.
		/// </summary>
		public string UninstallScript { get; set; }

		/// <summary>
		/// Gets the collection of model registrations associated with this solution.
		/// </summary>
		public IReadOnlyCollection<SdmObjectReference<ModelRegistration>> Models { get; internal set; } = new List<SdmObjectReference<ModelRegistration>>();
	}
}
