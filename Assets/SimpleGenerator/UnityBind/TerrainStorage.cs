using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class TerrainStorage
    {
        public float[,] Heights;

        private TerrainStorage(TerrainData data)
        {
            Heights = data.GetHeights(0,0,data.heightmapWidth,data.heightmapHeight);
        }

        public static TerrainStorage FromTerrainData(TerrainData data)
        {
            return new TerrainStorage(data);
        }
    }

    public static class TerrainStorageExtensions
    {
        public static void FromTerrainStorage(this TerrainData data, TerrainStorage storage)
        {
            data.SetHeights(0,0,storage.Heights);
        }

        public static void ApplyCoreData<T>(this TerrainData data, T[,] coreData) where T : Cell
        {

        }
    }
}