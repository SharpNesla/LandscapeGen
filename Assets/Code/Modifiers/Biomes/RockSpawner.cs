using Assets.SimpleGenerator;
using UnityEngine;

namespace Code.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class RockSpawner : MonoBehaviour, IBiome<CellImpl>
    {
        [Range(25, 10000)] public int RockChance;


        public int MinTreeHeight, MaxTreeHeight;
        public void Start()
        {
        }

        public void Callback(CellImpl current)
        {
            var value = current.Position.RandomFromPosition(0, RockChance, 54);
            if (value == 0)
            {
                current.Biomes.Add(this);
            }
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.Instances.Add(MakeTree(current, current.Core.Resolution));
        }

        TreeInstance MakeTree(CellImpl current, int localScale)
        {
            TreeInstance instance = new TreeInstance
            {
                prototypeIndex = 1,
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