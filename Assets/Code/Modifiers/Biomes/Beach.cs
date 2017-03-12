using Assets.SimpleGenerator;
using Code.Modifiers.Biomes;
using UnityEngine;

namespace SimpleGenerator.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class Beach : Biome<CellImpl>
    {

        public override void Callback(CellImpl current)
        {
            current.Biomes.Add(this);
        }

        public override void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 0] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 1] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 2] = 1f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 3] = 0f;
        }

    }
}