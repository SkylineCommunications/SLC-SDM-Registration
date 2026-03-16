# Skyline.DataMiner.SDM.Registration.Common

A comprehensive library for registering and managing DataMiner solutions and their data models across multiple environments. This package provides the core API for maintaining a central registry of your solution deployments, versions, and associated data types.

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
Install-Package Skyline.DataMiner.SDM.Registration.Common
```

Or via .NET CLI:

```bash
dotnet add package Skyline.DataMiner.SDM.Registration.Common
```

## Quick Start

### Registering a Solution

```csharp
using Skyline.DataMiner.SDM.Registration;

// Get the registrar from your connection
var registrar = new SdmRegistrar(connection);

// Create a solution registration
var solution = new SolutionRegistration
{
    ID = "my_solution",
    DisplayName = "My Solution",
    Version = "1.0.0",
    DefaultApiEndpoint = "/api/custom/my_solution",
    DefaultApiScriptName = "MySolution.CRUD",
    VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3"
};

// Register the solution
registrar.Solutions.Create(solution);
```

### Registering Models

```csharp
// Create a model registration linked to your solution
var model = new ModelRegistration
{
    Name = "customer",
    DisplayName = "Customer",
    Version = "1.0.0",
    Solution = solution,
    ApiEndpoint = "/api/custom/my_solution/customer",
    ApiScriptName = "MySolution.Customer.CRUD",
    VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3/Customers"
};

// Register the model
registrar.Models.Create(model);
```

### Querying Registrations

```csharp
// Retrieve a solution by ID
var existingSolution = registrar.Solutions.GetSolutionById("my_solution");

// Query all models for a solution
var models = registrar.Models.Read(
    ModelRegistrationExposers.Solution.Equal(solution.Identifier)
);

// Get all solutions
var allSolutions = registrar.Solutions.Read(new TRUEFilterElement<SolutionRegistration>());
```

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exists. In addition, you can leverage DataMiner Development Packages to build your own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

### About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.

<!-- Uncomment below and add more info to provide more information about how to use this package. -->
<!-- ## Getting Started -->
