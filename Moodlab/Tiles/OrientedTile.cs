using Microsoft.Xna.Framework;

namespace Moodlab.Tiles
{
    public abstract class OrientedTile : Tile
    {
        public enum Orientations { Up, Down, Left, Right }
        public Orientations Orientation { get; protected set; }

        public OrientedTile(Orientations orientation)
        {
            Orientation = orientation;
        }
    }
}
