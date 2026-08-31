# Licensed Asset Pack Inventory

## Dependency identity

Source package supplied by the project owner:

`AssetPack_ProjectSettings.zip`

Expected local paths:

- preferred: `LocalDependencies\AssetPack_ProjectSettings.zip`
- owner workstation fallback: `C:\Users\bhinds\Downloads\AssetPack_ProjectSettings.zip`

ZIP size observed during project setup: **268,352,049 bytes**

ZIP SHA-256:

`f27f1bf3bad614b829efa530cdd9247c52b69bc539bcfa987dd68ce59da4effb`

Embedded paid package:

`Xbox360TutorialAssets.unitypackage`

Observed size: **270,479,564 bytes**

Unitypackage SHA-256:

`e8569bd920b3dab4da2304f8b2c6b5006a95d27a16e2f597325a4288a59c62ea`

## Engine confirmation

The supplied `ProjectSettings/ProjectVersion.txt` explicitly identifies:

`m_EditorVersion: 5.4.1f1`

Unity **5.4.1f1** is therefore the locked project editor unless the owner explicitly authorizes a migration.

## Package summary

The `.unitypackage` contains approximately **793 project paths** and provides a substantial production foundation rather than a simple art pack.

Observed content counts:

| Type | Count |
|---|---:|
| Unity scenes | 10 |
| Prefabs | 101 |
| C# scripts | 42 |
| FBX/model/animation files | 151 |
| Image/texture/reflection files | ~145 |
| Materials | 102 |
| Audio files | 11 |

Major asset groups observed:

| Asset group | Approx. contained project paths |
|---|---:|
| `SciFi_Space_Soldier_Complete` | 451 |
| `SciFi_TopDown_SpaceStation` | 187 |
| `SciFi_Humanoid_Mech` | 97 |
| `SciFi_Space_Drone` | 39 |
| Doors | 8 |
| Lighting | 3 |
| EnemySpawner | 2 |
| Waypoint | 2 |
| Player | 2 |
| ItemSpawn | 2 |

## Player/combat foundation

The pack contains an existing top-down player and inventory/combat framework. Important scripts include:

- `PrTopDownCharController.cs`
- `PrTopDownCharInventory.cs`
- `PrTopDownCamera.cs`
- `PrPlayerSettings.cs`
- `PrWeapon.cs`
- `PrWeaponList.cs`
- `PrWeaponLaserBeam.cs`
- `PrBullet.cs`
- `PrPickupObject.cs`
- `PrPickupAmmo.cs`
- `PrPickupHealth.cs`
- `PrPickupKey.cs`
- `PrPickupWeapon.cs`

Codex must inspect these implementations before writing replacements.

### Weapon prefabs observed

- pistol
- rifle
- shotgun
- rocket launcher
- laser weapon
- melee weapon
- grenade
- simple turret weapon
- laser turret weapon
- physical/raycast bullet variants

Examples include:

- `Weapon_Pistol_1.prefab`
- `Weapon_Rifle_1.prefab`
- `Weapon_Shotgun_1.prefab`
- `Weapon_RocketLauncher_1.prefab`
- `Weapon_Laser_1.prefab`
- `Weapon_Melee_1.prefab`
- `Grenade.prefab`

## Character/enemy foundation

### Space soldier

The main package contains a complete sci-fi soldier character set, player prefabs and enemy variants.

Observed enemy prefabs include:

- `EnemySoldier.prefab`
- `EnemySoldier_Melee.prefab`
- `EnemySoldier_Melee_TowerDefense.prefab`

Existing AI/support scripts include:

- `PrEnemyAI.cs`
- `PrEnemySpawner.cs`
- `PrWaypointsRoute.cs`
- `PrCharacterRagdoll.cs`
- `PrDestroyableActor.cs`
- `PrBloodSplatter.cs`

### Humanoid mech

The package contains a humanoid mech with LOD0/LOD1 prefabs, rifle and melee animation controllers, materials, textures and multiple action animations.

Observed animation coverage includes:

- idle;
- walk/run/sprint;
- directional aiming walk;
- shooting;
- reload;
- grenade throw;
- melee attacks;
- roll;
- hit reactions;
- multiple deaths;
- pickup/use actions.

This asset is suitable for elite/heavy enemy and boss variants.

### Space drone

The drone package contains LOD prefabs, materials/textures and animations including:

- idle;
- movement;
- ranged shooting;
- melee attack;
- directional hit responses;
- death.

This is suitable for flying/hovering pressure enemies, scout enemies and boss-derived variants.

## Environment foundation

`SciFi_TopDown_SpaceStation` is a modular station kit with corridors, intersections, rooms, doors, wall/floor modules, vents, crates, barrels, panels and lighting props.

Observed modules include:

- straight corridors;
- L corridors;
- T intersections;
- X intersections;
- open corridor variants;
- side-door/two-door variants;
- modular rooms;
- wall/internal-wall pieces;
- columns/corners;
- multiple door types including glass;
- floor vents;
- crates/barrels;
- panel props;
- light props.

This kit is the environmental base for the campaign. Codex must create visually distinct levels through composition, lighting, hazards, enemy composition, pacing, scripted events and set dressing rather than requiring unrelated third-party environment packs.

## Interactive environment foundation

Existing scripts/prefabs cover:

- automatic doors (`PrDoorManager.cs`);
- destroyable actors;
- usable devices (`PrUsableDevice.cs`);
- environmental zones (`PrEnvironmentZone.cs`);
- traps (`PrTraps.cs`);
- neutral/friendly/enemy turrets (`PrTurret.cs`);
- item spawners;
- enemy spawners;
- timed destruction;
- lights/light animation;
- waypoint routes.

These systems should be adapted into mission objectives, arena locks, hazards and scripted sequences.

## VFX foundation

Observed VFX prefabs include:

- muzzle flashes;
- bullet impact effects;
- generic explosions;
- explosion decals;
- bullet decals;
- blood splatter/death effects;
- explosive death pieces;
- laser muzzle/impact effects;
- roll/jump wave effects;
- item spawn effects.

Codex should optimize particle counts and overdraw for Xbox 360 rather than simply enabling all effects at desktop settings.

## Existing scenes

Observed scenes inside the unitypackage:

- `SciFi_Humanoid_Mech/Scenes/DemoScene.unity`
- `SciFi_Space_Drone/Scenes/DemoScene.unity`
- `SciFi_Space_Soldier_Complete/Scenes/DemoScene_LocalMultiplayer.unity`
- `SciFi_Space_Soldier_Complete/Scenes/DemoScene_LocalMultiplayer_SplitScreen.unity`
- `SciFi_Space_Soldier_Complete/Scenes/DemoScene_MultiplayerStart.unity`
- `SciFi_Space_Soldier_Complete/Scenes/DemoScene_Scripts.unity`
- `SciFi_Space_Soldier_Complete/Scenes/DemoScene_Scripts_CoopMultiplayer.unity`
- `SciFi_Space_Soldier_Complete/Scenes/DemoScene_TowerDefenseTest.unity`
- `SciFi_TopDown_SpaceStation/Scenes/All_Objects.unity`
- `SciFi_TopDown_SpaceStation/Scenes/DemoScene.unity`

These are references and implementation sources. They are **not** Marine Slayer campaign levels.

## Existing local multiplayer capability

The package includes local multiplayer examples such as:

- `MultiplayerLocalGame.prefab`;
- `PrTopDownMutiplayerCam.cs`;
- local multiplayer player prefabs;
- local multiplayer/split-screen demo scenes.

Single-player campaign completion has priority. Local co-op may reuse these foundations after the campaign runtime is stable.

## Supplied ProjectSettings caveat

The archived EditorBuildSettings includes references to several scenes that are not present in the supplied unitypackage, including legacy/example entries such as:

- `Assets/XBOX360GAME/Level01.unity`
- `Assets/Stage01.unity`
- `Assets/XBOX360GAME/Horde01.unity`

Codex must therefore **rebuild the Marine Slayer build-scene list deliberately**. Do not assume the supplied `EditorBuildSettings.asset` describes the final game.

The supplied settings should be studied for input/platform compatibility, but production settings must be validated before use.

## Controller/input observations

The supplied InputManager contains multi-controller mappings and names including:

- `Horizontal`, `Vertical`;
- `Horizontal2`, `Horizontal3`, `Horizontal4`;
- `Vertical2`, `Vertical3`, `Vertical4`;
- `Fire`, `Fire2`, `Fire3`, `Fire4`;
- `FireTrigger`, `FireTrigger2`, `FireTrigger3`, `FireTrigger4`;
- multiple Xbox controller button mappings.

Codex must preserve working Xbox 360 controller behavior while creating a documented Marine Slayer input abstraction.

## Import procedure

1. Verify SHA-256 of the local ZIP.
2. Confirm Unity 5.4.1f1.
3. Create/prepare the Marine Slayer Unity project.
4. Import the supplied ProjectSettings only after backing up repository project settings.
5. Import `Xbox360TutorialAssets.unitypackage` locally.
6. Allow Unity to generate `.meta` files consistently.
7. Inspect console warnings/errors before changing code.
8. Record import findings in `docs/ASSET_IMPORT_REPORT.md`.
9. Do not commit the paid raw source package.
10. Do commit Marine Slayer-owned scripts/scenes/configuration and only those licensed-derived project artifacts whose source-control inclusion is permitted by the owner's license.

## Asset suitability matrix

Codex should begin with this mapping:

| Marine Slayer role | Primary supplied source |
|---|---|
| player marine | Space Soldier player prefab |
| standard ranged enemy | Enemy Soldier / alternate material |
| melee rush enemy | Enemy Soldier Melee |
| flying pressure enemy | Space Drone |
| heavy enemy | Humanoid Mech |
| elite ranged enemy | Humanoid Mech rifle variant |
| boss base | Mech/drone with project-owned behavioral extensions |
| basic rifle | Rifle prefab |
| close-range weapon | Shotgun prefab |
| sidearm | Pistol prefab |
| heavy weapon | Rocket launcher prefab |
| energy weapon | Laser prefab |
| melee | Melee prefab |
| throwable | Grenade prefab |
| station world | TopDown SpaceStation modules |
| defenses | Turret prefabs |
| hazards | Trap/explosive/destroyable systems |
| health/ammo | Pickup prefabs |
| scripted access | Door/UsableDevice/Key systems |

## Non-negotiable instruction

Do not decide that the pack is 'only a tutorial' and throw it away. It is the licensed visual/gameplay production base selected by the project owner. The correct approach is to understand it, retain useful systems, remove demo-only coupling, optimize it, and build the complete Marine Slayer game on top of it.