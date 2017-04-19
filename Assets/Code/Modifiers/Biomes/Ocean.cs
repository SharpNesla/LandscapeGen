using Assets.SimpleGenerator;
using Assets.SimpleGenerator.TerrainModules;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Code.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class Ocean : Biome<CellImpl>
    {
        public int GrassCount;
        public TerrainTexture SplatTexture;
        private int _index;
        public override void Callback(CellImpl current)
        {
            if (current.Height < 0.3f)
            {
                current.Biomes.Clear();
                current.Biomes.Add(this);
            }
        }

        public override void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 0] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, _index] = 1f;
        }

        public override void ApplyPrototypes(Terrain terrain)
        {
            _index = SplatTexture.ApplyTexture(terrain);
        }

    }
}