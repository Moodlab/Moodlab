using Microsoft.Xna.Framework;

namespace Moodlab.Entities
{
    public abstract class SolidEntity : Entity
    {
        public Vector2 Size { get; private set; }

        public SolidEntity(Vector2 position, Vector2 size): base(position){
            Size = size;
        }

        public override void Move(Vector2 motion, Map map){
            // TODO: Check Map
            if ((int)(Position.Y + motion.Y) > (int)Position.Y)
            {
                Tiles.Tile tile = map.GetTile((int)Position.X, (int)Position.Y + 1);
                if (tile == null || tile.Solid)
                {
                    motion.Y = 1 - Position.Y + (int)Position.Y;
                }
            }
            if ((int)(Position.Y + motion.Y) < (int)Position.Y)
            {
                Tiles.Tile tile = map.GetTile((int)Position.X, (int)Position.Y - 1);
                if (tile == null || tile.Solid)
                {
                    motion.Y = -Position.Y + (int)Position.Y;
                }
            }
            if ((int)(Position.X + motion.X) > (int)Position.X)
            {
                Tiles.Tile tile = map.GetTile((int)Position.X + 1, (int)Position.Y);
                if (tile == null || tile.Solid)
                {
                    motion.X = 1 - Position.X + (int)Position.X;
                }
            }
            if ((int)(Position.X + motion.X) < (int)Position.X)
            {
                Tiles.Tile tile = map.GetTile((int)Position.X - 1, (int)Position.Y);
                if (tile == null || tile.Solid)
                {
                    motion.X = -Position.X + (int)Position.X;
                }
            }

            base.Move(motion, map);
        }
    }
}
