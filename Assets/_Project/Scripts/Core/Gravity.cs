using System.Collections.Generic;

namespace CubeBurst.Core
{
    public readonly struct Fall
    {
        public readonly Position From;
        public readonly Position To;

        public Fall(Position from, Position to)
        {
            From = from;
            To = to;
        }
    }

    /// <summary>Compacts each column downward. Returns every move so the view can animate it.</summary>
    public static class Gravity
    {
        public static IReadOnlyList<Fall> Apply(Board board)
        {
            var falls = new List<Fall>();
            for (var x = 0; x < board.Width; x++)
            {
                var writeY = 0;
                for (var readY = 0; readY < board.Height; readY++)
                {
                    var from = new Position(x, readY);
                    var tile = board[from];
                    if (tile == null) continue;
                    if (readY != writeY)
                    {
                        var to = new Position(x, writeY);
                        board[to] = tile;
                        board[from] = null;
                        falls.Add(new Fall(from, to));
                    }
                    writeY++;
                }
            }
            return falls;
        }
    }
}
