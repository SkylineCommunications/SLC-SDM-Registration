namespace Skyline.DataMiner.SDM.Registration.Install
{
	using System;

	internal class Constants
	{
		internal static class Solution
		{
			public static readonly Guid Guid = new Guid("82ef1873-f90d-4c93-8215-40350ec7373c");

			public static readonly string ID = "standard_data_model_registrations";
			public static readonly string DisplayName = "SDM Registrations";
			public static readonly Shared.Version Version = new Shared.Version(1, 0, 2);
			public static readonly string DefaultApiScriptName = String.Empty;
			public static readonly string DefaultApiEndpoint = String.Empty;
			public static readonly string VisualizationEndpoint = String.Empty; // $"/app/804c0c67-f6c5-414d-b8ec-7e92d7c0303a/Solution%20View?data=%7B%22version%22%3A1%2C%22feed%22%3A%7B%22object%20manager%20instances%22%3A%5B%22%28slc%29standard_data_model%2F{Constants.Solution.Guid}%22%5D%7D%7D";
			public static readonly string UninstallScript = String.Empty;
		}

		internal static class Models
		{
			internal static class ModelRegistration
			{
				public static readonly Guid Guid = new Guid("fc50578f-697a-4801-a5da-854b7388ad5e");

				public static readonly string Name = "standard_data_model_model_registration";
				public static readonly string DisplayName = "Model Registration";
				public static readonly Shared.Version Version = new Shared.Version(1, 0, 2);
				public static readonly string ApiScriptName = String.Empty;
				public static readonly string ApiEndpoint = String.Empty;
				public static readonly string VisualizationEndpoint = String.Empty; // $"/app/804c0c67-f6c5-414d-b8ec-7e92d7c0303a/Model%20View?data=%7B%22version%22%3A1%2C%22feed%22%3A%7B%22object%20manager%20instances%22%3A%5B%22%28slc%29standard_data_model%2F{Constants.Models.ModelRegistration.Guid}%22%5D%7D%7D";
			}

			internal static class SolutionRegistration
			{
				public static readonly Guid Guid = new Guid("6e831d1f-0902-40b6-915d-8d34f818f7ba");

				public static readonly string Name = "standard_data_model_solution_registration";
				public static readonly string DisplayName = "Solution Registration";
				public static readonly Shared.Version Version = new Shared.Version(1, 0, 2);
				public static readonly string ApiScriptName = String.Empty;
				public static readonly string ApiEndpoint = String.Empty;
				public static readonly string VisualizationEndpoint = String.Empty; // $"/app/804c0c67-f6c5-414d-b8ec-7e92d7c0303a/Model%20View?data=%7B%22version%22%3A1%2C%22feed%22%3A%7B%22object%20manager%20instances%22%3A%5B%22%28slc%29standard_data_model%2F{Constants.Models.SolutionRegistration.Guid}%22%5D%7D%7D";
			}
		}
	}
}
