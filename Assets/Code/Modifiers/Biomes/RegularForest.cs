using System.Linq;
using Assets.SimpleGenerator;
using Assets.SimpleGenerator.TerrainModules;
using LibNoise.Generator;
using UnityEngine;

namespace Code.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class RegularForest : Biome<CellImpl>
    {
        private float _topBound, _lowBound;
        [Range(25,10000)]
        public int TreeChance;

        public int MinTreeHeight, MaxTreeHeight;
        public float ForestModulatorFrequency;

        public TerrainObject Tree;

        private Perlin _hillModulator;

        private int _index;

        public override void Start()
        {
            var grassBiome = gameObject.GetComponent<Grass>();
            _hillModulator = new Perlin{OctaveCount = 1, Frequency = ForestModulatorFrequency, Seed = 34};
            _topBound = grassBiome.TopBound;
            _lowBound = grassBiome.LowBound;
        }

        public override void Callback(CellImpl current)
        {
            var value = current.Position.RandomFromPosition(0, TreeChance, 54);
            if (current.Height < _topBound && current.Height > _lowBound
                &&value == 0 &&
                _hillModulator.GetValue(current.Position.X, 0, current.Position.Y) > 0.07
                && current.Biomes.Any(x=>x.GetType() == typeof(Grass)))
            {
                current.Biomes.Add(this);
            }
        }

        public override void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.Instances.Add(MakeTree(current, current.Core.Resolution));
        }

        private TreeInstance MakeTree(CellImpl current, int localScale)
        {
            var instance = new TreeInstance
            {
                prototypeIndex = _index,
                rotation = current.Position.RandomFromPosition(0, 359,54),
                heightScale = (float) current.Position.RandomFromPosition(Tree.MinimalTreeScale,Tree.MaximalTreeScale,54) / 10,
                position = new Vector3 ((float) current.LocalPosition.Y / localScale, current.Height,
                    (float) current.LocalPosition.X / localScale)
            };
            instance.widthScale = instance.heightScale;
            return instance;
        }

        public override void ApplyPrototypes(Terrain terrain)
        {
            _index = Tree.ApplyTree(terrain);
        }
    }
}