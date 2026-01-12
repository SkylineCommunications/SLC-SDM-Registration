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
		}

		internal static class Models
		{
			internal static class ModelRegistration
			{
				public static readonly Guid Guid = new Guid("fc50578f-697a-4801-a5da-854b7388ad5e");
				public static readonly string Name = "standard_data_model_model_registration";
				public static readonly string DisplayName = "Model Registration";
			}

			internal static class SolutionRegistration
			{
				public static readonly Guid Guid = new Guid("6e831d1f-0902-40b6-915d-8d34f818f7ba");
				public static readonly string Name = "standard_data_model_solution_registration";
				public static readonly string DisplayName = "Solution Registration";
			}
		}
	}
}
