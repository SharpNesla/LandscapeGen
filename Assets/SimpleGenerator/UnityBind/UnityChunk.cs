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
        public AsyncTask _refreshTask;

        private void Start()
        {
            var currentTerrainData = AssignTerrainData();
            _terra = gameObject.GetComponent<Terrain>();

            _terra.basemapDistance = 2000;
            _terra.terrainData = currentTerrainData;

            gameObject.GetComponent<TerrainCollider>().terrainData = currentTerrainData;

            _refreshTask = new AsyncTask(()=> {},()=>{});
        }

        private TerrainData AssignTerrainData()
        {
            var prototype = Instantiate(Parent.ChunkReference.GetComponent<Terrain>().terrainData);
            var data = new TerrainData
            {
                heightmapResolution = Parent.Resolution + 1,
                alphamapResolution = Parent.Resolution,
                baseMapResolution = Parent.Resolution,
                splatPrototypes = prototype.splatPrototypes,
                detailPrototypes = prototype.detailPrototypes,
                treePrototypes = prototype.treePrototypes,
                size = new Vector3(Parent.UnitySize.x, Parent.UnitySize.y, Parent.UnitySize.x)
            };

            data.SetDetailResolution(Parent.Resolution, 8);

            return data;
        }

        public void Refresh()
        {
            AsyncDispatcher.Abort(_refreshTask);

            var storage = TerrainStorage.FromTerrainData(_terra.terrainData);
            gameObject.transform.position = new Vector3((X + Parent.CurrentChunkPosition.x) * Parent.UnitySize.x, 0, (Y+Parent.CurrentChunkPosition.x) * Parent.UnitySize.x);

            var chunkTime = DateTime.UtcNow;
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

                    Debug.LogFormat("Refreshing chunk -> x:{0}, y:{1}, <>:{2}", X, Y, DateTime.Now - chunkTime);
                }
            );
            AsyncDispatcher.Queue(_refreshTask);
        }


    }
}