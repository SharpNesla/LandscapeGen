namespace Assets.SimpleGenerator
{
    public interface IBiome<T> : IModifier<T> where T : Cell
    {
        void Apply(TerrainStorage storage, CellImpl cell);
    }
}