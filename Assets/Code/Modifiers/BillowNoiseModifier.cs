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
        public float maximumHeight;
        public float minimumHeight;
        public float hillHeight;
        public void Start()
        {
            _noiseGenerator = new Billow{Frequency = Frequency,OctaveCount = Octaves, Seed = 34};
            _hillModulator = new Perlin{Frequency = HillModulatorFrequency, OctaveCount = 12};
        }

        public void Callback(CellImpl current)
        {
            var x = current.Position.X;
            var y = current.Position.Y;
            var f =  Mathf.Abs((float) (_noiseGenerator.GetValue(x,0,y) /1.4f + 0.28f));
            f = f * f / 2.5f + 0.3f;

            if (f < 0.3f)
            {
                f = 0.3f;
            }

            var b = _hillModulator.GetValue(x, 0, y);

            f += (float) (b / hillHeight);

            current.Height = f;
            maximumHeight = Mathf.Max(maximumHeight, current.Height);
            minimumHeight = Mathf.Min(minimumHeight, current.Height);
        }

        public void Refresh() {}
    }
}