# CODEX START HERE — Marine Slayer

## Mission

Build Marine Slayer into a **complete Xbox 360 game**, not a demo.

Before making project changes, read all required documents listed in `README.md`, beginning with `AGENTS.md`.

## First-run workflow

1. Confirm the current Git branch and repository status.
2. Work from `milestone/m001-complete-game` unless the owner directs otherwise.
3. Locate the licensed asset source. Preferred repository-local path after the owner copies it into the clone:
   - `LocalDependencies\AssetPack_ProjectSettings.zip`
4. If it is not there, check the owner's supplied source location:
   - `C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip`
5. Verify the dependency SHA-256 against `docs/ASSET_PACK_INVENTORY.md` before importing.
6. Confirm Unity **5.4.1f1** is installed. Do not open/save the project in a newer Unity version.
7. Detect the Xbox 360 Unity/XDK toolchain and document it in `docs/BUILD_ENVIRONMENT.md`.
8. Import the licensed package locally without committing paid source assets.
9. Inspect the imported demo scenes, prefabs and scripts before changing architecture.
10. Execute Milestone 001 phases in order unless a dependency makes a later phase necessary first. Document any reordering.

## Mandatory design intent

Marine Slayer is a controller-first, top-down sci-fi action shooter with:

- tactical movement and readable positioning;
- aggressive forward combat;
- dodge/roll mobility;
- ranged + melee combat;
- grenades;
- escalating arenas;
- distinct enemy archetypes;
- resource drops and pickups;
- environmental hazards;
- boss encounters;
- campaign progression;
- original story and dialogue.

Do not turn the game into an FPS, third-person shoulder shooter, twin-stick arcade score-only game, tower-defense game or multiplayer-first project.

## Asset-use policy

The supplied package already includes the core visual and gameplay foundation. Treat it as the game's licensed production kit.

Do not publish the raw paid package to GitHub. Do not replace it with random internet assets. New art/audio needed for completion must be project-owned, licensed for redistribution, generated from original/project-owned material, or deliberately represented by a clearly documented placeholder until the owner supplies final content.

## Story authority

Codex may implement and expand the original Marine Slayer story defined in `docs/STORY_DIALOGUE_BIBLE.md`.

Dialogue delivery should be implemented as text/subtitles/comms first. Do not block the game on recorded voice acting.

## Completion behavior

Keep implementing across adjacent phases where safe. Avoid asking for routine confirmation after every small subsystem.

Stop and request owner involvement only when genuinely necessary, such as:

- console/XDK authentication or credentials;
- a destructive operation that cannot be safely inferred;
- a licensing choice requiring the owner's authority;
- physical Xbox 360 interaction that Codex cannot perform;
- a design conflict not resolved by the authoritative documents.

Otherwise, make the best evidence-based implementation decision, document it, and continue.

## Current target

Execute:

`docs/MILESTONE_001_COMPLETE_GAME.md`

The milestone is complete only when the Xbox 360 build can progress from boot to final credits without a blocker and the release acceptance matrix passes.