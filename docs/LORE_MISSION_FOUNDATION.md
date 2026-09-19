# Lore terminal and mission-completion foundation

The foundation arena now ends with a controller-operated data terminal rather
than completing the test mission as soon as combat stops.

After all three waves are cleared, the objective changes to `ACCESS THE
CRYO-BAY DATA TERMINAL`. Approaching the terminal presents an Xbox `A` prompt.
Opening it pauses simulation and displays a text-first category, title and body
panel. Closing the entry completes the foundation mission, unlocks level 2 and
presents a return-to-menu state.

The reusable lore service persists separate collected and read IDs. The
terminal objective and mission service independently reject duplicate
completion, including after campaign-save reload. This separates optional lore
collection from progression logic: future terminals can omit a mission ID and
remain entirely optional.

The sample entry, `CRYO-BAY 09 // WAKE FAILURE`, is **derived/new connective
writing** for the foundation test. It only restates established canon: Eidolon
Station is undergoing neural-mesh synchronization, Cryo-Bay 09 is isolated from
the ASCENDANT network, and Rhyker Voss is awake and unlinked. It is not an
owner-authored Lore Bible quotation.

Automated runtime coverage verifies the interaction prompt, paused lore state,
collected/read persistence, one-time terminal completion, one-time mission
completion, next-level unlock and save-file reload. Physical Xbox validation of
TV-distance readability and controller feel remains open.
