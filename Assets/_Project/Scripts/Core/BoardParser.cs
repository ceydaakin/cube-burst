using System;

namespace CubeBurst.Core
{
    /// <summary>
    /// Reads boards written as text rows, top row first: r g b y = cubes, '.' = empty.
    /// Used by tests and by level files, so a level reads like a picture of the board.
    /// </summary>
    public static class BoardParser
    {
        public static Board Parse(params string[] rowsTopFirst)
        {
            if (rowsTopFirst.Length == 0) throw new ArgumentException("No rows.", nameof(rowsTopFirst));
            var width = rowsTopFirst[0].Length;
            var height = rowsTopFirst.Length;
            var board = new Board(width, height);

            for (var row = 0; row < height; row++)
            {
                var line = rowsTopFirst[row];
                if (line.Length != width)
                    throw new FormatException($"Row {row} has {line.Length} cells, expected {width}.");
                for (var x = 0; x < width; x++)
                    board[new Position(x, height - 1 - row)] = FromChar(line[x]);
            }
            return board;
        }

        public static Tile FromChar(char c)
        {
            switch (c)
            {
                case 'r': return new Cube(CubeColor.Red);
                case 'g': return new Cube(CubeColor.Green);
                case 'b': return new Cube(CubeColor.Blue);
                case 'y': return new Cube(CubeColor.Yellow);
                case '.': return null;
                default: throw new FormatException($"Unknown tile code '{c}'.");
            }
        }

        public static char ToChar(Tile tile)
        {
            switch (tile)
            {
                case null: return '.';
                case Cube cube:
                    switch (cube.Color)
                    {
                        case CubeColor.Red: return 'r';
                        case CubeColor.Green: return 'g';
                        case CubeColor.Blue: return 'b';
                        case CubeColor.Yellow: return 'y';
                    }
                    break;
            }
            throw new ArgumentException($"No char for {tile.GetType().Name}.", nameof(tile));
        }
    }
}
