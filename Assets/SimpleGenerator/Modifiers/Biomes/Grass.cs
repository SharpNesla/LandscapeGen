using System;
using UnityEditor;
using UnityEngine;
namespace Assets.SimpleGenerator.Biomes
{
    public class Grass : MonoBehaviour, IBiome<CellImpl>
    {
        public float TopBound, LowBound;
        public int GrassCount;
        public void Callback<TCore>(TCore core, CellImpl current) where TCore : Core<CellImpl>
        {
            if (current.Height < TopBound || current.Height < LowBound)
            {
                current.Biomes.Add(this);
            }
        }

        public void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 0] = 0f;
            storage.SplatMap[current.LocalPosition.X, current.LocalPosition.Y, 2] = 3f;
            storage.DetailLayer[current.LocalPosition.X, current.LocalPosition.Y] = GrassCount;
        }
    }
}