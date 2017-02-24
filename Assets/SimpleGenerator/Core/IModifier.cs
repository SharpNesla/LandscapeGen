using System;

namespace Assets.SimpleGenerator
{
    public interface IModifier<T> where T : Cell
    {
        void Callback<TCore>(TCore core, T current) where TCore : Core<T>;
    }

    public class LambdaModifier<T> : IModifier<T> where T : Cell
    {
            private readonly Action<Core<T>, T> _callbackLambda;

        public LambdaModifier(Action<Core<T>, T> callbackLambda)
        {
            _callbackLambda = callbackLambda;
        }

        public void Callback<TCore>(TCore core, T current) where TCore : Core<T>
        {
            _callbackLambda(core, current);
        }
    }
}