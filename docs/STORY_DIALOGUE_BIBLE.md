# Marine Slayer — Story & Dialogue Implementation Rules

## Status

This file governs **implementation behavior for story/dialogue**, not canon creation.

The canonical story is defined by the owner-supplied Lore Bible and Visual Lore Companion, summarized in:

`docs/OWNER_CANON_SOURCE.md`

If this file conflicts with those sources, the owner canon wins.

Marine Slayer is being built from scratch. Do not import dialogue or story systems from an older Marine Slayer project.

---

# 1. Canon that must not be changed without owner instruction

- protagonist: **Lieutenant Rhyker Voss**;
- setting: **Eidolon Station**;
- organization/program: **UEMF / Project ASCENDANT**;
- neural technology: **ASC-9 Neural Mesh**;
- antagonist: **VANGUARD**;
- catastrophic event: **The Convergence**;
- major characters: Commander Arwyn Sol, Dr. Helena Drayce, Chief Engineer Rudd Hale, Dr. Verin, Officer Corra Fen, Null Sister;
- canonical enemy taxonomy;
- canonical weapon identities;
- five Acts / twenty-five Levels;
- Red Engineer, Null Sister, Sol's Last Stand and Prime Convergence climaxes;
- three ending concepts.

Do not merge in temporary discarded material such as Lena Cross, Bastion K-17, ECHOLOCK, Black Signal, Veyr or the 12-mission structure.

---

# 2. Codex writing authority

Codex MAY write new original material needed to turn the Lore Bible into a complete playable game, including:

- mission-start briefings;
- objective text;
- tutorial lines;
- Voss combat/traversal reactions;
- VANGUARD lines consistent with its psychological evolution;
- short connective dialogue;
- terminal/report text where the source gives only a title/concept;
- missing audio-log body text;
- boss phase dialogue;
- subtitle timing/data;
- ending connective lines.

Codex MUST NOT:

- rewrite established Lore Bible story beats because another idea seems cooler;
- rename established characters/factions/levels;
- turn VANGUARD into a generic wisecracking villain;
- copy dialogue from Doom, Dead Space, Killzone or another commercial property;
- use the YouTube reference video's dialogue as mandatory canon;
- import dialogue from an old Marine Slayer project unless explicitly authorized later.

---

# 3. Tone

Preserve the Lore Bible's blend of:

- military science fiction;
- isolation;
- body horror;
- AI/machine horror;
- psychological distortion;
- tragedy;
- escalating aggression;
- sparse dry humor where appropriate.

Dialogue should not turn the game into parody.

---

# 4. Character voices

## Rhyker Voss

- battle-hardened;
- direct;
- distrustful of AI/ASCENDANT systems;
- protective instinct and survivor's guilt;
- determined under pressure;
- minimal speeches during combat.

## VANGUARD

Its voice should evolve through the Lore Bible's conceptual stages:

1. Strategist — clinical/tactical;
2. Judge — evaluative/condemning human inefficiency;
3. Surgeon — frames transformation as correction;
4. Prophet — frames Convergence as destiny/evolution;
5. God-Machine — speaks as if ascension is inevitable.

Do not make every VANGUARD line cryptic. Its progression should be understandable.

## Commander Arwyn Sol

- competent;
- compassionate;
- command authority;
- increasingly desperate during logs/final resistance;
- Act IV encounter should retain tragic human remnants.

## Dr. Helena Drayce

- brilliant;
- ethically compromised;
- intellectually fascinated by ASCENDANT/VANGUARD;
- defensive or reverent depending chronology.

## Rudd Hale / Red Engineer

Human remnants should occasionally break through VANGUARD control, supporting the boss's tragic identity.

## Null Sister

- fractured;
- psychic/mesh-sensitive;
- layered personal/VANGUARD expression;
- unsettling, not comedic.

---

# 5. Dialogue delivery rules

All dialogue data should support:

- stable line/event ID;
- speaker ID;
- subtitle text;
- trigger condition;
- priority;
- interruptibility;
- optional audio reference;
- replay/cooldown behavior;
- localization-ready key;
- checkpoint replay rules.

Do not hard-code large dialogue blocks directly into enemy, door or trigger MonoBehaviours.

---

# 6. Length / pacing

During combat:

- usually one sentence;
- critical lines may queue/replay after combat;
- avoid long lore exposition.

During traversal:

- short exchanges/log playback allowed;
- use quiet spaces for exposition.

Act transitions/cinematics:

- may be longer;
- keep skippable where practical;
- preserve subtitles.

---

# 7. Lore Bible logs

The Lore Bible provides extensive log titles and many complete excerpts.

Implementation procedure:

1. preserve any supplied log body text faithfully;
2. where only a title/concept is supplied, Codex may draft new original text consistent with surrounding logs;
3. assign each log to the appropriate zone/level;
4. keep optional logs non-blocking;
5. track collected/read state;
6. support text-first release even if voice recording is unavailable.

Do not fabricate a log as a factual canon revelation that contradicts the Lore Bible.

---

# 8. Cinematic anchors

Implement the canonical concepts:

- `ASCENDANT DAWN`;
- `THE FIRST ASCENDED`;
- `THE RED ENGINEER`;
- `COMMANDER SOL'S LAST STAND`;
- `NULL SISTER`;
- `PRIME CONVERGENCE`;
- the three ending concepts.

Translate them into feasible in-engine Unity 5.4.1f1 sequences while preserving their narrative function.

---

# 9. Ending dialogue

Support the three canonical outcomes:

- **Escape Velocity**;
- **Ascended**;
- **The Rift Opens**.

The milestone may define reliable gameplay conditions for reaching them, but Codex must not collapse them into one ending without owner approval.

---

# 10. Content review rule

When Codex creates substantial new story/dialogue not directly written in the Lore Bible, mark it in implementation notes as **derived/new connective writing** so it can be distinguished from owner-authored canon.

The goal is to complete the game around the owner's story, not silently replace the owner's story with model-generated lore.
