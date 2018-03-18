using System;
using Microsoft.Xna.Framework;

namespace Moodlab.Entities
{
    public abstract class SolidEntity : Entity
    {
        public Vector2 Size { get; protected set; }

        public SolidEntity(Vector2 position, Vector2 size): base(position){
            Size = size;
        }

        public override void Move(Vector2 motion, Map map)
        {
            // TODO: Include Size
            Position2 currentPos = Position2.Round(Position);
            Vector2 offset = Position - (Vector2)currentPos + new Vector2(0.5f, 0.5f);
            int max = (int)Math.Ceiling(Math.Max(Math.Abs(motion.X + offset.X), Math.Abs(motion.Y + offset.Y)));
            for (int i = 1; i <= max; i++)
            {
                Tiles.Tile testingTile = map.GetTile(Position2.Round(Position + motion * i / max));
                if (testingTile != null) //Do that after ?
                    testingTile.OnCollide(this);

                if(testingTile == null || testingTile.Solid)
                {
                    motion = Vector2.Zero; //TODO: Proper collision
                    break;
                }
            }
            base.Move(motion, map);
        }
    }
}
