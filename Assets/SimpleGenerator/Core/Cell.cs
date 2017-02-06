namespace Assets.SimpleGenerator
{
    public abstract class Cell
    {
        public float Height;
        public readonly Pair Coords;

        protected Cell(Pair coordinates, float height = 0)
        {
            Coords = coordinates;
            Height = height;
        }
    }
}