# CODEX START HERE — Marine Slayer

## Mission

Build **Marine Slayer from scratch** as a complete Xbox 360 game using **Unity 5.4.1f1**.

Do not continue or recover an earlier Marine Slayer project. The new build must be created inside this repository from a fresh Unity project.

## The three project inputs

### 1. Lore / content authority

Use the owner-supplied Marine Slayer lore documents as the game specification:

- **Marine Slayer — Complete Lore Bible**
- **Marine Slayer — Visual Lore Companion**

Preferred local paths inside the repository clone:

- `LocalReferences\Marine_Slayer_Lore_Bible.docx`
- `LocalReferences\Marine_Slayer_Lore_Companion.pdf`

These files are owner-local references and are intentionally ignored by Git. If they are not present, do not invent replacements or reconstruct the lore from memory. Ask the owner to place the supplied documents at those paths or use the repository canon summary until the source documents are available.

Repository canon summary/implementation authority:

`docs/OWNER_CANON_SOURCE.md`

These documents define the world, protagonist, antagonist, enemies, weapons, station zones, five acts, twenty-five levels, cinematics, audio-log concepts, bosses and endings.

### 2. Licensed Unity asset pack

Use the owner-supplied paid Unity asset pack as the production asset foundation.

Preferred local path:

`LocalDependencies\AssetPack_ProjectSettings.zip`

Owner workstation source/fallback:

`C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip`

Verify its hash against `docs/ASSET_PACK_INVENTORY.md` before import.

Do not commit the raw paid package or imported vendor source assets to this public repository.

### 3. Gameplay feel reference

Reference video:

https://youtu.be/cbfpxTSKEg4

Gameplay begins at approximately **04:00**.

Read:

`docs/PLAYSTYLE_REFERENCE.md`

Use the video only to understand the desired **top-down camera, control feel, combat readability and pacing**.

Do not copy or preserve the old video's code, scenes, HUD, dialogue, objectives, inventory, keycards, AI implementation or other implementation details unless the lore documents independently require them.

## First-run workflow

1. Confirm repository and active branch.
2. Read `AGENTS.md` in full.
3. Read `docs/OWNER_CANON_SOURCE.md`.
4. Confirm the two owner lore files exist under `LocalReferences\` and use them as the detailed content authority.
5. Read `docs/ASSET_PACK_INVENTORY.md`.
6. Read `docs/PLAYSTYLE_REFERENCE.md`.
7. Read `docs/XBOX360_PERFORMANCE_CONTRACT.md`.
8. Read `docs/TECHNICAL_ARCHITECTURE.md`.
9. Read `docs/MILESTONE_001_COMPLETE_GAME.md`.
10. Read `docs/TEST_MATRIX.md`.
11. Confirm Unity **5.4.1f1** is installed.
12. Detect the local Xbox 360 Unity/XDK toolchain and document factual paths/workflows in `docs/BUILD_ENVIRONMENT.md`.
13. Verify/stage the licensed asset pack locally.
14. Create a **new Unity 5.4.1f1 Marine Slayer project** in this repository.
15. Import the licensed asset pack into that new project.
16. Audit which vendor assets/scripts are appropriate for production use.
17. Begin Milestone 001 from its bootstrap phase.

## Hard prohibition on old-project continuation

Do NOT:

- search for an old Marine Slayer project;
- import an old Marine Slayer `Assets/` folder;
- copy old Marine Slayer scripts/scenes/prefabs;
- use the YouTube build as a production source;
- assume old gameplay systems are mandatory because they appear in the footage.

If old Marine Slayer files are encountered incidentally, ignore them unless the owner later explicitly authorizes reuse.

## Creative target

The game must implement the owner lore:

- Lieutenant **Rhyker Voss**;
- **Eidolon Station**;
- **Project ASCENDANT**;
- **VANGUARD**;
- **The Convergence**;
- the canonical enemies and weapons;
- **5 Acts / 25 Levels**;
- The Red Engineer, Null Sister, Commander Sol encounter, and Prime Convergence;
- three ending concepts;
- optional logs/terminals where practical.

The YouTube reference should influence only the play feel: elevated top-down action, controller-first movement/aiming, readable corridors/rooms and aggressive tactical combat.

## Completion behavior

Continue through adjacent work where safe. Do not stop after every minor feature for owner testing.

Request owner involvement only for genuine blockers such as:

- console/XDK authentication;
- physical Xbox 360 interaction;
- licensing questions requiring owner authority;
- destructive choices not resolved by repository rules.

## Current target

Execute:

`docs/MILESTONE_001_COMPLETE_GAME.md`

Milestone 001 is complete only when the new Xbox 360 build can progress:

**Boot → Main Menu → Levels 01–25 → Prime Convergence → Ending → Credits**

with controller-first play, working progression/checkpoints, acceptable target performance and the mandatory release tests passing.
