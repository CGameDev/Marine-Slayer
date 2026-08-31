# LocalDependencies

This directory is reserved for **local-only licensed dependencies** and is ignored by Git except for this README.

For Marine Slayer, place the supplied file here before Codex begins asset import:

`LocalDependencies\AssetPack_ProjectSettings.zip`

Expected SHA-256:

`f27f1bf3bad614b829efa530cdd9247c52b69bc539bcfa987dd68ce59da4effb`

The ZIP contains:

- Unity 5.4.1f1 ProjectSettings;
- `Xbox360TutorialAssets.unitypackage`;
- controller reference images.

The raw paid package must not be committed to this public repository unless the project owner later verifies that raw source redistribution is permitted under the applicable license.

Codex may unpack/import this dependency locally and use it to construct distributable game builds consistent with the owner's license.