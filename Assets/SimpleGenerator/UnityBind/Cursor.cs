using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Assets.SimpleGenerator
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
            var currentPosition = _vector3.position / Generator.UnitySize.x;
            Generator.CurrentChunkPosition = new Vector2((float)Math.Floor(currentPosition.x),
                (float)Math.Floor(currentPosition.z));
        }
    }
}