using Microsoft.Xna.Framework;

namespace Moodlab
{
    public abstract class Tile
    {
        public abstract bool Solid { get; }
        public abstract Color Color { get; }
}

    public abstract class OrientedTile : Tile
    {
        public enum Orientations { Up, Down, Left, Right }
        public Orientations Orientation { get; private set; }

        public OrientedTile(Orientations orientation){
            Orientation = orientation;
        }
    }

    public class Wall : Tile
    {
        public override bool Solid { get{ return true; } }
        public override Color Color { get { return Color.Black; } }
    }

    public class Ground : Tile
    {
        public override bool Solid { get { return false; } }
        public override Color Color { get { return Color.White; } }
    }

    public class Door : OrientedTile
    {
        public override bool Solid { get { return Open; } }
        public override Color Color { get { return Open ? Color.LightGreen : Color.OrangeRed; } }
        public bool Open { get; private set; }

        public Door(OrientedTile.Orientations orientation, bool open = false): base(orientation){
            Open = open;
        }
    }
}
