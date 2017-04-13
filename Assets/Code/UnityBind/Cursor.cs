using System;
using Assets.SimpleGenerator;
using Assets.SimpleGenerator.TerrainModules;
using UnityEngine;

namespace Code.UnityBind
{
    public class Cursor : MonoBehaviour
    {
        public UnityChunkedGenerator Generator;
        private Transform _vector3;

        private void Start()
        {
            Generator = gameObject.GetComponent<UnityChunkedGenerator>();
            _vector3 = FindObjectOfType<Camera>().transform;
        }

        private void Update()
        {
            var currentPosition = _vector3.position / Generator.TerrainSettings.TerrainScale.x;
            var currentNormalizedPosition = new Vector2((float) Math.Floor(currentPosition.x),
                (float) Math.Floor(currentPosition.z));
            if (Generator.CurrentChunkPosition != currentNormalizedPosition)
            {
                Generator.CurrentChunkPosition = currentNormalizedPosition;
                Generator.RefreshChunks();
            }
            Generator.CurrentChunkPosition = currentNormalizedPosition;

        }
    }
}