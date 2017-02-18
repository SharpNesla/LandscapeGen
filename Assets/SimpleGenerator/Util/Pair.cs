namespace Assets.SimpleGenerator
{
    public struct Pair
    {
        public int X, Y;
        public Pair(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Pair operator +(Pair first, Pair second)
        {
            return new Pair(first.X + second.X, first.Y + second.Y);
        }
    }
}