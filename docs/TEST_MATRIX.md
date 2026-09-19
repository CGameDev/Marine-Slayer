# Marine Slayer — Test Matrix

Status values:

- `NOT RUN`
- `PASS`
- `FAIL`
- `BLOCKED`

Codex must update this document or an equivalent generated test report with real results. Do not mark tests PASS based only on expected behavior.

## A. Project/toolchain

| ID | Test | Windows/Editor | Xbox 360 |
|---|---|---|---|
| A01 | Opens in Unity 5.4.1f1 without forced migration | PASS | N/A |
| A02 | Xbox 360 target is available | N/A | PASS (build and deploy) |
| A03 | Clean compile with no blocking errors | PASS | PASS |
| A04 | Shipping build excludes vendor demo/test scenes | PASS (foundation) | PASS (foundation) |
| A05 | Paid raw unitypackage is absent from Git-tracked/release files | PASS | PASS |
| A06 | Build manifest records commit/version | NOT RUN | NOT RUN |
| A07 | XDK console connect, deploy and remote file verification | N/A | PASS |
| A08 | XEX starts and remains in Unity runtime | N/A | PASS (Unity 5.4.1f1 runtime and game assembly loaded) |

## B. Boot/menu

| ID | Test | Editor | Xbox 360 |
|---|---|---|---|
| B01 | Boot reaches Main Menu | PASS (automated flow) | PASS (hardware framebuffer) |
| B02 | Controller can navigate menu without mouse | NOT RUN | NOT RUN |
| B03 | Continue disabled when no save exists | NOT RUN | NOT RUN |
| B04 | New Game opens difficulty selection | NOT RUN | NOT RUN |
| B05 | New Game confirmation protects existing progress | NOT RUN | NOT RUN |
| B06 | Options open/close safely | NOT RUN | NOT RUN |
| B07 | Credits open and return safely | NOT RUN | NOT RUN |
| B08 | Menu focus cannot become lost | NOT RUN | NOT RUN |

## C. Player/input

| ID | Test | Editor | Xbox 360 |
|---|---|---|---|
| C01 | Left-stick movement correct | PASS (automated movement) | PASS (owner hardware test) |
| C02 | Right-stick aim correct | NOT RUN | NOT RUN |
| C03 | Fire input responsive | NOT RUN | NOT RUN |
| C04 | Reload works and completes | PASS (automated ammo transfer) | NOT RUN |
| C05 | Roll works in all intended directions | NOT RUN | NOT RUN |
| C06 | Roll cannot pass through locked geometry | NOT RUN | NOT RUN |
| C07 | Interact prompt/action reliable | NOT RUN | NOT RUN |
| C08 | Melee works without stuck animation | NOT RUN | NOT RUN |
| C09 | Grenade works and decrements resource | NOT RUN | NOT RUN |
| C10 | Weapon switching maintains correct ammo/state | PASS (Gavel to Lancer) | NOT RUN |
| C11 | Pause/resume restores correct input/time | NOT RUN | PASS (owner hardware test) |
| C12 | Controller disconnect/reconnect handled as supported | NOT RUN | NOT RUN |
| C13 | Player movement respects scene collision | NOT RUN | PASS (owner hardware test) |

## D. Weapons

Test each weapon for fire, damage, ammo, reload/switch, VFX/SFX, death/restart and checkpoint persistence where relevant.

| ID | Weapon | Editor | Xbox 360 |
|---|---|---|---|
| D01 | M-77 Gavel Combat Shotgun | PASS (fire/damage/ammo/reload) | NOT RUN |
| D02 | VX-90 Lancer Assault Rifle | NOT RUN | NOT RUN |
| D03 | EID-3 Plasma Cutter | NOT RUN | NOT RUN |
| D04 | TX-40 Arc Thrower | NOT RUN | NOT RUN |
| D05 | Fury Gauntlet MKII | NOT RUN | NOT RUN |
| D06 | Horizon RIFT Grenade | NOT RUN | NOT RUN |
| D07 | UEMF Tri-Shot Rail Pistol | NOT RUN | NOT RUN |
| D08 | Helion Industrial Sawblade Launcher | NOT RUN | NOT RUN |
| D09 | ASCENDANT Unity Beam Rifle | NOT RUN | NOT RUN |

## E. Enemy archetypes

| ID | Enemy | Spawn | Navigation | Attack | Death/Cleanup | Xbox Perf |
|---|---|---|---|---|---|---|
| E01 | Convergence Thrall | PASS (automated roster) | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| E02 | Breacher | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| E03 | Suppressor | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| E04 | Scout Drone | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| E05 | Hunter Drone | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| E06 | Combat Mech | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| E07 | Mech Berserker | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| E08 | Turret | NOT RUN | N/A | NOT RUN | NOT RUN | NOT RUN |
| E09 | Defense Node | NOT RUN | N/A | NOT RUN | NOT RUN | NOT RUN |

## F. Encounter/objective system

| ID | Test | Editor | Xbox 360 |
|---|---|---|---|
| F01 | Encounter activates once | NOT RUN | NOT RUN |
| F02 | Doors lock at correct time | NOT RUN | NOT RUN |
| F03 | Multiple waves sequence correctly | NOT RUN | NOT RUN |
| F04 | Enemy count cannot deadlock on destroyed/disabled actor | NOT RUN | NOT RUN |
| F05 | Doors unlock on completion | NOT RUN | NOT RUN |
| F06 | Objective updates exactly once | NOT RUN | NOT RUN |
| F07 | Checkpoint after encounter records valid state | NOT RUN | NOT RUN |
| F08 | Restart restores known-safe encounter state | NOT RUN | NOT RUN |
| F09 | Terminal/device objectives cannot double-complete | NOT RUN | NOT RUN |
| F10 | Mission completion triggers once | NOT RUN | NOT RUN |

## G. Dialogue/narrative

| ID | Test | Editor | Xbox 360 |
|---|---|---|---|
| G01 | Subtitles display correct speaker/text | NOT RUN | NOT RUN |
| G02 | Combat dialogue remains readable | NOT RUN | NOT RUN |
| G03 | Dialogue priority/interrupt behavior works | NOT RUN | NOT RUN |
| G04 | Re-entering trigger does not spam non-repeat line | NOT RUN | NOT RUN |
| G05 | Dialogue queue clears correctly on mission load/restart | NOT RUN | NOT RUN |
| G06 | Subtitle setting persists | NOT RUN | NOT RUN |
| G07 | Main plot understandable without voice audio | NOT RUN | NOT RUN |

## H. Save/checkpoint

| ID | Test | Editor | Xbox 360 |
|---|---|---|---|
| H01 | Fresh save created | NOT RUN | NOT RUN |
| H02 | Continue loads correct mission | NOT RUN | NOT RUN |
| H03 | Continue loads correct checkpoint | NOT RUN | NOT RUN |
| H04 | Difficulty persists | NOT RUN | NOT RUN |
| H05 | Weapon unlocks persist | NOT RUN | NOT RUN |
| H06 | Completed missions persist | NOT RUN | NOT RUN |
| H07 | Campaign completion persists | NOT RUN | NOT RUN |
| H08 | Invalid/corrupt save fails safely | NOT RUN | NOT RUN |
| H09 | Restart checkpoint never puts player behind irreversible lock | NOT RUN | NOT RUN |
| H10 | Options persist independently of campaign restart | NOT RUN | NOT RUN |

## I. Mission-by-mission completion

Each mission must be tested from its normal campaign entry, not only by opening the scene directly.

| Mission | Fresh entry | Objective flow | Checkpoints | Death/retry | Dialogue | Completion/save | Xbox perf |
|---|---|---|---|---|---|---|---|
| L01 Cold Rebirth | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L02 Voices of the Lost | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L03 Broken Normalcy | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L04 Something in the Vents | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L05 First Light of the Machine God | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L06 The Station Breathes | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L07 Heat of the Machine | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L08 Core Pressure Rising | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L09 Where Flesh Meets Steel | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L10 Ashes of the Innocent | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L11 Ghosts in the Wires | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L12 The Choir of the Damned | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L13 Born Again Wrong | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L14 Voices Beneath the Skin | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L15 The Null Sister | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L16 Echoes of Authority | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L17 Dead Decisions | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L18 Voices That Should Be Silent | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L19 The Silence Between Stars | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L20 Sol's Last Stand | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L21 The Climb of the Damned | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L22 Graves of the Mindless | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L23 The World Bends | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L24 The Heart of Voices | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| L25 Prime Convergence | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |

## J. Boss matrix

| Boss | Intro | Phase logic | Telegraphs | Death/retry | Victory progression | Xbox performance |
|---|---|---|---|---|---|---|
| The Red Engineer | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| Null Sister | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| Commander Arwyn Sol | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| Prime Convergence | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |

## K. Difficulty

Run representative early/mid/late encounters on each difficulty.

| Difficulty | Enemy tuning | Resource tuning | Boss viability | Campaign selectable | Xbox pass |
|---|---|---|---|---|---|
| Recruit | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| Marine | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |
| Slayer | NOT RUN | NOT RUN | NOT RUN | NOT RUN | NOT RUN |

## L. Performance/soak

| ID | Test | Result |
|---|---|---|
| L01 | Core combat representative scene targets 30 FPS | NOT RUN |
| L02 | Full mixed roster representative arena targets 30 FPS | NOT RUN |
| L03 | Mid-campaign production mission target profile | NOT RUN |
| L04 | Worst boss load target profile | NOT RUN |
| L05 | No repeatable normal-play out-of-memory crash | NOT RUN |
| L06 | No uncontrolled memory growth across sequential missions | NOT RUN |
| L07 | No severe combat GC spike making input unreliable | NOT RUN |
| L08 | Loading transitions do not deadlock | NOT RUN |
| L09 | VFX/materials render correctly on Xbox | NOT RUN |
| L10 | Controller response remains usable in worst-case encounter | NOT RUN |

## M. Full campaign acceptance

| ID | Test | Result |
|---|---|---|
| M01 | Fresh-save boot-to-credits playthrough completed | NOT RUN |
| M02 | At least one Continue-after-relaunch validation in each Act | NOT RUN |
| M03 | Final boss -> ending -> credits -> post-game menu valid | NOT RUN |
| M04 | Completed-campaign save reload valid | NOT RUN |
| M05 | No debug shortcuts required to finish | NOT RUN |
| M06 | No Editor-only step required to finish Xbox build | NOT RUN |

## Release rule

Any failed item that blocks normal campaign progression, save reliability, controller operation, mission loading, final completion or target performance is a release blocker and must be fixed before M001 is closed.
