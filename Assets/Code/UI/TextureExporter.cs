using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Assets.SimpleGenerator;
using Code.Util;
using UnityEngine;

namespace Code.UI
{
    public class TextureExporter : MonoBehaviour
    {
        public string _fileName;
        public Pair _position;


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
            var generator =  FindObjectOfType<UnityChunkedGenerator>().Core;
            File.WriteAllBytes(_fileName, generator.BiomeTexture(_position).EncodeToPNG());
        }
    }
}