# Skyline.DataMiner.SDM.Registration.Automation

Extension methods for DataMiner Automation scripts to easily access the SDM Registration API. This package provides seamless integration between Automation scripts and the solution/model registration system.

## Features

- 🔍 **Solution Registration** - Register and track solution installations with version information
- 📊 **Model Management** - Register data models with API endpoints and visualization links
- ✅ **Validation** - Built-in validation middleware to ensure data integrity
- 🔄 **Synchronization** - Automatic synchronization between models and solutions
- 💾 **DOM Integration** - Seamless integration with DataMiner Object Model (DOM) storage
- 🎯 **Type-Safe Queries** - Strongly-typed repository pattern for efficient data access

## Installation

Install via NuGet Package Manager:

```powershell
Install-Package Skyline.DataMiner.SDM.Registration.Automation
```

Or via .NET CLI:

```bash
dotnet add package Skyline.DataMiner.SDM.Registration.Automation
```

## Quick Start

### Using in an Automation Script

```csharp
using Skyline.DataMiner.Automation;
using Skyline.DataMiner.SDM.Registration;

public class Script
{
    public void Run(IEngine engine)
    {
        // Get the SDM registrar from the engine
        var registrar = engine.GetSdmRegistrar();

        // Register your solution
        var solution = new SolutionRegistration
        {
            ID = "my_solution",
            DisplayName = "My Solution",
            Version = "1.0.0",
            DefaultApiEndpoint = "/api/custom/my_solution",
            VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3"
        };

        registrar.Solutions.Create(solution);
        engine.GenerateInformation("Solution registered successfully!");
    }
}
```

### Installation Script Example

```csharp
using Skyline.DataMiner.Automation;
using Skyline.DataMiner.SDM.Registration;

[AutomationEntryPoint(AutomationEntryPointType.Types.InstallAppPackage)]
public void Install(IEngine engine, AppInstallContext context)
{
    try
    {
        engine.Timeout = new TimeSpan(0, 10, 0);
        engine.GenerateInformation("Starting installation");

        // Get registrar and register solution
        var registrar = engine.GetSdmRegistrar();

        var solution = new SolutionRegistration
        {
            ID = "my_solution",
            DisplayName = "My Solution",
            Version = "1.0.0"
        };

        registrar.Solutions.Create(solution);

        // Register models
        var model = new ModelRegistration
        {
            Name = "customer",
            DisplayName = "Customer",
            Version = "1.0.0",
            Solution = solution
        };

        registrar.Models.Create(model);

        engine.GenerateInformation("Registration completed!");
    }
    catch (Exception e)
    {
        engine.ExitFail($"Installation failed: {e.Message}");
    }
}
```

## About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exists. In addition, you can leverage DataMiner Development Packages to build your own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

## About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.
