using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class TerrainStorage
    {
        public float[,] Heights;

        private TerrainStorage(TerrainData data)
        {
            Heights = new float[data.heightmapWidth,data.heightmapHeight];
        }

        public static TerrainStorage FromTerrainData(TerrainData data)
        {
            return new TerrainStorage(data);
        }

        public void ApplyCells<T>(Core<T> core,Pair size, Pair position) where T : Cell
        {
            var cells = core.GetRect(size, position);
            T[] top;
            T[] bottom;
            cells.Foreach(coords =>
            {
                Heights[coords.X,coords.Y] = cells[coords.X, coords.Y].Height;
            });
        }

    }

    public static class TerrainStorageExtensions
    {
        public static void FromTerrainStorage(this TerrainData data, TerrainStorage storage)
        {
            data.SetHeights(0,0,storage.Heights);

        }


    }
}