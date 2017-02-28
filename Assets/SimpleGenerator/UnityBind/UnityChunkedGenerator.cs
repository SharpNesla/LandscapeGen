using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;

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
        public List<UnityChunk> _refreshingChunks;
        private List<Pair> _refreshingPositions;
        private void Start()
        {
            Core = new CoreImpl(coords =>
                {
                    var i = new CellImpl(coords) {Core = Core};
                    return i;
                },Resolution,
                Array.FindAll(gameObject.GetComponents<IModifier<CellImpl>>(), modifier => ((MonoBehaviour) modifier).enabled));
            _chunks = CreateChunks();
            _refreshingChunks = new List<UnityChunk>();
            _refreshingPositions = new List<Pair>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Refresh();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                CoreUtils.Foreach(new Pair(ViewDistance * 2 + 1,ViewDistance * 2 + 1), position =>
                {
                    var current = _chunks[position.Y * (ViewDistance * 2 + 1) + position.X];
                    current.Position = position + new Pair(-ViewDistance, -ViewDistance) + CurrentChunkPosition;
                    current.Refresh();
                });
                var b = 0;
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

            _refreshingChunks.AddRange(_chunks);
            var positions = new List<Pair>();
            CoreUtils.Foreach(new Pair(ViewDistance * 2 + 1,ViewDistance * 2 + 1), localPosition =>
            {
                var position = localPosition + new Pair(-ViewDistance, -ViewDistance) + CurrentChunkPosition;
                var current = _refreshingChunks.Find(x => x.Position == position);
                if (current != null)
                {
                    _refreshingChunks.Remove(current);
                }
                else
                {
                    positions.Add(position);
                }
            });
            for (int i = 0; i < positions.Count; i++)
            {
                _refreshingChunks[i].Position = positions[i];
                _refreshingChunks[i].Refresh();
            }
            _refreshingChunks.Clear();
            _refreshingPositions.Clear();
        }

    }
}