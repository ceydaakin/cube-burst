namespace CubeBurst.Core
{
    public enum CubeColor
    {
        Red,
        Green,
        Blue,
        Yellow,
    }

    /// <summary>Anything that occupies a cell. Obstacles and power-ups will derive from this.</summary>
    public abstract class Tile
    {
    }

    public sealed class Cube : Tile
    {
        public CubeColor Color { get; }

        public Cube(CubeColor color) => Color = color;
    }
}
