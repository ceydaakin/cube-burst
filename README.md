# Cube Burst

A tap-to-blast mobile puzzle game made in Unity 6.3 LTS. Tap a group of two or
more same-colored cubes to blast it, clear the level goals before you run out
of moves.

> Work in progress. Playable WebGL build and gameplay GIF coming with the first
> vertical slice.

## What this project shows

- **Game rules in pure C#**, fully separated from Unity (`Assets/_Project/Scripts/Core`).
  The assembly has `noEngineReferences: true`, so the compiler rejects any
  `UnityEngine` call in the rules.
- **Tests that run twice**: the same NUnit files run in Unity's Test Runner and in
  `dotnet test`, so CI checks the rules on every push without a Unity license.
- **Deterministic gameplay**: randomness is injected (`IRandom`), so any level
  replays identically from a seed.

See [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) for the layers and the reasons behind them.

## Run the tests

```sh
dotnet test tests/CubeBurst.Core.Tests
```

## Open in Unity

Unity Hub -> Add -> select this folder. Editor: Unity 6.3 LTS.
