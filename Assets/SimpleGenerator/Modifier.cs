using System;

namespace Assets.SimpleGenerator
{
    public interface IModifier<T> where T : Cell
    {
        void Initialize();
        void Callback(Core<T> core, T current);
        void Finalize();
    }

    public class LambdaModifier<T> : IModifier<T> where T : Cell
    {
        private readonly Action<Core<T>, T> _callbackLambda;

        public LambdaModifier(Action<Core<T>, T> callbackLambda)
        {
            _callbackLambda = callbackLambda;
        }

        public void Initialize(){ }

        public void Finalize(){ }
        public void Callback(Core<T> core, T current)
        {
            _callbackLambda(core, current);
        }
    }
}