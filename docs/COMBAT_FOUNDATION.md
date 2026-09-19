# Combat Foundation

The first project-owned combat layer establishes one shared `IDamageable`
contract, `DamageInfo`, damage types, reusable `Health`, a bounded projectile
pool and a player weapon controller that supports hitscan or physical projectile
delivery. The pool is preallocated and capped to avoid continuous combat-time
Instantiate/Destroy behavior.

The foundation test room includes a damageable target. Automated Windows
runtime validation acquires a pooled shot through the player weapon and confirms
that it reduces target health through the shared damage contract. The same
combat code compiles into the Xbox 360 build.

This is infrastructure rather than a canonical finished weapon. M04 remains in
progress until all nine lore weapons have distinct data, ammo/reload behavior,
feedback, projectiles or beams as appropriate, and representative Xbox tests.
