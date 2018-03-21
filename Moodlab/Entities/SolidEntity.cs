using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Moodlab.Entities
{
    public abstract class SolidEntity : Entity
    {
        public Vector2 Size { get; protected set; }

        public SolidEntity(Vector2 position, Vector2 size) : base(position)
        {
            Size = size;
        }

        public override void Move(Vector2 motion, Map map)
        {
            //Pre move check
            List<Position2> checkedPositions = new List<Position2>();
            Position2 currentPos;
            Tiles.Tile currentTile;
            for (int x = 0; x <= (int)Math.Ceiling(Size.X); x++)
            {
                for (int y = 0; y <= (int)Math.Ceiling(Size.Y); y++)
                {
                    currentPos = Position2.Round(Position - Size / 2 + new Vector2(Math.Min(x, Size.X), Math.Min(y, Size.Y)));
                    if (!checkedPositions.Contains(currentPos))
                    {
                        currentTile = map.GetTile(currentPos);
                        if (currentTile != null)
                            currentTile.OnCollide(this);

                        if (currentTile == null || currentTile.Solid)
                            throw new Exception("SolidEntity in a wall..."); //FIXME: What to do ???

                        checkedPositions.Add(currentPos);
                    }
                }
            }

            //Moving check
            //TODO: Proper collision motion = offset
            //TODO: Include motion steps for high speed collisions
            //int motionSteps = (int)Math.Ceiling(Math.Max(Math.Abs(motion.X), Math.Abs(motion.Y)));
            //for (int currentStep = 1; currentStep <= motionSteps; currentStep++)
            //{
            Vector2 newMotion = motion;

            //Up Down
            for (int x = 0; x <= (int)Math.Ceiling(Size.X) && motion.Y != 0; x++)
            {
                currentPos = Position2.Round(Position + new Vector2(Math.Min(x, Size.X) - Size.X / 2, Size.Y * (motion.Y > 0 ? 1 : -1)));
                if (!checkedPositions.Contains(currentPos))
                {
                    currentTile = map.GetTile(currentPos);
                    if (currentTile != null)
                        currentTile.OnCollide(this);

                    if (newMotion.Y != 0 && (currentTile == null || currentTile.Solid))
                        newMotion.Y = 0;

                    checkedPositions.Add(currentPos);
                }
            }

            //Left Right
            for (int y = 0; y <= (int)Math.Ceiling(Size.Y) && motion.X != 0; y++)
            {
                currentPos = Position2.Round(Position + new Vector2(Size.X * (motion.X > 0 ? 1 : -1), Math.Min(y, Size.Y) - Size.Y / 2));
                //For diagonal motion, need to check collisions for X or Y
                currentTile = map.GetTile(currentPos);
                if (currentTile != null && !checkedPositions.Contains(currentPos))
                {
                    currentTile.OnCollide(this);
                    checkedPositions.Add(currentPos);
                }

                if (newMotion.X != 0 && (currentTile == null || currentTile.Solid))
                    newMotion.X = 0;
            }
            //}

            if (newMotion.X != 0 || newMotion.Y != 0)
                base.Move(newMotion, map);
        }
    }
}
