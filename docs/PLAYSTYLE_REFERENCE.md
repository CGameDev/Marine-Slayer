# Marine Slayer — Play-Style Reference

## Purpose

This document defines how Codex may use the owner's gameplay video while building the new Marine Slayer project.

Reference video:

https://youtu.be/cbfpxTSKEg4

Relevant gameplay begins at approximately **04:00**.

## Critical rule

The video is a **play-style goal/reference only**.

It is NOT an inherited build and is NOT a production source.

Marine Slayer must be built from scratch in a new Unity 5.4.1f1 project using:

1. the owner-supplied lore documents for game content/canon;
2. the owner-supplied licensed Unity asset pack for production assets/foundations;
3. this video only for desired feel and presentation style.

## What Codex MAY learn from the video

Use the video to understand high-level gameplay targets such as:

- elevated top-down / oblique tactical camera;
- player-to-environment scale;
- controller-first movement;
- independent directional aiming / twin-stick-like feel;
- responsive strafing while firing;
- readable close-to-medium-range combat;
- modular corridor/room navigation;
- tactical visibility of nearby enemies;
- aggressive but understandable combat pacing;
- visible projectile direction and impact feedback;
- Xbox 360-era graphical density rather than desktop-only visual excess;
- the general feel of a top-down action shooter running on actual Xbox 360 hardware.

The final game should evoke the same **kind of play experience** while implementing the Lore Bible's actual world, levels, enemies, weapons and story.

## What Codex MUST NOT copy or preserve from the video

Do not treat any of the following as mandatory merely because they appear in the reference build:

- existing project source code;
- existing scenes;
- prefab hierarchy;
- exact HUD layout;
- exact inventory screen;
- keycard mechanics;
- exact dialogue;
- old character naming displayed by the build if it conflicts with the lore documents;
- exact objective routing;
- exact room layouts;
- exact enemy-spawn logic;
- old AI implementation;
- old save/checkpoint implementation;
- old bugs;
- old performance characteristics;
- exact materials/textures from that build unless they are also present in the licensed asset pack and appropriate for the new game.

Codex should never search for the old video's Unity project or use it as a dependency.

## Camera target

The new game should use an elevated top-down/oblique camera with:

- strong player/threat visibility;
- smooth but responsive follow;
- camera distance suitable for tactical combat;
- no first-person or over-the-shoulder conversion;
- optional subtle aim look-ahead if it improves visibility without destabilizing control;
- low-cost occlusion handling where station geometry blocks the player;
- Xbox 360 performance as the final authority.

## Movement/aim target

The video demonstrates the desired general relationship between movement and aiming:

- fast controller response;
- simultaneous movement and directional engagement;
- quick facing changes;
- combat that feels immediate rather than animation-locked.

The new project may improve acceleration, animation matching, dead zones and collision behavior while preserving that responsiveness goal.

## Combat target

Aim for:

- rapid input-to-shot response;
- clear projectile/beam direction;
- readable enemy hit/death feedback;
- meaningful weapon differences;
- aggressive movement rather than stationary cover shooting;
- room-scale encounter composition readable from the tactical camera;
- limited/restrained camera shake so aiming remains clear.

Actual weapons, enemies and encounter progression come from the Lore Bible, not the reference video's old implementation.

## Level-flow target

The video demonstrates a useful top-down spatial language:

- corridors feeding into rooms;
- controlled combat spaces;
- interactable/route readability;
- quiet traversal between bursts of combat;
- larger spaces used for escalation.

Use that language when translating the Lore Bible's Crew Ring, Maintenance/Industrial, Research, Command and Spire spaces into playable levels.

Do not reproduce old room layouts one-for-one.

## Xbox 360 relevance

Because the video represents the desired type of gameplay on Xbox 360 hardware, it is useful as a qualitative reminder that:

- readability matters more than excessive effects;
- camera distance affects texture/detail needs;
- enemy density must be bounded;
- lighting/VFX must remain performant;
- control responsiveness must survive target load.

Use profiling and target tests for actual budgets; do not infer numerical performance limits from the video.

## Acceptance question

When evaluating moment-to-moment feel, ask:

> Does this new, scratch-built Lore-Bible game feel like the same *style of top-down Xbox 360 action shooter* shown in the reference video, while clearly being a new implementation of the canonical Marine Slayer game?

If yes, the reference has served its purpose.
