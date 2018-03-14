using System;

namespace Moodlab
{
    public struct Vector
    {
        public int X, Y;
        public Vector(int x, int y){
            X = x;
            Y = y;
        }

        public Vector Map(Func<int, int> fnX, Func<int, int> fnY)
        {
            return new Vector(fnX(X), fnY(Y));
        }
        public Vector Map(Func<int, int> fn){
            return Map(fn, fn);
        }
    }
}
