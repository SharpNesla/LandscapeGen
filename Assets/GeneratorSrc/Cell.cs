using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets;
using UnityEngine;

namespace StandaloneGenerator
{
    public class Cell
    {
        public float Height;
        public BiomeMap e;
        public Biomes Biome;
        public const int NilHeight = 0;
        public float Top, Bot, Left, Right;
        //public float Rdiag, Ldiag;
        public void Init(Cell[,] terrainCells, int x, int y)
        {
            Top     = GetCell(x, y + 1, terrainCells);
            Bot     = GetCell(x, y - 1, terrainCells);
            Left    = GetCell(x - 1, y, terrainCells);
            Right   = GetCell(x + 1, y, terrainCells);
            //Rdiag = GetCell(x + 1, y + 1, terrainCells);
            //Ldiag = GetCell(x - 1, y - 1, terrainCells);
        }
        public  float MaxNeighboursHeight()
        {
            return Mathf.Max(Top, Mathf.Max(Bot, Mathf.Max(Right, Mathf.Max(Left))));
        }

        public float MinNeighboursHeight()
        {
            return Mathf.Min(Top, Mathf.Min(Bot, Mathf.Min(Right, Mathf.Min(Left))));
        }

        private static float GetCell(int x, int y, Cell[,] terrainCells)
        {
            int scale = terrainCells.GetLength(0);
            if (x >= 0 && x < scale && y >= 0 && y < scale)
            {
                return terrainCells[y, x].Height;
            }
            return NilHeight;
        }


    }
}
