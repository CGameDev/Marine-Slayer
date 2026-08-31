# Marine Slayer — Milestone Status

## Active milestone

**M001 — Complete Xbox 360 Game**

Authoritative specification:

`docs/MILESTONE_001_COMPLETE_GAME.md`

Active implementation branch:

`milestone/m001-complete-game`

## Phase status

| Phase | Description | Status |
|---|---|---|
| M00 | Repository, toolchain and dependency verification | NOT STARTED |
| M01 | Asset import, vendor audit and clean project baseline | NOT STARTED |
| M02 | Core runtime/state/input/save architecture | NOT STARTED |
| M03 | Player movement, camera and combat foundation | NOT STARTED |
| M04 | Weapon arsenal, damage, pickups and pooling | NOT STARTED |
| M05 | Enemy roster, AI, spawning and hazards | NOT STARTED |
| M06 | Objectives, encounters, checkpoints and dialogue | NOT STARTED |
| M07 | Menus, HUD, pause, options and audio flow | NOT STARTED |
| M08 | Act I — Missions 01–03 | NOT STARTED |
| M09 | Act II — Missions 04–06 | NOT STARTED |
| M10 | Act III — Missions 07–09 | NOT STARTED |
| M11 | Act IV — Missions 10–12 | NOT STARTED |
| M12 | Boss hardening and campaign combat tuning | NOT STARTED |
| M13 | Difficulty, progression, mission select and final narrative integration | NOT STARTED |
| M14 | Xbox 360 performance hardening | NOT STARTED |
| M15 | Full campaign regression/soak and release candidate | NOT STARTED |
| M16 | Optional local co-op/challenge expansion after campaign stability | DEFERRED / OPTIONAL |

## Major target-hardware test gates

Do not require manual Xbox testing after every small commit. Batch target tests at:

- Gate T1 — after M03/M04 core combat is representative;
- Gate T2 — after M05/M06 full combat/encounter framework;
- Gate T3 — after Act I and one representative Act II production mission;
- Gate T4 — after all enemy/boss types are production-ready;
- Gate T5 — after full campaign content complete;
- Gate T6 — release-candidate multi-mission soak.

## Completion rule

M001 remains open until the game can be played on the Xbox 360 build from boot -> New Game -> Missions 01–12 -> ending -> credits without a progression blocker and the mandatory test matrix passes.
