namespace Assets.SimpleGenerator
{
    public interface IBiome<T> : IModifier<T> where T : Cell
    {
        void Apply(T current,TerrainStorage storage);
    }
}