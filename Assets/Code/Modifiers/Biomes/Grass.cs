using Assets.SimpleGenerator;
using SimpleGenerator.Modifiers.Biomes;
using UnityEngine;

namespace Code.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class Grass : MonoBehaviour, IBiome<CellImpl>
    {
        public float TopBound, LowBound;
        public int GrassCount;
        public float MaxCellElevation;
        public void Start()
        {
        }

        public void Callback(CellImpl current)
        {
            if (current.Height < TopBound && current.Height > LowBound &&current.Elevation < MaxCellElevation)
            {
                current.Biomes.Add(this);
            }
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 0] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 2] = 3f;
            storage.DetailLayer[current.LocalPosition.X, current.LocalPosition.Y] = GrassCount;
        }

    }
}