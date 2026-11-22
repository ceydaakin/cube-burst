using System;

namespace CubeBurst.Core
{
    /// <summary>A cell on the board. (0,0) is the bottom-left; Y grows upward, the way gravity reads.</summary>
    public readonly struct Position : IEquatable<Position>
    {
        public readonly int X;
        public readonly int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Up => new Position(X, Y + 1);
        public Position Down => new Position(X, Y - 1);
        public Position Left => new Position(X - 1, Y);
        public Position Right => new Position(X + 1, Y);

        public bool Equals(Position other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is Position other && Equals(other);
        public override int GetHashCode() => (X * 397) ^ Y;
        public override string ToString() => $"({X},{Y})";
    }
}
