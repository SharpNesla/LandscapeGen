using System;
using UnityEngine;

namespace Assets.SimpleGenerator.TerrainModules
{
    [Serializable]
    public class TerrainObject
    {
        public GameObject Prefab;
        public float BendFactor;
        public int MinimalTreeScale;
        public int MaximalTreeScale;
        public TreePrototype ToTreePrototype()
        {
            var i = new TreePrototype
            {
                prefab = Prefab,
                bendFactor = BendFactor
            };
            return i;
        }

        public int ApplyTree(Terrain terrain)
        {
            var treeprotos = terrain.terrainData.treePrototypes;
            Array.Resize(ref treeprotos, treeprotos.Length+1);
            treeprotos[treeprotos.Length - 1] = ToTreePrototype();
            terrain.terrainData.treePrototypes = treeprotos;
            return treeprotos.Length - 1;
        }

    }
}