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
$pathToDevPackTest = Join-Path $PathToTestPackageContent 'ValidateDevPackInstallation'

# Track script start time
$scriptStart = Get-Date

try {
    Write-Host "Running Test Package tests..." -ForegroundColor Cyan
    
    <#
        This is a placeholder for where the test execution logic would go.
    #>
    Write-Host "Running DevPack installation confirmation test..." -ForegroundColor Cyan

	 # Create temporary .cs file
    $tempDevPackTestPath = "$pathToDevPackTest.cs"
    Copy-Item $pathToPlaywrightUiTest $tempDevPackTestPath -Force

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

        try { Push-TestCaseResult -Outcome 'OK' -Name 'pipeline_ConfirmDevPackInstallation' -Duration ((Get-Date) - $scriptStart) -Message $devPackMessage -TestAspect Assertion } catch {}
    }
    else {
        if ([string]::IsNullOrWhiteSpace($devPackMessage)) { 
            $devPackMessage = "DevPack test failed." 
        }

        Write-Host "DevPack test FAILED." -ForegroundColor Red

        try { Push-TestCaseResult -Outcome 'Fail' -Name 'pipeline_ConfirmDevPackInstallation' -Duration ((Get-Date) - $scriptStart) -Message $devPackMessage -TestAspect Assertion } catch {}

        throw "DevPack test failed with exit code $devPackTestExitCode."
    }

    # Send OK result indicating that test package execution has finished successfully
    Push-TestCaseResult -Outcome 'OK' -Name "pipeline_TestPackageExecution" -Duration ((Get-Date) - $scriptStart) -Message "Test Package execution finished." -TestAspect Execution
} catch {
    Push-TestCaseResult -Outcome 'Fail' -Name "pipeline_TestPackageExecution" -Duration ((Get-Date) - $scriptStart) -Message "Exception during Test Package execution: $($_.Exception.Message)" -TestAspect Execution
    exit 1
}