using System;
using SimpleGenerator.Util;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class UnityChunk : MonoBehaviour
    {
        private Terrain _terra;
        public UnityChunkedGenerator Parent;

        public Pair Position;
        public AsyncTask _refreshTask;

        private void Start()
        {
            var exampleTerrainData = Parent.ChunkReference.GetComponent<Terrain>();
            var currentTerrainData = AssignTerrainData(Instantiate(exampleTerrainData.terrainData));

            _terra = gameObject.GetComponent<Terrain>();

            _terra.basemapDistance = exampleTerrainData.basemapDistance;
            _terra.detailObjectDistance = exampleTerrainData.detailObjectDistance;
            _terra.treeBillboardDistance = exampleTerrainData.treeDistance;
            _terra.terrainData = currentTerrainData;

            gameObject.GetComponent<TerrainCollider>().terrainData = currentTerrainData;

            _refreshTask = new AsyncTask(()=> {},()=>{});
        }

        private TerrainData AssignTerrainData(TerrainData prototype)
        {
            var data = new TerrainData
            {
                heightmapResolution = Parent.Resolution + 1,
                alphamapResolution = Parent.Resolution,
                baseMapResolution = Parent.Resolution,
                splatPrototypes = prototype.splatPrototypes,
                detailPrototypes = prototype.detailPrototypes,
                treePrototypes = prototype.treePrototypes,
                size = new Vector3(Parent.UnitySize.x, Parent.UnitySize.y, Parent.UnitySize.x),
                wavingGrassTint = prototype.wavingGrassTint,
            };

            data.SetDetailResolution(Parent.Resolution, 8);

            return data;
        }

        public void Refresh()
        {
            var storage = TerrainStorage.FromTerrainData(_terra.terrainData);

            var chunkTime = DateTime.UtcNow;
            _refreshTask = new AsyncTask(() =>
                {
                    chunkTime = DateTime.Now;
                    var coordinates = new Pair((int) (Position.X +  Parent.CurrentChunkPosition.x) * Parent.Resolution, (int) (Position.Y  +  Parent.CurrentChunkPosition.y)* Parent.Resolution);
                    var size = new Pair(Parent.Resolution, Parent.Resolution);
                    storage.ApplyCells(Parent.Core, size, coordinates);

                },
                () =>
                {
                    _terra.terrainData.ApplyTerrainStorage(storage);

                    gameObject.transform.position = new Vector3((Position.X + Parent.CurrentChunkPosition.x) * Parent.UnitySize.x, 0,
                        (Position.Y + Parent.CurrentChunkPosition.y) * Parent.UnitySize.x);
                    Debug.LogFormat("Refreshing chunk -> x:{0}, y:{1}, <>:{2}", Position.X, Position.Y, DateTime.Now - chunkTime);
                }
            );
            AsyncDispatcher.Queue(_refreshTask);
        }
    }
}