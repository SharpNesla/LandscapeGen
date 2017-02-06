using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class UnityChunk : MonoBehaviour
    {
        private UnityChunkedGenerator _parent;
        private Terrain _terra;

        public int X, Y;
        public int ChunkSize;

        private void Start()
        {
            _terra = gameObject.GetComponent<Terrain>();
            gameObject.transform.position = new Vector3();
        }

        public void Refresh(int x, int y)
        {

        }
    }
}