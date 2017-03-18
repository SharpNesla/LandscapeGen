using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Assets.SimpleGenerator;
using Code.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class TextureExporter : MonoBehaviour
    {
        public string _fileName;
        public Pair _position;

        public RawImage BiomeTexture, HeightmapTexture;

        public void SetFileName(string str)
        {
            _fileName = str;
        }

        public void SetX(string str)
        {
            int.TryParse(str, out _position.X);
        }

        public void SetY(string str)
        {
            int.TryParse(str, out _position.Y);
        }
        public void Generate()
        {
            var generator = FindObjectOfType<UnityChunkedGenerator>().Core;
            var biomeTexture = generator.BiomeTexture(_position);
            var heightTexture = generator.HeightTexture(_position);
            BiomeTexture.texture = biomeTexture;
            HeightmapTexture.texture = heightTexture;
        }

        public void Export()
        {
            var generator = FindObjectOfType<UnityChunkedGenerator>().Core;
            var biomeTexture = generator.BiomeTexture(_position);
            var heightTexture = generator.HeightTexture(_position);
            File.WriteAllBytes("BiomeMap.png" ,biomeTexture.EncodeToPNG());
            File.WriteAllBytes("HeightMap.png", heightTexture.EncodeToPNG());
        }
    }
}