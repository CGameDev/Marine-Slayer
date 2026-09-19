# Marine Slayer — Difficulty Foundation

The New Game flow exposes the three owner-canon difficulty names and records the selected value in campaign save data.

| Difficulty | Enemy damage | Enemy health | Player ammunition/resources |
|---|---:|---:|---:|
| Recruit | 0.75x | 0.85x | 1.25x |
| Marine | 1.00x | 1.00x | 1.00x |
| Slayer | 1.35x | 1.20x | 0.80x |

These values are foundation tuning, not final campaign balance. Enemy health is applied once when an actor enters play, enemy attacks query the active campaign profile, and initial weapon reserves are scaled when the player arsenal is created.

## Campaign safety

- New Game opens a confirmation screen when campaign data already exists.
- `CANCEL` is selected by default so an accidental confirm does not erase progress.
- Difficulty selection occurs only after overwrite confirmation.
- Starting a campaign replaces campaign progress but preserves independent options.
- Version 1 saves migrate to version 2 and default to Marine difficulty.

## Automated coverage

The foundation flow verifies confirmation/cancel behavior, selection of Recruit, scaled Thrall health and damage, scaled Gavel ammunition, save reload persistence, and Continue persistence. Full campaign balance and Xbox controller selection remain separate acceptance work.
