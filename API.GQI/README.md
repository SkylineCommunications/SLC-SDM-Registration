# Skyline.DataMiner.SDM.Registration.GQI

Extension methods for DataMiner GQI (Generic Query Interface) data sources to access the SDM Registration API. This package enables custom GQI data sources to query and display solution and model registrations in dashboards and low-code apps.
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
Install-Package Skyline.DataMiner.SDM.Registration.GQI
```

Or via .NET CLI:

```bash
dotnet add package Skyline.DataMiner.SDM.Registration.GQI
```

## Quick Start

### Using in a GQI Data Source

```csharp
using Skyline.DataMiner.Analytics.GenericInterface;
using Skyline.DataMiner.SDM.Registration;

[GQIMetaData(Name = "Solution Registry")]
public class SolutionRegistryDataSource : IGQIDataSource, IGQIOnInit
{
    private SdmRegistrar _registrar;

    public OnInitOutputArgs OnInit(OnInitInputArgs args)
    {
        // Get the SDM registrar from GQI context
        _registrar = args.GetSdmRegistrar();
        return new OnInitOutputArgs();
    }

    public GQIColumn[] GetColumns()
    {
        return new[]
        {
            new GQIStringColumn("Solution ID"),
            new GQIStringColumn("Display Name"),
            new GQIStringColumn("Version"),
            new GQIStringColumn("API Endpoint")
        };
    }

    public GQIPage GetNextPage(GetNextPageInputArgs args)
    {
        var solutions = _registrar.Solutions.Read(new TRUEFilterElement<SolutionRegistration>());
        var rows = new List<GQIRow>();

        foreach (var solution in solutions)
        {
            var cells = new[]
            {
                new GQICell { Value = solution.ID },
                new GQICell { Value = solution.DisplayName },
                new GQICell { Value = solution.Version },
                new GQICell { Value = solution.DefaultApiEndpoint }
            };

            rows.Add(new GQIRow(cells));
        }

        return new GQIPage(rows.ToArray()) { HasNextPage = false };
    }
}
```

### Model Registry Data Source

```csharp
using Skyline.DataMiner.Analytics.GenericInterface;
using Skyline.DataMiner.SDM.Registration;

[GQIMetaData(Name = "Model Registry")]
public class ModelRegistryDataSource : IGQIDataSource, IGQIOnInit
{
    private SdmRegistrar _registrar;

    public OnInitOutputArgs OnInit(OnInitInputArgs args)
    {
        _registrar = args.GetSdmRegistrar();
        return new OnInitOutputArgs();
    }

    public GQIColumn[] GetColumns()
    {
        return new[]
        {
            new GQIStringColumn("Model Name"),
            new GQIStringColumn("Display Name"),
            new GQIStringColumn("Solution"),
            new GQIStringColumn("Version"),
            new GQIStringColumn("API Endpoint")
        };
    }

    public GQIPage GetNextPage(GetNextPageInputArgs args)
    {
        var models = _registrar.Models.Read(new TRUEFilterElement<ModelRegistration>());
        var rows = new List<GQIRow>();

        foreach (var model in models)
        {
            var cells = new[]
            {
                new GQICell { Value = model.Name },
                new GQICell { Value = model.DisplayName },
                new GQICell { Value = model.Solution?.DisplayName ?? "N/A" },
                new GQICell { Value = model.Version },
                new GQICell { Value = model.ApiEndpoint }
            };

            rows.Add(new GQIRow(cells));
        }

        return new GQIPage(rows.ToArray()) { HasNextPage = false };
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
