using UnityEngine;

namespace Assets.SimpleGenerator.Biomes
{
    public class RegularForest : MonoBehaviour, IBiome<CellImpl>
    {
        public float TopBound, LowBound;
        [Range(25,10000)]
        public int TreeChance;

        private System.Random _randomizer;

        public int MinTreeHeight, MaxTreeHeight;

        private void Start()
        {
            _randomizer = new System.Random();
        }

        public void Callback<TCore>(TCore core, CellImpl current) where TCore : Core<CellImpl>
        {
            if (_randomizer.Next(0, TreeChance) == 0)
            {
                current.Biomes.Add(this);
            }
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            var resolution = storage.Heights.GetLength(0);
            storage.Instances.Add(MakeTree(current, resolution));
        }
        TreeInstance MakeTree(CellImpl current, int localScale)
        {
            TreeInstance instance = new TreeInstance
            {
                prototypeIndex = 0,
                rotation = _randomizer.Next(0, 359),
                heightScale = (float) _randomizer.Next(MinTreeHeight,MaxTreeHeight) / 10,
                position = new Vector3 ((float) current.LocalPosition.Y / localScale, current.Height,
                    (float) current.LocalPosition.X / localScale)
            };
            instance.widthScale = instance.heightScale;
            return instance;
        }
    }
}