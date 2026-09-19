# Menu, Continue and settings foundation

The main menu now provides controller-focused `Continue`, `New Game`, `Options`
and `Credits` paths using Xbox-safe world-space text.

- Left stick or keyboard arrows move focus.
- `A` or Enter selects.
- `B` or Escape returns from Options or Credits.
- Continue is visibly unavailable when no campaign save exists.
- Continue reloads the saved scene and checkpoint without resetting campaign
  progress.

Options currently expose master volume and subtitles. They are stored in a
separate versioned settings file, so New Game cannot overwrite user
preferences. Master volume is applied immediately through Unity's global audio
listener and the project audio service.

Automated coverage opens and closes Options and Credits, changes both settings,
creates a new campaign, reloads the independent settings file, completes the
foundation mission, returns to the menu and uses Continue to restore the
completed encounter checkpoint. Physical Xbox validation of stick navigation,
720p focus readability and button feel remains open.
