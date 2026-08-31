# AGENTS.md — Marine Slayer Codex Contract

This file is the mandatory repository-level instruction contract for Codex.

## 1. Project objective

Build **Marine Slayer from scratch** as a complete, polished, beginning-to-end top-down sci-fi action shooter for **Xbox 360 using Unity 5.4.1f1**.

This is a new production build. Do not continue, migrate, patch, import, or depend on any earlier Marine Slayer Unity project or prior playable build.

The finished game must be based on:

1. the owner-supplied **Marine Slayer Complete Lore Bible**;
2. the owner-supplied **Marine Slayer Visual Lore Companion**;
3. the owner-supplied paid Unity asset pack;
4. the YouTube gameplay video only as a **play-style reference**.

Do not treat the project as a prototype, tech demo, vertical slice, asset-pack showcase, or continuation of an older Marine Slayer build.

## 2. Source-of-truth order

When instructions conflict, use this order:

1. explicit current project-owner instruction;
2. this `AGENTS.md`;
3. `docs/OWNER_CANON_SOURCE.md`;
4. `docs/MILESTONE_001_COMPLETE_GAME.md`;
5. `docs/XBOX360_PERFORMANCE_CONTRACT.md`;
6. `docs/TECHNICAL_ARCHITECTURE.md`;
7. `docs/ASSET_PACK_INVENTORY.md`;
8. `docs/PLAYSTYLE_REFERENCE.md`;
9. other repository documentation.

The lore documents are the **creative/content authority**. The play-style video is not.

Do not silently reconcile contradictions by inventing a hybrid. Follow the higher-authority source and update lower-authority notes when needed.

## 3. From-scratch rule — NON-NEGOTIABLE

Codex must create a new Unity 5.4.1f1 project for Marine Slayer inside this repository.

Codex MUST NOT:

- locate or import a previous Marine Slayer Unity project;
- copy prior Marine Slayer scenes;
- copy prior Marine Slayer scripts;
- copy prior Marine Slayer HUD/UI prefabs;
- copy prior keycard/objective implementations;
- copy prior dialogue systems or dialogue text from an older build;
- copy prior AI/spawner implementations;
- treat the YouTube footage as proof that a previous implementation must be preserved;
- search the owner's drives for an older Marine Slayer project unless the owner explicitly asks for that in a future instruction.

If old Marine Slayer files happen to exist locally, ignore them.

The only pre-existing production material intentionally supplied for reuse is the **licensed Unity asset pack**.

## 4. Engine/platform lock

- Unity editor version: **5.4.1f1**.
- Primary runtime target: **Xbox 360**.
- Windows/Editor builds are development conveniences, not the shipping authority.
- Do not upgrade Unity.
- Do not migrate to URP/HDRP.
- Do not introduce packages/APIs requiring a newer Unity version.
- Do not replace Xbox 360 with Xbox One, UWP, modern .NET, or a contemporary Unity console workflow.

Codex must discover the owner's real Xbox 360 Unity/XDK toolchain rather than guessing paths or commands.

## 5. Canon authority

The owner-supplied lore documents define the game world and campaign.

Core canon includes:

- **Eidolon Station**;
- **Project ASCENDANT**;
- **UEMF**;
- **ASC-9 Neural Mesh**;
- **VANGUARD**;
- **ASCENDANT PRIME 4.2.U**;
- **The Convergence**;
- protagonist **Lieutenant Rhyker Voss**;
- the canonical enemy hierarchy;
- the canonical named weapons/tools;
- the station zones;
- the complete **5-Act / 25-Level campaign**;
- canonical bosses;
- major cinematics;
- optional logs/reports;
- the three ending concepts;
- DLC hooks as future-only material.

Codex may write implementation dialogue, objective text, tutorials, barks, transition scenes, and missing connective material only when needed to implement the lore. New writing must remain consistent with the documents and must not replace established canon.

## 6. Licensed asset-pack rule

The supplied paid asset pack is a **local licensed dependency** selected by the owner for use in the game.

Expected local source:

`C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip`

Repository-standard local staging path:

`LocalDependencies\AssetPack_ProjectSettings.zip`

The raw paid package and imported vendor source assets must NOT be committed to this public repository unless the owner explicitly provides redistribution authorization for the raw sources.

Codex may import and use those assets locally to create Marine Slayer and produce distributable game builds consistent with the owner's license.

## 7. Asset-pack implementation rule

The project starts from scratch, but Codex should make full practical use of the supplied asset pack.

Before building a subsystem, inventory what the pack provides. Reuse, wrap, refactor, or extend suitable vendor content such as:

- top-down character/controller foundations;
- weapons;
- enemy characters;
- mechs/drones;
- environment modules;
- doors;
- pickups;
- turrets;
- traps;
- VFX;
- animations;
- demo/reference scenes;
- useful scripts.

Using asset-pack code/assets is permitted because the owner supplied the pack specifically for this new build.

Do not confuse **asset-pack reuse** with **old Marine Slayer project reuse**.

## 8. YouTube play-style reference rule

Reference video:

https://youtu.be/cbfpxTSKEg4

Use gameplay from approximately **04:00 onward** only as a visual/play-feel reference.

The video may guide abstract targets such as:

- elevated top-down/oblique camera framing;
- controller-first movement and aiming;
- readable room/corridor combat;
- twin-stick-like directional combat feel;
- combat pacing;
- approximate player-to-environment scale;
- Xbox 360-appropriate visual density;
- readable projectile/impact feedback;
- tactical room-to-room action.

The video MUST NOT be treated as a source for:

- code;
- scenes;
- prefabs;
- HUD layout requirements;
- inventory implementation requirements;
- keycard systems;
- exact objective structure;
- exact dialogue;
- exact enemy AI architecture;
- save/checkpoint implementation;
- canon that is absent from the lore documents.

The goal is to capture a similar **style of play**, not reproduce the old build.

## 9. Complete-game rule

Milestone 001 targets the complete base campaign described in the lore documents:

**5 Acts / 25 Levels.**

The required release path includes at minimum:

- boot/splash;
- main menu;
- New Game / Continue;
- difficulty selection if retained by final design;
- Acts I–V;
- Levels 01–25;
- checkpoints and persistent progression;
- player movement/aim/combat;
- canonical weapon progression;
- canonical enemy progression;
- boss encounters;
- lore-consistent dialogue/objectives;
- optional terminal/log framework;
- HUD;
- pause/options;
- death/restart;
- ending selection/state for the three canonical ending concepts;
- credits;
- Xbox 360 controller-first usability;
- Xbox 360 build/deploy pipeline;
- target-hardware test evidence.

No feature is considered complete solely because it works in the Unity Editor.

## 10. Performance-first implementation

Xbox 360 limitations override visual excess.

Prefer:

- baked lighting where practical;
- controlled realtime lights;
- LODs;
- object pooling;
- bounded enemy counts;
- reusable materials;
- texture-size discipline;
- predictable allocations;
- minimal per-frame garbage;
- low-overhead AI updates;
- scene segmentation where needed;
- deterministic encounter cleanup.

Do not trade stable Xbox 360 performance for desktop-only visual improvements.

## 11. No hidden assumptions

Before implementing a major system, inspect:

- the lore authority;
- the relevant milestone phase;
- the licensed asset-pack capability;
- Unity 5.4.1f1 compatibility;
- Xbox 360 implications.

If an implementation detail is not specified by the lore or supplied assets, choose the smallest sensible implementation that supports the game and document the choice.

Do not invent SDK paths, XDK commands, credentials, or unsupported APIs.

## 12. Local toolchain discovery

At bootstrap, detect and document the actual local environment, including where available:

- Unity 5.4.1f1;
- Xbox 360 Unity support/plugin;
- Visual Studio/XDK integration;
- Xbox 360 XDK location/version;
- build/deploy/debug tools;
- profiling tools.

Record non-secret findings in `docs/BUILD_ENVIRONMENT.md`.

Never commit secrets, certificates, proprietary SDK contents, or paid vendor source packages.

## 13. Git workflow

GitHub is the project memory.

- Work from the active milestone branch.
- Commit meaningful checkpoints.
- Keep `main` stable.
- Update milestone status after major phases.
- Do not commit Unity `Library/`, `Temp/`, build outputs, local XDK files, or paid dependency source assets.

## 14. Testing cadence

Do not interrupt development for trivial manual hardware testing after every small change.

Use Editor/Windows builds for rapid validation where useful. Batch Xbox 360 testing at meaningful gates defined by the milestone and test matrix.

Target-hardware validation is mandatory before release completion.

## 15. Definition of done

Marine Slayer is complete only when the newly created Unity 5.4.1f1 project can be built for Xbox 360 and played from:

**Boot → Main Menu → Act I / Level 01 → all 25 levels → Prime Convergence → canonical ending flow → Credits**

without progression blockers, with working controller input, checkpoints/persistence, acceptable Xbox 360 performance, and required release tests passing.
