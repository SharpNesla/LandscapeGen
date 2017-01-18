using System;

namespace Assets.SimpleGenerator
{
    public class ChunkedCore : Core<CellImpl>
    {
        public ChunkedCore(Func<Pair, CellImpl> cellInitializer, params IModifier<CellImpl>[] modifiers) : base(cellInitializer, modifiers)
        {
        }
    }
}