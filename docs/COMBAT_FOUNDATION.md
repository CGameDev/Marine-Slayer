# Combat Foundation

The project-owned combat layer establishes one shared `IDamageable` contract,
`DamageInfo`, damage types, reusable `Health`, a bounded projectile pool and a
player weapon controller. The pool is preallocated and capped to avoid
continuous combat-time Instantiate/Destroy behavior.

The canonical runtime catalog now contains all nine weapons:

- M-77 Gavel Combat Shotgun
- VX-90 Lancer Assault Rifle
- EID-3 Plasma Cutter
- TX-40 Arc Thrower
- Fury Gauntlet MKII
- Horizon RIFT Grenade
- UEMF Tri-Shot Rail Pistol
- Helion Industrial Sawblade Launcher
- ASCENDANT Unity Beam Rifle

Each entry has an initial data-driven delivery mode, damage profile, cadence,
magazine, reserve, reload time, range and projectile behavior. The player can
fire, reload and cycle the catalog. Hitscan, projectile, melee and sustained
beam delivery modes share the same damage contract; the sustained beam
uses repeated hitscan ticks and a heat/overheat resource.

Distinct mechanics now represented in the runtime are:

- Gavel: six-pellet spread;
- Lancer: automatic ballistic fire;
- Plasma Cutter: two-target line penetration;
- Arc Thrower: two-target secondary chaining inside a bounded radius;
- Fury Gauntlet: short-range sphere-cast melee;
- RIFT Grenade: pooled projectile with radial damage and distance falloff;
- Tri-Shot Rail Pistol: three penetrating rails per trigger pull;
- Sawblade Launcher: pooled projectile with three bounded ricochets;
- Unity Beam Rifle: automatic beam ticks with heat, overheat lockout and cooling.

Hits and deaths now have project-owned color-flash and bounded collapse
feedback. Projectile, raycast, overlap and chain queries use fixed-capacity
buffers to avoid continuous combat-time allocations on Xbox 360.

The foundation test room includes a damageable target. Automated Windows
runtime validation confirms the full catalog is present, fires the Gavel,
checks magazine and reserve transfer during reload, switches to the Lancer and
confirms damage through the shared contract. The same combat code compiles into
the Xbox 360 build.

M04 remains in progress. Weapon-specific secondary effects (arc chaining,
plasma cuts, grenade fields, sawblade ricochet and beam heat), presentation,
audio, model/VFX integration, tuning and representative interactive Xbox tests
are still required before the catalog is production-complete.
