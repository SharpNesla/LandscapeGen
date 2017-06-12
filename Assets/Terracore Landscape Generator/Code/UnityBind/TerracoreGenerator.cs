using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.SimpleGenerator.TerrainModules;
using Code.Core;
using Code.Modifiers.Biomes;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class TerracoreGenerator : MonoBehaviour
    {
        public TerrainSettings TerrainSettings;
        public string DataFolderPath = "Terracore Terrains Data";
        public Vector2 GenerationPatchOffset;
        public Vector2 TerrainsRectScale;
        public CoreImpl Core;

        public void Place()
        {
            if (!AssetDatabase.IsValidFolder(String.Format("Assets/{0}", DataFolderPath)))
            {
                AssetDatabase.CreateFolder("Assets", DataFolderPath);
            }
            var modifiers = Array.FindAll(
                gameObject.GetComponents<IModifier<CellImpl>>(),
                modifier => ((MonoBehaviour)modifier).enabled);
            var biomes = modifiers.OfType<Biome<CellImpl>>();

            foreach (var modifier in modifiers)
            {
                modifier.Start();
            }

            Core = new CoreImpl(CellInitializer, TerrainSettings.Resolution,
                modifiers);
            CreateChunks(biomes);
        }

        public List<UnityChunk> CreateChunks(IEnumerable<Biome<CellImpl>> biomes)
        {
            var chunksCount = TerrainsRectScale.x * TerrainsRectScale.y;
            var chunks = new List<UnityChunk>((int)chunksCount);

            int counter = 0, target = (int)(TerrainsRectScale.x * TerrainsRectScale.y);

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
                        Position = new Pair((int)x, (int)y),
                        Parent = this,
                        Terrain = terrain
                    };
                    chunks.Add(chunk);
                    chunk.Create();

                    EditorUtility.DisplayProgressBar("Generating terrains...", String.Format(
                        "Was generated {0} chunks out of {1}", counter, target), (float)counter / target);
                }
            }

            EditorUtility.ClearProgressBar();

            return chunks;
        }

        private CellImpl CellInitializer(Pair coords)
        {
            return new CellImpl(coords) { Core = this.Core };
        }
    }
}