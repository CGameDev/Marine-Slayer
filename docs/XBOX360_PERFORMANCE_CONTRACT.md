# Marine Slayer — Xbox 360 Performance Contract

## 1. Platform reality

Xbox 360 is the primary performance authority for Marine Slayer.

The hardware provides a three-core PowerPC CPU and 512 MB of unified system memory shared by CPU/GPU workloads. The project must therefore be designed as a bounded console game, not as a desktop Unity game later reduced at the end.

## 2. Frame-rate target

### Mandatory baseline

- **30 FPS target** on Xbox 360.
- Frame-time budget: approximately **33.3 ms**.
- Stable pacing is more important than occasional high peaks.

### Stretch target

- 60 FPS is not required for Milestone 001.
- If isolated scenes can run at 60 FPS, do not redesign the entire game around that unless all campaign scenes can sustain it reliably.

### Failure threshold

Repeated combat drops that make aiming/rolling feel unreliable or create long periods materially below the 30 FPS target are release blockers.

## 3. Performance authority order

When a visual/gameplay choice exceeds target budgets:

1. preserve core input responsiveness;
2. preserve gameplay correctness;
3. preserve enemy readability;
4. reduce nonessential VFX/lighting;
5. reduce background/detail density;
6. simplify materials/shaders;
7. reduce simultaneous active enemy count only as a last gameplay-facing adjustment.

Do not fix performance by breaking the intended combat loop.

## 4. Memory policy

Do not guess that all 512 MB is available to game content.

At bootstrap, Codex must measure actual Unity/Xbox runtime memory consumption on target hardware using the available Unity/XDK profiling tools.

Create `docs/PERFORMANCE_BASELINE.md` containing:

- empty/system scene baseline;
- imported demo-scene baseline;
- Marine Slayer representative combat scene baseline;
- texture memory estimate;
- mesh memory estimate;
- audio memory estimate;
- managed heap behavior;
- runtime native memory;
- observed headroom before instability.

Only after this measurement should final per-category budgets be locked.

## 5. Provisional memory discipline

Until target profiling is available:

- avoid loading multiple full missions at once;
- unload unused scenes/assets between missions;
- avoid giant always-resident texture sets;
- avoid uncompressed long audio unless proven affordable;
- use LOD assets supplied by the pack;
- limit duplicate materials/textures;
- keep persistent services minimal;
- avoid runtime-generated texture copies;
- clean up unused mission references.

A mission should not rely on memory behavior that only works in the Windows Editor.

## 6. Scene loading

Each campaign mission should be independently loadable.

Use loading transitions to:

- unload prior mission content;
- release unused assets where safe;
- initialize the next mission cleanly;
- avoid overlapping high-memory scenes unless profiling demonstrates it is safe.

Never keep every campaign level resident.

## 7. Texture rules

Codex must audit imported textures.

For each major texture family:

- confirm maximum source size;
- select Xbox-appropriate max size;
- use compression supported by the target pipeline;
- disable Read/Write when not needed;
- avoid mipmaps for UI where unnecessary;
- use mipmaps for 3D surfaces where beneficial;
- avoid duplicated copies with differing import settings unless justified.

Environment textures should prioritize visual consistency over excessive resolution.

## 8. Mesh/animation rules

- Prefer supplied LOD0/LOD1 variants for soldier/mech/drone assets.
- Use LOD switching appropriate for the elevated camera.
- Disable skinned renderers for dead/removed enemies after effects complete if the body is no longer required.
- Avoid keeping many ragdolls active simultaneously.
- Cap persistent corpses/debris.
- Reuse animation controllers where possible.

## 9. Lighting rules

Preferred hierarchy:

1. baked/static lighting for station structure;
2. limited realtime lights for gameplay-critical effects;
3. emissive materials/VFX for ambience;
4. dynamic shadows only where their cost is justified.

Avoid many overlapping realtime shadow-casting lights.

Mission identity should come from controlled lighting composition, not sheer light count.

## 10. Shader/material rules

- Validate all vendor shaders on Xbox 360.
- Replace unsupported/expensive shader features with compatible variants.
- Minimize material proliferation.
- Prefer shared material instances.
- Avoid runtime material instantiation through `renderer.material` in hot/repeated code unless intentionally required; prefer shared-material behavior or controlled cached instances.
- Profile transparency/overdraw from particles and glass.

## 11. Particle/VFX rules

- Pool frequent VFX.
- Limit simultaneous explosions.
- Cap decals.
- Cap blood effects.
- Avoid long-lived particle systems.
- Reduce overdraw in full-screen/top-down camera view.
- Lower particle count before removing gameplay feedback entirely.

## 12. CPU/Mono rules

Avoid unnecessary per-frame work:

- no repeated scene-wide `Find*` calls;
- no repeated `GetComponent` in hot loops when references can be cached;
- no LINQ in combat update paths;
- avoid string construction/logging each frame;
- minimize managed allocations in `Update`/`LateUpdate`/AI loops;
- reuse lists/buffers where practical;
- avoid large reflection-based systems;
- stagger AI decision updates;
- pool projectiles/effects.

Garbage collection spikes during combat are release defects if perceptible.

## 13. AI budget

Active AI count is governed by target profiling.

Start encounter development with conservative simultaneous counts and scale upward only after representative console tests.

Required techniques:

- sleeping/dormant enemies outside active encounter;
- bounded spawner counts;
- pooled or cleanly destroyed actors;
- staggered perception/path decisions;
- pathfinding only when needed;
- no off-screen AI simulation without gameplay purpose.

## 14. Physics budget

- Use simple colliders where possible.
- Avoid excessive MeshColliders on dynamic objects.
- Keep projectile physics bounded.
- Use raycast weapons where the supplied system makes sense and physical projectiles only when they improve gameplay.
- Disable physics on settled debris/corpses after a short period.
- Keep layer collision matrix intentional.

## 15. Audio budget

- Tune import/compression settings per clip role.
- Avoid loading unnecessary long clips uncompressed.
- Limit simultaneous one-shot SFX spam.
- Use pooled/reused AudioSources where practical.
- Music/dialogue transitions must not allocate excessively during combat.

## 16. UI budget

- Keep canvases/screens simple.
- Avoid rebuilding large UI hierarchies every frame.
- Update numeric text only when values change where practical.
- Ensure subtitle/UI effects do not generate continuous garbage.

## 17. Resolution and safe area

Primary design target: **1280x720 (720p), 16:9**.

UI must respect television-safe margins.

If lower output modes are supported by the platform/build, layout must remain readable and not depend on tiny text.

## 18. Profiling checkpoints

Target-hardware profiling is mandatory after these grouped phases:

### P1 — Core combat

Test:

- player;
- one soldier archetype;
- rifle/projectiles;
- HUD;
- representative station room.

### P2 — Full combat roster

Test:

- soldiers;
- melee;
- drones;
- mechs;
- turrets;
- VFX;
- multiple waves.

### P3 — Representative production level

Test a complete mid-campaign mission with final-like lighting and encounter density.

### P4 — Boss load

Test worst-case boss encounter plus adds/VFX/hazards.

### P5 — Full campaign soak

Play multiple missions sequentially to detect memory growth/leaks/stale persistent objects.

## 19. Performance regression rule

For every major regression:

1. reproduce on target;
2. capture scene/encounter;
3. identify CPU/GPU/memory/GC category;
4. fix root cause;
5. re-test same scenario;
6. document the result.

Do not reduce quality randomly without identifying the bottleneck.

## 20. Release gate

Marine Slayer cannot be called Xbox 360 complete until:

- all campaign missions load on target;
- no mission consistently violates the gameplay frame-rate floor;
- no repeatable out-of-memory crash exists in normal campaign progression;
- checkpoint reloads remain stable;
- boss fights meet responsiveness requirements;
- sequential mission play does not show uncontrolled memory growth;
- controller input remains responsive during worst-case encounters.