using Assets.SimpleGenerator;
using Code.Core;

namespace Code.Modifiers.Biomes
{

    public interface IBiome<T> : IModifier<T> where T : Cell
    {
        void Apply(T current,TerrainStorage storage);
    }
}