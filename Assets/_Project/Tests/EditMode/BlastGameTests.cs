using NUnit.Framework;

namespace CubeBurst.Core.Tests
{
    public class BlastGameTests
    {
        [Test]
        public void Tapping_a_group_of_two_or_more_blasts_it_and_spends_a_move()
        {
            var board = BoardParser.Parse(
                "gb",
                "rr");
            var game = new BlastGame(board, moves: 5, new FixedRandom(3));

            var turn = game.Tap(new Position(0, 0));

            Assert.IsTrue(turn.Accepted);
            Assert.AreEqual(2, turn.Blasted.Count);
            Assert.AreEqual(4, game.MovesLeft);
            Assert.AreEqual("yy\ngb", board.ToString(), "cubes fall, then the top refills");
        }

        [Test]
        public void Tapping_a_lone_cube_is_rejected_and_costs_nothing()
        {
            var board = BoardParser.Parse("rg");
            var game = new BlastGame(board, moves: 5, new FixedRandom(0));

            var turn = game.Tap(new Position(0, 0));

            Assert.IsFalse(turn.Accepted);
            Assert.AreEqual(5, game.MovesLeft);
            Assert.AreEqual("rg", board.ToString());
        }

        [Test]
        public void No_taps_are_accepted_after_moves_run_out()
        {
            var board = BoardParser.Parse("rr");
            var game = new BlastGame(board, moves: 1, new FixedRandom(0));

            Assert.IsTrue(game.Tap(new Position(0, 0)).Accepted);
            Assert.IsFalse(game.Tap(new Position(0, 0)).Accepted);
        }
    }

    /// <summary>Test double: always returns the same value so refills are predictable.</summary>
    internal sealed class FixedRandom : IRandom
    {
        private readonly int _value;
        public FixedRandom(int value) => _value = value;
        public int Next(int maxExclusive) => _value % maxExclusive;
    }
}
