# Marine Slayer — Milestone Status

## Active milestone

**M001 — Complete Scratch-Built Xbox 360 Game**

Authoritative specification:

`docs/MILESTONE_001_COMPLETE_GAME.md`

Creative/content authority:

`docs/OWNER_CANON_SOURCE.md`

Gameplay-feel reference rules:

`docs/PLAYSTYLE_REFERENCE.md`

Active implementation branch:

`milestone/m001-complete-game`

## Build model

**FROM SCRATCH.**

Create a new Unity 5.4.1f1 project in this repository. Do not continue or import any older Marine Slayer Unity project.

## Phase status

| Phase | Description | Status |
|---|---|---|
| M00 | Clean repository / Unity 5.4.1f1 / Xbox toolchain / licensed dependency verification | COMPLETE — Windows and Xbox baseline builds pass; physical console pending T0 |
| M01 | Licensed asset import + Lore-to-Asset mapping | COMPLETE — verified pack imported; initial audit and canon mapping documented |
| M02 | Core runtime / state / scene / checkpoint / persistence foundation | COMPLETE — automated Boot/Menu/Test/Pause/Death/Restart/Menu flow passes; console interaction pending T0 |
| M03 | Player / input / top-down camera / movement | IN PROGRESS — automated movement passes; owner Xbox movement, collision and camera-follow checks pass; broader controller tuning remains |
| M04 | Combat / canonical weapon arsenal / damage / pooling | IN PROGRESS — all nine weapons have distinct bounded mechanics; automated combat flow and Xbox deployment pass; full owner weapon matrix remains |
| M05 | Canonical Convergence enemy taxonomy / AI | IN PROGRESS — all six non-boss archetypes have distinct sandbox behavior and bounded encounter ownership; Thrall Xbox pursuit/damage passes; broader actor testing remains |
| M06 | Menus / HUD / objectives / checkpoints / lore-log systems | IN PROGRESS — HUD, menu, checkpoint, encounter, text-first lore terminal and one-time mission completion pass automated flow; Continue/options and campaign-scale UI remain |
| M07 | Act I — Levels 01–05 | NOT STARTED |
| M08 | Act II — Levels 06–10 + Red Engineer | NOT STARTED |
| M09 | Act III — Levels 11–15 + Null Sister | NOT STARTED |
| M10 | Act IV — Levels 16–20 + Sol's Last Stand | NOT STARTED |
| M11 | Act V — Levels 21–25 + Prime Convergence | NOT STARTED |
| M12 | Cinematics / dialogue / terminal & audio-log integration | NOT STARTED |
| M13 | Three canonical ending flows | NOT STARTED |
| M14 | Audio / presentation polish | NOT STARTED |
| M15 | Xbox 360 performance hardening | NOT STARTED |
| M16 | Full campaign regression / soak | NOT STARTED |
| M17 | Release candidate | NOT STARTED |
| M18 | Optional local co-op / challenge expansion after campaign stability | DEFERRED / OPTIONAL |

## Major target-hardware test gates

Do not require manual Xbox testing after every small commit. Batch target tests at:

- T0 — clean project/toolchain baseline;
- T1 — representative player/camera/combat;
- T2 — canonical weapon/enemy framework;
- T3 — Act I complete;
- T4 — Act II complete;
- T5 — Act III complete;
- T6 — Act IV complete;
- T7 — Act V + Prime Convergence complete;
- T8 — performance hardening;
- T9 — release-candidate full campaign soak.

## Completion rule

M001 remains open until the newly created Xbox 360 build can be played:

**Boot -> New Game -> Levels 01–25 -> Prime Convergence -> Canonical Ending -> Credits**

without a progression blocker and the mandatory target-hardware test matrix passes.
