using System.Collections.Generic;

namespace CubeBurst.Core
{
    /// <summary>Injected randomness: tests pass a fixed source, the game passes a seeded one.</summary>
    public interface IRandom
    {
        int Next(int maxExclusive);
    }

    public sealed class SeededRandom : IRandom
    {
        private readonly System.Random _random;
        public SeededRandom(int seed) => _random = new System.Random(seed);
        public int Next(int maxExclusive) => _random.Next(maxExclusive);
    }

    public readonly struct Spawn
    {
        public readonly Position At;
        public readonly Tile Tile;

        public Spawn(Position at, Tile tile)
        {
            At = at;
            Tile = tile;
        }
    }

    /// <summary>Drops a new random cube into every empty cell. Run after Gravity, so gaps are only at the top.</summary>
    public static class Refill
    {
        private const int ColorCount = 4;

        public static IReadOnlyList<Spawn> Apply(Board board, IRandom random)
        {
            var spawns = new List<Spawn>();
            for (var x = 0; x < board.Width; x++)
            for (var y = 0; y < board.Height; y++)
            {
                var p = new Position(x, y);
                if (board[p] != null) continue;
                var cube = new Cube((CubeColor)random.Next(ColorCount));
                board[p] = cube;
                spawns.Add(new Spawn(p, cube));
            }
            return spawns;
        }
    }
}
