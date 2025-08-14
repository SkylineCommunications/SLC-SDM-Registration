// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Registration.Tests
{
	using FluentAssertions;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM.Linq;
	using Skyline.DataMiner.SDM.Registration.Install.DOM;
	using Skyline.DataMiner.Utils.DOM.Builders;
	using Skyline.DataMiner.Utils.DOM.UnitTesting;

	[TestClass]
	public class ModelRegistrationTests
	{
		private DomConnectionMock _connection;

		[TestInitialize]
		public void Setup()
		{
			var messageHandler = new DomSLNetMessageHandler();
			messageHandler.SetInstances(
				ModelRegistrationDomMapper.ModuleId,
				[

					// Service Model
					new DomInstanceBuilder()
						.WithID(new DomInstanceId(new Guid("9b561e12-9da5-4c49-a553-be4c6f5bbc0e"))
						{
							ModuleId = SolutionRegistrationDomMapper.ModuleId,
						})
						.WithDefinition(ModelRegistrationDomMapper.DomDefinitionId)
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Name,
							"service")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.DisplayName,
							"Service")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Version,
							"1.0.1")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.ApiScriptName,
							"ServiceManagement.CRUD.service")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.ApiEndpoint,
							"https://localhost/service_management/api/service")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationEndpoint,
							"https://localhost/app/e3210645-842d-49e5-bb4d-905e69a67cf3/service")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Solution,
							new Guid("962208d1-15ad-4373-9262-84857fb2a4f8")) // Service Management Solution
						.Build(),

					new DomInstanceBuilder()
						.WithID(new DomInstanceId(new Guid("be58bf7e-cbce-415f-84de-520f184dbae3"))
						{
							ModuleId = SolutionRegistrationDomMapper.ModuleId,
						})
						.WithDefinition(ModelRegistrationDomMapper.DomDefinitionId)
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Name,
							"rack")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.DisplayName,
							"Rack")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Version,
							"2.0.5")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.ApiScriptName,
							"NetworkManagement.CRUD.service")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.ApiEndpoint,
							"https://localhost/network_management/api/rack")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationEndpoint,
							"https://localhost/app/7528ed0c-3d00-45a1-9e34-98a08910070c/rack")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Solution,
							new Guid("962208d1-15ad-4373-9262-84857fb2a4f8")) // Service Management Solution
						.Build(),

					// Network Model
					new DomInstanceBuilder()
						.WithID(new DomInstanceId(new Guid("d5e726a1-08af-4479-96ac-59a194fd7993"))
						{
							ModuleId = SolutionRegistrationDomMapper.ModuleId,
						})
						.WithDefinition(ModelRegistrationDomMapper.DomDefinitionId)
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Name,
							"network")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.DisplayName,
							"Network")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.ApiScriptName,
							"NetworkManagement.CRUD.network")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.ApiEndpoint,
							"https://localhost/network_management/api/network")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.VisualizationEndpoint,
							"https://localhost/app/7528ed0c-3d00-45a1-9e34-98a08910070c/network")
						.WithFieldValue(
							ModelRegistrationDomMapper.ModelRegistrationProperties.SectionDefinitionId,
							ModelRegistrationDomMapper.ModelRegistrationProperties.Solution,
							new Guid("ccb76df3-4085-446e-9f5a-732a05a94731"))
						.Build(),

					// Service Management Solution
					new DomInstanceBuilder()
						.WithID(new DomInstanceId(new Guid("962208d1-15ad-4373-9262-84857fb2a4f8"))
						{
							ModuleId = SolutionRegistrationDomMapper.ModuleId,
						})
						.WithDefinition(SolutionRegistrationDomMapper.DomDefinitionId)
						.AddSection(new DomSectionBuilder()
							.WithID(new Net.Sections.SectionID(Guid.NewGuid()))
							.WithSectionDefinitionID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId)
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.ID,
								"service_management")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.DisplayName,
								"Service Management")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiScriptName,
								"ServiceManagement.CRUD")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiEndpoint,
								"https://localhost/service_management/api")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationEndpoint,
								"https://localhost/app/e3210645-842d-49e5-bb4d-905e69a67cf3")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.Models,
								new List<Guid>
								{
									new Guid("9b561e12-9da5-4c49-a553-be4c6f5bbc0e"),
								}))
						.Build(),

					// Network Management Solution
					new DomInstanceBuilder()
						.WithID(new DomInstanceId(new Guid("ccb76df3-4085-446e-9f5a-732a05a94731"))
						{
							ModuleId = SolutionRegistrationDomMapper.ModuleId,
						})
						.WithDefinition(SolutionRegistrationDomMapper.DomDefinitionId)
						.AddSection(new DomSectionBuilder()
							.WithID(new Net.Sections.SectionID(Guid.NewGuid()))
							.WithSectionDefinitionID(SolutionRegistrationDomMapper.SolutionRegistrationProperties.SectionDefinitionId)
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.ID,
								"network_management")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.DisplayName,
								"Network Management")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiScriptName,
								"NetworkManagement.CRUD")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.DefaultApiEndpoint,
								"https://localhost/network_management/api")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.VisualizationEndpoint,
								"https://localhost/app/7528ed0c-3d00-45a1-9e34-98a08910070c")
							.WithFieldValue(
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.Models,
								new List<Guid>
								{
									new Guid("d5e726a1-08af-4479-96ac-59a194fd7993"),
								}))
						.Build(),
				]);
			_connection = new DomConnectionMock(messageHandler);
			new DomInstaller(_connection).InstallDefaultContent();
		}

		[TestMethod]
		public void SdmRegistrar_Creation()
		{
			var act = () => new SdmRegistrar(_connection);

			act.Should().NotThrow();
		}

		[TestMethod]
		public void SdmRegistrar_Create()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);
			var solution = new SolutionRegistration
			{
				ID = "service_management",
				DisplayName = "Service Management",
				DefaultApiEndpoint = "https://localhost/service_management/api",
				DefaultApiScriptName = "ServiceManagement.CRUD",
				VisualizationEndpoint = "https://localhost/app/e3210645-842d-49e5-bb4d-905e69a67cf3",
				Version = "1.0.1",
			};
			registrar.RegisterSolution(solution);

			// Act
			var models1 = new List<ModelRegistration>
			{
				new ModelRegistration
				{
					Name = "service",
					DisplayName = "Service",
					Solution = solution,
				},
				new ModelRegistration
				{
					Name = "rack",
					DisplayName = "Rack",
					Solution = solution,
				},
			};
			var modelAct = () => registrar.Models.CreateOrUpdate(models1);

			// Assert
			modelAct.Should().NotThrow();

			// Act
			solution = registrar.GetSolutionByGuid(solution.Guid);

			// Assert
			solution.Models.Should().HaveCount(2);
		}

		[TestMethod]
		public void SdmRegistrar_Count_Name()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Models.Count(ModelRegistrationExposers.Name.Equal("rack"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_DisplayName()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Models.Count(ModelRegistrationExposers.DisplayName.Equal("Rack"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_ApiScriptName()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Models.Count(ModelRegistrationExposers.ApiScriptName.Contains("Network"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(2);
		}

		[TestMethod]
		public void SdmRegistrar_Count_ApiEndpoint()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Models.Count(ModelRegistrationExposers.ApiEndpoint.Contains("service"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_VisualizationEndpoint()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Models.Count(ModelRegistrationExposers.VisualizationEndpoint.Contains("service"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_CustomFilter()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);
			var solution = registrar.Solutions.AsQuery().First();
			var allModels = registrar.Models.Read(new TRUEFilterElement<ModelRegistration>()).ToList();
			var filter = new ANDFilterElement<ModelRegistration>(
				ModelRegistrationExposers.Solution.Equal(solution.Reference.Guid),
				ModelRegistrationExposers.Version.Contains("1.0"));

			// Act
			long result = -1;
			var expected = allModels.Count(m => m.Solution.Guid == solution.Guid && m.Version.Contains("1.0"));
			var act = () => result = registrar.Models.Count(filter);

			// Assert
			act.Should().NotThrow();
			result.Should().Be(expected);
		}
	}
}