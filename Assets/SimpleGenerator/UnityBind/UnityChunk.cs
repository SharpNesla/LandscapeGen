using System;
using System.Runtime.InteropServices;
using SimpleGenerator.Util;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class UnityChunk : MonoBehaviour
    {
        public UnityChunkedGenerator Parent;
        private Terrain _terra;

        public int X, Y;
        public int ChunkSize;
        private AsyncTask _refreshTask;

        private void Start()
        {
            _terra = gameObject.GetComponent<Terrain>();
            _terra.terrainData.heightmapResolution = Parent.Resolution;
            _terra.terrainData.size = new Vector3(Parent.UnitySize, 300, Parent.UnitySize);
            _refreshTask = new AsyncTask(()=> {},()=>{});
        }

        public void Refresh()
        {
            if (_refreshTask != null)
            {
                _refreshTask.StopTaskExecuting();
            }

            _terra.terrainData = Instantiate(
                Parent.ChunkReference.GetComponent<Terrain>().terrainData
            );

            TerrainStorage storage = TerrainStorage.FromTerrainData(_terra.terrainData);
            DateTime chunkTime = DateTime.UtcNow;
            _refreshTask = new AsyncTask(() =>
                {
                    chunkTime = DateTime.Now;
                    var coordinates = new Pair(X * Parent.Resolution, Y * Parent.Resolution);
                    var size = new Pair(Parent.Resolution, Parent.Resolution);
                    storage.ApplyCells(Parent.Core, size, coordinates);

                },
                () =>
                {
                    _terra.terrainData.FromTerrainStorage(storage);
                    gameObject.transform.position = new Vector3(X * Parent.UnitySize, 0, Y * Parent.UnitySize);
                    Debug.LogFormat("Refreshing chunk -> x:{0}, y:{1}, <>:{2}", X, Y, DateTime.Now - chunkTime);
                }
            );
            AsyncDispatcher.Queue(_refreshTask);
        }


    }
}