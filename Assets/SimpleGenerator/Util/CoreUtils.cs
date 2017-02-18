using System;

namespace Assets.SimpleGenerator
{
    public static class CoreUtils
    {
        public static T[,] Foreach<T>(this T[,] massive,Pair size, Action<Pair> callback)
        {
            for (var y = 0; y < size.Y; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    callback(new Pair(x,y));
                }
            }
            return massive;
        }
        public static T[,] Foreach<T>(this T[,] massive, Action<Pair> callback)
        {
            for (var y = 0; y < massive.GetLength(1); y++)
            {
                for (var x = 0; x < massive.GetLength(0); x++)
                {
                    callback(new Pair(x,y));
                }
            }
            return massive;
        }
    }

}