using Assets.SimpleGenerator;
using SimpleGenerator.Modifiers.Biomes;
using UnityEngine;

namespace Code.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class Cliff : Biome<CellImpl>
    {

        public override void Callback(CellImpl current)
        {
            current.Biomes.Add(this);
        }

        public override void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 0] = 1f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 1] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 2] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 3] = 0f;
            storage.DetailLayer[current.LocalPosition.X, current.LocalPosition.Y] = 0;
        }

    }
}