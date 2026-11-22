using System.Collections.Generic;

namespace CubeBurst.Core
{
    /// <summary>Flood fill (breadth-first) from the tapped cell over same-colored neighbours.</summary>
    public static class GroupFinder
    {
        public static IReadOnlyList<Position> Find(Board board, Position start)
        {
            var result = new List<Position>();
            if (!board.Contains(start) || !(board[start] is Cube origin)) return result;

            var seen = new HashSet<Position> { start };
            var queue = new Queue<Position>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                result.Add(p);
                foreach (var n in new[] { p.Up, p.Down, p.Left, p.Right })
                {
                    if (!board.Contains(n) || seen.Contains(n)) continue;
                    if (board[n] is Cube c && c.Color == origin.Color)
                    {
                        seen.Add(n);
                        queue.Enqueue(n);
                    }
                }
            }
            return result;
        }
    }
}
