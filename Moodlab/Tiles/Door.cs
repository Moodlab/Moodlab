using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Moodlab.Tiles
{
    public class Door : OrientedTile
    {
        public override bool Solid { get { return !Open; } }
        public override Color Color { get { return Open ? Color.LightGreen : Color.OrangeRed; } }
        public bool Open { get; protected set; }

        public Door(OrientedTile.Orientations orientation, bool open = false) : base(orientation)
        {
            Open = open;
        }

        public override void OnCollide(Entities.Entity entity)
        {
            if (entity is Entities.Entity)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Open = true;
            }
        }
    }
}
