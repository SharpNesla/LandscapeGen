using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Assets.SimpleGenerator
{
    public class CoreImpl : Core<CellImpl>
    {
        public CoreImpl(Func<Pair, CellImpl> cellInitializer, params IModifier<CellImpl>[] modifiers) : base(cellInitializer, modifiers)
        {

        }


        public override CellImpl[,] GetRect(Pair size, Pair coordinate)
        {
            return base.GetRect(size, coordinate);
        }
    }
}