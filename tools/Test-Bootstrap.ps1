[CmdletBinding()]
param(
    [ValidateSet('Validate', 'Windows', 'Xbox360')]
    [string]$Target = 'Validate',
    [string]$UnityPath = 'C:\Program Files\Unity\Editor\Unity.exe'
)
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
if (-not (Test-Path -LiteralPath $UnityPath)) { throw "Unity not found: $UnityPath" }
$methods = @{ Validate = 'Validate'; Windows = 'BuildWindowsBaseline'; Xbox360 = 'BuildXboxBaseline' }
$markers = @{ Validate = 'MARINE_SLAYER_BOOTSTRAP_PASS'; Windows = 'MARINE_SLAYER_WINDOWS_BASELINE_BUILD_PASS'; Xbox360 = 'MARINE_SLAYER_XBOX_BASELINE_BUILD_PASS' }
$logRoot = Join-Path $projectRoot 'Logs'
New-Item -ItemType Directory -Path $logRoot -Force | Out-Null
$log = Join-Path $logRoot ('bootstrap-' + $Target + '-' + (Get-Date -Format 'yyyyMMdd-HHmmss') + '.log')
$graphicsArgument = if ($Target -eq 'Xbox360') { '' } else { ' -nographics' }
$arguments = '-batchmode' + $graphicsArgument + ' -quit -projectPath "' + $projectRoot + '" -executeMethod MarineSlayer.EditorTools.BootstrapValidation.' + $methods[$Target] + ' -logFile "' + $log + '"'
$process = Start-Process -FilePath $UnityPath -ArgumentList $arguments -WindowStyle Hidden -PassThru
$process.WaitForExit()
Write-Output "Unity exit code: $($process.ExitCode). Log: $log"
if ($process.ExitCode -ne 0 -or -not (Select-String -LiteralPath $log -SimpleMatch $markers[$Target] -Quiet)) {
    throw "Bootstrap $Target failed. Inspect $log"
}
Write-Output "Bootstrap $Target passed. This does not verify gameplay or console deployment."
