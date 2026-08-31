# MILESTONE 001 — MARINE SLAYER COMPLETE XBOX 360 GAME

## Milestone classification

**Type:** complete-game production milestone

**Build model:** FROM SCRATCH

**Primary target:** Xbox 360

**Engine:** Unity 5.4.1f1

**Repository:** `CGameDev/Marine-Slayer`

**Active branch:** `milestone/m001-complete-game`

**Campaign authority:** owner-supplied Marine Slayer Complete Lore Bible + Visual Lore Companion

**Production assets:** owner-supplied licensed Unity asset pack

**Gameplay-feel reference:** https://youtu.be/cbfpxTSKEg4 from approximately 04:00 onward

---

# 0. NON-NEGOTIABLE PROJECT DIRECTIVE

Codex must create a **new Unity 5.4.1f1 project from scratch** and build the complete Marine Slayer game defined by the owner lore.

This milestone is NOT:

- a continuation of an old Marine Slayer build;
- a port of an older project;
- a cleanup of old scenes;
- a recreation of the YouTube demo scene;
- a vertical slice;
- a one-level prototype;
- an asset-pack showcase;
- a PC-first game later pushed to Xbox 360.

The YouTube video is a **play-style reference only**. Do not search for or import the old Marine Slayer Unity project.

The required finished product is the canonical **5-Act / 25-Level** Marine Slayer campaign centered on:

- Lieutenant Rhyker Voss;
- Eidolon Station;
- Project ASCENDANT;
- VANGUARD;
- the Convergence;
- the canonical enemy taxonomy;
- the canonical weapons/tools;
- the canonical bosses/cinematics/endings.

---

# 1. REQUIRED READING ORDER

Before implementation, read in order:

1. `AGENTS.md`
2. `CODEX_START_HERE.md`
3. `docs/OWNER_CANON_SOURCE.md`
4. `docs/ASSET_PACK_INVENTORY.md`
5. `docs/PLAYSTYLE_REFERENCE.md`
6. `docs/XBOX360_PERFORMANCE_CONTRACT.md`
7. `docs/TECHNICAL_ARCHITECTURE.md`
8. this milestone
9. `docs/TEST_MATRIX.md`
10. `MILESTONE_STATUS.md`

If a lower-authority document still contains an obsolete 12-mission or old-build-continuation instruction, ignore that obsolete section and update the document when practical.

---

# 2. CORE DELIVERY CONTRACT

M001 is complete only when a fresh player can:

1. launch the Xbox 360 build;
2. reach the main menu;
3. start a new campaign;
4. play Level 01 through Level 25 in canonical order;
5. die and restart from deterministic checkpoints;
6. Continue a campaign after restarting the game using the implemented Xbox-compatible persistence path;
7. acquire/use the campaign's weapon/tool progression;
8. encounter the canonical enemy progression;
9. defeat The Red Engineer;
10. defeat Null Sister;
11. survive/defeat the Commander Sol Act IV climax;
12. defeat Prime Convergence;
13. resolve one of the three canonical ending states;
14. reach credits;
15. return to a valid post-campaign/menu state;
16. complete target-hardware QA without progression blockers.

No missing level may be represented by a menu button, placeholder corridor, instant completion trigger, or text card standing in for gameplay.

---

# 3. SOURCE SEPARATION CONTRACT

Codex must keep these inputs separate:

## Lore documents = WHAT THE GAME IS

They define:

- story;
- world;
- characters;
- factions;
- enemies;
- weapons;
- station zones;
- 25-level campaign;
- bosses;
- major cinematics;
- logs;
- endings.

## Licensed Unity asset pack = WHAT CODEX BUILDS WITH

Use it for suitable:

- characters;
- enemies;
- animation;
- environment modules;
- weapons;
- doors;
- pickups;
- turrets;
- traps;
- VFX;
- top-down controller foundations;
- reusable scripts.

Vendor/demo content may be reused/refactored because it is part of the supplied licensed pack.

## YouTube reference = HOW THE ACTION SHOULD GENERALLY FEEL

Use it only for:

- elevated top-down framing;
- controller response;
- tactical combat readability;
- approximate room/corridor scale;
- aggressive action pacing;
- reasonable Xbox 360 presentation density.

Do not reproduce old implementation details simply because they appear in the video.

---

# 4. GLOBAL ENGINE / TARGET RULES

- Use Unity **5.4.1f1**.
- Xbox 360 is the shipping authority.
- Do not upgrade the Unity project.
- Do not use newer Unity packages/APIs.
- Discover actual Xbox 360 Unity/XDK build support locally.
- Document actual tool paths in `docs/BUILD_ENVIRONMENT.md`.
- Never commit XDK files, certificates, credentials, build outputs, or paid vendor source assets.
- Prefer controller-first UI and gameplay at 720p television distance.
- Target stable **30 FPS** unless measured target behavior proves a higher target is sustainably achievable without compromising the game.

---

# 5. REPOSITORY / PROJECT STRUCTURE TARGET

Create a new Unity project in the repository with Marine Slayer-owned content organized approximately as:

```text
Assets/
  MarineSlayer/
    Art/
    Audio/
    Data/
    Editor/
    Materials/
    Prefabs/
      Characters/
      Enemies/
      Weapons/
      Environment/
      UI/
      Systems/
    Resources/
    Scenes/
      Boot/
      Menus/
      Test/
      Act01/
      Act02/
      Act03/
      Act04/
      Act05/
    Scripts/
      Core/
      Input/
      Player/
      Camera/
      Combat/
      AI/
      Encounter/
      Objectives/
      Dialogue/
      Lore/
      UI/
      Save/
      Platform/
      Audio/
      Performance/
  <licensed vendor folders remain in their imported locations>
ProjectSettings/
docs/
tools/
```

Do not move vendor asset folders casually if it risks breaking references.

Marine Slayer-owned code should not be scattered through vendor folders.

---

# M00 — CLEAN BOOTSTRAP / TOOLCHAIN / LICENSED DEPENDENCY

## Objective

Create a reproducible clean development environment without touching any older Marine Slayer project.

## Tasks

- Confirm repository state.
- Work from `milestone/m001-complete-game`.
- Record starting commit SHA.
- Locate Unity 5.4.1f1.
- Locate Xbox 360 build support/plugin.
- Locate XDK/Visual Studio/deploy/profiling tools.
- Populate `docs/BUILD_ENVIRONMENT.md` using facts only.
- Locate `AssetPack_ProjectSettings.zip`.
- Verify its expected hash from `docs/ASSET_PACK_INVENTORY.md`.
- Verify `.gitignore` protects the package and imported vendor source directories.
- Create a **new** Unity 5.4.1f1 project inside the repository.
- Do not import any previous Marine Slayer project files.

## Gate M00

PASS when a clean Unity 5.4.1f1 project opens from the repository and Codex knows the factual Xbox 360 build workflow or has clearly isolated any owner-dependent toolchain blocker.

---

# M01 — LICENSED ASSET IMPORT / CAPABILITY AUDIT

## Objective

Import the supplied asset pack into the new project and map it to the Lore Bible's needs.

## Tasks

- Import the supplied `.unitypackage` locally.
- Compare supplied `ProjectSettings` carefully; do not overwrite blindly.
- Allow scripts/materials/importers to settle.
- Record compiler warnings/errors.
- Open vendor demo/reference scenes only to understand supplied capability.
- Inventory useful player controller, camera, weapons, enemy AI, animations, environment pieces, doors, traps, pickups, turrets, VFX and multiplayer examples.
- Create `docs/ASSET_IMPORT_REPORT.md`.

For each important vendor system record:

```text
System
Source asset/script
Purpose
Works in Unity 5.4.1f1? yes/no/partial
Xbox 360 risk
Lore use
Decision: USE / WRAP / REFACTOR / REPLACE
Reason
```

## Canon mapping task

Create `docs/LORE_TO_ASSET_MAPPING.md` mapping canonical content to available assets.

At minimum map:

- Rhyker Voss player representation;
- Thralls;
- Spinewalkers;
- Apex Hunters;
- Brutes;
- Mesh Sirens;
- Riftbound Abominations;
- Red Engineer;
- Null Sister;
- Prime Convergence;
- canonical weapon set;
- Crew Ring;
- Maintenance/Industrial spaces;
- Research Ring;
- Command Ring;
- Spire.

If exact art does not exist, document how composite prefabs/materials/VFX/animation variants will approximate the canonical identity.

## Gate M01

PASS when the asset pack is usable in the new project and every major canonical content class has a planned asset implementation path.

---

# M02 — CORE RUNTIME / GAME STATE / SCENE FLOW

## Objective

Create the project-wide runtime foundation.

Required game states:

- Boot;
- MainMenu;
- NewGameSetup;
- Loading;
- Playing;
- Paused;
- PlayerDead;
- CheckpointRestart;
- LevelComplete;
- ActTransition;
- CampaignComplete;
- Ending;
- Credits.

Required services:

- game-state manager;
- input wrapper;
- scene/level loader;
- checkpoint/progression manager;
- save backend abstraction;
- dialogue/subtitle manager;
- audio manager;
- lore/log manager;
- platform/Xbox abstraction;
- deterministic global event system or similarly controlled communication mechanism.

Prevent duplicate persistent objects after scene transitions.

## Gate M02

PASS when Boot -> Menu -> Test Scene -> Pause -> Death -> Restart -> Menu works in the new project without state corruption.

---

# M03 — PLAYER / CAMERA / INPUT / MOVEMENT

## Objective

Establish the new game's core feel using the asset pack and YouTube reference.

## Player target

- 360-degree analog movement;
- responsive controller input;
- independent directional aiming where practical;
- firing while moving/strafing;
- melee;
- interact/use;
- weapon switching;
- grenade/tool use;
- pause;
- dodge/roll only if it fits the asset animations and does not contradict the lore/game feel.

## Camera target

- elevated top-down/oblique perspective;
- similar tactical readability to the reference video;
- smooth responsive follow;
- no first-person or over-shoulder conversion;
- optional modest aim look-ahead;
- low-cost player-occlusion treatment where needed;
- Xbox 360 performance authority.

## Gate M03

PASS when movement/aiming/camera feel good with an Xbox 360 controller in a Marine Slayer-owned test room and remain stable under representative enemy load.

---

# M04 — COMBAT / DAMAGE / CANONICAL WEAPONS

## Objective

Build the combat grammar and Lore Bible arsenal.

Canonical weapon identities:

1. UEMF M-77 `Gavel` Combat Shotgun
2. VX-90 `Lancer` Assault Rifle
3. EID-3 Plasma Cutter
4. TX-40 Arc Thrower
5. Fury Gauntlet MKII
6. Horizon RIFT Grenade
7. UEMF Tri-Shot Rail Pistol
8. Helion Industrial Sawblade Launcher
9. ASCENDANT `Unity` Beam Rifle

Utility concepts:

- breach torch;
- mesh disruptor;
- stims;
- magnetic grappler.

For each weapon define:

- gameplay role;
- damage model;
- cadence;
- range;
- ammo/resource behavior;
- reload/charge behavior;
- feedback/VFX/SFX;
- HUD data;
- pooling needs;
- Xbox 360 cost.

Do not force every weapon into the same projectile model.

Implement shared:

- damage interface;
- health/death;
- hit reactions;
- surface impacts;
- bounded decals/VFX;
- projectile/effect pooling.

## Gate M04

PASS when the canonical arsenal has a functional production representation or clearly documented staged implementation in the combat sandbox.

---

# M05 — CANONICAL ENEMY TAXONOMY / AI

## Objective

Implement the Lore Bible's enemy hierarchy, not a generic asset-pack roster.

Required classes:

### Convergence Thrall

Early pressure enemy; aggressive, erratic, coordinated packs.

### Spinewalker

Ceiling/wall/crawl-oriented ambusher where technically feasible. If true surface navigation is too expensive or unstable, simulate this identity with scripted vent/ceiling entry and drop attacks.

### Apex Hunter

Fast intelligent elite stalker with strong target pressure.

### Convergence Brute

Heavy close-range force, high mass/durability, environmental pressure.

### Mesh Siren

Psychic/deception archetype using readable visual/audio distortion without making gameplay illegible.

### Riftbound Abomination

Late-game anomalous enemy using controlled displacement/flicker/teleport-like behavior rather than expensive uncontrolled physics.

Bosses handled in dedicated phases:

- Red Engineer;
- Null Sister;
- assimilated Commander Sol encounter;
- Prime Convergence.

## AI requirements

- bounded detection/update rates;
- valid navigation;
- no wall traversal;
- no invalid/off-map spawn;
- deterministic cleanup;
- encounter activation/deactivation;
- no unreachable enemy progression locks;
- varied tactical roles;
- Xbox-appropriate CPU cost.

## Gate M05

PASS when each non-boss canonical enemy has a production-ready or accepted placeholder representation with distinct behavior and clean encounter lifecycle.

---

# M06 — HUD / MENU / CHECKPOINT / LORE / OBJECTIVE SYSTEMS

## Objective

Create all supporting game systems before 25-level content production scales.

Required UI flow:

- boot/splash;
- main menu;
- New Game;
- Continue;
- options;
- credits;
- pause;
- restart checkpoint;
- objective updates;
- level/act transitions;
- ending/credits flow.

Required HUD:

- health;
- active weapon;
- ammo/resource;
- grenade/tool count where relevant;
- interaction prompt;
- objective prompt/update;
- boss health;
- checkpoint feedback.

Required lore system:

- terminal/report collectible;
- title/category/body text;
- collected/read state;
- optional audio reference;
- subtitle/text-first support;
- pause/lore menu or contextual read view.

Required checkpoint system:

- deterministic checkpoint ID;
- player spawn;
- essential inventory/progression state;
- objective state;
- encounter state handling;
- safe retry resource floor;
- no duplicated progression-critical pickups;
- versioned persistence abstraction.

## Gate M06

PASS when a complete test level can be started, checkpointed, failed, resumed, completed and returned to menu with correct HUD/objective/lore behavior.

---

# M07 — ACT I: THE AWAKENING (LEVELS 01–05)

## Level 01 — Cryo-Bay 09: `Cold Rebirth`

Required identity from lore:

- malfunctioning cryobay;
- steam/flickering emergency lighting;
- dead personnel;
- Voss awakening from cryostasis;
- movement/basic combat onboarding;
- early melee emphasis;
- unstable Thralls;
- escape into Crew Ring.

Use the YouTube video only for top-down gameplay feel, not as a requirement to copy its old level logic.

## Level 02 — Crew Quarters: `Voices of the Lost`

- abandoned human residential spaces;
- meals/personal effects/barricade aftermath;
- blood trails/vent implications;
- introduce Spinewalker threat grammar;
- 4 placed lore logs selected from relevant Crew/maintenance material.

## Level 03 — Rec Wing: `Broken Normalcy`

- corrupted recreation spaces;
- arcade/display systems affected by VANGUARD;
- first clear arena escalation;
- coordinated Thrall mini-swarm;
- contrast normal human leisure with station horror.

## Level 04 — Maintenance Access: `Something in the Vents`

- darker claustrophobic routes;
- utility corridors/crawlspaces;
- machinery behaving incorrectly;
- vent pressure/ambush grammar;
- survival-horror pacing without abandoning action.

## Level 05 — Crew Ring Transit: `First Light of the Machine God`

- Act I climax;
- transit visual landmark;
- first mass Convergence display;
- Thralls moving with disturbing coordination;
- stronger VANGUARD presence;
- desperate transition into Industrial Ring.

## Act I gate

PASS when Levels 01–05 play consecutively, introduce the game's core mechanics, and the act transition functions on target hardware.

---

# M08 — ACT II: INTO THE DEPTHS (LEVELS 06–10)

## Level 06 — Lower Industrial Access: `The Station Breathes`

- autonomous machinery;
- breathing/pulsing industrial atmosphere;
- Thrall + first Brute escalation;
- moving machinery as hazard.

## Level 07 — Cooling Tunnels: `Heat of the Machine`

- steam/coolant maze;
- scalding bursts;
- molten/spill hazards;
- collapsing route elements;
- overhead/ambush pressure.

## Level 08 — Reactor Support Floors: `Core Pressure Rising`

- unstable reactor support area;
- heartbeat-like lighting/alarm design;
- first Apex Hunters;
- tighter precision-combat arenas.

## Level 09 — Industrial Forge Line: `Where Flesh Meets Steel`

- assembly-line body horror;
- active forge machinery;
- Brute-heavy engagements;
- shifting/moving industrial hazards;
- strongest visual commitment to flesh/steel Convergence so far.

## Level 10 — Furnace Core: `Ashes of the Innocent`

Boss: **The Red Engineer / Rudd Hale**.

Encounter requirements:

- industrial/furnace arena;
- mechanically readable phases;
- molten/environmental hazard control;
- saw/industrial attack identity;
- intermittent remnants of Hale's human voice/identity;
- defeat destabilizes Industrial Ring and drives Voss toward Research.

## Act II gate

PASS when Levels 06–10 play consecutively and Red Engineer is a complete target-tested boss, not a large normal enemy with extra health.

---

# M09 — ACT III: ENGINEERING THE DAMNED (LEVELS 11–15)

## Level 11 — Research Wing Entrance: `Ghosts in the Wires`

- sterile labs corrupted by biomechanical residue;
- glitched research displays;
- first Mesh Siren gameplay;
- restrained perception distortion.

## Level 12 — Neural Mesh Chambers: `The Choir of the Damned`

- neural pods/troopers mid-transformation;
- synchronized victims;
- Apex Hunter pressure;
- stronger mesh/psychic presentation;
- reveal more of VANGUARD's evolution logic.

## Level 13 — Bio-Synthesis Labs: `Born Again Wrong`

- failed prototypes;
- Riftbound introduction;
- mesh-conduit objective sequences;
- biological lab horror;
- avoid effects that make target performance/readability collapse.

## Level 14 — Experimentation Wing: `Voices Beneath the Skin`

- psychic interference;
- hallucination-style moments;
- false movement/duplicate-like threats where feasible;
- Voss memory distortions;
- maintain player control/readability.

## Level 15 — Nexus Interface: `The Null Sister`

Boss: **Null Sister**.

Encounter requirements:

- gravity/psychic chamber identity;
- readable distortion phases;
- illusion/false-target mechanic that remains fair;
- layered Null Sister/VANGUARD dialogue;
- defeat collapses the psychic field and opens Command Ring progression.

## Act III gate

PASS when the Research act delivers meaningful visual/mechanical escalation and Null Sister works consistently on Xbox 360 hardware.

---

# M10 — ACT IV: COMMAND & CATASTROPHE (LEVELS 16–20)

## Level 16 — Command Ring Approach: `Echoes of Authority`

- damaged ascent toward command;
- external/panoramic station cues where feasible;
- synchronized enemy patrols;
- multi-direction combat.

## Level 17 — Tactical Operations Hall: `Dead Decisions`

- corrupted holo/tactical displays;
- remains of final defensive positions;
- elite Convergence forces;
- environmental storytelling of Sol's failed resistance.

## Level 18 — Council Chambers: `Voices That Should Be Silent`

- political/leadership space;
- neural echoes of final arguments;
- intermittent combat;
- Mesh Siren/Riftbound tension.

## Level 19 — Communications Nexus: `The Silence Between Stars`

- blocked outgoing communications;
- signal arrays/processors;
- layered enemy waves;
- augmented Convergence Commander/miniboss;
- reveal stakes of VANGUARD escaping Eidolon.

## Level 20 — Command Bridge: `Sol's Last Stand`

Act IV boss/climax: **Commander Arwyn Sol**, assimilated/animated by VANGUARD.

Encounter requirements:

- retain tragic human identity;
- unnatural precision rather than generic monster behavior;
- remnants of Sol's voice break through;
- VANGUARD overrides those moments;
- defeat opens the path to the Spire.

## Act IV gate

PASS when Levels 16–20 play consecutively and the Sol encounter lands narratively and mechanically rather than functioning as a generic boss skin.

---

# M11 — ACT V: THE SPIRE (LEVELS 21–25)

## Level 21 — Spire Antechamber: `The Climb of the Damned`

- gravity instability;
- layered victim echoes;
- Riftbound/Spinewalker pressure;
- architecture begins breaking normal rules.

## Level 22 — Ascension Galleries: `Graves of the Mindless`

- suspended victims wired into mesh;
- tracking eyes/body horror;
- Mesh Sirens;
- controlled reality-warp hazards.

## Level 23 — Gravitic Containment Core: `The World Bends`

- altered/zero-gravity gameplay concept;
- moving/orbiting platforms where technically reliable;
- grappler/tool usage if implemented;
- Apex Hunter pressure;
- avoid physics chaos that destabilizes Xbox 360.

## Level 24 — Mesh Reservoir: `The Heart of Voices`

- VANGUARD direct psychological confrontation;
- hallucinated squadmate/history imagery where feasible;
- elite enemy gauntlet;
- final resource preparation;
- Prime Convergence buildup.

## Level 25 — Core Ascendant: `Prime Convergence`

Final boss: **Prime Convergence**.

Required principles:

- visually communicates VANGUARD's ultimate bio-mechanical vessel;
- multi-phase fight;
- uses learned campaign mechanics;
- controlled gravity/distortion attacks;
- changing vulnerability windows;
- strong but bounded effects;
- completion routes into ending-state logic.

## Act V gate

PASS when Levels 21–25 form a complete final act and Prime Convergence is beatable, readable and stable on Xbox 360.

---

# M12 — CINEMATICS / DIALOGUE / LORE CONTENT

Implement canonical cinematic anchors as achievable in-engine sequences:

- **ASCENDANT DAWN** opening;
- **THE FIRST ASCENDED**;
- **THE RED ENGINEER** intro;
- **COMMANDER SOL'S LAST STAND**;
- **NULL SISTER** intro;
- **PRIME CONVERGENCE** intro.

Dialogue rules:

- preserve established names/lore;
- write only connective implementation dialogue where lore lacks exact lines;
- concise during combat;
- longer exchanges during traversal/act transitions;
- subtitle-first;
- skippable non-interactive cinematics where practical;
- no dependency on voice acting for first complete build.

Implement optional logs/reports through the reusable lore system.

---

# M13 — THREE ENDINGS / CAMPAIGN STATE

Implement the three canonical ending concepts:

1. **ESCAPE VELOCITY**
2. **ASCENDED**
3. **THE RIFT OPENS**

Choose a reliable condition model using campaign state such as:

- optional objectives;
- critical lore/containment actions;
- selected decisions;
- final mission choices;
- a combination that remains testable and understandable.

Do not choose conditions randomly.

Requirements:

- ending condition is saved;
- correct ending reliably plays;
- ending transitions to credits;
- completed-campaign state is valid after credits;
- replay/mission-select behavior does not corrupt progression.

---

# M14 — AUDIO / MUSIC / PRESENTATION POLISH

Required audio categories:

- weapon fire/impact;
- enemy vocalization;
- VANGUARD presence;
- machinery/ambience;
- UI;
- cinematic stingers;
- boss phases;
- music layers where licensed/original audio is available.

Do not use copyrighted commercial-game audio as placeholder in committed/released builds.

Keep audio memory/decompression choices appropriate for Xbox 360.

---

# M15 — XBOX 360 PERFORMANCE HARDENING

Profile representative worst-case scenes from each Act.

Audit:

- memory;
- texture sizes/formats;
- material count;
- shader complexity;
- draw calls;
- transparent overdraw;
- particles;
- realtime lights/shadows;
- enemy count;
- AI update frequency;
- physics;
- Instantiate/Destroy spikes;
- garbage allocations;
- audio memory;
- level loading;
- checkpoint serialization;
- boss effects;
- Spire distortion/gravity mechanics.

Create an evidence-based performance budget from actual profiling.

Do not invent hard numerical budgets without measurement.

Target stable gameplay at the agreed Xbox 360 frame target, defaulting to 30 FPS.

---

# M16 — COMPLETE CAMPAIGN QA

Mandatory end-to-end runs:

- New Game -> Level 25 -> each ending path;
- Continue after console/game restart at representative checkpoints;
- death/restart in every level;
- controller disconnect/reconnect behavior where platform support permits;
- pause/unpause;
- level transition repetition;
- boss retry;
- low-resource retry;
- optional log collection;
- mission/act completion;
- credits return;
- completed campaign reload.

Regression test every level after late core-system changes.

No progression blocker is acceptable.

---

# M17 — RELEASE CANDIDATE

Create a release candidate only after:

- all 25 levels exist and are complete;
- all mandatory boss encounters pass;
- ending flows pass;
- required menus/HUD/checkpoints work;
- target performance is acceptable;
- no blocker/critical test failure remains;
- repository docs reflect the actual shipped implementation;
- raw paid vendor source assets are not accidentally published;
- XDK/proprietary build files are not committed.

The release artifact may contain licensed game output as permitted by the owner's asset license, but the public source repository must remain clean of raw vendor redistributables unless specifically authorized.

---

# 6. TESTING CADENCE RULE

Codex should not stop after every small implementation step.

Batch work into meaningful gates:

- T0 after M01/M02 foundation;
- T1 after core player/combat systems;
- T2 after canonical enemy/weapon set;
- T3 after Act I;
- T4 after Act II;
- T5 after Act III;
- T6 after Act IV;
- T7 after Act V;
- T8 performance hardening;
- T9 full release-candidate soak.

Use Editor/Windows tests between gates where useful.

Owner/physical Xbox interaction should be requested only when genuinely required.

---

# 7. COMPLETION STANDARD

Milestone 001 is complete only when the **new scratch-built Unity 5.4.1f1 project** delivers:

```text
Boot
  -> Main Menu
  -> New Game
  -> ACT I (Levels 01–05)
  -> ACT II (Levels 06–10)
  -> ACT III (Levels 11–15)
  -> ACT IV (Levels 16–20)
  -> ACT V (Levels 21–25)
  -> Prime Convergence
  -> Canonical ending state
  -> Credits
  -> Valid post-game state
```

on the Xbox 360 target without progression blockers and with acceptable performance.

The Lore Bible defines the game.

The asset pack supplies the building blocks.

The YouTube video supplies the desired play feel.

The implementation itself must be new.
