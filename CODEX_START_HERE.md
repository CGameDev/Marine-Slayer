# CODEX START HERE — Marine Slayer

## Mission

Build Marine Slayer into a **complete Xbox 360 game**, not a demo.

Before making project changes, read all required documents listed in `README.md`, beginning with `AGENTS.md`.

The current playable game is **not a blank slate**. Treat the footage from **04:00 onward** in https://youtu.be/cbfpxTSKEg4 as the inherited gameplay baseline and read `docs/CURRENT_GAMEPLAY_BASELINE.md` before altering the existing player, camera, combat, HUD, inventory, AI/spawn logic, keycards, doors or Mission 01.

## First-run workflow

1. Confirm the current Git branch and repository status.
2. Work from `milestone/m001-complete-game` unless the owner directs otherwise.
3. Read `AGENTS.md`.
4. Read `docs/CURRENT_GAMEPLAY_BASELINE.md` and note the current-game preservation requirements.
5. Locate the licensed asset source. Preferred repository-local path after the owner copies it into the clone:
   - `LocalDependencies\AssetPack_ProjectSettings.zip`
6. If it is not there, check the owner's supplied source location:
   - `C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip`
7. Verify the dependency SHA-256 against `docs/ASSET_PACK_INVENTORY.md` before importing.
8. Confirm Unity **5.4.1f1** is installed. Do not open/save the project in a newer Unity version.
9. Detect the Xbox 360 Unity/XDK toolchain and document it in `docs/BUILD_ENVIRONMENT.md`.
10. Import the licensed package locally without committing paid source assets.
11. Inspect the existing Marine Slayer scenes/project content and imported demo scenes, prefabs and scripts before changing architecture.
12. Identify which local scene corresponds to the current first-level gameplay shown in the owner's video.
13. Record the implementing player/camera/HUD/inventory/keycard/door/enemy/spawner scripts and prefabs before replacing or restructuring any of them.
14. Execute Milestone 001 phases in order unless a dependency makes a later phase necessary first. Document any reordering.

## Mandatory inherited gameplay identity

The current build already establishes:

- **Operative Voss** as the player identity;
- a cryogenic-interruption/cryobay opening;
- a station/computer AI assisting Voss;
- **Thralls** as an established hostile family;
- Blue Armory, Yellow Sick Bay and Red Research Lab access/keycard progression;
- a Research Lab path tied to opening/restoring the shuttle-bay route;
- an elevated top-down camera;
- responsive controller movement/aiming;
- working firearm combat;
- current HUD/radar/status UI;
- an inventory screen;
- doors/access interactions;
- patrol/path and horde/free-roam enemy behavior concepts;
- checkpoint-based recovery as the intended persistence direction.

Known current defects such as wall-clipping enemies, invalid/out-of-bounds spawning and performance hitching are **bugs to fix**, not identity to preserve.

## Mandatory design intent

Marine Slayer is a controller-first, top-down sci-fi action shooter with:

- tactical movement and readable positioning;
- aggressive forward combat;
- dodge/roll mobility where it integrates cleanly with the existing controls;
- ranged + melee combat;
- grenades;
- escalating arenas;
- distinct enemy archetypes;
- resource drops and pickups;
- environmental hazards;
- boss encounters;
- campaign progression;
- original story and dialogue that expands rather than erases current canon.

Do not turn the game into an FPS, third-person shoulder shooter, twin-stick arcade score-only game, tower-defense game or multiplayer-first project.

## Asset-use policy

The supplied package already includes the core visual and gameplay foundation. Treat it as the game's licensed production kit.

Do not publish the raw paid package to GitHub. Do not replace it with random internet assets. New art/audio needed for completion must be project-owned, licensed for redistribution, generated from original/project-owned material, or deliberately represented by a clearly documented placeholder until the owner supplies final content.

## Story authority

Codex may implement and expand the Marine Slayer story defined in `docs/STORY_DIALOGUE_BIBLE.md`, but `docs/CURRENT_GAMEPLAY_BASELINE.md` is authoritative over older invented story details when the two conflict.

Do not silently rename or replace Operative Voss, Thralls, the cryobay opening, the station AI relationship or Mission 01's established access/shuttle structure.

Dialogue delivery should be implemented as text/subtitles/comms first. Existing approved/in-project dialogue should be preserved unless the owner asks for a rewrite or technical constraints require a documented change.

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

with the mandatory gameplay amendments in:

`docs/CURRENT_GAMEPLAY_BASELINE.md`

The milestone is complete only when the Xbox 360 build can progress from boot to final credits without a blocker and the release acceptance matrix passes.