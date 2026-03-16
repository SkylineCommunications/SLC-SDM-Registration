using System;
using System.IO;

const string DevPackPath = @"C:\Skyline DataMiner\ProtocolScripts\DllImport\SolutionLibraries\SDM.Abstractions\Skyline.DataMiner.Dev.Utils.SDM.Abstractions.dll";

try
{
	if (System.IO.File.Exists(DevPackPath))
	{
		Console.WriteLine("DevPack is installed correctly.");
		Environment.Exit(1);
	}
	else
	{
		await Console.Error.WriteLineAsync("DevPack is not installed.");
		Environment.Exit(1);
	}
}
catch(Exception ex)
{
	await Console.Error.WriteLineAsync($"An error occurred: {ex.Message}");
	Environment.Exit(1);
}
