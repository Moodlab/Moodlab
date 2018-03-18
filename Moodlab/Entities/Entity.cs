using Microsoft.Xna.Framework;

namespace Moodlab.Entities
{
    public abstract class Entity
    {
        public Vector2 Position { get; protected set; }

        public Entity(Vector2 position){
            Position = position;
        }

        public virtual void Move(Vector2 motion, Map map){
            Position += motion;
        }

        public abstract void Update(GameTime gameTime, Map map);
    }
}
