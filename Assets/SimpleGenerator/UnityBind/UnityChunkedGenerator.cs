using System;
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
        public Pair Test;
        public Vector2 UnitySize;

        public CoreImpl Core;

        private void Start()
        {
            Core = new CoreImpl(coords =>
                {
                    var i = new CellImpl(coords) {Core = Core};
                    return i;
                },Resolution,
                Array.FindAll(gameObject.GetComponents<IModifier<CellImpl>>(), modifier => ((MonoBehaviour) modifier).enabled));
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
                    chunk.Position = new Pair(x,y);
                    chunk.Parent = this;
                    chunks.Add(chunk);
                }
            }
            return chunks;
        }

        public void Refresh()
        {
            //var i = new List<Pair>();
            //var cellCache = new List<UnityChunk>(_chunks);
            //for (var y = -ViewDistance + CurrentChunkPosition.y; y <= ViewDistance + CurrentChunkPosition.y; y++)
            //{
            //    for (var x = -ViewDistance + CurrentChunkPosition.x; x <= ViewDistance + CurrentChunkPosition.y; x++)
            //    {
            //        var position = new Pair((int) x, (int) y);
            //        var matchingCell = cellCache.Find(chunk => chunk.Position == position);
            //        if (matchingCell == null)
            //        {
            //            i.Add(position);
            //        }
            //        else
            //        {
            //            cellCache.Remove(matchingCell);
            //        }
            //    }
            //}
            //for (int j = 0; j < i.Capacity; j++)
            //{
            //    cellCache[j].Position = i[j];
            //    cellCache[j].Refresh();
            //}
            foreach (var unityChunk in _chunks)
            {
                unityChunk.Refresh();
            }
        }

    }
}