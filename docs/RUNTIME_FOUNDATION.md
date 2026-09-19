# Runtime Foundation

M02 provides a fresh project-owned runtime layer under
`Assets/MarineSlayer/Scripts`. It includes the full game-state enum, one
persistent GameRoot, controlled scene loading, checkpoint/save services,
an input wrapper, dialogue/audio/lore services, platform isolation and global
state/checkpoint events.

The initial scenes are:

- `MS_Boot`: creates the persistent root and enters the menu;
- `MS_MainMenu`: controller/keyboard-accessible foundation menu;
- `MS_FoundationTest`: exercises pause, death, checkpoint restart and menu return.

The development save backend is versioned JSON under Unity's persistent data
path and fails safely on invalid input. Xbox profile/storage integration is a
later platform task; physical-console persistence is not claimed here.

Automated validation launches the Windows player with a smoke-test argument
and verifies:

`Boot -> MainMenu -> Playing -> Paused -> PlayerDead -> CheckpointRestart -> Playing -> MainMenu`

The flow passed on 2026-09-19. Windows and Xbox 360 foundation builds also
passed. Interactive controller feel, console launch, storage and target
performance remain hardware test work.
