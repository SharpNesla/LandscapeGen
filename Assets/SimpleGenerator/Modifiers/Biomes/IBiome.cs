using Assets.SimpleGenerator;

namespace SimpleGenerator.Modifiers.Biomes
{

    public interface IBiome<T> : IModifier<T> where T : Cell
    {
        void Apply(T current,TerrainStorage storage);
    }
}