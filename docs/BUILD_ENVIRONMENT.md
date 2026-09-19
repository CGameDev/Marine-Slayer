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
  exist under XDK `bin\win32`. Console connectivity has NOT been tested.
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
Console launch, controller behavior, save storage and profiling remain untested.

Verified build results:

- Package import and script compilation: PASS.
- Windows development baseline build: PASS.
- Xbox 360 development baseline build: PASS.
- Unity warns that no valid Xbox title ID is configured, so a title ID was not
  embedded. This must be resolved before title configuration or release work.

The original discovery checklist below remains to be completed as each
workflow is validated; the observations above supersede its TBD entries.

## Required local environment

| Component | Required | Detected version/path | Status |
|---|---|---|---|
| Unity Editor | 5.4.1f1 | TBD | NOT VERIFIED |
| Xbox 360 Unity support | yes | TBD | NOT VERIFIED |
| Xbox 360 XDK | yes | TBD | NOT VERIFIED |
| Visual Studio/XDK integration | yes | TBD | NOT VERIFIED |
| Xbox console deployment/debug tool | yes | TBD | NOT VERIFIED |
| Git | yes | TBD | NOT VERIFIED |
| Licensed asset ZIP | yes | TBD | NOT VERIFIED |

## Asset dependency verification

Expected ZIP SHA-256:

`f27f1bf3bad614b829efa530cdd9247c52b69bc539bcfa987dd68ce59da4effb`

Expected embedded unitypackage SHA-256:

`e8569bd920b3dab4da2304f8b2c6b5006a95d27a16e2f597325a4288a59c62ea`

Verification result: TBD

## Unity discovery

- executable path: TBD
- exact `5.4.1f1` confirmation: TBD
- Xbox 360 build target visible: TBD
- license/editor startup issue: TBD

## XDK discovery

- XDK root: TBD
- installed XDK version: TBD
- environment variables: TBD
- compiler/linker integration: TBD
- console manager/deployment utility: TBD
- profiler/debug utility: TBD

Do not commit proprietary XDK files.

## Build workflow

### Windows development build

Verified command/menu workflow: TBD

### Xbox 360 build

Verified command/menu workflow: TBD

### Deploy to development/RGH target

Verified deployment workflow: TBD

### Launch/debug

Verified launch/debug workflow: TBD

## Console information

Do not commit IP addresses, credentials or sensitive console identifiers unless the owner explicitly approves them.

Connection status/procedure: TBD

## Known compatibility notes

TBD after initial asset import and first Xbox build.

## Discovery completion gate

This document is complete when Codex can answer, with evidence:

1. which exact Unity executable opens the project;
2. whether Xbox 360 is an available build target;
3. which XDK is installed;
4. how an Xbox 360 build is produced;
5. how that build is deployed/launched on the target console;
6. where build logs/errors are obtained;
7. which parts require manual owner interaction.
