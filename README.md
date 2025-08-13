# SDM Registration

When working with multiple DataMiner solutions across different environments, it’s important to keep track of:
- The installed solution version
- The data types included in that installation

This API serves as a central registry for that information.
It comes with:
- A C# library for registering your DataMiner solution and its data types during installation
- A Low-Code app for browsing and reviewing all registered solutions and their data types

## How to use

The entry point for the library is always the **SdmRegistrar**, you can get it by calling the *GetSdmRegistrar()* method on your DataMiner connection.

```csharp


[AutomationEntryPoint(AutomationEntryPointType.Types.InstallAppPackage)]
public void Install(IEngine engine, AppInstallContext context)
{
	try
	{
		engine.Timeout = new TimeSpan(0, 10, 0);
		engine.GenerateInformation("Starting installation");
		var installer = new AppInstaller(Engine.SLNetRaw, context);
		installer.InstallDefaultContent();

		var registrar = engine.GetSdmRegistrar()
		var solution = new SolutionRegistration
		{
			ID = "my_solution",
			DisplayName = "My Solution",
			DefaultApiEndpoint = "/api/custom/my_solution",
			DefaultApiScriptName = "MySolution.CRUD",
			VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3",
			Version = "1.0.1",
		};

		registrar.RegisterSolution(solution);

		var myModel1 = new ModelRegistration
		{
			Name = "model1",
			DisplayName = "Model 1",
			Version = "1.0.1",
			ApiScriptName = "MySolution.Model1.CRUD",
			ApiEndpoint = "/api/custom/my_solution/model1",
			VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3/MyModel%20Overview",
			Solution = solution,
		};
		registrar.RegisterModels(myModel1);
	}
	catch (Exception e)
	{
		engine.ExitFail($"Exception encountered during installation: {e}");
	}
}
```

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exists. In addition, you can leverage DataMiner Development Packages to build your own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

### About Skyline Communications

At Skyline Communications, we deal with world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.

<!-- Uncomment below and add more info to provide more information about how to use this package. -->
<!-- ## Getting Started -->
