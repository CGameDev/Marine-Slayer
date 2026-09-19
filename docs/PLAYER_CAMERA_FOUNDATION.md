# Player and Camera Foundation

The foundation test room instantiates the licensed Soldier LOD0 prefab as the
visual source, disconnects its demo-prefab instance, and removes vendor demo
scripts, cameras, HUD behavior, colliders and rigidbodies. The resulting scene
keeps licensed meshes, materials and animation data while using project-owned
runtime components.

`PlayerMotor` currently provides:

- normalized 360-degree movement from the Marine Slayer input service;
- independent right-stick facing with movement-facing fallback;
- rigidbody movement and bounded rotation;
- pause/state gating through GameStateService.

`TopDownCameraRig` provides an elevated 45-degree camera, smoothed following
and modest aim look-ahead. Tuning is provisional until an Xbox 360 controller
and representative room geometry are tested interactively.

Automated Windows runtime validation confirms that the player object exists
and moves under injected input. The same scene and scripts compile into the
Xbox 360 foundation build. M03 remains in progress because controller response,
animation integration, collision/roll behavior and representative enemy-load
stability have not been validated on target hardware.
