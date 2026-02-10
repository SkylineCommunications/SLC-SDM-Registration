# Reading Solutions and Models with SdmRegistrar

This guide explains how to read a registered solution, check its version, and verify if a model exists using the `SdmRegistrar`.

## Prerequisites

- Reference the SDM Registration library in your project.
- Obtain a DataMiner connection and the `SdmRegistrar` instance.

## Registration Entrypoint

The entry point is the `SdmRegistrar` class, which you can obtain from your DataMiner connection:

```csharp
// Automation Script
public void Run(IEngine engine)
{
    var registrar = engine.GetSdmRegistrar();
}

// Protocol
public static void Run(SLProtocol protocol)
{
    var registrar = protocol.GetSdmRegistrar();
}

// GQI
public OnInitOutputArgs OnInit(OnInitInputArgs args)
{
	var registrar = args.DMS.GetSdmRegistrar();
}
```

## Reading a Registered Solution

You can retrieve a solution by its ID, GUID or any form of custom filters:

```csharp
// Automation Script, read by id
public void Run(IEngine engine)
{
    var registrar = engine.GetSdmRegistrar();
    var solution = registrar.Solutions.GetSolutionById("standard_data_model_abstractions");

    engine.Log($"{solution.DisplayName} has version {solution.Version}");
}

// Protocol, read by Id
public static void Run(SLProtocol protocol)
{
    var registrar = protocol.GetSdmRegistrar();
    var solution = registrar.Solutions.GetSolutionById("standard_data_model_registrations");

    protocol.Log($"{solution.DisplayName} has version {solution.Version}");
}

// GQI, read using custom filter
public OnInitOutputArgs OnInit(OnInitInputArgs args)
{
	var registrar = args.DMS.GetSdmRegistrar();
    var solutions = registrar.Solutions.Read(
        new ORFilterElement<SolutionRegistration>(
				SolutionRegistrationExposers.ID.Equal("service_management"),
				SolutionRegistrationExposers.ID.Equal("network_management"))
    );
}
```

## Reading a Registered Models

You can retrieve a models by its Guid, Name, Solution or any form of custom filters:

```csharp
// Automation Script, read by Name
public void Run(IEngine engine)
{
    var registrar = engine.GetSdmRegistrar();
    var model = registrar.Models.GetModelByName("my_model");

    engine.Log($"{model.DisplayName} has version {model.Version}");
}

// Protocol, read by Solution
public static void Run(SLProtocol protocol)
{
    var registrar = protocol.GetSdmRegistrar();
    
    // By reference 
    var solutionRef = new SdmObjectReference<SolutionRegistration>(new Guid("00000000-0000-0000-0000-000000000000"));
    var modelsByRef = registrar.Models.GetModelsBySolution(solutionRef);

    // By object
    var solution = registrar.Solutions.GetSolutionById("standard_data_model_registrations");
    var models = registrar.Models.GetModelsBySolution(solution);

    foreach(var model in models)
    {
        protocol.Log($"{model.DisplayName} has version {model.Version}");
    }
}

// GQI, read using custom filter
public OnInitOutputArgs OnInit(OnInitInputArgs args)
{
	var registrar = args.DMS.GetSdmRegistrar();
    var solution = registrar.Solutions.GetSolutionById("my_solutions");
    var models = registrar.Models.Read(
        new ORFilterElement<ModelRegistration>(
				ModelRegistrationExposers.Solution.Equal(solution),
				ModelRegistrationExposers.Version.Contains("1.0"))
    );
}
```