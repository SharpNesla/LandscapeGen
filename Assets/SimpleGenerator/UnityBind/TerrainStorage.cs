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
            Heights = new float[data.heightmapWidth,data.heightmapHeight];
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

        public void ApplyCells(Core<CellImpl> core,Pair size, Pair position)
        {
            var cells = core.GetRect(size, position);
            var top = core.GetRect(new Pair(size.X, 1), new Pair(size.X, 0) + position);
            var bottom = core.GetRect(new Pair(1, size.X), new Pair(0, size.Y) + position);

            for (int x = 0; x < size.X; x++)
            {
                Heights[x, size.Y] = top[x, 0].Height;
            }

            for (int y = 0; y < size.X; y++)
            {
                Heights[size.X, y] = bottom[0, y].Height;
            }
            cells.Foreach((coords, cell) =>
            {
                Heights[coords.X,coords.Y] = cells[coords.X, coords.Y].Height;
                cell.LocalPosition = coords;
                for (var index = 0; index < cell.Biomes.Count; index++)
                {
                    var biome = cell.Biomes[index];
                    biome.Apply(cell, this);
                }
            });
        }
    }

    public static class TerrainStorageExtensions
    {
        public static void FromTerrainStorage(this TerrainData data, TerrainStorage storage)
        {
            data.SetHeights(0,0,storage.Heights);
            data.SetAlphamaps(0,0, storage.SplatMap);
            data.SetDetailLayer(0,0,0,storage.DetailLayer);
            data.treeInstances = storage.Instances.ToArray();

        }
    }
}