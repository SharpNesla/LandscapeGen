using System;
using System.Threading;
using Code.Core;
using LibNoise.Generator;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class BillowNoiseModifier : MonoBehaviour, IModifier<CellImpl>
    {
        public float Frequency;
        public int Octaves;
        private Billow _noiseGenerator;
        private Perlin _hillModulator, _continentModulator;
        public float HillModulatorFrequency;
        public void Start()
        {
            _noiseGenerator = new Billow{Frequency = Frequency,OctaveCount = Octaves, Seed = 34};
            _hillModulator = new Perlin{Frequency = HillModulatorFrequency, OctaveCount = 1};
        }

        public void Callback(CellImpl current)
        {
            var x = current.Position.X;
            var y = current.Position.Y;
            current.Height = (float) _noiseGenerator.GetValue(x,0,y) /1.4f + 0.28f;
            //Monitor.Enter(maximumHeight);
            //maximumHeight = Mathf.Max(maximumHeight, current.Height);
            //minimumHeight = Mathf.Min(minimumHeight, current.Height);
            //Monitor.Exit(maximumHeight);
            current.Height = current.Height * current.Height;
        }
    }
}