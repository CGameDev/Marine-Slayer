[CmdletBinding()]
param(
    [string]$SourcePath = 'C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip',
    [string]$DestinationRoot = (Join-Path $PSScriptRoot '..\LocalDependencies'),
    [switch]$Force
)

$ErrorActionPreference = 'Stop'

$ExpectedZipSha256 = 'f27f1bf3bad614b829efa530cdd9247c52b69bc539bcfa987dd68ce59da4effb'
$ExpectedUnityPackageSha256 = 'e8569bd920b3dab4da2304f8b2c6b5006a95d27a16e2f597325a4288a59c62ea'

Write-Host 'Marine Slayer - Local Licensed Asset Preparation'
Write-Host '--------------------------------------------------'

if (-not (Test-Path -LiteralPath $SourcePath -PathType Leaf)) {
    throw "Asset ZIP was not found: $SourcePath"
}

$zipHash = (Get-FileHash -LiteralPath $SourcePath -Algorithm SHA256).Hash.ToLowerInvariant()
Write-Host "ZIP SHA-256: $zipHash"

if ($zipHash -ne $ExpectedZipSha256) {
    throw "Asset ZIP hash does not match the project-approved dependency. Expected $ExpectedZipSha256"
}

New-Item -ItemType Directory -Path $DestinationRoot -Force | Out-Null

$destinationZip = Join-Path $DestinationRoot 'AssetPack_ProjectSettings.zip'

if ((Test-Path -LiteralPath $destinationZip) -and -not $Force) {
    $existingHash = (Get-FileHash -LiteralPath $destinationZip -Algorithm SHA256).Hash.ToLowerInvariant()
    if ($existingHash -eq $ExpectedZipSha256) {
        Write-Host 'Approved ZIP is already staged locally.'
    }
    else {
        throw "A different file already exists at $destinationZip. Re-run with -Force only if you intend to replace it."
    }
}
else {
    Copy-Item -LiteralPath $SourcePath -Destination $destinationZip -Force
    Write-Host "Staged ZIP: $destinationZip"
}

$extractRoot = Join-Path $DestinationRoot 'AssetPack_Extracted'
if (Test-Path -LiteralPath $extractRoot) {
    if ($Force) {
        Remove-Item -LiteralPath $extractRoot -Recurse -Force
    }
    else {
        Write-Host 'Local extraction folder already exists; preserving it.'
    }
}

if (-not (Test-Path -LiteralPath $extractRoot)) {
    New-Item -ItemType Directory -Path $extractRoot -Force | Out-Null
    Expand-Archive -LiteralPath $destinationZip -DestinationPath $extractRoot -Force
    Write-Host "Extracted locally: $extractRoot"
}

$unityPackage = Join-Path $extractRoot 'Xbox360TutorialAssets.unitypackage'
if (-not (Test-Path -LiteralPath $unityPackage -PathType Leaf)) {
    throw "Expected unitypackage was not found after extraction: $unityPackage"
}

$packageHash = (Get-FileHash -LiteralPath $unityPackage -Algorithm SHA256).Hash.ToLowerInvariant()
Write-Host "Unitypackage SHA-256: $packageHash"

if ($packageHash -ne $ExpectedUnityPackageSha256) {
    throw "Embedded unitypackage hash mismatch. Expected $ExpectedUnityPackageSha256"
}

$projectVersion = Join-Path $extractRoot 'ProjectSettings\ProjectVersion.txt'
if (Test-Path -LiteralPath $projectVersion) {
    Write-Host 'Supplied Unity version:'
    Get-Content -LiteralPath $projectVersion | ForEach-Object { Write-Host "  $_" }
}

Write-Host ''
Write-Host 'SUCCESS'
Write-Host 'The licensed dependency is verified and staged locally.'
Write-Host 'Do NOT git-add files under LocalDependencies.'
Write-Host "Unity package to import: $unityPackage"
