# Marine Slayer — Technical Architecture

## 1. Architecture goals

The architecture must support a complete campaign on Xbox 360 while remaining understandable enough for Codex to extend without destabilizing unrelated systems.

Primary goals:

- Unity 5.4.1f1 compatibility;
- Xbox 360 controller-first runtime;
- deterministic mission progression;
- bounded runtime allocations;
- reusable encounter logic;
- data-driven dialogue/objectives where practical;
- separation of game logic from demo-scene coupling;
- clean save/progression abstraction;
- clean platform/build abstraction;
- easy target-hardware profiling.

Do not introduce modern Unity frameworks that did not exist in 5.4.1f1.

## 2. Proposed repository/Unity structure

After the local licensed asset import, Marine Slayer-owned content should be organized separately from vendor content.

Recommended structure:

```text
Assets/
├── MarineSlayer/
│   ├── Art/
│   │   ├── UI/
│   │   ├── Materials/
│   │   └── ProjectOwned/
│   ├── Audio/
│   │   ├── Music/
│   │   ├── SFX/
│   │   └── Dialogue/
│   ├── Data/
│   │   ├── Dialogue/
│   │   ├── Encounters/
│   │   ├── Missions/
│   │   └── Balance/
│   ├── Prefabs/
│   │   ├── Characters/
│   │   ├── Enemies/
│   │   ├── Weapons/
│   │   ├── Gameplay/
│   │   └── UI/
│   ├── Scenes/
│   │   ├── System/
│   │   ├── Missions/
│   │   └── Test/
│   ├── Scripts/
│   │   ├── Core/
│   │   ├── Input/
│   │   ├── Player/
│   │   ├── Combat/
│   │   ├── AI/
│   │   ├── Encounters/
│   │   ├── Missions/
│   │   ├── Objectives/
│   │   ├── Dialogue/
│   │   ├── UI/
│   │   ├── Save/
│   │   ├── Audio/
│   │   ├── Performance/
│   │   └── Platform/
│   └── Shaders/
│
├── SciFi_Space_Soldier_Complete/   # licensed imported vendor content
├── SciFi_TopDown_SpaceStation/     # licensed imported vendor content
├── SciFi_Humanoid_Mech/            # licensed imported vendor content
└── SciFi_Space_Drone/               # licensed imported vendor content
```

Do not move vendor assets gratuitously because path/GUID changes can break prefabs and scenes.

## 3. Scene architecture

### System scenes

Recommended scenes:

- `MS_Boot.unity`
- `MS_MainMenu.unity`
- `MS_Credits.unity`

Campaign scenes:

- `MS_M01_DeadArrival.unity`
- `MS_M02_Lockdown.unity`
- `MS_M03_RedDeck.unity`
- `MS_M04_TheFoundry.unity`
- `MS_M05_BlackLab.unity`
- `MS_M06_Warden.unity`
- `MS_M07_GunDeck.unity`
- `MS_M08_GhostCircuit.unity`
- `MS_M09_NoSafeRoom.unity`
- `MS_M10_TheSpine.unity`
- `MS_M11_PraetorGate.unity`
- `MS_M12_ZeroHour.unity`

Test scenes should be excluded from shipping build settings.

The build-scene list must be regenerated because the supplied archived EditorBuildSettings references missing legacy/demo scenes.

## 4. Boot flow

`MS_Boot` responsibilities:

1. initialize persistent core services;
2. load settings/save metadata;
3. apply platform settings;
4. initialize input;
5. initialize audio;
6. display required splash/loading state;
7. transition to Main Menu.

Do not place campaign-specific content in Boot.

## 5. Persistent service model

Use a small, explicit persistent service root rather than dozens of unrelated `DontDestroyOnLoad` objects.

Suggested services:

- `GameRoot`
- `GameStateService`
- `InputService`
- `SaveService`
- `AudioService`
- `DialogueService`
- `PlatformService`
- optional `Telemetry/DebugService` compiled for development only.

Avoid generic service-locator complexity. The project is small enough to use explicit references/interfaces.

## 6. Game state model

Suggested top-level states:

```text
Boot
MainMenu
Loading
Playing
Paused
PlayerDead
MissionComplete
CampaignComplete
Credits
```

State changes should be centralized enough to prevent menus, mission scripts and death logic from fighting over `Time.timeScale`, input enablement or scene transitions.

## 7. Input architecture

Create a Marine Slayer input abstraction over the supplied InputManager names.

Example API intent:

```csharp
public interface IPlayerInput
{
    Vector2 Move { get; }
    Vector2 Aim { get; }
    bool FireHeld { get; }
    bool ReloadPressed { get; }
    bool DodgePressed { get; }
    bool InteractPressed { get; }
    bool GrenadePressed { get; }
    bool MeleePressed { get; }
    bool WeaponNextPressed { get; }
    bool PausePressed { get; }
}
```

This is an architectural example, not mandatory syntax.

Rules:

- preserve existing package mappings until verified;
- avoid hard-coded joystick button checks scattered across gameplay code;
- support controller disconnect/reconnect handling where platform APIs allow;
- keep menu and gameplay input contexts distinct;
- support up to two local players only if/when co-op is enabled.

## 8. Player architecture

The existing `PrTopDownCharController`, `PrTopDownCharInventory`, `PrWeapon*` and related scripts are the starting point.

Codex must first produce an `ASSET_IMPORT_REPORT` that documents:

- what works unchanged;
- what is demo-specific;
- what allocates excessively;
- what is tightly coupled;
- what is safe to extend;
- what must be replaced.

Preferred player separation after refactor:

```text
PlayerRoot
├── movement/controller
├── aim/facing
├── health/damage receiver
├── inventory
├── weapon controller
├── grenade controller
├── interaction
├── animation bridge
└── HUD presenter reference
```

Do not create multiple competing player-health/inventory implementations.

## 9. Combat/damage model

Establish one common damage contract used by player, enemies, destructibles and bosses.

Required concepts:

- damage amount;
- source/team;
- hit position/direction where needed;
- damage type if useful (ballistic/energy/explosive/melee/environmental);
- death event;
- optional stagger event.

Avoid reflection/dynamic dispatch in hot combat paths.

## 10. Weapon architecture

Keep weapon behavior data separate from player input.

Each weapon should define or expose:

- fire mode;
- damage;
- rate of fire;
- magazine size;
- reserve ammo;
- reload duration;
- projectile/raycast behavior;
- muzzle VFX;
- impact behavior;
- audio event;
- recoil/spread if used;
- AI compatibility if applicable.

Do not instantiate/destroy bullets, explosions and decals continuously if pooling is feasible.

## 11. Pooling

Mandatory candidates for pooling:

- projectile objects;
- impact VFX;
- muzzle VFX when appropriate;
- decals where practical;
- explosions;
- common enemy spawn instances if stable;
- temporary pickups where encounter design benefits.

Pool capacity must be bounded and tuned from target-hardware profiling.

## 12. Enemy AI architecture

Use the supplied `PrEnemyAI` as the initial implementation source.

Production AI needs explicit states such as:

```text
Dormant
Patrol/Guard
AcquireTarget
MoveToAttack
Attack
Reposition
Stagger
Melee/Charge
Retreat if archetype needs it
Dead
```

Not every archetype needs every state.

AI rules:

- expensive sensing must not run for every enemy every frame;
- use staggered update intervals for non-critical decisions;
- avoid repeated path recalculation;
- cap active enemies;
- deactivate distant/non-participating AI;
- use encounter spawners to control lifetime;
- do not leave dead agents/path objects consuming update time.

## 13. Encounter system

Campaign scenes should use a reusable encounter controller rather than custom ad-hoc wave scripts per room.

A production encounter should support:

- trigger volume or objective activation;
- one or more spawn groups;
- timed/delayed waves;
- door locking/unlocking;
- hazard/turret activation;
- completion conditions;
- reinforcement conditions;
- music/combat-state event;
- checkpoint activation after completion;
- dialogue event hooks;
- cleanup.

Encounter data can be serialized components or ScriptableObjects compatible with Unity 5.4.

## 14. Mission/objective system

Each mission needs a deterministic objective graph.

Objective types:

- reach location;
- eliminate encounter;
- interact with terminal;
- restore/disable power;
- destroy target;
- defend for bounded duration;
- boss defeat;
- escape/extract.

Objective state must be serializable enough to restore from checkpoints.

Do not derive progression solely from scene object destruction if a checkpoint reload could make state ambiguous.

## 15. Checkpoint system

A checkpoint captures the minimum authoritative state needed to retry safely:

- checkpoint ID;
- current mission;
- player position/spawn marker;
- health/resource floor or exact values as designed;
- weapon unlock/current inventory;
- completed mission objectives;
- persistent doors/devices relevant after the checkpoint;
- boss/encounter state should reset to a known checkpoint-safe configuration rather than serialize every projectile/enemy.

Prefer curated checkpoint restart states over full-world serialization.

## 16. Save architecture

Define an interface such as:

```text
ISaveBackend
├── DevelopmentFileBackend
└── Xbox360ProfileStorageBackend
```

Actual class names may differ.

Save data must be versioned.

On load:

- validate version;
- validate range/mission IDs;
- fail safely on corruption;
- never crash boot because a save file is unreadable.

## 17. Dialogue system

Dialogue is data-driven.

Required fields:

- ID;
- speaker;
- subtitle;
- trigger/event;
- optional audio reference;
- priority;
- interrupt behavior;
- cooldown/replay flag.

`DialogueService` owns subtitle timing and arbitration so combat scripts do not create their own subtitle UI.

## 18. UI architecture

Use Unity's UI system available in 5.4.1f1 or an existing package-compatible UI implementation.

Screens/presenters:

- Main Menu;
- Difficulty Select;
- HUD;
- Pause;
- Options;
- Mission Complete;
- Campaign Complete;
- Credits;
- optional Mission Select/Challenge.

All UI must be controller navigable with no mouse dependency.

## 19. Audio architecture

Audio groups:

- music;
- SFX;
- dialogue/voice if later supplied;
- UI.

Options must persist separate volume levels where practical.

Do not rely on hundreds of always-loaded clips. Large/long audio must be profiled and compression/import settings tuned for Xbox 360.

## 20. Platform abstraction

Platform-specific code should be isolated behind a small surface.

Potential responsibilities:

- save/profile storage;
- controller/user handling;
- Xbox-specific build/runtime checks;
- system UI integration if used;
- achievements only if explicitly authorized and technically available;
- debug console/deployment support.

Gameplay must not be littered with platform preprocessor directives.

Unity documentation from the relevant era exposes `BuildTarget.XBOX360` and `RuntimePlatform.XBOX360`; Codex should use the installed 5.4.1f1/Xbox support environment as the final API authority.

## 21. Build automation

Create an Editor build utility only after local toolchain discovery.

Desired capabilities:

- verify Unity version;
- verify required scenes;
- verify no test scenes in shipping list;
- verify paid raw packages are not in Assets/release output;
- build Windows development player if useful;
- build Xbox 360 target using the actual installed support module;
- write a build manifest/version file;
- place outputs outside source asset directories.

Do not invent command-line flags. Discover them locally.

## 22. Logging

Development builds should log:

- mission transitions;
- checkpoint IDs;
- save failures;
- controller disconnects;
- critical missing references;
- scene load failures;
- encounter deadlocks;
- performance warnings where useful.

Shipping builds should avoid noisy per-frame logging.

## 23. Failure prevention

Codex must actively guard against:

- duplicate persistent service objects after scene loads;
- missing scene references;
- null event subscribers;
- enemy counts never reaching zero due to disabled/dead objects;
- locked doors not reopening after encounter completion;
- checkpoint reload into invalid objective state;
- pause leaving input/time state broken;
- mission completion firing twice;
- dialogue queues persisting across scene changes unexpectedly;
- object pools returning active stale state;
- vendor demo singletons conflicting with Marine Slayer systems.

## 24. Vendor code modification policy

Prefer adapters/subclasses/wrappers or carefully documented edits where feasible.

If vendor scripts must be modified directly:

- explain why;
- keep the change minimal;
- preserve original behavior needed by assets;
- document changed files in `docs/VENDOR_MODIFICATIONS.md`;
- test affected demo-derived functionality.

## 25. Architecture completion test

The architecture is acceptable only if:

- every mission uses the same core progression framework;
- save/checkpoint flow works without scene-specific hacks;
- weapons/enemies can be tuned without rewriting player input;
- dialogue can change without recompiling dozens of gameplay scripts;
- Xbox 360-specific concerns are isolated;
- the game remains profiled and debuggable on target hardware.