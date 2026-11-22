# Architecture

## Layers

```
 Input (tap)                                       Screen
     |                                               ^
     v                                               |
 +-----------+   Tap(position)   +-----------+  TurnResult  +-----------+
 |   View    | ----------------> |   Core    | -----------> |   View    |
 | (Unity)   |                   | (pure C#) |  (what       | animates  |
 +-----------+                   +-----------+   changed)   +-----------+
```

| Layer | Folder | Owns | May reference |
|---|---|---|---|
| Core | `Scripts/Core` | Board, rules, turn order, goals | nothing (no UnityEngine) |
| View | `Scripts/View` (next) | Sprites, tweens, particles, input | Core |
| UI | `Scripts/UI` (next) | HUD, menus, win/lose popups | Core |
| Levels | `Levels/*.json` (next) | Level layouts, move counts, goals | read by a loader in View |

## Rules of the code

1. **Core decides, View shows.** The view never changes the board. It calls
   `BlastGame.Tap` and plays back the returned `TurnResult` (blasted, falls,
   spawns) in order. One way in, one way out, so a turn can't half-happen.
2. **One class per rule.** `GroupFinder` (flood fill), `Gravity` (column
   compaction), `Refill` (spawn). `BlastGame` only sequences them.
3. **Randomness is injected.** `IRandom` lets tests fix the outcome and lets the
   game replay a level from a seed.
4. **Boards are written as text.** `BoardParser` reads rows like `"rgb."`, so
   tests and level files look like the board they describe.

## Why not a DI framework or ECS?

The game has one scene's worth of systems. Constructor parameters wire them
in a few lines; Zenject/VContainer or DOTS would add ceremony a reviewer has to
read past without changing what the code can do.
