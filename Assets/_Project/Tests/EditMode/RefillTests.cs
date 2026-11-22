using NUnit.Framework;

namespace CubeBurst.Core.Tests
{
    public class RefillTests
    {
        [Test]
        public void Fills_every_empty_cell_with_a_cube_from_the_random_source()
        {
            var board = BoardParser.Parse(
                "..",
                "r.");

            var spawns = Refill.Apply(board, new FixedRandom(2));

            Assert.AreEqual(3, spawns.Count);
            Assert.AreEqual("bb\nrb", board.ToString());
        }
    }
}
