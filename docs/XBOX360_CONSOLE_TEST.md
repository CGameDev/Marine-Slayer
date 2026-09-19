# Xbox 360 Console Test — 2026-09-19

## Result

The project can build and deploy to the available Xbox 360 reviewer kit, but
the current retail-kernel/RGH configuration cannot load the Unity development
XEX. DashLaunch routing was tested deliberately and reversibly: the game path
was selected, the console reached the Xbox loader, and the loader displayed
"The game couldn't start."

No console address, MAC address, account credential or sensitive console
identifier is recorded in this repository.

## Verified

- Target connection: PASS through the Xbox debug monitor.
- Target type: reviewer kit, 512 MB.
- Console kernel: 17559; console XDK monitor: 21076.11.
- Host XDK tools: 21256.0.
- Xbox foundation build: PASS with Unity 5.4.1f1.
- Development execution ID: PASS. `imagexex /dump` reports title ID
  `FFFF4D53`. This is development-only and must be replaced for release.
- Deployment: PASS to both the XDK `E:\MarineSlayer\Foundation` development
  volume and DashLaunch-visible `Hdd:\MarineSlayer\Foundation`.
- Remote file verification: PASS; the deployed XEX size matches 10,080,256
  bytes and all 78 sibling `Media` files were transferred.
- Framebuffer capture: PASS through `xbcapture`.
- Persistent debug notification capture: PASS through
  `tools/Xbox360RuntimeProbe.cs`; network identifiers and credential-like lines
  are redacted before output.

## DashLaunch routing test

The active `Hdd:\launch.ini` was backed up locally before testing. Its original
SHA-256 is
`99CBC62EBE0633BF3D5CA04ED7B762027961BE0CE2CAC39960A25A066E9BA0F8`.
Only `[Paths] Default` was changed, from Aurora to
`Hdd:\MarineSlayer\Foundation\FoundationXbox360.xex`; Aurora's existing
`BUT_B` recovery mapping and every other option were preserved.

A cold reboot proved that DashLaunch followed the changed path, but the Xbox
loader rejected the development XEX before Unity started. The original Unity
output reports `Devkit` machine format and imports `xbdm.xex` 21256. A second
test used the already-installed XexTool 6.3 to convert copies of the main XEX
and all 12 managed XEX modules to Retail format and all-media loading. The
loader still rejected that build, so retail re-encryption alone is not enough
for this console.

Both the XDK drive alias and canonical device path had previously been tested
for remote title reboot. The console loaded DashLaunch helper `lhelper.xex` and
returned to Aurora before Unity was loaded. The XDK automation-controller
command also returns `XBDM_INVALIDCMD` on this target.

After both configuration tests, the exact backed-up `launch.ini` was restored,
read back with the original SHA-256, and cold-booted. A final framebuffer
capture confirmed Aurora was again the active dashboard.

## Remaining hardware prerequisite

The build imports XDK/XBDM functions that are not resolved by the current
retail-kernel setup. Before console runtime tests can continue, use one of:

1. A compatible DashLaunch `HvP2.xex` plugin for kernel 17559, loaded in an
   unused plugin slot; or
2. A real development/test kit or an RGLoader/XDKBuild development-kernel
   environment.

No HvP2 binary was found in the inspected project, asset-pack or owner source
directories. The open-source HvP2 project documents this exact purpose:
<https://github.com/XeAssert/HvP2>. Treat installation as a separate,
owner-approved console change; do not download an unverified binary or flash a
different NAND as part of the normal project build.

## Interpretation

The development machine has the required editor, Xbox module, compiler, XDK,
deployment tools and reachable target. Project work can continue now in the
Editor and Windows build. Physical Xbox runtime acceptance is blocked by the
missing debug-XEX compatibility layer/development kernel. Release packaging
also requires the official Microsoft-assigned title ID and signing/configuration
data.
