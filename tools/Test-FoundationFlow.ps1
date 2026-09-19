[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$player = Join-Path $projectRoot 'Builds\FoundationWindows\MarineSlayerFoundation.exe'
if (-not (Test-Path -LiteralPath $player)) { throw "Foundation player not found. Run Test-Foundation.ps1 -Target Windows first." }
$logRoot = Join-Path $projectRoot 'Logs'
New-Item -ItemType Directory -Path $logRoot -Force | Out-Null
$log = Join-Path $logRoot ('foundation-flow-' + (Get-Date -Format 'yyyyMMdd-HHmmss') + '.log')
$arguments = '-batchmode -nographics -marineSlayerSmoke -logFile "' + $log + '"'
$process = Start-Process -FilePath $player -ArgumentList $arguments -WindowStyle Hidden -PassThru
if (-not $process.WaitForExit(30000))
{
    $process.Kill()
    throw "Foundation flow timed out. Inspect $log"
}
if (-not (Select-String -LiteralPath $log -SimpleMatch 'MARINE_SLAYER_FOUNDATION_FLOW_PASS' -Quiet))
{
    throw "Foundation flow failed. Inspect $log"
}
Write-Output "Foundation flow passed. Log: $log"
