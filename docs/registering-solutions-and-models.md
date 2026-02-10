# Registering Solutions and Models with SdmRegistrar

This guide explains how to use the `SdmRegistrar` to register a DataMiner solution and its associated models.

## Prerequisites

- Reference the SDM Registration library in your project (e.g., the `Skyline.DataMiner.SDM.Registration.Protocol` nuget package for use in a Connector).
- Obtain a DataMiner connection (e.g., via `IEngine` in Automation scripts).

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

## Registering a new Solution

After you have you're registrar you can start registering your solution and models:

> [!IMPORTANT]
> The solution should always be registered first, before adding models. You cannot have a lingering model.


```csharp
var solution = new SolutionRegistration 
{ 
    /* Required */  ID = "my_solution", 
    /* Required */  DisplayName = "My Solution", 
    /* Required */  Version = "1.0.1",
    /* Optional */  DefaultApiEndpoint = "/api/custom/my_solution",
    /* Optional */  DefaultApiScriptName = "MySolution.CRUD",
    /* Optional */  VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3",
};

registrar.Solutions.Create(solution);

var model1 = new  
{ 
    /* Required */  Name = "model1", 
    /* Required */  DisplayName = "Model 1", 
    /* Required */  Version = "1.0.1", 
    /* Optional */  ApiScriptName = "MySolution.Model1.CRUD", 
    /* Optional */  ApiEndpoint = "/api/custom/my_solution/model1", 
    /* Optional */  VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3/MyModel%20Overview", 
    /* Required */  Solution = solution,
};

registrar.Models.Create(model1);

```

## Updating an existing Solution

After you have you're registrar you can start registering your solution and models:

> [!IMPORTANT]
> The solution should always be registered first, before adding models. You cannot have a lingering model.


```csharp
var solution = new SolutionRegistration 
{ 
    /* Required */  ID = "my_solution", 
    /* Required */  DisplayName = "My Solution", 
    /* Required */  Version = "1.0.1",
    /* Optional */  DefaultApiEndpoint = "/api/custom/my_solution",
    /* Optional */  DefaultApiScriptName = "MySolution.CRUD",
    /* Optional */  VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3",
};

registrar.Solutions.Update(solution);

var model1 = new  
{ 
    /* Required */  Name = "model1", 
    /* Required */  DisplayName = "Model 1", 
    /* Required */  Version = "1.0.1", 
    /* Optional */  ApiScriptName = "MySolution.Model1.CRUD", 
    /* Optional */  ApiEndpoint = "/api/custom/my_solution/model1", 
    /* Optional */  VisualizationEndpoint = "/app/e3210645-842d-49e5-bb4d-905e69a67cf3/MyModel%20Overview", 
    /* Required */  Solution = solution,
};

registrar.Models.Update(model1);

```