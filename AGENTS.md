# AGENTS.md — Marine Slayer Codex Contract

This file is the mandatory repository-level instruction contract for Codex.

## 1. Project objective

Build **Marine Slayer** into a complete, polished, beginning-to-end top-down sci-fi action shooter for **Xbox 360 using Unity 5.4.1f1**.

Do not treat the project as a prototype, tech demo, vertical slice or asset-pack showcase. Milestone 001 targets a complete game.

## 2. Authority order

When instructions conflict, use this order:

1. explicit current project-owner instruction;
2. this `AGENTS.md`;
3. `docs/MILESTONE_001_COMPLETE_GAME.md`;
4. `docs/XBOX360_PERFORMANCE_CONTRACT.md`;
5. `docs/TECHNICAL_ARCHITECTURE.md`;
6. `docs/GAME_DESIGN_BIBLE.md`;
7. `docs/STORY_DIALOGUE_BIBLE.md`;
8. `docs/ASSET_PACK_INVENTORY.md`;
9. other repository documentation.

Do not silently resolve contradictions. Document any unavoidable conflict in the milestone status log.

## 3. Engine/platform lock

- Unity editor version is **5.4.1f1** unless the owner explicitly changes it.
- Xbox 360 is the primary runtime target.
- Windows/Editor builds are development conveniences, not the shipping authority.
- Do not upgrade Unity, migrate to URP/HDRP, use packages requiring newer Unity, or introduce APIs unavailable in Unity 5.4.1f1.
- Do not replace the Xbox 360 target with Xbox One, UWP, modern .NET, or a contemporary Unity console workflow.

## 4. Licensed asset-pack rule

The supplied paid asset pack is a **local licensed dependency**.

Expected original local source on the owner's workstation:

`C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip`

Repository-standard local copy location:

`LocalDependencies\AssetPack_ProjectSettings.zip`

The package must NOT be committed to this public repository unless the owner later supplies explicit proof that raw source redistribution is permitted.

Do not upload or redistribute the raw `.unitypackage`, original paid models, textures, source animations, source materials or other paid source files through GitHub.

Codex may import and use those assets locally to create the game and build distributable game outputs consistent with the owner's license.

## 5. Asset-pack first rule

Before creating replacement systems, inventory what the licensed pack already provides.

The pack contains usable top-down player, weapons, enemy AI, soldier/mech/drone characters, station environment modules, doors, pickups, turrets, traps, VFX, animations, local multiplayer examples and demo scenes.

Reuse, repair, refactor and extend those systems where appropriate.

Do NOT blindly preserve demo code if it is unsuitable for Xbox 360 performance or production reliability.

Do NOT rebuild working functionality merely to make the code look newer.

## 6. Creative direction rule

High-level inspiration may come from tactical top-down military shooters and modern high-intensity arena shooters. The final game must be original.

Do NOT copy from Killzone, Doom or any other commercial game:

- characters;
- names;
- dialogue;
- story beats in substantially similar expression;
- maps or encounter layouts;
- art/audio;
- logos;
- UI layouts;
- enemy designs;
- proprietary code;
- trademarked branding.

Use only abstract design principles such as tactical readability, combat pressure, fast weapon switching, aggressive enemy waves, arena escalation and forward momentum.

## 7. Complete-game rule

The required release path includes at minimum:

- boot/splash flow;
- main menu;
- New Game / Continue;
- difficulty selection;
- complete campaign;
- multiple distinct missions;
- checkpoints and persistent progression;
- complete player movement/combat;
- weapon progression;
- enemy roster and encounter escalation;
- boss encounters;
- story/dialogue delivery;
- HUD;
- pause menu;
- options;
- death/restart flow;
- ending;
- credits;
- Xbox 360 controller-first usability;
- Xbox 360 build pipeline;
- target-hardware test evidence.

No feature is considered complete solely because it works in the Unity Editor.

## 8. Story/dialogue authority

Codex is authorized to write original story material and dialogue for Marine Slayer within `docs/STORY_DIALOGUE_BIBLE.md` and project data files.

Preserve established canon once committed unless a contradiction or implementation blocker is found. Do not casually rewrite previously approved story content from milestone to milestone.

Dialogue must support subtitles by default. Voice acting is not required for Milestone 001 unless licensed recordings are supplied later.

## 9. Performance-first implementation

Xbox 360 limitations override visual excess.

Prefer:

- baked lighting where practical;
- controlled realtime light counts;
- LODs already supplied by the pack;
- object pooling;
- bounded enemy counts;
- reusable materials;
- texture-size discipline;
- predictable allocations;
- minimal per-frame garbage;
- low-overhead AI update scheduling;
- scene segmentation where needed;
- deterministic encounter cleanup.

Do not trade stable Xbox 360 performance for desktop-only visual improvements.

## 10. No hidden assumptions

Before implementing a major system, inspect:

- current repository state;
- relevant milestone section;
- asset-pack capability;
- Unity 5.4.1f1 compatibility;
- Xbox 360 implications.

If an implementation detail is unknown, investigate the local environment or document the assumption explicitly. Do not invent SDK paths, XDK commands, title IDs, signing credentials or unsupported Unity APIs.

## 11. Local toolchain discovery

At project bootstrap, detect and document the owner's actual local environment, including where available:

- Unity 5.4.1f1;
- Xbox 360 Unity support module/plugin;
- Visual Studio/XDK integration;
- Xbox 360 XDK location;
- console deployment/debug tools;
- build/deploy commands;
- target console connection details supplied interactively by the owner.

Record non-secret environment findings in `docs/BUILD_ENVIRONMENT.md`.

Never commit secrets, console credentials, private certificates or proprietary SDK contents.

## 12. Git workflow

GitHub is the project memory.

- Work from a milestone branch.
- Commit meaningful checkpoints.
- Keep `main` stable.
- Update milestone status when a major phase completes.
- Do not erase working code without understanding why it exists.
- Do not commit Unity `Library/`, `Temp/`, build outputs, local XDK files or paid dependency source packages.

## 13. Testing cadence

Do not interrupt implementation for trivial manual hardware testing after every small change.

Batch related systems into meaningful test checkpoints. Use Editor/Windows testing for rapid iteration, then perform target-console tests at the gates defined in `docs/MILESTONE_001_COMPLETE_GAME.md` and `docs/TEST_MATRIX.md`.

Major target-hardware testing is mandatory before declaring a phase release-ready.

## 14. Definition of done

A task is done only when its implementation, integration, failure handling, documentation and relevant tests are complete.

Milestone 001 is done only when the entire campaign can be started, played through and completed on the Xbox 360 target build without progression blockers, with acceptable performance and all mandatory release flows functioning.