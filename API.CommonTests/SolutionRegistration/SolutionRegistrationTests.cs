// Ignore Spelling: SDM Api

namespace Skyline.DataMiner.SDM.Registration.Tests
{
	using FluentAssertions;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM.Registration.Exceptions;
	using Skyline.DataMiner.SDM.Registration.Install.DOM;
	using Skyline.DataMiner.Utils.DOM.Builders;
	using Skyline.DataMiner.Utils.DOM.UnitTesting;

	[TestClass]
	public class SolutionRegistrationTests
	{
		private DomConnectionMock _connection;

		[TestInitialize]
		public void Setup()
		{
			var messageHandler = new DomSLNetMessageHandler();
			messageHandler.SetInstances(
				Constants.ModuleId,
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
							ModelRegistrationDomMapper.ModelRegistrationProperties.Version,
							"1.0.1")
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
						.Build(),

					// Service Management Solution
					new DomInstanceBuilder()
						.WithID(new DomInstanceId(Guid.NewGuid())
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
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version,
								"1.0.1")
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
						.WithID(new DomInstanceId(Guid.NewGuid())
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
								SolutionRegistrationDomMapper.SolutionRegistrationProperties.Version,
								"1.0.1")
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
			var registrar = new SdmRegistrar(_connection);

			var solution1 = new SolutionRegistration
			{
				ID = "media_ops_plan",
				Version = "1.0.1",
			};

			var solutionAct = () => registrar.Solutions.Create(solution1);

			solutionAct.Should().Throw<ValidationException>();
			solution1.DisplayName = "Media Ops.Plan";
			solutionAct.Should().NotThrow();

			registrar.Solutions.Read(new TRUEFilterElement<SolutionRegistration>()).Should().Contain(solution1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_ID()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Solutions.Count(SolutionRegistrationExposers.ID.Contains("network"));

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
			var act = () => result = registrar.Solutions.Count(SolutionRegistrationExposers.DisplayName.Contains("Service"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_DefaultApiScriptName()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Solutions.Count(SolutionRegistrationExposers.DefaultApiScriptName.Equal("ServiceManagement.CRUD"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_DefaultApiEndpoint()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Solutions.Count(SolutionRegistrationExposers.DefaultApiEndpoint.Contains("service_management"));

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
			var act = () => result = registrar.Solutions.Count(SolutionRegistrationExposers.VisualizationEndpoint.Equal("https://localhost/app/e3210645-842d-49e5-bb4d-905e69a67cf3"));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void SdmRegistrar_Count_Models()
		{
			// Arrange
			var registrar = new SdmRegistrar(_connection);

			// Act
			long result = -1;
			var act = () => result = registrar.Solutions.Count(SolutionRegistrationExposers.Models.Contains(
				new SdmObjectReference<ModelRegistration>("9b561e12-9da5-4c49-a553-be4c6f5bbc0e")));

			// Assert
			act.Should().NotThrow();
			result.Should().Be(1);
		}

		[TestMethod]
		public void JsonTest()
		{
			// Arrange
			var jsonString = @"{
  ""ID"": ""string"",
  ""DisplayName"": ""string"",
  ""Version"": ""string"",
  ""DefaultApiScriptName"": ""string"",
  ""DefaultApiEndpoint"": ""string"",
  ""VisualizationEndpoint"": ""string"",
  ""VisualizationCreateEndpoint"": ""string"",
  ""VisualizationUpdateEndpoint"": ""string"",
  ""VisualizationDeleteEndpoint"": ""string"",
  ""UninstallScript"": ""string"",
  ""Models"": [
    ""123e4567-e89b-12d3-a456-426614174000""
  ],
  ""Identifier"": ""string""
}";

			// Act
			var result = default(SolutionRegistration);
			var act = () => result = Newtonsoft.Json.JsonConvert.DeserializeObject<SolutionRegistration>(jsonString);

			// Assert
			act.Should().NotThrow();
			result.Should().NotBeNull();
			result.ID.Should().Be("string");
			result.DisplayName.Should().Be("string");
			result.Version.Should().Be("string");
			result.DefaultApiScriptName.Should().Be("string");
			result.DefaultApiEndpoint.Should().Be("string");
			result.VisualizationEndpoint.Should().Be("string");
			result.VisualizationCreateEndpoint.Should().Be("string");
			result.VisualizationUpdateEndpoint.Should().Be("string");
			result.VisualizationDeleteEndpoint.Should().Be("string");
			result.UninstallScript.Should().Be("string");
			result.Models.Should().HaveCount(1);
			result.Models.Should().ContainSingle(m => m.Identifier == "123e4567-e89b-12d3-a456-426614174000");
			result.Identifier.Should().Be("string");
		}
	}
}