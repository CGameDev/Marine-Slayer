# Marine Slayer — Level 01: Cold Rebirth

`MS_L01_ColdRebirth` is the first campaign-production scene and the default destination for New Game.

## Canonical identity

- Location: Cryo-Bay 09.
- Mission: `Cold Rebirth`.
- Rhyker Voss awakens amid failed cryogenic equipment, steam, flickering emergency lights and dead personnel.
- The level teaches interaction, movement, melee combat and checkpoint recovery.
- The first enemies are unstable Convergence Thralls.
- Completion represents Voss escaping toward the Crew Ring and unlocks Level 02.

The scene reuses the licensed station floor, vent, panel, crate, barrel and soldier assets while keeping all campaign logic project-owned.

## Objective flow

1. Access the Cryo-Bay release console and read the emergency wake report.
2. Move through Cryo-Bay 09 to decontamination.
3. Survive two bounded Thrall waves while the containment gates are locked.
4. Resume from the decontamination checkpoint if Voss dies during the encounter.
5. Cross the reopened route and reach the Crew Ring exit.
6. Persist `l01-cold-rebirth`, unlock Level 02 and enter the level-complete state.

## Checkpoints

| Checkpoint | Meaning |
|---|---|
| `l01-awakening` | Initial wake position in Cryo-Bay 09 |
| `l01-decon` | Decontamination reached; encounter restarts safely |
| `l01-thralls-cleared` | Thrall encounter completed and gates reopened |
| `l01-complete` | Crew Ring exit reached and mission completed |

## Verification boundary

The automated flow validates fresh campaign entry, environmental identity, release-console interaction, both waves, a simulated death/reload at decontamination, objective deduplication, completion, Level 02 unlock and save reload. Windows and Xbox builds pass, the complete package is deployed with a matching remote XEX hash, and the Xbox cold boot is clean. Manual Xbox traversal, combat feel and performance remain open acceptance work.
