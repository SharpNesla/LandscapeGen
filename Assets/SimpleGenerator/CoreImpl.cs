using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Assets.SimpleGenerator
{
    public class CoreImpl : Core<CellImpl>
    {
        public readonly int Resolution;
        public CoreImpl(Func<Pair, CellImpl> cellInitializer, int resolution, params IModifier<CellImpl>[] modifiers) : base(cellInitializer, modifiers)
        {
            Resolution = resolution;
        }

        public CellImpl[,] GetChunk(Pair position)
        {
            CellImpl[,] i = GetRect(new Pair(Resolution + 2, Resolution + 2), position + new Pair(-1,-1));
            return i.Foreach((pair, impl) => impl.LocalCache = i);
        }
    }
}