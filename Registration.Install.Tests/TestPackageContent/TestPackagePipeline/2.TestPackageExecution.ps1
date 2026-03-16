param (
    [Parameter(Mandatory = $true)]
    [string]$PathToTestPackageContent
)

$ErrorActionPreference = 'Stop'

# Import common code module
Import-Module -Name (Join-Path $PSScriptRoot 'CommonCode.psm1')

$pathToTestHarvesting = Join-Path $PathToTestPackageContent 'TestHarvesting'
$pathToGeneratedTests = Join-Path $pathToTestHarvesting 'tests.generated'
$pathToGeneratedDependencies = Join-Path $pathToTestHarvesting 'dependencies.generated'
$pathToTests = Join-Path $PathToTestPackageContent 'Tests'
$pathToDependencies = Join-Path $PathToTestPackageContent 'Dependencies'
$pathToDevPackTest = Join-Path $pathToGeneratedTests 'ValidateDevPackInstallation'

# Track script start time
$scriptStart = Get-Date

try {
    Write-Host "Running Test Package tests..." -ForegroundColor Cyan

	# Installing nuget.org as a source
	Write-Host "Checking NuGet sources..." -ForegroundColor Cyan

    $nugetSourcesOutput = & dotnet nuget list source 2>&1
    $nugetSourcesExitCode = $LASTEXITCODE

    if ($nugetSourcesExitCode -ne 0) {
        throw "Failed to list NuGet sources. Output: $($nugetSourcesOutput | Out-String)"
    }

    $nugetSourcesText = ($nugetSourcesOutput | Out-String)

    if ($nugetSourcesText -notmatch '(?im)^\s*\d+\.\s+nuget\.org\s*\[Enabled\]') {
        Write-Host "nuget.org source not found. Adding nuget.org..." -ForegroundColor Yellow

        $addNugetSourceOutput = & dotnet nuget add source 'https://api.nuget.org/v3/index.json' --name 'nuget.org' 2>&1
        $addNugetSourceExitCode = $LASTEXITCODE

        if ($addNugetSourceExitCode -ne 0) {
            throw "Failed to add nuget.org NuGet source. Output: $($addNugetSourceOutput | Out-String)"
        }

        Write-Host "nuget.org source added successfully." -ForegroundColor Green
    }
    else {
        Write-Host "nuget.org source already exists." -ForegroundColor Green
    }

    <#
        This is a placeholder for where the test execution logic would go.
    #>
    Write-Host "Running DevPack installation confirmation test..." -ForegroundColor Cyan

	 # Create temporary .cs file
    $tempDevPackTestPath = "$pathToDevPackTest.cs"
    Copy-Item $pathToDevPackTest $tempDevPackTestPath -Force

    $devPackTestResult = & dotnet run $tempDevPackTestPath 2>&1
    $devPackTestExitCode = $LASTEXITCODE

    $devPackMessage = ($devPackTestResult | Out-String).Trim()

    Write-Host "DevPack Test Output: $devPackMessage" -ForegroundColor DarkGray
    Write-Host $devPackMessage

    if ($devPackTestExitCode -ne 0) {
       if ([string]::IsNullOrWhiteSpace($devPackMessage)) { 
            $devPackMessage = "DevPack test passed." 
        }

        Write-Host "DevPack test SUCCEEDED." -ForegroundColor Green

        try { Push-TestCaseResult -Outcome 'OK' -Name 'ValidateDevPackInstallation' -Duration ((Get-Date) - $scriptStart) -Message $devPackMessage -TestAspect Assertion } catch {}
    }
    else {
        if ([string]::IsNullOrWhiteSpace($devPackMessage)) { 
            $devPackMessage = "DevPack test failed." 
        }

        Write-Host "DevPack test FAILED." -ForegroundColor Red

        try { Push-TestCaseResult -Outcome 'Fail' -Name 'ValidateDevPackInstallation' -Duration ((Get-Date) - $scriptStart) -Message $devPackMessage -TestAspect Assertion } catch {}

        throw "DevPack test failed with exit code $devPackTestExitCode."
    }

    # Send OK result indicating that test package execution has finished successfully
    Push-TestCaseResult -Outcome 'OK' -Name "pipeline_TestPackageExecution" -Duration ((Get-Date) - $scriptStart) -Message "Test Package execution finished." -TestAspect Execution
} catch {
    Push-TestCaseResult -Outcome 'Fail' -Name "pipeline_TestPackageExecution" -Duration ((Get-Date) - $scriptStart) -Message "Exception during Test Package execution: $($_.Exception.Message)" -TestAspect Execution
    exit 1
}