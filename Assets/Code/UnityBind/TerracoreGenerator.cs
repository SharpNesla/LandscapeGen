using System;
using System.Collections.Generic;
using System.Linq;
using Assets.SimpleGenerator.TerrainModules;
using Code.Core;
using Code.Modifiers.Biomes;
using UnityEditor;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class TerracoreGenerator : MonoBehaviour
    {
        public TerrainSettings TerrainSettings;
        public Vector2 GenerationPatchOffset;
        public Vector2 TerrainsRectScale;
        public CoreImpl Core;

        public void Place()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Terracore Terrains Data"))
            {
                AssetDatabase.CreateFolder("Assets","Terracore Terrains Data");
            }
            var modifiers = Array.FindAll(
                gameObject.GetComponents<IModifier<CellImpl>>(),
                modifier => ((MonoBehaviour) modifier).enabled);
            var biomes = modifiers.OfType<Biome<CellImpl>>();

            foreach (var modifier in modifiers)
            {
                modifier.Start();
            }

            Core = new CoreImpl(CellInitializer,TerrainSettings.Resolution,
                modifiers);
            CreateChunks(biomes);
        }

        public List<UnityChunk> CreateChunks(IEnumerable<Biome<CellImpl>> biomes)
        {
            var chunksCount = TerrainsRectScale.x * TerrainsRectScale.y;
            var chunks = new List<UnityChunk>((int) chunksCount);

            for (float y = GenerationPatchOffset.y; y < TerrainsRectScale.x + GenerationPatchOffset.y; y++)
            {
                for (float x = GenerationPatchOffset.x; x < TerrainsRectScale.x + GenerationPatchOffset.y; x++)
                {
                    var terrain = TerrainSettings.CreateTerrain();
                    terrain.gameObject.transform.parent = transform;
                    foreach (var biome in biomes)
                    {
                        biome.ApplyPrototypes(terrain);
                    }
                    var chunk = new UnityChunk
                    {
                        Position = new Pair((int) x, (int) y),
                        Parent = this,
                        Terrain = terrain
                    };
                    chunks.Add(chunk);
                    chunk.Create();
                }
            }

            return chunks;
        }

        private CellImpl CellInitializer(Pair coords)
        {
            return new CellImpl(coords) {Core = Core};
        }
    }
}