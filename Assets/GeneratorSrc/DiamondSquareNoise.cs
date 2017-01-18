using System;
using System.Collections.Generic;
using System.Linq;
namespace StandaloneGenerator
{
    public class DiamonSquareNoise
    {
        public int[] map;
        public int scale;
        public int roughness;
        public int octaves;
        public float[,] floatmap;
        public const int Nil = 0;
        public Random rand;
        private int Coords(int y, int x)
        {
            return y * scale + x;
        }
        public DiamonSquareNoise(int depth, int roughness,Random randomizer)
        {
            rand = randomizer;
            octaves = depth;
            scale = CalculateSize(depth);
            this.roughness = roughness;
            GenerateMap();
            MainAlgoritm();
            Power();
            GetFloatMap();
        }

        public int CalculateSize(int depth)
        {
            return (int) (Math.Pow(2f, depth) + 1);
        }

        private void GetFloatMap()
        {
            floatmap = new float[scale, scale];
            int max = 0;
            foreach (var e in map)
            {
                if (max < e)
                {
                    max = e;
                }
            }
            for (int index00 = 0; index00 < scale; index00++)
            {
                for (int index01 = 0; index01 < scale; index01++)
                {
                    floatmap[index00, index01] = Normalize(map[Coords(index00, index01)], max);
                }
            }
        }

        public void Power()
        {
            for (int t0 = 0; t0 < scale; t0++)
                for (int t1 = 0; t1 < scale; t1++)
                {
                    map[Coords(t0, t1)] = map[Coords(t0, t1)] * map[Coords(t0, t1)];
                }
        }

        private static float Normalize(int n, int max)
        {
            return Math.Abs((float)n / max);
        }

        private void GenerateMap()
        {
            map = new int[scale * scale];
            map[Coords(0, 0)] = rand.Next(1, 1000);
            map[Coords(0, scale - 1)] = rand.Next(1, 1000);
            map[Coords(scale - 1, 0)] = rand.Next(1, 1000);
            map[Coords(scale - 1, scale - 1)] = rand.Next(1, 1000);
        }

        private void Diamond(int stepsize)
        {
            for (int i = stepsize / 2; i < scale; i += stepsize)
            {
                for (int j = stepsize / 2; j < scale; j += stepsize)
                {
                    map[Coords(i, j)] = (map[Coords(i - (stepsize / 2), j - (stepsize / 2))]
                                         + map[Coords(i + (stepsize / 2), j + (stepsize / 2))]
                                         + map[Coords(i + (stepsize / 2), j - (stepsize / 2))]
                                         + map[Coords(i - (stepsize / 2), j + (stepsize / 2))]) / 4
                                        + Getrand(stepsize);

                }
            }
        }

        private void Square(int stepsize)
        {
            bool flag = false;
            for (int i = 0; i < scale; i += stepsize / 2)
            {
                for (int j = flag ? 0 : stepsize / 2; j < scale; j += stepsize)
                {
                    map[Coords(i, j)] = (CheckCell(i, j + (stepsize / 2)) +
                                         CheckCell(i, j - (stepsize / 2)) +
                                         CheckCell(i + stepsize / 2, j) +
                                         CheckCell(i - stepsize / 2, j)) /
                                        4 + Getrand(stepsize);
                }
                flag = !flag;
            }
        }

        private int CheckCell(int i, int j)
        {
            if (i >= 0 && i < scale && j >= 0 && j < scale)
            {
                return map[Coords(i, j)];
            }
            return Nil;
        }

        private void MainAlgoritm()
        {
            int stepsize = scale - 1;
            for (int octave = 1; octave <= octaves; octave++)
            {
                Diamond(stepsize);
                Square(stepsize);
                stepsize /= 2;
            }
        }

        private int Getrand(int stepsize)
        {
            return rand.Next(-roughness * stepsize, roughness * stepsize);
        }

    }

    
    
    public class CellGenerator
    {
        
    }
}