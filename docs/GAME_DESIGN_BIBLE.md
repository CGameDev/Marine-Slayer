# Marine Slayer — Game Design Bible

## 1. Game identity

**Marine Slayer** is a controller-first, top-down sci-fi action shooter designed specifically around the strengths and constraints of Xbox 360 hardware and Unity 5.4.1f1.

The game combines:

- tactical spatial readability;
- deliberate positioning and flanking;
- fast movement and dodge/roll evasion;
- aggressive combat momentum;
- rapid weapon switching;
- arena escalation;
- environmental hazards;
- compact mission objectives;
- checkpoint-driven campaign pacing.

The final game must feel purposeful and authored, not like a collection of asset-pack demo scenes.

## 2. Camera and perspective

Primary camera:

- elevated top-down / tactical oblique camera;
- keeps player, threats and combat space readable;
- smoothly follows without nausea-inducing lag;
- may zoom slightly by encounter type but must remain top-down;
- never becomes first person;
- never becomes a conventional over-the-shoulder third-person shooter.

Camera collision/occlusion handling must prevent station geometry from hiding the player during combat.

## 3. Core combat loop

The intended loop is:

1. enter/scan a combat space;
2. identify priority threats;
3. move, roll, flank or use cover geometry;
4. engage using the appropriate weapon;
5. push aggressively when enemies stagger or break;
6. recover ammo/health/resource drops;
7. use doors, terminals, traps or environment to change the encounter;
8. defeat the wave/commander/objective;
9. advance quickly into the next combat or narrative beat.

Combat should reward momentum without becoming mindless. Staying still should usually be less effective than controlled movement.

## 4. Player movement

Mandatory movement set:

- 360-degree analog movement;
- independent aiming where supported by the existing top-down controller;
- sprint or fast-move state if stable on controller;
- dodge/roll with brief defensive utility;
- directional facing/aiming;
- interaction/use;
- weapon switch;
- reload;
- melee;
- grenade;
- pause.

Movement must remain responsive under enemy load on target hardware.

## 5. Recommended Xbox 360 controller mapping

Exact mapping must be validated against the supplied InputManager and existing controller code before implementation, but the preferred player-facing scheme is:

| Input | Action |
|---|---|
| Left Stick | Move |
| Right Stick | Aim |
| RT | Fire |
| LT | alternate aim/secondary behavior if required by weapon system |
| A | Interact / confirm |
| B | Dodge/Roll |
| X | Reload / contextual action if safe |
| Y | Weapon switch / weapon wheel shortcut |
| RB | Grenade |
| Right Stick Click | Melee if reliable |
| LB | secondary weapon / quick swap / equipment depending final control validation |
| Start | Pause |
| Back | Objective/status screen if implemented |

Do not finalize mapping until existing package input behavior has been tested on an Xbox controller.

## 6. Weapons

The campaign should use the supplied weapon assets as a deliberate arsenal.

### 6.1 Sidearm

Pistol:

- reliable;
- accurate;
- low damage;
- economical ammo;
- fallback weapon.

### 6.2 Assault rifle

Rifle:

- default general-purpose weapon;
- medium range;
- sustained fire;
- good against standard soldiers/drones.

### 6.3 Shotgun

- high close-range burst;
- strong against rushers;
- limited magazine;
- rewards aggressive positioning.

### 6.4 Laser weapon

- energy-based sustained/precision damage;
- useful against armor or drones;
- visually distinct;
- may use heat/energy ammo depending existing asset framework.

### 6.5 Rocket launcher

- rare heavy weapon;
- strong splash damage;
- limited ammo;
- used for heavy enemies/bosses and crowd control.

### 6.6 Melee weapon / combat strike

- fast close-range emergency damage;
- may stagger weakened enemies;
- should never lock the player into a long animation that becomes unfair from top-down view.

### 6.7 Grenade

- limited throwable resource;
- arc/target indicator if implementation remains readable;
- useful for groups and armored positions.

## 7. Resource economy

Resources:

- health;
- weapon ammo;
- grenades;
- optional temporary armor/overcharge if implemented with existing systems.

Rules:

- the player should rarely become permanently soft-locked because of zero ammo;
- low-resource states should create tension, not require restarting entire missions;
- aggressive kills/arena completion can spawn controlled recovery pickups;
- heavy weapon ammo remains scarce;
- checkpoints should restore only enough resources to keep a retry viable, not trivialize difficulty.

## 8. Enemy roster

Use existing supplied assets and create behavior variants through code, materials, weapons, stats and encounter design.

### E1 — Rifle Trooper

Base: Enemy Soldier.

Behavior:

- medium-range fire;
- moves between combat positions;
- pressures stationary player;
- foundation enemy.

### E2 — Breacher

Base: melee enemy soldier.

Behavior:

- rapidly closes distance;
- forces movement;
- can arrive through doors/side routes;
- low-to-medium durability.

### E3 — Suppressor

Base: soldier with rifle variant.

Behavior:

- slower movement;
- more sustained fire;
- pins player lanes;
- paired with Breachers.

### E4 — Scout Drone

Base: Space Drone.

Behavior:

- mobile ranged harassment;
- bypasses some ground positioning;
- low durability;
- prioritizes movement/aim disruption.

### E5 — Hunter Drone

Base: Drone with aggressive behavior variant.

Behavior:

- alternates ranged burst and close attack;
- higher speed;
- flanks from alternative approach routes.

### E6 — Combat Mech

Base: Humanoid Mech.

Behavior:

- armored heavy enemy;
- rifle fire;
- high stagger resistance;
- telegraphed attacks;
- requires movement and weapon choice.

### E7 — Mech Berserker

Base: Mech melee animation set.

Behavior:

- aggressive melee heavy;
- high threat radius;
- punish predictable dodging;
- used sparingly.

### E8 — Turret

Base: supplied turret prefabs.

Behavior:

- static area denial;
- may be disabled through terminals or destroyed;
- used to shape combat routes.

### E9 — Corrupted Defense Node

Base: turret/device/environment combination.

Behavior:

- shields/activates other hazards or spawners;
- becomes objective target;
- creates encounter puzzle without slowing combat excessively.

## 9. Boss philosophy

Bosses should be built by extending supplied mech/drone/defense assets rather than requiring entirely new character models.

Each boss must introduce a mechanically distinct test and include readable attack telegraphs.

### B1 — Warden Havelock

Heavy mech commander with rotating rifle, grenade and charge phases.

### B2 — The Harrow Swarm

Drone-controller encounter where the true objective alternates between drone waves and vulnerable control cores.

### B3 — Twin Praetors

Two complementary elite mechs: one ranged and one melee. Player must manage space and target priority.

### B4 — Marshal Veyr / Rift Harness

Final encounter using a heavily modified mech platform plus station hazard phases. Must be beatable through learned campaign mechanics rather than a completely unrelated gimmick.

## 10. Encounter design rules

Every combat arena should answer:

- where can the player move?
- what enemy threatens them first?
- what prevents camping?
- what makes this encounter distinct from the previous one?
- what resource decision is being tested?
- how does the arena visually communicate exits/objectives?

Avoid simply spawning more enemies in the same empty rectangular room.

Use:

- staggered doors;
- side corridors;
- destructibles;
- traps;
- turrets;
- alternating enemy roles;
- reinforcements;
- terminals;
- darkness/emergency lighting;
- timed lockdowns;
- optional flank routes;
- environmental explosions.

## 11. Campaign structure

The campaign is divided into four acts and twelve missions.

### ACT I — BREACH

#### Mission 01 — Dead Arrival

Purpose:

- teach movement, aim, fire, reload and interaction;
- establish station crisis;
- introduce Rifle Trooper and basic pickups.

Location identity:

- docking/arrival corridors;
- clean station architecture degrading into emergency lighting.

#### Mission 02 — Lockdown

Purpose:

- introduce automatic doors, terminals and Breachers;
- first meaningful arena lockdown;
- introduce grenade use.

Location identity:

- security checkpoint / habitation access.

#### Mission 03 — Red Deck

Purpose:

- introduce turrets and traps;
- first Scout Drone encounter;
- reinforce environmental combat.

Location identity:

- maintenance/red-emergency-power deck.

### ACT II — DESCENT

#### Mission 04 — The Foundry

Purpose:

- industrial hazard pacing;
- introduce Combat Mech;
- use explosive props and machinery zones.

#### Mission 05 — Black Lab

Purpose:

- heavier story delivery;
- darker lighting;
- introduce Hunter Drones;
- optional data-terminal lore.

#### Mission 06 — Warden

Purpose:

- culminate Act II with first boss, Warden Havelock;
- combine troopers, hazards and boss phases.

### ACT III — COUNTERSTRIKE

#### Mission 07 — Gun Deck

Purpose:

- faster pace;
- large firefights constrained by Xbox 360 enemy budgets;
- unlock/feature heavy weapon use.

#### Mission 08 — Ghost Circuit

Purpose:

- disable corrupted defense network;
- multi-stage objective route;
- Harrow Swarm boss encounter.

#### Mission 09 — No Safe Room

Purpose:

- sustained pressure mission;
- minimal quiet time;
- escalates mixed enemy compositions;
- ends with the protagonist choosing to proceed deeper rather than evacuate.

### ACT IV — SLAYER

#### Mission 10 — The Spine

Purpose:

- vertical-feeling station traversal using modular spaces;
- alternating tight corridors and combat chambers;
- elite/mech concentration.

#### Mission 11 — Praetor Gate

Purpose:

- Twin Praetors boss;
- final major equipment test;
- reveal route to the Rift Engine.

#### Mission 12 — Zero Hour

Purpose:

- final assault;
- callback to mechanics from earlier missions;
- final boss Marshal Veyr / Rift Harness;
- shutdown/escape sequence;
- ending and credits.

## 12. Visual distinction between levels

Because the environment kit is modular, mission identity must be achieved through authored composition rather than unrelated external assets.

Use distinct:

- lighting temperature;
- emergency/alarm states;
- fog/darkness only if affordable;
- room/corridor composition;
- door color/state;
- hazard placement;
- prop density;
- destruction state;
- enemy faction/material variants;
- audio ambience;
- objective types;
- encounter pacing.

No two consecutive missions should feel like the same corridor arrangement with different enemy counts.

## 13. Progression

### Campaign progression

- sequential missions;
- checkpoint resume within current mission;
- completed missions unlock Mission Select;
- New Game resets campaign progress after confirmation;
- Continue resumes latest valid checkpoint.

### Weapon progression

Recommended unlock order:

1. pistol + rifle;
2. grenade;
3. shotgun;
4. laser;
5. rocket launcher;
6. late-game increased availability of full arsenal.

Avoid RPG-style stat trees unless clearly justified; the game should emphasize action mastery rather than menu-heavy progression.

## 14. Difficulty levels

Mandatory:

- Recruit;
- Marine;
- Slayer.

Optional post-completion:

- Nightmare-equivalent original name such as `Extinction` only if tuning time permits.

Difficulty may adjust:

- enemy damage;
- aggression;
- reaction time;
- projectile pressure;
- pickup generosity;
- checkpoint resource floor;
- elite frequency.

Do not simply multiply enemy health excessively.

## 15. HUD

Mandatory HUD elements:

- health;
- active weapon;
- ammo/current magazine;
- grenade count;
- contextual interaction prompt;
- objective update;
- boss health where relevant.

HUD must be readable at 720p television distance and remain within title-safe boundaries.

Do not copy the UI of any inspiration title.

## 16. Menu flow

Boot -> Main Menu.

Main Menu:

- Continue (disabled when no save exists);
- New Game;
- Mission Select (after at least one mission completion, or after campaign completion depending final design);
- Challenge / Co-op if implemented;
- Options;
- Credits.

New Game -> Difficulty -> introductory cinematic/text sequence -> Mission 01.

Pause Menu:

- Resume;
- Restart Checkpoint;
- Options;
- Return to Main Menu.

## 17. Save/checkpoint model

Save data must include at minimum:

- campaign version;
- current mission;
- latest checkpoint;
- difficulty;
- unlocked missions;
- weapon unlock flags;
- completion state;
- options that should persist;
- corruption/version validation.

If platform-specific profile storage is unavailable in the first integration pass, implement a clean storage abstraction so Xbox 360 persistence can replace/test the development fallback without rewriting gameplay systems.

## 18. Narrative delivery

Narrative should occur through:

- short pre/post mission sequences;
- radio/comms dialogue during traversal;
- brief in-engine encounters;
- objective text;
- optional terminal logs;
- boss dialogue used sparingly.

Avoid long unskippable cutscenes. Maintain the action focus.

## 19. Optional challenge mode

After campaign stability, Codex may implement a compact arena/challenge mode using existing levels and encounter systems.

Potential modes:

- Survival;
- Time Attack;
- Kill Chain;
- Boss Rush after campaign completion.

Challenge mode is secondary and must not delay the complete campaign.

## 20. Local co-op

The supplied package contains local multiplayer groundwork. Two-player local co-op is desirable but is **Phase 2 priority behind campaign completion**.

If implemented:

- prefer shared camera if readable and stable;
- split-screen only if Xbox 360 performance is acceptable;
- preserve campaign objectives;
- scale enemy composition carefully rather than doubling everything;
- both controllers must work reliably.

## 21. Content completeness bar

A mission is not complete because it has a start and exit trigger.

Each mission requires:

- authored entry;
- gameplay identity;
- at least one memorable encounter;
- objective logic;
- checkpoints;
- failure recovery;
- dialogue/story beats;
- enemy/resource tuning;
- final exit/completion sequence;
- target-hardware performance validation.

The campaign is complete only when Missions 01–12 form a coherent escalating game from opening to credits.