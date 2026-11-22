using NUnit.Framework;

namespace CubeBurst.Core.Tests
{
    public class GroupFinderTests
    {
        [Test]
        public void Finds_all_orthogonally_connected_cubes_of_the_same_color()
        {
            var board = BoardParser.Parse(
                "rrg",
                "rgg",
                "bbr");

            var group = GroupFinder.Find(board, new Position(0, 2));

            CollectionAssert.AreEquivalent(
                new[] { new Position(0, 2), new Position(1, 2), new Position(0, 1) },
                group);
        }

        [Test]
        public void Does_not_connect_diagonally()
        {
            var board = BoardParser.Parse(
                "rg",
                "gr");

            Assert.AreEqual(1, GroupFinder.Find(board, new Position(0, 1)).Count);
        }

        [Test]
        public void Returns_empty_for_an_empty_cell()
        {
            var board = BoardParser.Parse(".r");

            Assert.IsEmpty(GroupFinder.Find(board, new Position(0, 0)));
        }
    }
}
