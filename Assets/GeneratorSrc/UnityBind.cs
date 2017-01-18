using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using StandaloneGenerator;
using UnityEngine;
using Random = System.Random;

namespace Assets
{
    public class UnityBind : MonoBehaviour
    {
        private DiamonSquareNoise noiseGen;

        [Header("GlobalParameters")] public string seed = "21";

        [Header("Parameters of heightmap generator")] public int Depth;
        public int Roughness;
        public int TerrainScale;

        [Header("Parameters of biomemap generator")] public float waterlevel;
        public float BeachLevel;
        public float GrassLevel;
        public float ForestMinLevel;
        public float ForestMaxLevel;
        public float RockyLevel;
        public float SnowLevel;

        public float GrassElevation = 0.004f;

        public float SnowElevation = 0.004f;

        public float RockyElevation = 0.004f;
        [Header("Parameters of result terrain")] public int GrassDepth = 15;
        public int TreeChance;
        public int MaxTreeHeight;
        public int MinTreeHeight;

        private Random _randomizer;

        private Terrain _terra;
        void Start()
        {
            
            TimeBanch(() =>
            {
                Terrain a = Terrain.activeTerrain;
                _randomizer = new Random(seed.GetHashCode());
                noiseGen = new DiamonSquareNoise(Depth, 16, _randomizer);
                a.terrainData.SetHeights(0, 0, noiseGen.floatmap);
                var Params = new GeneratorParams(waterlevel, BeachLevel, GrassLevel, RockyLevel,
                    ForestMinLevel, ForestMaxLevel, RockyElevation, GrassElevation, SnowElevation, TreeChance);
                _terra = Terrain.activeTerrain;
                BiomeMap m = new BiomeMap(noiseGen, Params);
                MakeSplatMap(_terra.terrainData, m);
            });

        }

        void TimeBanch(Action a)
        {
            var dateTime = System.DateTime.Now;
            a();
            Debug.Log(DateTime.Now - dateTime);
        }

        public void MakeSplatMap(TerrainData terrainData, BiomeMap gen)
        {
            _terra.terrainData.treeInstances = new List<TreeInstance>().ToArray();
            
            var splatmap = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
            var detailLayer = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailWidth, 0);

            var splatmapdepth = splatmap.GetLength(2);

            BiomeMap.TerrainBrute(gen.Tilemap, gen.Scale, (x, y, cell) =>
            {
                for (int i = 0; i < splatmapdepth; i++)
                {
                    splatmap[y,x, i] = 0f;
                }
                detailLayer[y, x] = 0;
                switch (cell.Biome)
                {
                    case Biomes.AlpSnow:
                        splatmap[y, x, 1] = 1f;
                        break;
                    case Biomes.Rocks:
                        splatmap[y, x, 0] = 1f;
                        break;
                    case Biomes.Forest:
                        splatmap[y, x, 2] = 1f;
                        detailLayer[y, x] = GrassDepth;
                        _terra.AddTreeInstance(MakeTree(gen.Scale - 1,x,y));
                        break;
                    case Biomes.Grass:
                        splatmap[y, x, 2] = 1f;
                        detailLayer[y, x] = GrassDepth;
                        break;
                    case Biomes.Water:
                        splatmap[y, x, 3] = 1f;
                        break;
                    case Biomes.RockSand:
                        splatmap[y, x, 0] = 1f;
                        break;
                    case Biomes.Beach:
                        splatmap[y, x, 3] = 1f;
                        break;
                }
            });
            terrainData.SetAlphamaps(0,0, splatmap);
            terrainData.SetDetailLayer(0,0,0,detailLayer);
            _terra.Flush();
        }

        TreeInstance MakeTree(int scale, int x, int y)
        {
            TreeInstance instance = new TreeInstance
            {
                prototypeIndex = 0,
                rotation = _randomizer.Next(0, 359),
                heightScale = (float) _randomizer.Next(MinTreeHeight,MaxTreeHeight) / 10,
                position = new Vector3 ((float) x / scale, 0f, (float) y / scale)
            };
            instance.widthScale = instance.heightScale;
            return instance;
        }
    }
}
