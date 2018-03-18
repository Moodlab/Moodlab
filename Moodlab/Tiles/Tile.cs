using Microsoft.Xna.Framework;

namespace Moodlab.Tiles
{
    public abstract class Tile
    {
        public const int SIZE = 20;
        public abstract bool Solid { get; }
        public abstract Color Color { get; }

        public virtual void OnCollide(Entities.Entity entity){ }
    }
}
