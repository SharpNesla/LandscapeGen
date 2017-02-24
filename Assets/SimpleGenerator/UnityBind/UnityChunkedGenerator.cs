using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    [RequireComponent (typeof (Cursor))]
    public class UnityChunkedGenerator : MonoBehaviour
    {
        private List<UnityChunk> _chunks;
        public GameObject ChunkReference;

        public Vector2 CurrentChunkPosition;

        public int ViewDistance;
        public int Resolution;

        public Vector2 UnitySize;

        public Core<CellImpl> Core;

        private void Start()
        {

            Core = new CoreImpl(coords =>
                {
                    var i = new CellImpl(coords);
                    return i;
                },
                gameObject.GetComponents<IModifier<CellImpl>>());
            _chunks = CreateChunks();

        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Refresh();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (var unityChunk in _chunks)
                {
                    AsyncDispatcher.Abort(unityChunk._refreshTask);
                }
            }
        }

        public List<UnityChunk> CreateChunks()
        {
            var chunksCount = (ViewDistance * 2 + 1) * (ViewDistance * 2 + 1);
            var chunks = new List<UnityChunk>(chunksCount);
            for (var y = -ViewDistance; y <= ViewDistance; y++)
            {
                for (var x = -ViewDistance; x <= ViewDistance; x++)
                {
                    var chunk = Instantiate(ChunkReference).GetComponent<UnityChunk>();
                    chunk.X = x;
                    chunk.Y = y;
                    chunk.Parent = this;
                    chunks.Add(chunk);
                }
            }
            return chunks;
        }

        public void Refresh()
        {
            foreach (var unityChunk in _chunks)
            {
                unityChunk.Refresh();
            }
        }

    }
}