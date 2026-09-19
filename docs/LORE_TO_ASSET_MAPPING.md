# Marine Slayer — Lore-to-Asset Mapping

This is the initial production mapping after importing the verified licensed
pack into the fresh Unity 5.4.1f1 project. Vendor names are implementation
sources only; all player-facing identities follow the owner canon.

## Characters and enemies

| Canonical role | Licensed foundation | Production treatment |
|---|---|---|
| Lieutenant Rhyker Voss | `Soldier_LOD0/LOD1` plus top-down controller and inventory | Project-owned player prefab, material treatment and input wrapper |
| Convergence Thrall | `EnemySoldier_Melee` | Degraded organic/tech material, melee behavior, low-cost swarm role |
| Spinewalker | Space Drone | Low hovering profile, fast flanking/lunge behavior and spine-like VFX silhouette |
| Apex Hunter | Ranged Enemy Soldier or Drone | Fast ranged stalker variant with controlled repositioning |
| Convergence Brute | Humanoid Mech LOD0/LOD1 | Heavy silhouette, stagger resistance and telegraphed close-range pressure |
| Mesh Siren | Drone plus emissive/VFX treatment | Ranged disruption/support behavior; avoid expensive full-screen effects |
| Riftbound Abomination | Mech/drone composite | Material/scale/VFX variant with bounded distortion attacks |
| The Red Engineer | Humanoid Mech plus industrial props/traps | Multi-phase forge boss using hazards and heavy melee/ranged states |
| Null Sister | Soldier/drone composite | Psychic conduit presentation, summons and telegraphed area denial |
| Commander Arwyn Sol | Soldier foundation | Unique armor/material and elite tactical boss behavior |
| Prime Convergence | Mech/drone composite | Unique project-owned assembly, multi-phase behavior and controlled gravity/VFX |

Shared vendor AI is an implementation reference. Production enemies will use
project-owned archetype/state components. `PrEnemyAI` needs refactoring because
it performs scene searches, repeated component lookups, direct Instantiate/
Destroy operations and demo-specific input/debug work.

## Canonical weapons and tools

| Canonical item | Licensed foundation | Planned identity |
|---|---|---|
| UEMF Tri-Shot Rail Pistol | `Weapon_Pistol_1` | Burst/tri-shot sidearm tuning and rail impact treatment |
| VX-90 Lancer Assault Rifle | `Weapon_Rifle_1` | Primary automatic rifle |
| UEMF M-77 Gavel Combat Shotgun | `Weapon_Shotgun_1` | Close-range spread and force response |
| EID-3 Plasma Cutter | Laser weapon/beam scripts | Short precise energy cutter |
| TX-40 Arc Thrower | Laser foundation | Chained electrical damage with strict target/effect cap |
| Fury Gauntlet MKII | `Weapon_Melee_1` | Project-owned melee timing, impact and cooldown |
| Horizon RIFT Grenade | `Grenade` | Bounded pull/distortion field built for Xbox limits |
| Helion Sawblade Launcher | Rocket/projectile foundation | Reusable projectile with ricochet/return behavior after profiling |
| ASCENDANT Unity Beam Rifle | `Weapon_Laser_1` | Late-game sustained beam with heat/resource limits |
| Breach torch | Usable-device and laser foundations | Context interaction tool |
| Mesh disruptor coils | Trap/grenade foundations | Deployable disruption utility |
| Emergency stim | Health pickup | Inventory consumable |
| Magnetic grappler | No direct match | Project-owned utility, scoped to required level design |

Weapon logic will be wrapped behind Marine Slayer input, damage and pooling
contracts. Vendor prefabs remain in place to preserve GUID references.

## World and progression

All six zones use the modular `SciFi_TopDown_SpaceStation` corridors, rooms,
doors and props. Visual identity comes from layout, lighting, hazards,
materials and encounter composition:

| Zone | Composition plan |
|---|---|
| Crew Ring | Rooms and open corridors, warmer remnants, human-scale props |
| Maintenance Ring | Narrow corridors, vents, door machinery and repair hazards |
| Research Ring | Cleaner room modules, glass doors, emissive panels and containment set dressing |
| Industrial Ring | Larger rooms, crates/barrels, traps, steam/fire hazard extensions |
| Command Ring | Ordered intersections, tactical panels, defensive turrets and elite encounters |
| The Spire | Recombined modules, dark/emissive variants, controlled distortion and gravity set pieces |

Automatic doors, usable devices, turrets, traps, pickups and spawners are
reference foundations for the project-owned encounter and objective framework.
The campaign's 25 scenes will be newly composed; none of the ten vendor demo
scenes is a campaign level.

## Known gaps and controls

- Canon-specific UI, dialogue, save/checkpoint, objectives, bosses and mission
  data require project-owned implementation.
- Exact canonical creature art is unavailable. Composite prefabs, materials,
  scale, animation and bounded VFX will establish distinct identities.
- No asset has yet passed physical-console performance or controller testing.
- Raw vendor sources remain local and ignored; project-owned scripts, scenes
  and configuration are the Git-tracked production layer.
