using Microsoft.Xna.Framework;

namespace Moodlab.Tiles
{
    public class Wall : Tile
    {
        public override bool Solid { get { return true; } }
        public override Color Color { get { return Color.Black; } }
    }
}
