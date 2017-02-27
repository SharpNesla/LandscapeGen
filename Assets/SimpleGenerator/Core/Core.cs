using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace Assets.SimpleGenerator
{
    public class Core<T> where T : Cell
    {
        private readonly Func<Pair,T> _cellInitializer;
        private readonly IModifier<T>[] _modifiers;

        public Core(Func<Pair,T> cellInitializer ,params IModifier<T>[] modifiers)
        {
            _cellInitializer = cellInitializer;
            _modifiers = modifiers;
        }

        public T GetCell(Pair coordinates)
        {
            var i = _cellInitializer(coordinates);
            for (var index = 0; index < _modifiers.Length; index++)
            {
                _modifiers[index].Callback(i);
            }
            return i;
        }

        public T[,] GetRect(Pair size, Pair coordinate)
        {
            var i = new T[size.X, size.Y];
            return i.Foreach(size,coord => i[coord.X, coord.Y] =
                GetCell(coord + new Pair(coordinate.Y, coordinate.X)));
        }
    }
}