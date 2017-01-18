using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class UnityChunkPool : MonoBehaviour
    {
        private void Start()
        {
            var i = new ChunkedCore(coords => new CellImpl(coords),
            new LambdaModifier<CellImpl>((core, current) =>
            {
                current.Height = Mathf.PerlinNoise(current.Coords.X,
                    current.Coords.Y);
            }));
            i.GetCell(new Pair(3, 3));
        }
    }
}