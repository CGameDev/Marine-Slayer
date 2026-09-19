# Licensed asset import — 2026-09-19

Imported the verified Xbox360TutorialAssets.unitypackage into the fresh
Unity 5.4.1f1 repository project. Import completed successfully; log is
`Logs/asset-import.log` (local only). Vendor sources remain unmodified and
Git ignored, including their root folder metadata.

Inventory: 42 vendor C# scripts, 10 vendor scenes, 101 prefabs, 151 FBX files,
102 materials, 14 shaders, 9 MP3 and 2 WAV files. The independently created
toolchain test scene and editor utility are Marine Slayer-owned.

Only the supplied InputManager was adopted after backing up fresh settings.
Other supplied settings require a separate audit before adoption. No legacy
scene list was imported. Vendor demos are not campaign scenes.

## Initial source observations (not runtime acceptance)

| System | Source | Initial decision | Remaining work |
|---|---|---|---|
| Movement/input | PrTopDownCharController | WRAP | Centralize input; inspect inventory/camera coupling and debug keys |
| Camera | PrTopDownCamera | WRAP | Validate framing, follow and controller aim |
| Weapons | PrWeapon and related prefabs | WRAP/REFACTOR | Audit allocations, pool shots/effects, map canonical weapons |
| Enemies | PrEnemyAI | REFACTOR | Scene tag searches, repeated component lookup, destructive death cleanup and VFX spawning need review |
| Station modules | SciFi_TopDown_SpaceStation | USE | Build new layouts, validate collision and Xbox shaders |
| Soldier/mech/drone | Corresponding vendor asset groups | USE | Map canonical silhouettes and animation roles |

The import alone does not validate prefab references, gameplay, controller
behavior or Xbox performance. M01 remains in progress until the capability
audit and lore-to-asset mapping are complete. Full Resources directories
can be included even in a minimal baseline build; its size is not a final
production memory budget.
