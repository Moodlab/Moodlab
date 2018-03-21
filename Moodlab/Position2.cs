using System;
using Microsoft.Xna.Framework;

namespace Moodlab
{
    public struct Position2
    {
        public int X, Y;
        public Position2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position2 Map(Func<int, int> fnX, Func<int, int> fnY)
        {
            return new Position2(fnX(X), fnY(Y));
        }
        public Position2 Map(Func<int, int> fn)
        {
            return Map(fn, fn);
        }

        public static Position2 Round(Vector2 vector)
        {
            return new Position2(
                (int)Math.Round(vector.X),
                (int)Math.Round(vector.Y)
            );
        }

        public static explicit operator Vector2(Position2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            Position2 v = (Position2)obj;
            return v.X == X && v.Y == Y;
        }

        public override int GetHashCode()
        {
            return X + Y * 205168153;
        }
    }
}
