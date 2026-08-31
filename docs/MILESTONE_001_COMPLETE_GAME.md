# MILESTONE 001 — MARINE SLAYER COMPLETE XBOX 360 GAME

## Milestone classification

**Type:** full production milestone

**Primary target:** Xbox 360

**Engine:** Unity 5.4.1f1

**Repository:** `CGameDev/Marine-Slayer`

**Active branch:** `milestone/m001-complete-game`

**Goal:** produce a complete, beginning-to-end, replayable Marine Slayer campaign rather than a prototype or asset-pack demonstration.

---

# 0. NON-NEGOTIABLE PROJECT DIRECTIVE

Codex must not reinterpret the assignment as any of the following:

- a vertical slice;
- a one-level proof of concept;
- a Unity tutorial cleanup;
- an FPS;
- an over-the-shoulder shooter;
- a tower-defense title;
- an asset browser;
- a multiplayer-only project;
- a PC game that happens to compile on Xbox 360.

The required product is a **complete top-down Xbox 360 action shooter** with twelve campaign missions, original story/dialogue, progression, bosses, menus, save/checkpoint flow, options, ending/credits and target-hardware validation.

The supplied licensed asset pack is the production foundation selected by the owner.

The raw paid package remains local and must not be committed to the public GitHub repository.

---

# 1. REQUIRED READING ORDER

Before implementation, read:

1. `AGENTS.md`
2. `CODEX_START_HERE.md`
3. `MILESTONE_STATUS.md`
4. `docs/ASSET_PACK_INVENTORY.md`
5. `docs/GAME_DESIGN_BIBLE.md`
6. `docs/STORY_DIALOGUE_BIBLE.md`
7. `docs/TECHNICAL_ARCHITECTURE.md`
8. `docs/XBOX360_PERFORMANCE_CONTRACT.md`
9. this milestone
10. `docs/TEST_MATRIX.md`

Codex must not implement a major subsystem based only on the milestone title.

---

# 2. CORE DELIVERY CONTRACT

Milestone M001 is complete only when a fresh player can:

1. boot the Xbox 360 build;
2. reach the main menu;
3. start New Game;
4. choose difficulty;
5. play Mission 01;
6. progress through Missions 02–12;
7. die/retry from checkpoints safely;
8. save and Continue across sessions;
9. encounter the full weapon/enemy/boss progression;
10. receive the main narrative through subtitles/comms/objectives;
11. defeat the final boss;
12. complete the extraction/ending;
13. reach credits;
14. return to a valid menu/post-completion state;
15. reload a completed-campaign save without corruption.

No missing middle mission may be represented by a menu button, placeholder text or instant scene-complete trigger.

---

# 3. CREATIVE / IP BOUNDARY

The owner requested inspiration from tactical top-down military shooters and modern Doom-era high-intensity combat.

Codex may use abstract principles such as:

- elevated tactical camera;
- readable positioning;
- aggressive forward momentum;
- dodge/roll evasion;
- weapon swapping;
- escalating arenas;
- resource recovery;
- enemy-role combinations;
- boss phases.

Codex must not copy commercial-game expressive content such as protected levels, characters, lore, names, dialogue, logos, audio, UI layouts, enemy models or maps.

Marine Slayer canon is defined in `STORY_DIALOGUE_BIBLE.md`.

---

# 4. DEVELOPMENT BEHAVIOR

## 4.1 Continue through related work

Codex should complete multiple related implementation phases before requesting major owner testing where safe.

Do not repeatedly stop after trivial changes to ask the owner to test one button, one weapon or one script.

Use Editor/Windows builds for rapid validation and batch Xbox 360 testing at the gates in this document.

## 4.2 Do not hide blockers

If a real blocker occurs:

- document it;
- implement everything that is not blocked;
- isolate the blocked item;
- continue adjacent work where safe.

Examples of legitimate owner-dependent blockers:

- XDK login/deployment authorization;
- physical console interaction;
- missing licensed dependency;
- license question requiring owner confirmation.

## 4.3 No invented environment details

Discover actual:

- Unity path;
- XDK path/version;
- build workflow;
- deploy workflow;
- console tooling.

Populate `docs/BUILD_ENVIRONMENT.md` with facts.

---

# M00 — REPOSITORY, TOOLCHAIN & DEPENDENCY VERIFICATION

## Objective

Establish a reproducible local development environment without changing game content prematurely.

## Tasks

### M00.1 Repository state

- Clone/pull `CGameDev/Marine-Slayer`.
- Confirm `main` is clean.
- Check out/create `milestone/m001-complete-game`.
- Read repository instructions.
- Record starting commit SHA in milestone log.

### M00.2 Licensed dependency

Locate:

`LocalDependencies\AssetPack_ProjectSettings.zip`

or fallback owner source:

`C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip`

Verify SHA-256:

`f27f1bf3bad614b829efa530cdd9247c52b69bc539bcfa987dd68ce59da4effb`

Verify embedded unitypackage SHA-256 where practical:

`e8569bd920b3dab4da2304f8b2c6b5006a95d27a16e2f597325a4288a59c62ea`

If hash differs, do not silently substitute it. Record the difference and inspect before import.

### M00.3 Unity

Confirm exact editor:

`5.4.1f1`

Do not open/save the production project in a newer Unity version.

### M00.4 Xbox support/XDK

Discover:

- Xbox 360 build target availability;
- XDK version;
- Visual Studio integration;
- deploy/debug tools;
- build/deploy commands or GUI steps;
- profiling tools.

Populate `docs/BUILD_ENVIRONMENT.md`.

### M00.5 Local dependency protection

Verify `.gitignore` prevents:

- `.unitypackage`;
- `LocalDependencies/*` except README;
- XDK/SDK folders;
- XEX build outputs;
- secrets/certificates.

## Deliverables

- updated `docs/BUILD_ENVIRONMENT.md`;
- initial milestone log/checkpoint commit;
- confirmed local dependency hash;
- confirmed Unity 5.4.1f1 path;
- confirmed or clearly documented Xbox toolchain status.

## Gate M00

PASS when Codex knows how the local project will be opened, built and eventually deployed without guessing.

---

# M01 — ASSET IMPORT, VENDOR AUDIT & CLEAN BASELINE

## Objective

Import the licensed production kit locally and determine which supplied systems should be retained, refactored or replaced.

## M01.1 Prepare project

- Create/open Marine Slayer Unity 5.4.1f1 project in repository.
- Preserve repository docs.
- Establish `Assets/MarineSlayer/` structure.
- Do not move vendor assets after import without reason.

## M01.2 Import package

Import `Xbox360TutorialAssets.unitypackage` from the local licensed ZIP.

ProjectSettings from the supplied archive may be used as reference/input foundation, but do not overwrite repository settings blindly.

Back up/compare before replacing any settings.

## M01.3 Import health check

After import:

- allow scripts to compile;
- record compile errors;
- record warnings relevant to Unity 5.4/Xbox;
- open core demo scenes;
- verify player prefab;
- verify rifle fire;
- verify enemy AI;
- verify camera;
- verify pickup;
- verify doors;
- verify drone/mech prefabs.

## M01.4 Vendor audit

Create `docs/ASSET_IMPORT_REPORT.md` containing a table for each important vendor system:

```text
System
Source file(s)
Works as-is? yes/no/partial
Demo coupling
Xbox risk
Allocation/performance concern
Production decision: KEEP / WRAP / REFACTOR / REPLACE
Reason
```

At minimum audit:

- `PrTopDownCharController`;
- `PrTopDownCharInventory`;
- `PrTopDownCamera`;
- `PrEnemyAI`;
- `PrEnemySpawner`;
- `PrWeapon`;
- `PrWeaponList`;
- `PrBullet`;
- pickups;
- doors;
- traps;
- turrets;
- waypoints;
- VFX;
- local multiplayer framework.

## M01.5 Build settings cleanup

The archived EditorBuildSettings references missing legacy/example scenes.

Do not preserve those as final build entries.

Create a clean Marine Slayer scene plan and remove demo/test scenes from the shipping build list.

## M01.6 First clean baseline

Create a Marine Slayer-owned sandbox scene:

`Assets/MarineSlayer/Scenes/Test/MS_Test_CombatSandbox.unity`

It should include:

- one player;
- top-down camera;
- station room;
- rifle;
- one enemy soldier;
- one health pickup;
- one ammo pickup;
- one door;
- minimal HUD debug/readout if needed.

This scene proves the imported assets can be used outside their original demo scene.

## Gate M01

PASS when a Marine Slayer-owned test scene compiles and plays with imported assets and Codex has documented what vendor code will be used.

---

# M02 — CORE RUNTIME / STATE / INPUT / SAVE FOUNDATION

## Objective

Create the stable systems every campaign mission will share.

## M02.1 Game root

Implement a controlled persistent root containing only necessary services.

Required concepts:

- game state;
- input;
- save/progression;
- dialogue;
- audio;
- platform abstraction.

Prevent duplicate persistent objects after scene transitions.

## M02.2 State machine

Implement states:

- Boot;
- MainMenu;
- Loading;
- Playing;
- Paused;
- PlayerDead;
- MissionComplete;
- CampaignComplete;
- Credits.

Centralize input/time-state transitions.

## M02.3 Input wrapper

Wrap the existing InputManager mappings.

Requirements:

- movement;
- aim;
- fire;
- reload;
- dodge/roll;
- interact;
- melee;
- grenade;
- weapon switch;
- pause.

Do not finalize physical button mapping until existing Xbox controller behavior is tested.

## M02.4 Save schema

Create versioned save data with at least:

- save schema version;
- current mission;
- current checkpoint;
- selected difficulty;
- completed/unlocked missions;
- weapon unlocks;
- campaign completion;
- persistent option values.

Create a backend abstraction so development file persistence and Xbox profile/storage behavior can differ.

## M02.5 Scene transition service

Implement safe scene loads with:

- loading state;
- input suppression;
- stale dialogue cleanup;
- service deduplication;
- previous mission unload;
- error path if a scene is missing.

## M02.6 Checkpoint skeleton

Create checkpoint IDs/spawn points and restart flow independent of full mission content.

## Gate M02

PASS when Boot -> Menu -> Test Mission -> Pause -> Death -> Restart Checkpoint -> Menu can be exercised without broken state.

---

# M03 — PLAYER, CAMERA & CORE COMBAT

## Objective

Make moment-to-moment play production quality before building twelve missions.

## M03.1 Movement

Requirements:

- responsive analog movement;
- acceleration/deceleration tuned for combat;
- no input lag from camera logic;
- no snagging on simple station geometry;
- safe movement during diagonal aim.

## M03.2 Aiming

Requirements:

- independent right-stick aim if compatible with package design;
- visible facing/weapon direction;
- stable dead-zone behavior;
- no aim rotation jitter.

## M03.3 Dodge/Roll

Use supplied roll animation where possible.

Requirements:

- directional roll;
- bounded cooldown;
- cannot spam indefinitely;
- invulnerability/defensive window only if clearly implemented and tuned;
- no clipping through locked doors/walls;
- animation and control return cleanly.

## M03.4 Health/damage/death

Implement common damage contract.

Player flow:

- hit feedback;
- health update;
- death state;
- short death presentation;
- Restart Checkpoint.

No physics or UI may continue behaving as though the player is alive.

## M03.5 Camera

Requirements:

- elevated top-down framing;
- smooth follow;
- readable combat radius;
- handles corridors/rooms;
- avoids occlusion where feasible;
- no camera geometry clipping that makes aiming impossible;
- performant on Xbox.

## M03.6 Interaction

Context interaction for:

- doors;
- terminals;
- pickups if not auto-pickup;
- mission devices.

Use one prompt system rather than per-object custom HUD.

## M03.7 Combat feel pass

Tune:

- move speed;
- turn/aim rate;
- roll distance/duration;
- hit reaction;
- camera responsiveness;
- default rifle fire cadence.

## Target Test Gate T1

After M03 plus enough of M04 to represent real combat, perform first meaningful Xbox 360 test.

Do not stop M03 after each micro-change for console testing.

---

# M04 — WEAPON ARSENAL, DAMAGE, PICKUPS & POOLING

## Objective

Make the complete campaign arsenal reliable and reusable.

## M04.1 Production weapon interface

Normalize vendor weapons to a consistent Marine Slayer contract without unnecessarily rewriting working code.

## M04.2 Required weapons

Implement/tune:

- pistol;
- rifle;
- shotgun;
- laser;
- rocket launcher;
- melee;
- grenade.

Each requires:

- damage;
- rate of fire;
- ammo behavior;
- reload behavior;
- VFX;
- SFX hookup;
- HUD data;
- AI/player compatibility where appropriate;
- target-hardware sanity.

## M04.3 Weapon switching

Requirements:

- quick swap;
- cannot switch into invalid weapon state;
- preserves/reports ammo correctly;
- cancels/coordinates reload safely;
- no duplicated weapon models.

## M04.4 Projectiles/raycast

Use the vendor implementation that best fits each weapon.

Do not make every weapon a physical rigidbody projectile if raycast is more efficient and already supported.

## M04.5 Pooling

Implement bounded pools for common high-frequency runtime objects.

At minimum evaluate:

- bullets;
- impacts;
- muzzle flashes;
- explosions;
- decals;
- blood effects.

## M04.6 Pickups

Implement health/ammo/grenade/weapon pickup rules.

Prevent checkpoint soft-locks caused by depleted resources.

## M04.7 Environmental damage

Damage model must support:

- traps;
- explosive props;
- turret fire;
- explosions;
- boss hazards.

## Gate M04

PASS when the full arsenal can be tested in Combat Sandbox, resources/HUD are correct, and no major state corruption occurs after repeated swapping/reloading/death.

---

# M05 — ENEMY ROSTER, AI, SPAWNING & HAZARDS

## Objective

Build the full combat grammar before campaign content scales up.

## M05.1 Standard archetypes

Implement and tune:

- E1 Rifle Trooper;
- E2 Breacher;
- E3 Suppressor;
- E4 Scout Drone;
- E5 Hunter Drone;
- E6 Combat Mech;
- E7 Mech Berserker;
- E8 Turret;
- E9 Corrupted Defense Node.

## M05.2 AI requirements

All AI must:

- acquire player reliably;
- navigate intended level geometry;
- stop attacking after death;
- clean up correctly;
- respect encounter activation;
- avoid obvious deadlocks;
- avoid continuous expensive perception/path queries.

## M05.3 Enemy behavior separation

Do not create every enemy by changing only health/damage.

Archetypes must differ by:

- range;
- movement;
- attack pattern;
- pressure role;
- vulnerability window;
- encounter usage.

## M05.4 Spawning

Spawner supports:

- bounded maximum alive count;
- delayed wave;
- door/route spawn points;
- completion signal;
- cleanup;
- difficulty scaling hooks.

## M05.5 Hazards

Productionize:

- turrets;
- explosive trap;
- area-damage trap;
- destructible environmental props;
- usable device/terminal disable path.

## M05.6 LOD/performance

Enable/tune LODs supplied for soldier/mech/drone where appropriate.

## Target Test Gate T2

Representative arena on Xbox 360 must include mixed soldiers + drone + mech + turret + VFX and verify responsiveness/performance before mission production becomes too expensive to revise.

---

# M06 — ENCOUNTERS, OBJECTIVES, CHECKPOINTS & DIALOGUE

## Objective

Create the authoring systems that allow twelve levels to be built consistently.

## M06.1 Encounter controller

Required features:

- activation trigger;
- spawn groups;
- multiple waves;
- doors lock/unlock;
- optional hazards;
- optional reinforcement conditions;
- completion condition;
- combat music signal;
- dialogue hooks;
- checkpoint-after-complete;
- cleanup.

## M06.2 Objectives

Support:

- Reach;
- Eliminate;
- Interact;
- Disable/Restore;
- Destroy;
- Defend;
- Boss;
- Escape.

Objective UI must update clearly.

## M06.3 Checkpoint restart

Checkpoint reset must return mission to a curated valid state.

Do not attempt to serialize every enemy/projectile.

## M06.4 Dialogue

Implement data-driven subtitles/comms using story bible.

Requirements:

- speaker;
- subtitle text;
- timing;
- priority;
- interrupt rules;
- optional audio reference;
- mission trigger.

Voice recordings are optional. Subtitle dialogue is mandatory.

## M06.5 Narrative trigger safety

Dialogue should not replay endlessly if player crosses a trigger repeatedly unless intentionally repeatable.

## Gate M06

PASS when a mini production mission can run:

entry dialogue -> objective -> lockdown encounter -> terminal interaction -> checkpoint -> second encounter -> mission complete -> save/progression.

---

# M07 — MENUS, HUD, PAUSE, OPTIONS & AUDIO FLOW

## Objective

Complete the shell around the campaign before content lock.

## M07.1 Boot

Create `MS_Boot`.

Responsibilities:

- initialize services;
- load settings/save metadata;
- transition to menu;
- handle missing/corrupt save safely.

## M07.2 Main menu

Mandatory entries:

- Continue;
- New Game;
- Mission Select when unlocked;
- Options;
- Credits.

Challenge/Co-op may appear only when implemented and stable.

## M07.3 New Game

Flow:

New Game -> confirmation if existing campaign -> difficulty select -> introduction -> Mission 01.

## M07.4 HUD

Mandatory:

- health;
- weapon;
- magazine/reserve ammo;
- grenade count;
- interaction prompt;
- objective text;
- boss health when relevant.

## M07.5 Pause

- Resume;
- Restart Checkpoint;
- Options;
- Return to Main Menu.

Ensure pause does not break dialogue, physics, input or scene transition.

## M07.6 Options

Minimum:

- music volume;
- SFX volume;
- dialogue/voice volume if voice channel exists;
- subtitle toggle (default ON is recommended because dialogue is text-first; if disabled, essential mission objective text must remain);
- brightness/gamma only if technically stable;
- controller/help screen.

## M07.7 Credits

Include project-owned credits and licensed asset attribution only as required/appropriate by license.

Do not invent attribution obligations—inspect license documentation available to the owner/package.

## Gate M07

PASS when a controller-only user can navigate all mandatory shell screens without mouse/keyboard.

---

# M08 — ACT I PRODUCTION (MISSIONS 01–03)

## Shared Act I requirement

Act I teaches the game without feeling like a tutorial pop-up sequence.

### Mission 01 — Dead Arrival

Scene:

`MS_M01_DeadArrival.unity`

Mandatory gameplay:

- establish damaged docking environment;
- movement tutorialization through play;
- rifle/pistol introduction;
- aim/fire/reload;
- interact with door/terminal;
- Rifle Trooper encounters;
- health/ammo pickup;
- first arena;
- checkpoint(s);
- end at route into security sector.

Mandatory story beats:

- Cross survives insertion;
- Rusk contact;
- Resolute status;
- station security hostile;
- evidence enemies share a common command source.

Completion condition:

Cross reaches Security Control route and mission progress saves.

### Mission 02 — Lockdown

Scene:

`MS_M02_Lockdown.unity`

Mandatory gameplay:

- denser door/terminal interactions;
- Breacher introduction;
- grenade introduction;
- locked-room arena;
- first mixed Rifle Trooper + Breacher encounter;
- alternate/maintenance routing aided by Maddox;
- at least one environmental trap/destructible opportunity.

Story:

- Maddox introduced;
- station is deliberately sealing compartments;
- first ECHOLOCK naming/evidence.

### Mission 03 — Red Deck

Scene:

`MS_M03_RedDeck.unity`

Mandatory gameplay:

- emergency power visual identity;
- turret introduction;
- trap/environmental hazard;
- Scout Drone introduction;
- terminal objective;
- arena combining ground enemy + drone + turret;
- Act I escalation checkpoint/finish.

Story:

- Sorrel recording introduces Black Signal terminology;
- command requests ECHOLOCK preservation;
- Cross rejects prioritizing hardware over survival.

## Act I quality gate

A new player must understand movement, aim, roll, weapons, grenade, pickups, objectives, doors, checkpoints and primary enemy language by the end of Mission 03.

## Target Test Gate T3A

Run full Act I on Xbox 360 before building every later mission from unchecked assumptions.

---

# M09 — ACT II PRODUCTION (MISSIONS 04–06)

### Mission 04 — The Foundry

Scene:

`MS_M04_TheFoundry.unity`

Identity:

- fabrication/industrial lighting;
- heavier environmental hazards;
- explosive props;
- moving through production spaces.

Gameplay:

- Combat Mech introduction;
- first heavy enemy arena;
- environment used offensively;
- shotgun availability if not already fully unlocked;
- escalating mixed encounters.

Story:

- station is manufacturing/arming defenders;
- Maddox assists power reroute.

### Mission 05 — Black Lab

Scene:

`MS_M05_BlackLab.unity`

Identity:

- research spaces;
- darker/controlled lighting;
- more narrative quiet moments without long cutscenes.

Gameplay:

- Hunter Drone introduction;
- laser weapon introduction/featured use;
- optional terminal lore;
- mixed drone/soldier encounters;
- objective sequence revealing relays.

Story:

- live Sorrel contact;
- ECHOLOCK purpose;
- Veyr's override decision;
- three relay plan.

### Mission 06 — Warden

Scene:

`MS_M06_Warden.unity`

Gameplay:

- assault Relay One;
- elite security encounters;
- first formal boss: Warden Havelock;
- boss uses readable rifle/grenade/charge or melee phases;
- checkpoint immediately before boss or fair retry position;
- post-boss Relay One destruction.

Story:

- first Veyr direct communication;
- philosophy conflict established;
- destroying relay visibly disrupts Signal coordination.

## Target Test Gate T3B

Mission 05 or 06 must be profiled as a representative mid-campaign production scene on Xbox 360.

---

# M10 — ACT III PRODUCTION (MISSIONS 07–09)

### Mission 07 — Gun Deck

Scene:

`MS_M07_GunDeck.unity`

Gameplay:

- faster combat pace;
- station weapons/defense theme;
- heavy weapon/rocket launcher featured;
- larger mixed encounters within profiled enemy limits;
- turrets as both hazard and optionally disableable defense.

Story:

- player shifts fully to counterattack;
- Rusk refuses withdrawal order.

### Mission 08 — Ghost Circuit

Scene:

`MS_M08_GhostCircuit.unity`

Gameplay:

- defense network sabotage;
- multiple linked objectives;
- drone-heavy encounter design;
- boss: Harrow Swarm;
- alternate between drone waves/control nodes/vulnerable phase;
- destroy Relay Two.

Story:

- evidence Signal is beyond Veyr's control.

### Mission 09 — No Safe Room

Scene:

`MS_M09_NoSafeRoom.unity`

Gameplay:

- sustained-pressure mission;
- shorter intervals between fights;
- mixed full roster excluding unrevealed final elites if appropriate;
- controlled resource tension;
- environmental lockdowns/purges;
- extraction option presented narratively, but route goes deeper.

Story:

- surviving compartments threatened;
- Cross refuses evacuation until core can no longer weaponize station.

## Act III gate

Campaign must now feel mechanically broader, not merely harder.

---

# M11 — ACT IV PRODUCTION (MISSIONS 10–12)

### Mission 10 — The Spine

Scene:

`MS_M10_TheSpine.unity`

Gameplay:

- high-security central traversal;
- tight corridor bursts alternating with larger chambers;
- elite/mech pressure;
- advanced enemy combinations;
- mechanics callback from early missions used with higher stakes.

Story:

- Veyr increasingly network-integrated;
- destroying final relay will expose core and release containment.

### Mission 11 — Praetor Gate

Scene:

`MS_M11_PraetorGate.unity`

Gameplay:

- Relay Three assault;
- final pre-core arena;
- Twin Praetors boss:
  - ranged elite mech;
  - melee elite mech;
  - complementary behavior;
  - readable attacks;
  - fair target-priority challenge;
- destroy Relay Three.

Story:

- core exposed;
- Veyr enters command harness;
- overload plan begins.

### Mission 12 — Zero Hour

Scene:

`MS_M12_ZeroHour.unity`

Gameplay stages:

1. full-roster final assault;
2. core approach;
3. final boss Veyr/Rift Harness;
4. post-boss terminal/core actions;
5. escape/extraction sequence;
6. ending transition.

Final boss requirements:

- multiple distinct phases;
- uses mechanics player learned earlier;
- no unexplained instant-kill gimmicks;
- stable checkpoint retry;
- no unbounded adds;
- Xbox performance tested.

Ending:

- ECHOLOCK/Black Signal removed from station network;
- Bastion goes dark;
- Cross extracted by Rusk;
- Sorrel/Maddox resolution via dialogue;
- credits;
- post-completion save state.

## Gate M11

A complete campaign path now exists from Mission 01 through final credits.

Content presence alone does not yet equal release readiness; proceed to hardening.

---

# M12 — BOSS HARDENING & COMBAT TUNING

## Objective

Make bosses and late-game combat feel authored rather than scaled enemy prefabs.

## Required bosses

- Warden Havelock;
- Harrow Swarm;
- Twin Praetors;
- Marshal Veyr / Rift Harness.

## Boss acceptance criteria

Each boss:

- has intro/recognition moment;
- has clear health/state feedback;
- has at least two meaningful behavior phases or encounter transformations;
- telegraphs major attacks;
- cannot be trivially stun-locked;
- cannot trap player in unavoidable repeated damage;
- has valid checkpoint/retry;
- cleans up all spawned actors/effects on victory/death/reload;
- triggers story/progression exactly once;
- meets Xbox frame target.

## Combat pass

Tune:

- time-to-kill by enemy role;
- projectile speed;
- aggression;
- spawn timing;
- pickup economy;
- weapon usefulness;
- grenade availability;
- roll safety/cooldown;
- boss damage.

No required weapon should become useless for large portions of the game.

---

# M13 — DIFFICULTY, PROGRESSION & FINAL NARRATIVE INTEGRATION

## M13.1 Difficulties

Implement:

- Recruit;
- Marine;
- Slayer.

Tune behavior rather than only health multiplication.

## M13.2 Mission progression

- Continue;
- unlock next mission;
- Mission Select policy;
- completed campaign state;
- New Game overwrite confirmation.

## M13.3 Dialogue completion

Implement all required mission dialogue from story bible plus necessary original connective lines.

Review for:

- continuity;
- repeated lines;
- trigger timing;
- subtitles readable during combat;
- no dialogue from future story states.

## M13.4 Intro/ending

Create lightweight original intro/ending presentation using in-engine camera, text, station visuals and dialogue rather than requiring expensive prerecorded cinematics.

All cinematic sequences must be skippable or short enough not to frustrate retries.

## M13.5 Credits

Complete credits and return-to-menu/post-game flow.

---

# M14 — XBOX 360 PERFORMANCE HARDENING

## Objective

Turn the complete campaign into a reliable Xbox 360 game.

Follow `XBOX360_PERFORMANCE_CONTRACT.md`.

## M14.1 Establish measured baseline

Populate `docs/PERFORMANCE_BASELINE.md` with real target data.

## M14.2 Profile every mission

For Missions 01–12 record:

- worst observed frame-rate/frame time;
- high-load encounter;
- memory trend;
- GC spikes;
- loading issue;
- visual feature reductions if made.

## M14.3 Optimize systematically

Potential work:

- texture import reductions;
- shader simplification;
- baked lighting;
- realtime light/shadow reduction;
- particle caps;
- pooling;
- AI stagger scheduling;
- pathing fixes;
- corpse cleanup;
- audio compression;
- mission object cleanup;
- material sharing;
- scene memory cleanup.

Do not randomly delete effects without finding bottleneck.

## Target Test Gate T4

Worst boss and mixed-enemy arena.

## Target Test Gate T5

All twelve missions individually boot and complete on console.

---

# M15 — FULL CAMPAIGN QA, SOAK & RELEASE CANDIDATE

## M15.1 Fresh-save playthrough

From no save:

Boot -> New Game -> Mission 01 -> ... -> Mission 12 -> Credits.

No developer scene loading or inspector intervention allowed.

## M15.2 Continue testing

At several checkpoints:

- quit to menu;
- exit game where practical;
- relaunch;
- Continue;
- verify mission/checkpoint/resources.

## M15.3 Death/retry sweep

Deliberately die:

- before encounter;
- during encounter;
- after objective interaction;
- boss fight;
- escape sequence.

Ensure checkpoint state remains valid.

## M15.4 Menu regression

Test:

- Continue availability;
- New Game;
- difficulty;
- pause/resume;
- restart checkpoint;
- options persistence;
- subtitles;
- credits;
- mission select.

## M15.5 Controller regression

- disconnect/reconnect if supported;
- start game with controller;
- no keyboard/mouse dependency;
- menu focus cannot become lost.

## M15.6 Sequential soak

Play multiple missions without restarting console/game to reveal:

- persistent-object duplication;
- memory growth;
- stale audio;
- stale dialogue;
- save corruption;
- input duplication;
- scene unloading failures.

## M15.7 Release candidate

Produce documented Xbox 360 release candidate build with:

- version identifier;
- commit SHA;
- build date;
- known issues;
- test result;
- deployment instructions for owner.

Do not commit proprietary XDK binaries or credentials.

## Gate T6 — Final target hardware acceptance

PASS only when the owner/target test confirms campaign completion and no release-blocking issue remains.

---

# M16 — OPTIONAL POST-CAMPAIGN LOCAL CO-OP / CHALLENGE MODE

This phase is deliberately **not allowed to delay the single-player v1.0 campaign**.

The asset pack contains local multiplayer foundations, so after M15 stability Codex may implement:

- 2-player local co-op;
- shared camera preferred if readable;
- split-screen only if performance permits;
- Survival challenge;
- Boss Rush;
- Time Attack.

If co-op destabilizes campaign performance, keep it on a later branch/release.

---

# 5. MISSION AUTHORING QUALITY RULES

Every campaign mission must contain:

- unique start state;
- objective flow;
- multiple traversal spaces;
- combat encounters;
- at least one distinguishing mechanic/encounter;
- dialogue/story progression;
- checkpoints;
- resource placement;
- death/retry support;
- mission-complete transition;
- Xbox performance pass.

## Prohibited level-production shortcuts

Do not:

- duplicate the same scene twelve times and only rename it;
- use identical enemy waves for every arena;
- use a single giant open room for entire missions;
- leave vendor demo signage/instructions as final content;
- rely on debug keys to progress;
- ship missing-texture/pink-material assets;
- use placeholder cubes for mandatory final-game enemies when licensed models are available;
- make story scenes require unprovided voice files.

---

# 6. UI / PRESENTATION COMPLETENESS

The release must look like a game, not an editor test.

Required polish:

- coherent Marine Slayer title/logo treatment using project-owned text/graphics;
- loading presentation;
- consistent menu typography;
- readable HUD;
- subtitle backing/readability;
- objective messages;
- boss health;
- mission completion screen/transition;
- credits.

Do not copy another game's UI.

---

# 7. AUDIO COMPLETENESS

The supplied pack includes limited audio.

Codex must:

- reuse licensed supplied SFX where appropriate;
- map weapon/UI/environment sounds consistently;
- avoid missing AudioClip exceptions;
- support music/SFX volume controls;
- treat voice as optional.

If final music is not included in the licensed pack, use only legally redistributable/project-owned music or clearly document the final music dependency. Do not scrape copyrighted tracks from commercial games.

---

# 8. STORY/DIALOGUE COMPLETENESS

Codex is explicitly authorized to write original connective dialogue.

Rules:

- remain consistent with Lena Cross, Rusk, Sorrel, Maddox and Veyr characterization;
- no copyrighted quotes;
- no long exposition during heavy combat;
- subtitles must function without voice files;
- each mission must advance the story or character stakes;
- final boss/ending dialogue must match canonical event order.

---

# 9. SAVE / PROGRESSION RELEASE BLOCKERS

These are release-blocking defects:

- Continue loads wrong mission;
- checkpoint spawns behind locked progression;
- completed objectives reset inconsistently;
- New Game corrupts existing state without confirmation;
- Mission Select unlocks inaccessible/broken scene;
- final completion does not persist;
- save corruption crashes boot.

---

# 10. PERFORMANCE RELEASE BLOCKERS

Release blockers include:

- repeatable out-of-memory crash;
- severe sustained frame-rate collapse during required encounter;
- long GC hitch that makes combat input unreliable;
- progressive memory leak across missions;
- boss encounter consistently below acceptable responsiveness;
- loading deadlock;
- Xbox-only shader/material failure hiding critical gameplay.

---

# 11. CODE QUALITY RULES

Codex must favor maintainable Unity 5.4-era C#.

Avoid:

- async/await dependencies not appropriate to the target runtime;
- modern Unity package APIs;
- ECS/DOTS;
- URP/HDRP;
- reflection-heavy architecture;
- scene-wide searches every frame;
- allocation-heavy LINQ in update loops;
- giant manager scripts owning every game system;
- duplicated logic per mission when reusable systems exist.

Comment non-obvious Xbox 360/platform workarounds.

---

# 12. GIT / CHECKPOINT RULES

Commit meaningful checkpoints such as:

- `M01 asset import and vendor audit`
- `M03 production player combat`
- `M05 enemy roster complete`
- `M08 Act I complete`
- `M09 Act II complete`
- `M10 Act III complete`
- `M11 full campaign content complete`
- `M14 Xbox performance hardening`
- `M15 release candidate`

Do not create a commit for every tiny inspector value.

Update `MILESTONE_STATUS.md` as phases close.

---

# 13. REQUIRED DOCUMENTATION GENERATED DURING IMPLEMENTATION

Codex must create/update:

- `docs/ASSET_IMPORT_REPORT.md`
- `docs/VENDOR_MODIFICATIONS.md`
- `docs/BUILD_ENVIRONMENT.md`
- `docs/PERFORMANCE_BASELINE.md`
- `docs/KNOWN_ISSUES.md`
- `docs/RELEASE_CHECKLIST.md`
- `MILESTONE_STATUS.md`

Documentation must reflect actual state, not aspirational claims.

---

# 14. FINAL DEFINITION OF DONE

Do **not** close M001 because:

- all scripts compile;
- all scenes exist;
- the game works on Windows;
- one level works on Xbox;
- the final boss prefab exists;
- a menu can jump directly to credits.

M001 is DONE only when:

### Content

- Missions 01–12 are authored and complete;
- story is coherent from beginning to ending;
- all required enemy types are used;
- all required bosses work;
- all required weapons function;
- credits exist.

### Systems

- boot/menu;
- input;
- HUD;
- pause;
- options;
- save;
- Continue;
- checkpoints;
- objectives;
- dialogue;
- mission transitions;
- death/retry;
- progression;
- campaign completion.

### Xbox 360

- builds successfully with actual local toolchain;
- launches on target hardware;
- controller works;
- every mission is playable;
- performance is acceptable;
- no normal-play out-of-memory failure;
- final campaign flow reaches credits.

### QA

- mandatory `TEST_MATRIX.md` items pass or any remaining non-blockers are explicitly documented and accepted by the owner.

The standard is a **complete homebrew Xbox 360 game**, not a promise that more work could eventually turn it into one.