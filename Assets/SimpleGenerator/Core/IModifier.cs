using System;

namespace Assets.SimpleGenerator
{
    public interface IModifier<T> where T : Cell
    {
        void Init();
        void Callback<TCore>(TCore core, T current) where TCore : Core<T>;
        void Out();
    }

    public class LambdaModifier<T> : IModifier<T> where T : Cell
    {
            private readonly Action<Core<T>, T> _callbackLambda;

        public LambdaModifier(Action<Core<T>, T> callbackLambda)
        {
            _callbackLambda = callbackLambda;
        }

        public void Init(){ }

        public void Out(){ }
        public void Callback<TCore>(TCore core, T current) where TCore : Core<T>
        {
            _callbackLambda(core, current);
        }
    }
}