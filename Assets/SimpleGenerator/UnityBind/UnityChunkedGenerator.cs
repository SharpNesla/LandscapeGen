﻿using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class UnityChunkedGenerator : MonoBehaviour
    {
        public List<UnityChunk> Chunks;
        public GameObject ChunkReference;

        public int ViewDistance;
        public int Resolution;
        public int UnitySize;

        public Core<CellImpl> Core;

        private void Start()
        {
            Chunks = CreateChunks();

            Core = new Core<CellImpl>(coords=> new CellImpl(coords),
                gameObject.GetComponents<IModifier<CellImpl>>());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Refresh();
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
                    chunk.ChunkSize = UnitySize;
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
            foreach (var unityChunk in Chunks)
            {
                unityChunk.Refresh();
            }
        }

    }
}