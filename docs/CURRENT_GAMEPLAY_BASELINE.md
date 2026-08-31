# Marine Slayer — Current Gameplay Baseline

## Purpose

This document records the **existing playable Marine Slayer identity** shown by the project owner in the current Xbox 360 gameplay video. It exists to prevent Codex from treating the game as a blank-slate asset-pack project or accidentally replacing working/approved gameplay with a different interpretation.

**Source video:** https://youtu.be/cbfpxTSKEg4

**Gameplay analysis window:** **04:00 through the end of the video**. Material before 04:00 is commentary/setup and must not be used as gameplay evidence.

This document is a preservation and improvement baseline, not a requirement to preserve bugs.

---

# 1. Authority Rule

The existing gameplay shown from 04:00 onward is an **authoritative inherited baseline** for the project.

Codex must preserve the recognizable identity of the current build unless the project owner explicitly requests a change.

Do not replace a working existing mechanic merely because a different design might be easier to implement.

When the master milestone or an older design note conflicts with clearly established current gameplay, use this order:

1. explicit current owner instruction;
2. `AGENTS.md`;
3. this current-gameplay baseline;
4. the current master milestone/design documents.

Document any material conflict before changing established behavior.

---

# 2. Existing Narrative Canon Shown in Gameplay

The current build already establishes narrative facts that must be treated as inherited canon unless the owner changes them.

## Player identity

The player character is referred to as:

**Operative Voss**

Do not silently replace Voss with a newly invented protagonist.

The exact first name, rank, organization and wider biography may remain undefined until supported by existing project files or explicitly approved story expansion.

## Opening state

The playable opening begins after a **cryogenic interruption** in a damaged/failing cryobay or cryogenic facility area.

Voss wakes to:

- destroyed/dead personnel;
- poor visibility / damaged facility conditions;
- a station/computer AI voice providing status and tactical information;
- nearby hostile biological signatures;
- an initially unarmed or minimally armed state.

The existing opening should be preserved as the basis of Mission 01 rather than replaced with a completely different arrival sequence.

## Existing enemy terminology

The AI classifies the nearby hostile creatures as **Thralls**.

Thralls are therefore an established enemy class in Marine Slayer.

Do not remove or rename the entire Thrall concept merely to fit later soldier/mech/drone content. Later enemy families may expand the combat roster, but the opening biological threat must remain recognizable.

## Existing progression/objective language

The current level establishes three colored facility keycards:

- **Blue — Armory access**
- **Yellow — Sick Bay access**
- **Red — Research Lab access**

The Research Lab / red-key route controls access required to open the shuttle-bay route.

This keycard structure is part of the current level identity and should be preserved or upgraded rather than discarded.

## Existing escape objective

The opening level is structured around restoring/accessing the route to the **shuttle bay**.

For the expanded twelve-mission campaign, reaching the shuttle bay does not have to end the whole game. The story may reveal that immediate exfiltration is impossible, compromised, destroyed or deliberately blocked, forcing Voss deeper into the facility. However, that continuation must preserve the current level's logic rather than pretending the shuttle objective never existed.

---

# 3. Existing Gameplay Identity to Preserve

## 3.1 Camera

Observed from approximately 04:00 onward:

- elevated oblique/top-down camera;
- player-centered tracking;
- clear tactical view of rooms/corridors;
- stable orientation suitable for controller play;
- camera framing consistent with the existing top-down project identity.

### Preserve

- the elevated top-down perspective;
- the approximate tactical viewing distance;
- immediate readability of the player and close threats;
- controller-friendly tracking.

### Improve without changing identity

- smooth follow/interpolation where current movement feels rigid;
- optional modest aim/look-ahead when the right stick is held toward the edge of the visible combat space;
- occlusion handling for tall corridor walls that can hide the player/enemies;
- never convert the game into over-the-shoulder or first-person presentation.

Any look-ahead system must remain subtle enough that the current camera still feels like the same game.

---

# 4. Player Movement / Aiming Baseline

The current build already demonstrates:

- responsive analog movement;
- independent directional aiming / twin-stick-style combat behavior;
- rapid turning toward the aim vector;
- strafing while engaging enemies;
- controller-first play.

### Preserve

The existing snappy movement/aim relationship is part of the Marine Slayer feel.

### Improve

- tune acceleration/deceleration to add weight without making movement sluggish;
- remove visible foot sliding where animation speed and movement speed disagree;
- confirm dead zones on actual Xbox 360 controllers;
- prevent aim jitter near stick center;
- maintain response under enemy load.

Do not add heavy cinematic movement inertia that makes the top-down combat feel unresponsive.

---

# 5. Combat Baseline

The footage shows sustained automatic firearm combat against close-range hostile groups.

Existing feedback includes:

- visible projectiles/tracers;
- muzzle/fire effects;
- blood/hit effects;
- enemy death animation/effects;
- spatially readable weapon direction.

### Preserve

- fast, readable weapon response;
- visible shot direction;
- aggressive close-to-medium engagement pacing;
- clear enemy death confirmation.

### Required polish

Codex should add/tune, subject to Xbox 360 performance:

- stronger but restrained muzzle flash;
- weapon recoil/kick response;
- subtle camera impulse for heavier weapons only;
- impact particles appropriate to surface type;
- enemy hit reaction/flinch where animation support permits;
- damage reaction without excessive long stun-lock;
- better audio layering for fire/hit/kill confirmation;
- bounded decal/blood lifetimes or pooling so effects do not accumulate indefinitely.

Camera shake must not interfere with aiming/readability.

---

# 6. Existing AI Modes and Known Bug

The owner explicitly describes two existing enemy behavior styles:

1. **Patrol/path-based behavior** — appropriate for corridor/room environments.
2. **Horde/free-roam behavior** — intended for larger open spaces without restrictive walls/doors.

This distinction must be retained as a design concept where useful.

## Current blocking defect

The footage demonstrates enemies:

- spawning outside intended map/playable bounds;
- walking through walls/solid geometry;
- using free-roam/horde behavior in spaces where corridor-constrained navigation is required.

This is a production blocker, not an acceptable quirk.

## Required Codex correction

Codex must audit the current enemy AI/spawner implementation before replacing it.

Implement/verify:

- spawn-volume validation;
- valid-floor/valid-room spawn checks;
- wall/door collision respect;
- appropriate patrol/waypoint logic for corridor maps;
- horde mode restricted to arenas that support free navigation;
- bounded enemy count;
- enemy cleanup after encounter completion;
- recovery/failsafe if an enemy becomes unreachable outside the playable area;
- no progression gate should wait forever for an unreachable enemy.

If Unity navigation features used by the asset pack are unsuitable for the Xbox 360 target, use a lightweight waypoint/graph/steering solution instead of introducing an expensive modern dependency.

---

# 7. Level Structure Baseline

The current level already establishes a useful Marine Slayer level grammar:

```text
exploration
  -> locked route / objective clue
  -> keycard or access objective
  -> combat room
  -> inventory/resource management
  -> additional access route
  -> shuttle/extraction objective
```

The final campaign should expand this grammar rather than abandon it.

### Preserve

- modular sci-fi corridors and rooms;
- locked/access-controlled routes;
- colored keycard logic where appropriate;
- meaningful backtracking kept short and readable;
- environmental navigation tied to objectives;
- room-to-room combat cadence;
- distinct larger spaces such as the shuttle/hangar area.

### Expand

Later missions should add:

- branching short routes;
- arena lockdowns;
- powered doors/terminals;
- traps/turrets;
- optional resource rooms;
- multi-stage objectives;
- stronger encounter escalation;
- mission-specific environmental identity.

Do not turn every level into a featureless arena wave sequence.

---

# 8. HUD / Inventory Baseline

The current build visibly contains working UI that should be treated as inherited functionality.

Observed elements include:

- top-left radar/minimap-style display;
- top-right health/status/ammo-style information;
- interaction/keycard feedback;
- existing inventory screen;
- Xbox 360-era sci-fi visual framing.

### Preserve first

Codex must inspect the actual Unity UI implementation before building a replacement HUD.

Do not discard the current HUD/inventory just because the master design document described generic required HUD elements.

### Upgrade goals

- maintain existing placement identity where readable;
- ensure 720p title-safe visibility;
- improve information hierarchy where necessary;
- add objective/checkpoint feedback consistently;
- preserve controller-only usability;
- remove debug-only data from release presentation;
- avoid modern flat/mobile UI redesigns that visually disconnect from the current build.

---

# 9. Visual / Environment Baseline

Current gameplay uses:

- bright gray/white modular sci-fi station surfaces;
- strong contrast between floor/wall geometry and actors;
- colored/neon indicators for interactive objects;
- emergency/red lighting elements;
- blood decals/effects that remain readable against the environment.

### Preserve

- clean top-down readability;
- high contrast between actors and geometry;
- interactable color language;
- modular station identity.

### Improve carefully

- lighting variety by level;
- baked shadow/contrast detail where affordable;
- emergency states;
- selective darkness rather than making the whole game hard to read;
- stronger room landmarks;
- environment damage/decals where affordable.

Do not introduce expensive realtime shadow/lighting features simply to make the PC/Editor version prettier.

---

# 10. Performance Baseline / Observed Hitching

The owner notes visible lag/hitching in the current level and attributes part of it to texture/settings load.

The footage shows occasional micro-stutter during traversal/transition through the environment.

Codex must treat optimization as a continuation of the existing game, not a final afterthought.

Audit at minimum:

- texture dimensions and import formats;
- mipmaps where appropriate;
- texture memory;
- material count;
- shader complexity;
- realtime light/shadow count;
- particle overdraw;
- blood/decal accumulation;
- AI update frequency;
- active enemy count;
- instantiate/destroy spikes;
- Resources.Load or synchronous loading spikes;
- mesh renderer counts;
- unnecessary Update/LateUpdate loops;
- audio clip loading/decompression strategy;
- USB-vs-HDD deployment/loading differences during testing.

Do not assume the visible hitch is caused by one system until profiling/testing supports the conclusion.

---

# 11. Checkpoint / Persistence Direction Already Established by Owner

The owner explicitly states that the project should move toward a **checkpoint-based progression/retry model**, in part because the originally attempted save path/API produced compatibility problems in the existing Unity/Xbox 360 workflow.

Therefore:

- checkpoints are mandatory;
- death should return the player to a valid checkpoint;
- level transitions must establish deterministic restart state;
- checkpoint state must not depend on unsupported/deprecated APIs;
- persistence must be abstracted so the exact Xbox 360 storage implementation can be discovered/tested without rewriting gameplay logic.

Do not remove checkpoint progression in favor of a desktop-only save method.

---

# 12. Mission 01 Preservation Contract

Mission 01 must be rebuilt/extended from the current playable first-level identity, not replaced wholesale.

Required inherited beats:

1. Voss awakens after cryogenic interruption.
2. Facility/station AI establishes the immediate situation.
3. Nearby hostile Thralls are detected.
4. Voss begins with little/no ranged weapon capability and must survive the opening state.
5. The level introduces access/keycard progression.
6. Blue Armory access exists in the level logic.
7. Yellow Sick Bay access exists in the level logic.
8. Red Research Lab access exists in the level logic.
9. Research Lab access is tied to opening/restoring the shuttle-bay route.
10. The player fights/explores through the station using the established top-down gameplay.
11. The mission reaches the shuttle/hangar/exfiltration route.
12. The expanded campaign then provides a credible reason Voss cannot simply leave and end the game.

The current first level may be polished, expanded, optimized, rearranged for better flow, or have bugs fixed, but its recognizable story/gameplay structure must remain.

---

# 13. Development Tasks Added From Gameplay Review

These are mandatory additions to Milestone 001.

## GP-01 — Preserve current playable systems before refactor

- inventory current player/camera/HUD/inventory/keycard/door/enemy/spawner systems;
- capture baseline screenshots/video locally if possible;
- record which scripts/prefabs/scenes implement each system;
- do not replace them until behavior and dependencies are understood.

## GP-02 — AI navigation/spawn correction

- eliminate wall traversal;
- eliminate outside-map spawning;
- validate patrol-vs-horde mode use;
- add unreachable-enemy encounter failsafe.

## GP-03 — Camera/occlusion polish

- preserve camera identity;
- evaluate subtle aim look-ahead;
- implement low-cost wall/player occlusion handling if needed;
- verify no camera change compromises 30 FPS target.

## GP-04 — Combat-feel polish

- tune recoil/impact/flinch/audio feedback;
- correct movement/animation foot sliding;
- preserve existing responsiveness.

## GP-05 — Current HUD/inventory integration

- audit rather than replace;
- preserve existing visual language;
- make objective/checkpoint/status additions fit the existing HUD.

## GP-06 — Checkpoint implementation

- implement deterministic checkpoint restart;
- integrate mission objective/keycard state;
- verify death/retry does not duplicate pickups/enemies/doors incorrectly.

## GP-07 — Xbox 360 performance audit of current level

- profile current first level before adding large amounts of new content;
- identify measured CPU/GPU/memory/loading bottlenecks;
- establish an optimized baseline that later missions must not exceed without evidence.

---

# 14. Acceptance Criteria for Preserving the Current Feel

Before the project moves deep into campaign production, the upgraded first mission must satisfy all of the following:

- recognizable as the same Marine Slayer shown in the 04:00+ gameplay footage;
- same top-down tactical gameplay identity;
- Voss remains the established player identity;
- Thralls remain an established enemy family;
- keycard/access progression remains recognizable;
- current HUD/inventory behavior is preserved or clearly improved rather than arbitrarily replaced;
- no enemies walk through solid walls;
- no intended encounter enemies spawn outside the playable space;
- no encounter can soft-lock because an enemy is unreachable;
- movement/aiming remain responsive;
- weapon feedback is improved without excessive screen shake;
- checkpoint retry works;
- performance is at least as stable as the optimized baseline on actual Xbox 360 hardware.

---

# 15. What Codex Must NOT Infer From the Footage

The gameplay video does not by itself authorize Codex to assume:

- exact final weapon roster;
- exact final number of enemy types;
- exact final campaign length beyond the approved milestone;
- a final name/personality for the station AI if project files do not define one;
- final lore behind the Thralls unless supported by existing project data or the approved story bible;
- that current visual bugs are intentional;
- that current UI/debug values are final production presentation;
- that every current parameter value must remain unchanged.

Preserve identity, fix defects, and expand deliberately.
