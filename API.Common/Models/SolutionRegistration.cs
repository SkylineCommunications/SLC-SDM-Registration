namespace Skyline.DataMiner.SDM.Registration
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Represents the registration details of a solution.
	/// </summary>
	/// <remarks>
	/// This class stores comprehensive information about a solution's registration including
	/// its identity, versioning, API endpoints, visualization endpoints, and associated models.
	/// It serves as the central registration point for solutions within the SDM framework.
	/// </remarks>
	[GenerateExposers]
	[SdmDomStorage(Constants.ModuleId)]
	public class SolutionRegistration : SdmObject<SolutionRegistration>
	{
		/// <summary>
		/// Gets or sets the unique identifier for this solution registration instance.
		/// </summary>
		/// <value>
		/// A GUID-based string that uniquely identifies this registration instance.
		/// Automatically initialized with a new GUID.
		/// </value>
		public override string Identifier { get; set; } = Guid.NewGuid().ToString();

		/// <summary>
		/// Gets or sets the unique identifier of the solution.
		/// </summary>
		/// <value>
		/// The solution's unique identifier used for internal reference and tracking.
		/// </value>
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the display name of the solution.
		/// </summary>
		/// <value>
		/// The human-readable name of the solution shown in the user interface.
		/// </value>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the version of the solution.
		/// </summary>
		/// <value>
		/// The version string of the solution (e.g., "1.0.0").
		/// </value>
		public string Version { get; set; }

		/// <summary>
		/// Gets or sets the name of the default CRUD script associated with the solution.
		/// </summary>
		/// <value>
		/// The script name used for Create, Read, Update, and Delete operations.
		/// </value>
		public string DefaultApiScriptName { get; set; }

		/// <summary>
		/// Gets or sets the default CRUD API endpoint for the solution.
		/// </summary>
		/// <value>
		/// The base API endpoint URL for CRUD operations on this solution.
		/// </value>
		public string DefaultApiEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the solution's visualization.
		/// </summary>
		/// <value>
		/// The URL endpoint for accessing the solution's visualization interface.
		/// </value>
		public string VisualizationEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the creation of the solution's visualization.
		/// </summary>
		/// <value>
		/// The URL endpoint specifically for creating new visualization instances.
		/// </value>
		public string VisualizationCreateEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the update of the solution's visualization.
		/// </summary>
		/// <value>
		/// The URL endpoint specifically for updating existing visualization instances.
		/// </value>
		public string VisualizationUpdateEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the deletion of the solution's visualization.
		/// </summary>
		/// <value>
		/// The URL endpoint specifically for deleting visualization instances.
		/// </value>
		public string VisualizationDeleteEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the name of the script used to uninstall the solution.
		/// </summary>
		/// <value>
		/// The script name that handles the uninstallation process for this solution.
		/// </value>
		public string UninstallScript { get; set; }

		/// <summary>
		/// Gets the collection of model registrations associated with this solution.
		/// </summary>
		/// <value>
		/// A read-only collection of references to <see cref="ModelRegistration"/> objects
		/// that are registered as part of this solution. This collection is initialized
		/// as an empty list and populated internally.
		/// </value>
		public IReadOnlyCollection<SdmObjectReference<ModelRegistration>> Models { get; internal set; } = new List<SdmObjectReference<ModelRegistration>>();
	}
}
