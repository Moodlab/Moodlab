using Microsoft.Xna.Framework;

namespace Moodlab.Tiles
{
    public abstract class Tile
    {
        public const int SIZE = 75;
        public abstract bool Solid { get; }
        public abstract Color Color { get; }
    }
}
