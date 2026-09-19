# Enemy Foundation

The combat sandbox now contains the first project-owned canonical enemy:
three Convergence Thrall placeholders. They use a deliberately small runtime
surface suitable for Xbox 360:

- bounded eight-unit detection range;
- collision-aware Rigidbody pursuit;
- arena position clamps that prevent off-map movement;
- fixed melee range, damage and attack cadence;
- shared `Health` and `DamageInfo` contracts;
- bounded 0.4-second collapse followed by deterministic deactivation;
- damage-type color flashes for readable hit confirmation;
- no per-frame object allocation, path search or unbounded target scan.

The current capsule visuals are accepted development placeholders. Production
work still needs an asset-pack model/material adaptation, animation, hit/death
feedback, audio, encounter ownership and representative Xbox performance
measurements. Automated runtime coverage now verifies spawn count, melee damage
and death cleanup. The other five non-boss canonical enemy classes remain
pending.

The second represented archetype is a Spinewalker placeholder. It begins in an
elevated, non-colliding dormant state, watches a bounded horizontal trigger
radius, then performs a deterministic 0.45-second drop before enabling normal
collision, pursuit and heavier melee attacks. This intentionally follows the
milestone fallback of a scripted vent/ceiling ambush instead of unstable or
CPU-heavy arbitrary surface navigation. Automated runtime coverage verifies
that the ambush activates and reaches its deployed state.

The remaining non-boss taxonomy is represented by bounded sandbox
placeholders:

- Apex Hunter: fast pursuit with a lateral stalking component and rapid melee;
- Convergence Brute: slow, high-health, high-mass pressure with heavy strikes;
- Mesh Siren: range-keeping movement and periodic energy damage with a readable
  scale pulse standing in for later psychic presentation;
- Riftbound Abomination: cooldown-limited, arena-clamped displacement followed
  by close energy attacks.

All four cache the player once, use fixed arena bounds, share the damage/death
contracts and deactivate after bounded death feedback. Automated coverage
verifies that exactly one correctly configured instance of each archetype is
present. The taxonomy is now integrated into the bounded three-wave foundation
encounter. Production visuals, navigation stress tests and Xbox performance
measurements remain open.

The same sandbox update adds project-owned camera-rendered HUD text for health,
active weapon, ammunition, reload state, objective/controls, pause and death.
It replaces the former `OnGUI` test overlay, which did not render reliably on
the Xbox player. Player death now transitions through the shared game-state
service, and controller A restarts from the active checkpoint.
