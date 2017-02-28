using Assets.SimpleGenerator;
using Assets.SimpleGenerator.Biomes;
using LibNoise.Generator;
using UnityEngine;

namespace SimpleGenerator.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class RegularForest : MonoBehaviour, IBiome<CellImpl>
    {
        private float _topBound, _lowBound;
        [Range(25,10000)]
        public int TreeChance;

        public int MinTreeHeight, MaxTreeHeight;
        public float ForestModulatorFrequency;
        private Perlin _hillModulator;
        public void Start()
        {
            var grassBiome = gameObject.GetComponent<Grass>();
            _hillModulator = new Perlin{OctaveCount = 1, Frequency = ForestModulatorFrequency, Seed = 34};
            _topBound = grassBiome.TopBound;
            _lowBound = grassBiome.LowBound;
        }

        public void Callback(CellImpl current)
        {
            var value = current.Position.RandomFromPosition(0, TreeChance, 54);
            if (current.Height < _topBound && current.Height > _lowBound
                &&value == 0 &&
                _hillModulator.GetValue(current.Position.X, 0, current.Position.Y) > 0.07)
            {
                current.Biomes.Add(this);
            }
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.Instances.Add(MakeTree(current, current.Core.Resolution));
        }

        private TreeInstance MakeTree(CellImpl current, int localScale)
        {
            var instance = new TreeInstance
            {
                prototypeIndex = 0,
                rotation = current.Position.RandomFromPosition(0, 359,54),
                heightScale = (float) current.Position.RandomFromPosition(MinTreeHeight,MaxTreeHeight,54) / 10,
                position = new Vector3 ((float) current.LocalPosition.Y / localScale, current.Height,
                    (float) current.LocalPosition.X / localScale)
            };
            instance.widthScale = instance.heightScale;
            return instance;
        }
    }
}