using UnityEngine;
namespace Assets.SimpleGenerator.Biomes
{
    public class Grass : MonoBehaviour, IBiome<CellImpl>
    {

        public void Callback<TCore>(TCore core, CellImpl current) where TCore : Core<CellImpl>
        {
            current.Biomes.Add(this);
        }

        public void Apply(TerrainStorage storage, CellImpl cell)
        {
            storage.SplatMap[cell.Coords.X, cell.Coords.Y, 1] = 1f;
        }
        public void Out()
        {

        }
        public void Init()
        {

        }
    }
}