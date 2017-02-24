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
        public int modul;
        public void Start()
        {
            _noiseGenerator = new Billow{Frequency = Frequency,OctaveCount = Octaves, Seed = 34};
            _hillModulator = new Perlin{Frequency = HillModulatorFrequency, OctaveCount = 1};
        }

        public void Callback<TCore>(TCore core, CellImpl current) where TCore : Core<CellImpl>
        {
            var x = current.Coords.X;
            var y = current.Coords.Y;
            current.Height = (float) (_noiseGenerator.GetValue(x,0,y) / 3 + 0.6f * _hillModulator.GetValue(x,0,y));
            current.Height = current.Height * current.Height;
        }
    }
}