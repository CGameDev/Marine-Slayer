# Enemy Foundation

The combat sandbox now contains the first project-owned canonical enemy:
three Convergence Thrall placeholders. They use a deliberately small runtime
surface suitable for Xbox 360:

- bounded eight-unit detection range;
- collision-aware Rigidbody pursuit;
- arena position clamps that prevent off-map movement;
- fixed melee range, damage and attack cadence;
- shared `Health` and `DamageInfo` contracts;
- immediate deterministic deactivation on death;
- no per-frame object allocation, path search or unbounded target scan.

The current capsule visuals are accepted development placeholders. Production
work still needs an asset-pack model/material adaptation, animation, hit/death
feedback, audio, encounter ownership and representative Xbox performance
measurements. The other five non-boss canonical enemy classes remain pending.

The same sandbox update adds project-owned camera-rendered HUD text for health,
active weapon, ammunition, reload state, objective/controls, pause and death.
It replaces the former `OnGUI` test overlay, which did not render reliably on
the Xbox player. Player death now transitions through the shared game-state
service, and controller A restarts from the active checkpoint.
