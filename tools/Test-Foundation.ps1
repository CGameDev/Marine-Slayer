[CmdletBinding()]
param(
    [ValidateSet('Windows', 'Xbox360')]
    [string]$Target = 'Windows',
    [string]$UnityPath = 'C:\Program Files\Unity\Editor\Unity.exe'
)
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
if (-not (Test-Path -LiteralPath $UnityPath)) { throw "Unity not found: $UnityPath" }
$method = if ($Target -eq 'Windows') { 'BuildWindows' } else { 'BuildXbox360' }
$marker = if ($Target -eq 'Windows') { 'MARINE_SLAYER_FOUNDATION_WINDOWS_BUILD_PASS' } else { 'MARINE_SLAYER_FOUNDATION_XBOX360_BUILD_PASS' }
$graphicsArgument = if ($Target -eq 'Xbox360') { '' } else { ' -nographics' }
$logRoot = Join-Path $projectRoot 'Logs'
New-Item -ItemType Directory -Path $logRoot -Force | Out-Null
$log = Join-Path $logRoot ('foundation-' + $Target + '-' + (Get-Date -Format 'yyyyMMdd-HHmmss') + '.log')
$arguments = '-batchmode' + $graphicsArgument + ' -quit -projectPath "' + $projectRoot + '" -executeMethod MarineSlayer.EditorTools.FoundationSceneBuilder.' + $method + ' -logFile "' + $log + '"'
$processInfo = [System.Diagnostics.ProcessStartInfo]::new()
$processInfo.FileName = $UnityPath
$processInfo.Arguments = $arguments
$processInfo.UseShellExecute = $false
$processInfo.CreateNoWindow = $true
$pathValue = $env:PATH
@($processInfo.Environment.Keys | Where-Object { $_ -ieq 'PATH' }) | ForEach-Object { [void]$processInfo.Environment.Remove($_) }
$processInfo.Environment['PATH'] = $pathValue
$process = [System.Diagnostics.Process]::Start($processInfo)
$process.WaitForExit()
Write-Output "Unity exit code: $($process.ExitCode). Log: $log"
if ($process.ExitCode -ne 0 -or -not (Select-String -LiteralPath $log -SimpleMatch $marker -Quiet)) {
    throw "Foundation $Target build failed. Inspect $log"
}
Write-Output "Foundation $Target build passed. Runtime input flow still requires interactive validation."
