using System;
using System.Collections.Generic;

namespace Assets.SimpleGenerator
{
    public class CellImpl : Cell
    {
        public List<IBiome<CellImpl>> Biomes;
        public Pair LocalPosition;
        public CoreImpl Core;
        public CellImpl[,] LocalCache;
        public CellImpl(Pair coordinates, float height = 0) : base(coordinates, height)
        {
            Biomes = new List<IBiome<CellImpl>>();
        }
    }
}