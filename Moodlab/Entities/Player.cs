using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Moodlab.Entities
{
    public class Player : SolidEntity
    {
        public float Speed { get; protected set; }

        public Player() : base(new Vector2(2, 2), new Vector2(0.8f, 0.8f))
        {
            Speed = 0.1f;
        }

        public override void Move(Vector2 motion, Map map)
        {
            base.Move(motion * Speed, map);
        }

        public override void Update(GameTime gameTime, Map map)
        {
            Vector2 motion = Vector2.Zero;
            float speed = Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                motion.Y -= speed;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                motion.Y += speed;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                motion.X -= speed;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                motion.X += speed;

            if (motion.X != 0 && motion.Y != 0)
                motion *= (float)(Math.Sqrt(2) / 2);


            Move(motion, map);
        }
    }
}
