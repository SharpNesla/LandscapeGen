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

        public override CellImpl[,] GetRect(Pair size, Pair coordinate)
        {
            var i = new CellImpl[size.X, size.Y];
            return i.Foreach(size,coord => i[coord.X, coord.Y] =
                CellInitializer(coord + new Pair(coordinate.Y, coordinate.X)));
        }

        public CellImpl[,] GetChunk(Pair position)
        {
            var i = GetRect(new Pair(Resolution + 2, Resolution + 2), position + new Pair(-1,-1));
            i.Foreach((pair, impl) =>
            {
                impl.LocalCache = i;
                impl.LocalPosition = pair + new Pair(-1, -1);
            });
            for (var j = 0; j < Modifiers.Length; j++)
            {
                var modifier = Modifiers[j];
                i.Foreach((pair, impl) => { modifier.Callback(impl); });
            }
            return i;
        }
    }
}