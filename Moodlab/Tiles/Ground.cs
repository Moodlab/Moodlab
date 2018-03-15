using Microsoft.Xna.Framework;

namespace Moodlab.Tiles
{
    public class Ground : Tile
    {
        public override bool Solid { get { return false; } }
        public override Color Color { get { return Color.White; } }
    }
}
