using Assets.SimpleGenerator;
using UnityEngine;

namespace Code.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class AlpSnow : MonoBehaviour, IBiome<CellImpl>
    {
        public float TopBound, LowBound;
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
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 1] = current.Height + 0.1f;
        }

    }
}