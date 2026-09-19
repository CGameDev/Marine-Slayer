# Marine Slayer — Build Environment

## Verified bootstrap observations — 2026-09-19

Workspace: `C:\Users\CGAmeDev\Documents\ChatGPT\Marine Slayer`.
Branch: `milestone/m001-complete-game`; starting commit: `aabc86c`.

- Unity: `C:\Program Files\Unity\Editor\Unity.exe`. Fresh project creation
  passed; generated ProjectVersion is 5.4.1f1. Log: `Logs/create-project.log`.
- Xbox support: `Editor\Data\PlaybackEngines\XenonPlayer` under Unity;
  extension DLLs and runtime variations present. A development Xbox build
  completed and emitted `Builds/XboxBaseline/XboxBaseline.xex` plus Media.
- XDK: `C:\Program Files (x86)\Microsoft Xbox 360 SDK`; XEDK is set correctly.
  imagexex/xbmanage/xbreboot report 2.0.21256.0. Compiler cl.exe reports
  16.00.11886.00; link.exe reports 10.00.11886.00.
- Visual Studio: `C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe`,
  version 10.0.40219.1. Xbox VC templates are installed.
- Deploy/debug/profile tools: xbcp, xbmanage, xbreboot, xbwatson and xbperfview
  exist under XDK `bin\win32`. A reviewer kit was reached, queried, deployed to,
  traced and captured successfully. No console address or identifier is committed.
- Both approved asset hashes match. ZIP and extracted package are staged in
  LocalDependencies. Both owner lore files are staged in LocalReferences.
- Fresh ProjectSettings backup: `LocalDependencies/FreshProjectSettings`.
  Only the supplied InputManager was copied. Archived build-scene entries
  and other title-specific settings were not copied.
- Git remote fetched successfully. Public fetch does not verify push access.

The bundled Xbox manual names XDK 21250.7; installed tools report 21256.0.
The baseline build passed with that installed toolchain.

### Repeatable checks

Close this project's editor before running:

```powershell
.\tools\Test-Bootstrap.ps1 -Target Validate
.\tools\Test-Bootstrap.ps1 -Target Windows
.\tools\Test-Bootstrap.ps1 -Target Xbox360
```

The script checks Unity's exit code and the expected success marker and
keeps timestamped logs in ignored `Logs/`. Builds go to ignored `Builds/`.
The generated MS_ToolchainBaseline scene is a setup test, not a campaign level.
Controller behavior, save storage and profiling remain partially or wholly
untested. Console runtime is enabled by the locally source-built HvP2 plugin;
see `docs/XBOX360_CONSOLE_TEST.md` for provenance and hardware evidence.

Verified build results:

- Package import and script compilation: PASS.
- Windows development baseline build: PASS.
- Xbox 360 development baseline build: PASS.
- A development-only title ID (`FFFF4D53`) is embedded for local hardware tests.
  It removes the invalid-title warning and appears in the XEX execution header.
  It must be replaced by the Microsoft-assigned title ID before release.

The original discovery checklist below remains to be completed as each
workflow is validated; the observations above supersede its TBD entries.

## Required local environment

| Component | Required | Detected version/path | Status |
|---|---|---|---|
| Unity Editor | 5.4.1f1 | `C:\Program Files\Unity\Editor\Unity.exe` | PASS |
| Xbox 360 Unity support | yes | Unity `PlaybackEngines\XenonPlayer` | PASS (build) |
| Xbox 360 XDK | yes | `C:\Program Files (x86)\Microsoft Xbox 360 SDK`, tools 21256.0 | PASS (build) |
| Visual Studio/XDK integration | yes | Visual Studio 2010 10.0.40219.1 + Xbox templates | PASS (build) |
| Xbox console deployment/debug tool | yes | xbcp/xbmanage/xbreboot/xbwatson/xbperfview | PASS (connect/deploy/trace/capture) |
| Git | yes | 2.55.0 | PASS |
| Licensed asset ZIP | yes | LocalDependencies, expected SHA-256 | PASS |

## Asset dependency verification

Expected ZIP SHA-256:

`f27f1bf3bad614b829efa530cdd9247c52b69bc539bcfa987dd68ce59da4effb`

Expected embedded unitypackage SHA-256:

`e8569bd920b3dab4da2304f8b2c6b5006a95d27a16e2f597325a4288a59c62ea`

Verification result: PASS for both ZIP and embedded unitypackage.

## Unity discovery

- executable path: `C:\Program Files\Unity\Editor\Unity.exe`
- exact `5.4.1f1` confirmation: PASS
- Xbox 360 build target visible: PASS through successful BuildPipeline output
- license/editor startup issue: none observed in batch mode

## XDK discovery

- XDK root: `C:\Program Files (x86)\Microsoft Xbox 360 SDK`
- installed XDK version: tool binaries report 2.0.21256.0
- environment variables: XEDK points to the detected XDK root
- compiler/linker integration: PASS through Xbox foundation build
- console manager/deployment utility: xbmanage/xbcp/xbreboot present
- profiler/debug utility: xbperfview/xbwatson present

Do not commit proprietary XDK files.

## Build workflow

### Windows development build

`tools\Test-Foundation.ps1 -Target Windows` builds the three-scene foundation.
`tools\Test-FoundationFlow.ps1` runs the automated state-flow smoke test.

### Xbox 360 build

`tools\Test-Foundation.ps1 -Target Xbox360` builds the three-scene foundation.
The development XEX embeds title ID `FFFF4D53`. Replace it with the assigned
release ID before packaging or release work.

### Deploy to development/RGH target

PASS on the connected reviewer kit. The 79-file foundation build was copied to
the XDK `E:\MarineSlayer\Foundation` volume and to the DashLaunch-visible
`Hdd:\MarineSlayer\Foundation` folder. The XEX and required Media files were
verified remotely.

### Launch/debug

PASS for build launch and runtime inspection. HvP2 was built locally from the
reviewed `XeAssert/HvP2` source at commit
`360dc718c78b97f2694624efaf7fbd01289e8ee5`, converted to a retail/all-media
plugin with the installed XexTool, deployed to `Hdd:\HvP2.xex`, and loaded in
DashLaunch `plugin3`. The Unity title, game assembly and HvP2 are concurrently
loaded; a framebuffer capture verifies the scripted main menu renders on the
physical console. XDK controller automation remains unsupported by this debug
monitor, so interactive controller checks are still pending.

## Console information

Do not commit IP addresses, credentials or sensitive console identifiers unless the owner explicitly approves them.

Connection status: PASS. The target identifies as a 512 MB reviewer kit running
kernel 17559 with XDK monitor 21076.11. Host tools are 21256.0. No console
address, MAC address, account credential or sensitive identifier is committed.

## Known compatibility notes

- Xbox foundation build succeeds with installed XDK 21256.0 despite bundled
  Unity documentation naming 21250.7.
- Xbox builds must run Unity with graphics enabled; `-nographics` prevents
  reflection cubemap processing and causes the build to report errors.
- Development hardware builds use `FFFF4D53`; an official assigned title ID is
  still required before release configuration.
- This target runs a retail kernel with Aurora/DashLaunch. HvP2 2.0.17559.0 is
  loaded in the previously unused `plugin3` slot to satisfy the Unity Devkit
  XEX's XDK/XBDM imports. Aurora remains mapped to the `B` recovery shortcut.
- The original DashLaunch configuration is backed up byte-for-byte. The active
  development configuration starts Marine Slayer and retains Aurora recovery.

## Discovery completion gate

This document is complete when Codex can answer, with evidence:

1. which exact Unity executable opens the project;
2. whether Xbox 360 is an available build target;
3. which XDK is installed;
4. how an Xbox 360 build is produced;
5. how that build is deployed/launched on the target console;
6. where build logs/errors are obtained;
7. which parts require manual owner interaction.
