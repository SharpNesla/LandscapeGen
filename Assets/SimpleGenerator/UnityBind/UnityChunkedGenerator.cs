using System.Collections.Generic;
using Test;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class UnityChunkedGenerator : MonoBehaviour
    {
        public List<UnityChunk> Chunks;
        public GameObject ChunkReference;
        public int ViewDistance;

        private void Start()
        {
            Chunks = CreateChunks();

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

            }
            if (Input.GetKeyDown(KeyCode.F))
            {

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
                    chunk.ChunkSize =
                    chunk.X = x;
                    chunk.Y = y;
                    chunks.Add(chunk);
                }
            }
            return chunks;
        }
    }
}