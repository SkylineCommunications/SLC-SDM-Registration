# Skyline.DataMiner.SDM.Registration.Protocol

Extension methods for DataMiner protocols (connectors/drivers) to access the SDM Registration API. This package enables protocols to register and query solution metadata directly from QActions.

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
Install-Package Skyline.DataMiner.SDM.Registration.Protocol
```

Or via .NET CLI:

```bash
dotnet add package Skyline.DataMiner.SDM.Registration.Protocol
```

## Quick Start

### Using in a QAction

```csharp
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.SDM.Registration;

public class QAction
{
    public static void Run(SLProtocol protocol)
    {
        // Get the SDM registrar from the protocol
        var registrar = protocol.GetSdmRegistrar();

        // Query registered solutions
        var solutions = registrar.Solutions.Read(new TRUEFilterElement<SolutionRegistration>());

        foreach (var solution in solutions)
        {
            protocol.Log($"Found solution: {solution.DisplayName} v{solution.Version}");
        }
    }
}
```

### Registering from a Protocol

```csharp
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.SDM.Registration;

public class QAction
{
    public static void Run(SLProtocol protocol)
    {
        try
        {
            var registrar = protocol.GetSdmRegistrar();

            // Register a solution
            var solution = new SolutionRegistration
            {
                ID = "device_manager",
                DisplayName = "Device Manager",
                Version = "1.0.0",
                DefaultApiEndpoint = "/api/custom/device_manager",
                DefaultApiScriptName = "DeviceManager.CRUD"
            };

            registrar.Solutions.Create(solution);
            protocol.Log("Solution registered successfully!");
        }
        catch (Exception ex)
        {
            protocol.Log($"Registration failed: {ex.Message}", LogType.Error, LogLevel.NoLogging);
        }
    }
}
```

### Checking Solution Versions

```csharp
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.SDM.Registration;

public class QAction
{
    public static void Run(SLProtocol protocol)
    {
        var registrar = protocol.GetSdmRegistrar();

        try
        {
            // Check if a specific solution is registered
            var solution = registrar.Solutions.GetSolutionById("my_solution");

            // Update protocol parameter with version info
            protocol.SetParameter(100, solution.Version);
            protocol.SetParameter(101, solution.DisplayName);

            protocol.Log($"Solution version: {solution.Version}");
        }
        catch (RegistrationNotFoundException ex)
        {
            protocol.Log($"Solution not found: {ex.Message}", LogType.Error, LogLevel.NoLogging);
        }
    }
}
```

### Listing Models for Integration

```csharp
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.SDM.Registration;

public class QAction
{
    public static void Run(SLProtocol protocol)
    {
        var registrar = protocol.GetSdmRegistrar();

        // Get all models from a specific solution
        var solution = registrar.Solutions.GetSolutionById("inventory_system");
        var models = registrar.Models.Read(
            ModelRegistrationExposers.Solution.Equal(solution.Identifier)
        );

        // Build a list of available models for dropdown
        var modelNames = models.Select(m => m.DisplayName).ToArray();
        protocol.Log($"Available models: {string.Join(", ", modelNames)}");
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
