using System;
using StandaloneGenerator;
using UnityEngine;
using Random = System.Random;

namespace Assets
{
    public class BiomeMap

    {
        public Cell[,] Tilemap;
        public int Scale;
        private GeneratorParams _genparams;
        public DiamonSquareNoise e;
        public static void TerrainBrute<T>(T[,] massive, int scale, Action<int, int, T> bruteCallBack)
        {
            for (int y = 0; y < scale; y++)
            {
                for (int x = 0; x < scale; x++)
                {
                    bruteCallBack(x, y, massive[y, x]);
                }
            }
        }

        public BiomeMap(DiamonSquareNoise gen, GeneratorParams genParams)
        {
            Scale = gen.scale - 1;
            _genparams = genParams;
            TilesInit(gen);
            MakeBiomeMap();
        }

        private void TilesInit(DiamonSquareNoise gen)
        {
            float[,] heights = gen.floatmap;
            Tilemap = new Cell[Scale, Scale];
            TerrainBrute(Tilemap, Scale, (x, y, cell) =>
            {
                Tilemap[y, x] = new Cell {Height = heights[y, x], e = this};
            });
            e = gen;
        }

        public void MakeBiomeMap()
        {
            float mar = 0.5f;
            float sscale = 4f;
            TerrainBrute(Tilemap, Scale, (x, y, cell) =>
            {
                cell.Init(Tilemap, x, y);

                float xCoord = (float) x/Scale*sscale;
                float yCoord = (float) y/Scale*sscale;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);


                cell.Biome = Biomes.Water;
                float max = cell.MaxNeighboursHeight(),
                    min = cell.MinNeighboursHeight(),
                    height = cell.Height;

                if (height > _genparams.WaterLevel && height <= _genparams.BeachLevel)
                {
                    cell.Biome = max - min > _genparams.GrassElevation ? Biomes.Rocks : Biomes.Beach;
                }
                if (height > _genparams.BeachLevel && height <= _genparams.GrassLevel)
                {
                    if (max - min > _genparams.GrassElevation)
                    {
                        cell.Biome = Biomes.Rocks;
                    }
                    else
                    {
                        if (sample > mar && UnityEngine.Random.Range(0, _genparams.MaxTreeChance) == 0)
                        {
                           cell.Biome = Biomes.Forest;
                        }
                        else{cell.Biome = Biomes.Grass;}
                    }
                }
                if (height > _genparams.GrassLevel && height <= _genparams.RockyLevel)
                {
                    cell.Biome = max - min > _genparams.RockyElevation ? Biomes.Rocks : Biomes.RockSand;
                }
                if (height > _genparams.RockyLevel && height <= 1)
                {
                    cell.Biome = max - min > _genparams.SnowElevation ? Biomes.Rocks : Biomes.AlpSnow;
                }
            });

        }
    }
}
