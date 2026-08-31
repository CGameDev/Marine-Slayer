# Marine Slayer — Gameplay Implementation Guide

## Status

This is a **subordinate implementation guide**, not a source of canon.

Creative/content authority is:

`docs/OWNER_CANON_SOURCE.md`

The game must be built from scratch in Unity 5.4.1f1 using the licensed asset pack.

The YouTube footage is play-style reference only:

`docs/PLAYSTYLE_REFERENCE.md`

If this file conflicts with the Lore Bible/canon source, the canon source wins.

---

# 1. Game identity

Marine Slayer is a controller-first, elevated top-down sci-fi action/horror shooter for Xbox 360.

Core design pillars:

- tactical spatial readability;
- responsive movement and aiming;
- aggressive combat momentum;
- clear enemy-role recognition;
- purposeful room/corridor encounter design;
- strong environmental storytelling;
- escalating body/industrial/psychic horror across Acts I–V;
- checkpoint-driven progression;
- stable Xbox 360 performance.

The game is not an FPS, over-the-shoulder shooter, mobile arena game, tower-defense game, or score-only twin-stick arcade title.

---

# 2. Camera / movement target

Use the YouTube reference only for the desired general feel.

Target:

- elevated oblique/top-down camera;
- tactical visibility of the player and nearby threats;
- smooth responsive follow;
- controller-first movement;
- independent aim direction where practical;
- strafing while firing;
- quick but readable facing changes;
- no excessive cinematic inertia;
- optional low-cost aim look-ahead;
- low-cost occlusion treatment where necessary.

Never make camera presentation more expensive at the cost of target stability.

---

# 3. Combat philosophy

Combat should reward movement, target prioritization and weapon choice.

Typical loop:

1. enter/scan space;
2. identify threats;
3. reposition/strafe/dodge if available;
4. engage with appropriate weapon;
5. exploit stagger/vulnerability;
6. recover resources;
7. use environment/tools;
8. complete objective/encounter;
9. advance.

Avoid static bullet-sponge combat.

---

# 4. Canonical arsenal

Use the identities defined by the Lore Bible:

- M-77 `Gavel` Combat Shotgun;
- VX-90 `Lancer` Assault Rifle;
- EID-3 Plasma Cutter;
- TX-40 Arc Thrower;
- Fury Gauntlet MKII;
- Horizon RIFT Grenade;
- Tri-Shot Rail Pistol;
- Helion Sawblade Launcher;
- ASCENDANT `Unity` Beam Rifle.

Utility concepts:

- breach torch;
- mesh disruptor;
- stims;
- magnetic grappler.

Use supplied asset-pack prefabs/models/scripts as internal foundations where suitable, but final player-facing names/roles must align with canon.

---

# 5. Canonical enemy gameplay roles

## Thralls

Early aggressive pack pressure.

## Spinewalkers

Ambush/vertical threat. Simulate wall/ceiling behavior with controlled scripted routes if true surface navigation is too expensive.

## Apex Hunters

Fast elite stalkers with intelligent pressure.

## Convergence Brutes

Heavy force/space-denial enemies.

## Mesh Sirens

Psychic/deception enemies using readable distortion and false cues.

## Riftbound Abominations

Late anomalous threats using controlled displacement/flicker/teleport behaviors.

Boss-class:

- Red Engineer;
- Null Sister;
- assimilated Commander Sol encounter;
- Prime Convergence.

Do not replace these with generic vendor enemy names in player-facing content.

---

# 6. Level design grammar

Each level should combine some subset of:

- traversal;
- combat spaces;
- environmental storytelling;
- lore logs/terminals;
- doors/power/control interactions;
- hazards;
- optional resource routes;
- scripted escalation;
- act-specific visual identity;
- checkpoints;
- exit/transition sequence.

Do not make every level a corridor followed by the same rectangular arena.

No two consecutive levels should feel interchangeable.

---

# 7. Five-Act escalation

## Act I — The Awakening

Focus:

- disorientation;
- cryo aftermath;
- human spaces;
- early Thralls/Spinewalkers;
- learning movement/combat;
- first evidence of the station becoming wrong.

## Act II — Into the Depths

Focus:

- industrial hazards;
- machinery behaving as biology;
- Brutes/Apex escalation;
- aggressive action;
- Red Engineer climax.

## Act III — Engineering the Damned

Focus:

- ASCENDANT research horror;
- psychic/mesh systems;
- Mesh Sirens/Riftbound;
- perception distortion;
- Null Sister climax.

## Act IV — Command & Catastrophe

Focus:

- failed human resistance;
- tactical/command spaces;
- stronger coordinated enemies;
- emotional tragedy of Commander Sol;
- command-collapse climax.

## Act V — The Spire

Focus:

- architecture/reality becoming abnormal;
- gravity/mesh hazards;
- elite enemy combinations;
- direct VANGUARD confrontation;
- Prime Convergence.

See `docs/OWNER_CANON_SOURCE.md` for the authoritative 25-level list.

---

# 8. HUD / UI principles

Build a new UI for this scratch project.

Required information:

- health;
- active weapon;
- ammo/resource;
- grenade/tool state;
- interaction prompt;
- objective update;
- checkpoint feedback;
- boss health;
- lore/terminal reading UI;
- pause/options.

Requirements:

- controller-only usable;
- readable at 720p TV distance;
- title-safe;
- consistent sci-fi military/horror visual language;
- not copied from the old video HUD or another commercial game.

---

# 9. Checkpoint / progression principles

Required:

- deterministic checkpoint IDs;
- safe death/retry;
- level completion tracking;
- act/campaign progression;
- ending-state tracking;
- persistence abstraction compatible with the discovered Xbox 360 environment;
- version/corruption handling.

Do not make checkpoint logic dependent on unsupported modern Unity APIs.

---

# 10. Lore / terminal integration

The Lore Bible contains extensive optional logs/reports.

Implement a reusable system supporting:

- category;
- title;
- body text;
- collected/read state;
- optional audio clip;
- subtitles/text;
- replay from a lore menu where practical.

Optional lore must enrich, not block, core campaign comprehension.

---

# 11. Performance principles

Xbox 360 is the final authority.

Prefer:

- bounded enemy counts;
- pooled projectiles/VFX;
- simple AI scheduling;
- baked lighting;
- controlled realtime effects;
- LODs;
- texture discipline;
- reusable materials;
- limited transparent overdraw;
- deterministic cleanup;
- segmented scenes where needed.

Profile before claiming a numerical budget.

---

# 12. Completion

This gameplay guide is successfully implemented only when the scratch-built game supports the full canonical 5-Act / 25-Level campaign defined by the owner lore and passes the master milestone's Xbox 360 acceptance gates.
