using System;
using System.Collections.Generic;

namespace CubeBurst.Core
{
    /// <summary>What one tap did, in order, so the view can play it back step by step.</summary>
    public sealed class TurnResult
    {
        public static readonly TurnResult Rejected =
            new TurnResult(false, Array.Empty<Position>(), Array.Empty<Fall>(), Array.Empty<Spawn>());

        public bool Accepted { get; }
        public IReadOnlyList<Position> Blasted { get; }
        public IReadOnlyList<Fall> Falls { get; }
        public IReadOnlyList<Spawn> Spawns { get; }

        public TurnResult(bool accepted, IReadOnlyList<Position> blasted, IReadOnlyList<Fall> falls, IReadOnlyList<Spawn> spawns)
        {
            Accepted = accepted;
            Blasted = blasted;
            Falls = falls;
            Spawns = spawns;
        }
    }

    /// <summary>
    /// The single entry point for a turn: tap -> blast -> gravity -> refill.
    /// The view calls Tap and animates the result; it never edits the board itself.
    /// </summary>
    public sealed class BlastGame
    {
        public const int MinGroupSize = 2;

        private readonly Board _board;
        private readonly IRandom _random;

        public int MovesLeft { get; private set; }

        public BlastGame(Board board, int moves, IRandom random)
        {
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            if (moves <= 0) throw new ArgumentOutOfRangeException(nameof(moves));
            MovesLeft = moves;
        }

        public TurnResult Tap(Position at)
        {
            if (MovesLeft == 0) return TurnResult.Rejected;

            var group = GroupFinder.Find(_board, at);
            if (group.Count < MinGroupSize) return TurnResult.Rejected;

            foreach (var p in group) _board[p] = null;
            MovesLeft--;

            var falls = Gravity.Apply(_board);
            var spawns = Refill.Apply(_board, _random);
            return new TurnResult(true, group, falls, spawns);
        }
    }
}
