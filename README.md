# Marine Slayer

**Marine Slayer** is a from-scratch top-down sci-fi action/horror game targeting **Xbox 360** through **Unity 5.4.1f1**.

## Project inputs

The game is built from three clearly separated inputs:

1. **Owner lore documents** — define the game itself: Rhyker Voss, Eidolon Station, Project ASCENDANT, VANGUARD, the Convergence, canonical enemies/weapons, five Acts, twenty-five levels, bosses, cinematics, logs and endings.
2. **Owner-supplied licensed Unity asset pack** — provides the production asset/code foundation. Raw paid vendor source remains local and is not stored in this public repository.
3. **Gameplay reference video** — https://youtu.be/cbfpxTSKEg4 from approximately 04:00 onward, used only as a reference for top-down camera/control/combat feel. It is not an inherited project or source of implementation.

## Scratch-build rule

Codex must create a **new Unity 5.4.1f1 project** in this repository.

Do not continue, import, migrate, search for, or depend on any earlier Marine Slayer Unity project.

Use the licensed asset pack, but do not reuse old Marine Slayer scenes/scripts/UI/objective logic from another project.

## Primary target

- Engine: Unity 5.4.1f1
- Primary platform: Xbox 360
- Secondary development target: Windows Editor/standalone for iteration only
- Input: Xbox 360 controller first
- Camera: elevated top-down / tactical action camera
- Core mode: single-player campaign
- Campaign size: 5 Acts / 25 Levels
- Secondary mode: local co-op/challenge only after complete campaign stability

## Start here

Codex must read, in order:

1. `AGENTS.md`
2. `CODEX_START_HERE.md`
3. `docs/OWNER_CANON_SOURCE.md`
4. `docs/ASSET_PACK_INVENTORY.md`
5. `docs/PLAYSTYLE_REFERENCE.md`
6. `docs/XBOX360_PERFORMANCE_CONTRACT.md`
7. `docs/TECHNICAL_ARCHITECTURE.md`
8. `docs/MILESTONE_001_COMPLETE_GAME.md`
9. `docs/TEST_MATRIX.md`
10. `MILESTONE_STATUS.md`

## Canonical campaign

The base game follows the Lore Bible's progression:

- Act I — **The Awakening** — Levels 01–05
- Act II — **Into the Depths** — Levels 06–10
- Act III — **Engineering the Damned** — Levels 11–15
- Act IV — **Command & Catastrophe** — Levels 16–20
- Act V — **The Spire** — Levels 21–25

Final level: **Core Ascendant — Prime Convergence**.

## Creative direction

The game should deliver readable, controller-first top-down combat with aggressive action momentum, while remaining faithful to the owner-created Marine Slayer lore.

Commercial titles may inspire abstract qualities such as tactical readability, pressure and pacing, but Codex must not copy protected characters, maps, story expression, UI, audio, models, names or other commercial content.

## Repository policy

GitHub is the permanent project memory and source of truth.

Commit:

- Marine Slayer-owned source code;
- new scenes;
- project-owned UI/data;
- documentation;
- tests;
- configuration;
- redistributable project-owned content.

Do not commit:

- raw paid vendor asset packages/source assets unless explicitly authorized;
- Unity `Library/`/`Temp/`;
- XDK/SDK files;
- credentials/certificates;
- Xbox build outputs unless intentionally released through an appropriate artifact/release path.

## Definition of success

Milestone 001 is a complete-game milestone.

Success means the new Unity 5.4.1f1 Xbox 360 build can be played:

**Boot -> Main Menu -> Levels 01–25 -> Prime Convergence -> Canonical Ending -> Credits**

with working controller input, campaign progression/checkpoints, acceptable target performance and no progression blocker.
