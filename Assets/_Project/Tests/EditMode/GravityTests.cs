using NUnit.Framework;

namespace CubeBurst.Core.Tests
{
    public class GravityTests
    {
        [Test]
        public void Cubes_fall_down_to_fill_gaps_and_keep_their_order()
        {
            var board = BoardParser.Parse(
                "r",
                ".",
                "g",
                ".");

            var falls = Gravity.Apply(board);

            Assert.AreEqual(BoardParser.Parse(".", ".", "r", "g").ToString(), board.ToString());
            Assert.AreEqual(2, falls.Count);
        }

        [Test]
        public void A_full_column_does_not_move()
        {
            var board = BoardParser.Parse("r", "g");

            Assert.IsEmpty(Gravity.Apply(board));
        }
    }
}
