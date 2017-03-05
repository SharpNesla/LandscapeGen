using Assets.SimpleGenerator;
using Code.Modifiers.Biomes;
using UnityEngine;

namespace SimpleGenerator.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class Beach : MonoBehaviour, IBiome<CellImpl>
    {

        public void Start()
        {

        }
        public void Callback(CellImpl current)
        {
            current.Biomes.Add(this);
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 0] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 1] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 2] = 1f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 3] = 0f;
        }

    }
}