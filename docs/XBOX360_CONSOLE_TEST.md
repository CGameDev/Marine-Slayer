# Xbox 360 Console Test — 2026-09-19

## Result

PASS. The project builds, deploys and runs on the available Xbox 360 reviewer
kit. A locally built HvP2 compatibility plugin supplies the development-XEX
environment required by Unity 5.4.1f1 on this retail-kernel/RGH console. The
title remains in Unity runtime and renders the project-owned main menu.

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
- HvP2, `FoundationXbox360.xex` and `Assembly-CSharp.dll.xex` concurrently
  loaded: PASS.
- Script-created camera/TextMesh main menu visible in an Xbox framebuffer:
  PASS.

## HvP2 installation and DashLaunch routing

The active `Hdd:\launch.ini` was backed up locally before testing. Its original
SHA-256 is
`99CBC62EBE0633BF3D5CA04ED7B762027961BE0CE2CAC39960A25A066E9BA0F8`.
HvP2 was cloned from `https://github.com/XeAssert/HvP2` and reviewed at exact
commit `360dc718c78b97f2694624efaf7fbd01289e8ee5`. The source targets dashboard
17559, validates the expected hypervisor header/instruction before applying its
runtime-only patch, and unloads on an unsupported target. It was built locally
with the installed Xbox 360 XDK and converted with the already-installed
XexTool 6.3 to Retail, encrypted, compressed, all-regions and all-media format.

Installed plugin SHA-256:
`5D32239CA213003EC0F41D8A8F45B207D6B5AB46EF878BCC83D17D206B05149D`.

The active configuration uses the previously empty DashLaunch `plugin3` slot
for `Hdd:\HvP2.xex` and sets `[Paths] Default` to
`Hdd:\MarineSlayer\Foundation\FoundationXbox360.xex`. Aurora's existing
`BUT_B` recovery mapping is preserved. The original configuration and each
tested candidate are backed up locally and verified by readback hashes.

Both the XDK drive alias and canonical device path had previously been tested
for remote title reboot. The console loaded DashLaunch helper `lhelper.xex` and
returned to Aurora before Unity was loaded. The XDK automation-controller
command also returns `XBDM_INVALIDCMD` on this target.

The plugin modifies hypervisor/kernel state only in memory and is reset by a
reboot. No NAND image was flashed. Removing `plugin3` from `launch.ini` and
cold-booting restores the pre-HvP2 runtime state.

## Interpretation

The development machine and console now support the full edit, Windows smoke,
Xbox build, deploy, launch, trace and framebuffer-capture loop. The debug
monitor does not implement XDK automation-controller commands, so controller
input still requires physical interaction or separate reviewed tooling. Release
packaging still requires the official Microsoft-assigned title ID and signing/
configuration data.
