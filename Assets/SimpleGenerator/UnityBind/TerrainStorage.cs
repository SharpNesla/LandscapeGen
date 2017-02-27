using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityStandardAssets.CinematicEffects;

namespace Assets.SimpleGenerator
{
    public class TerrainStorage
    {
        public float[,] Heights;
        public float[,,] SplatMap;
        public int[,] DetailLayer;
        public List<TreeInstance> Instances;

        private TerrainStorage(TerrainData data)
        {
            Heights = new float[data.heightmapWidth, data.heightmapHeight];
            SplatMap = new float
            [data.alphamapWidth,
                data.alphamapWidth,
                data.splatPrototypes.Length];
            DetailLayer = new int[data.detailHeight, data.detailWidth];
            Instances = new List<TreeInstance>();
        }

        public static TerrainStorage FromTerrainData(TerrainData data)
        {
            return new TerrainStorage(data);
        }

        public void ApplyCells(CoreImpl core, Pair size, Pair position)
        {
            var cells = core.GetChunk(position).Foreach((pair, impl) => impl.LocalPosition = pair + new Pair(-1,-1));

            for (var y = 0; y < size.X; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    var cell = cells[x + 1, y + 1];
                    Heights[x,y] = cells[x, y].Height;
                    cell.LocalPosition = new Pair(x,y);
                    for (var index = 0; index < cell.Biomes.Count; index++)
                    {
                        var biome = cell.Biomes[index];
                        biome.Apply(cell, this);
                    }
                }
            }

            for (var x = 0; x <= size.X; x++)
            {
                Heights[x, size.Y] = cells[x, size.Y].Height;
            }

            for (var y = 0; y < size.X; y++)
            {
                Heights[size.X, y] = cells[size.X, y].Height;
            }
        }
    }

    public static class TerrainStorageExtensions
    {
        public static void ApplyTerrainStorage(this TerrainData data, TerrainStorage storage)
        {
            data.SetHeights(0, 0, storage.Heights);
            data.SetAlphamaps(0, 0, storage.SplatMap);
            data.SetDetailLayer(0, 0, 0, storage.DetailLayer);
            data.treeInstances = storage.Instances.ToArray();
        }
    }
}