namespace Skyline.DataMiner.SDM.Registration
{
	using System;

	/// <summary>
	/// Represents the registration details of a data model.
	/// </summary>
	/// <remarks>
	/// This class is used to store metadata about models within the SDM (Solution Data Model) framework.
	/// It includes information about the model's identity, versioning, API endpoints, and visualization endpoints.
	/// The class is decorated with attributes for DOM storage and automatic exposer generation.
	/// </remarks>
	[GenerateExposers]
	[SdmDomStorage(Constants.ModuleId)]
	public class ModelRegistration : SdmObject<ModelRegistration>
	{
		/// <summary>
		/// Gets or sets the unique identifier for the model registration.
		/// </summary>
		/// <value>
		/// A string representation of a GUID that uniquely identifies this model registration instance.
		/// Defaults to a new GUID when the object is instantiated.
		/// </value>
		public override string Identifier { get; set; } = Guid.NewGuid().ToString();

		/// <summary>
		/// Gets or sets the unique name of the model.
		/// </summary>
		/// <value>
		/// A string containing the unique identifier name for the model within the SDM framework.
		/// This name should be unique across all registered models.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the display name of the model.
		/// </summary>
		/// <value>
		/// A string containing the human-readable name displayed to users in the UI.
		/// </value>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the version of the model.
		/// </summary>
		/// <value>
		/// A string representing the version number of the model (e.g., "1.0.0").
		/// Used for version tracking and compatibility checks.
		/// </value>
		public string Version { get; set; }

		/// <summary>
		/// Gets or sets the name of the automation script associated with the model.
		/// This script should be able to do CRUD operation on a model.
		/// </summary>
		/// <value>
		/// The name of the automation script that handles Create, Read, Update, and Delete operations for this model.
		/// </value>
		public string ApiScriptName { get; set; }

		/// <summary>
		/// Gets or sets the CRUD API endpoint for the model.
		/// </summary>
		/// <value>
		/// The URL or path to the API endpoint that handles CRUD operations for this model.
		/// </value>
		public string ApiEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the model's visualization.
		/// </summary>
		/// <value>
		/// The URL or path to the endpoint that displays or retrieves the model's visual representation.
		/// </value>
		public string VisualizationEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the creation of the model's visualization.
		/// </summary>
		/// <value>
		/// The URL or path to the endpoint used when creating a new visualization for the model.
		/// </value>
		public string VisualizationCreateEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the update of the model's visualization.
		/// </summary>
		/// <value>
		/// The URL or path to the endpoint used when updating an existing visualization for the model.
		/// </value>
		public string VisualizationUpdateEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the endpoint for the deletion of the model's visualization.
		/// </summary>
		/// <value>
		/// The URL or path to the endpoint used when deleting the model's visualization.
		/// </value>
		public string VisualizationDeleteEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the reference to the solution registration this model belongs to.
		/// </summary>
		/// <value>
		/// An <see cref="SdmObjectReference{T}"/> that links this model to its parent <see cref="SolutionRegistration"/>.
		/// This establishes the hierarchical relationship between models and solutions.
		/// </value>
		public SdmObjectReference<SolutionRegistration> Solution { get; set; }
	}
}
