using System;
using SimpleGenerator.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class UnityChunk
    {
        public Terrain Terrain;
        public TerracoreGenerator Parent;

        public Pair Position;
        public AsyncTask _refreshTask;
        private TerrainStorage _storage;

        [ExecuteInEditMode]
        public void Create()
        {
            _storage = TerrainStorage.FromTerrainData(Terrain.terrainData);
            AssetDatabase.CreateAsset(Terrain.terrainData,
                String.Format("Assets/Terracore Terrains Data/Chunk({0};{1}).asset",
                    Position.X - Parent.GenerationPatchOffset.x,
                    Position.Y - Parent.GenerationPatchOffset.y));
                Terrain.gameObject.name = String.Format("Chunk({0};{1})",
                Position.X - Parent.GenerationPatchOffset.x,
                Position.Y - Parent.GenerationPatchOffset.y);

            var resolution = Parent.TerrainSettings.Resolution;
            var scale = Parent.TerrainSettings.TerrainScale;
            var coordinates = new Pair(Position.X * resolution, Position.Y * resolution);
            var size = new Pair(resolution, resolution);

            _storage.ApplyCells(Parent.Core, size, coordinates);
            Terrain.terrainData.ApplyTerrainStorage(_storage);


            Terrain.gameObject.transform.position = new Vector3((Position.X - Parent.GenerationPatchOffset.x) * scale.x,
                0, (Position.Y - Parent.GenerationPatchOffset.y) * scale.z);
            Debug.LogFormat("Refreshing chunk -> x:{0}, y:{1}, <>", Position.X, Position.Y);
        }
    }
}