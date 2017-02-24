using UnityEngine;

namespace Assets.SimpleGenerator.Biomes
{
    public class Cliff : MonoBehaviour, IBiome<CellImpl>
    {

        public void Callback<TCore>(TCore core, CellImpl current) where TCore : Core<CellImpl>
        {
            current.Biomes.Add(this);
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 0] = 1f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 1] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 2] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 3] = 0f;
        }

    }
}