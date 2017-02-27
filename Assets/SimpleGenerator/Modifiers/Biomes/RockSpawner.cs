using Assets.SimpleGenerator;
using UnityEngine;

namespace SimpleGenerator.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class RockSpawner : MonoBehaviour, IBiome<CellImpl>
    {
        [Range(25, 10000)] public int RockChance;

        private System.Random _randomizer;

        public int MinTreeHeight, MaxTreeHeight;
        public void Start()
        {
            _randomizer = new System.Random(32);
        }

        public void Callback(CellImpl current)
        {
            int value;
            lock (_randomizer)
            {
                value = _randomizer.Next(0, RockChance);
            }
            if (value == 0)
            {
                current.Biomes.Add(this);
            }
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            var resolution = storage.Heights.GetLength(0);
            storage.Instances.Add(MakeTree(current, resolution));
        }

        private TreeInstance MakeTree(CellImpl current, int localScale)
        {
            TreeInstance instance = new TreeInstance
            {
                prototypeIndex = _randomizer.Next(1,4),
                rotation = _randomizer.Next(0, 359),
                heightScale = (float) _randomizer.Next(MinTreeHeight, MaxTreeHeight) / 10,
                position = new Vector3((float) current.LocalPosition.Y / localScale, current.Height,
                    (float) current.LocalPosition.X / localScale)
            };
            instance.widthScale = instance.heightScale;
            return instance;
        }
    }
}