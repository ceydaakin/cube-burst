using System;
using System.Text;

namespace CubeBurst.Core
{
    /// <summary>
    /// The grid and nothing else: what sits where. Rules live in separate classes
    /// (GroupFinder, Gravity, Refill) so each can be read and tested on its own.
    /// Mutable on purpose: a board changes every turn, and the view learns what
    /// changed from the immutable result records each rule returns.
    /// </summary>
    public sealed class Board
    {
        private readonly Tile[,] _cells;

        public int Width { get; }
        public int Height { get; }

        public Board(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Board needs at least one cell.");
            Width = width;
            Height = height;
            _cells = new Tile[width, height];
        }

        public bool Contains(Position p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

        public Tile this[Position p]
        {
            get => _cells[p.X, p.Y];
            set => _cells[p.X, p.Y] = value;
        }

        /// <summary>Top row first, one char per cell (same format BoardParser reads).</summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var y = Height - 1; y >= 0; y--)
            {
                for (var x = 0; x < Width; x++)
                    sb.Append(BoardParser.ToChar(_cells[x, y]));
                if (y > 0) sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
