using System;

namespace Assets.SimpleGenerator
{
    public interface IModifier<T> where T : Cell
    {
        void Callback(T current);
        void Start();
    }
}