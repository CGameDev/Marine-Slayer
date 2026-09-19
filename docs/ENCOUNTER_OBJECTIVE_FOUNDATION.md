# Encounter and objective foundation

The foundation arena now exercises a reusable encounter lifecycle instead of
starting every enemy at scene load.

## Runtime sequence

1. The encounter activates once and locks both containment gates.
2. The pressure wave deploys three Convergence Thralls.
3. The ambush wave deploys the scripted Spinewalker and Apex Hunter.
4. The anomaly wave deploys the Convergence Brute, Mesh Siren and Riftbound
   Abomination.
5. Clearing the final wave unlocks both gates, completes the objective and
   records the `foundation-encounter-cleared` checkpoint.

Destroyed, dead or externally disabled actors no longer hold the wave open.
This gives the controller a deterministic escape from common encounter
deadlocks while keeping the actor count bounded for Xbox 360.

## Persistence contract

Completed objective IDs are stored in the version-one campaign save as an
additive field. Older development saves remain readable; a missing list is
created on demand. Restarting the cleared checkpoint reconstructs the arena in
its safe completed state: enemies remain dormant, gates remain unlocked and no
objective-completion event is emitted a second time.

The camera-rendered HUD reads the active objective from the persistent
`ObjectiveService`, so objective changes use the same Xbox-compatible path as
health, weapon and pause status.

## Automated coverage

The Windows runtime flow verifies one-time activation, gate lock/unlock,
three-wave ordering, disabled-actor deadlock recovery, deduplicated objective
updates, checkpoint creation and safe checkpoint restore. Physical Xbox play
still needs owner validation for pacing, gate readability and controller feel.
