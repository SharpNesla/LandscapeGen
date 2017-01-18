using System;
using System.Collections.Generic;

namespace Assets.SimpleGenerator
{
    public abstract class Pool<T>
    {
        private readonly Queue<T> _objectsQueue;

        protected abstract void Reset(T @object);
        public abstract T ObjectInitializer();

        protected Pool(int startCount = 1)
        {
            _objectsQueue = new Queue<T>();
        }

        public T Get()
        {
            if (_objectsQueue.Count == 0)
            {
                _objectsQueue.Enqueue(ObjectInitializer());
            }
            return _objectsQueue.Dequeue();
        }

        public void Collect(params T[] collectables)
        {
            foreach (var collectable in collectables)
            {
                Reset(collectable);
                _objectsQueue.Enqueue(collectable);
            }
        }
    }

    public class LambdaPool<T> : Pool<T>
    {
        private readonly Func<T> _initializer;
        private readonly Action<T> _resetter;


        public LambdaPool(Func<T> initializer, Action<T> resetter ,int startCount = 1) : base(startCount)
        {
            _initializer = initializer;
            _resetter = resetter;
        }

        protected override void Reset(T @object)
        {
            _resetter(@object);
        }

        public override T ObjectInitializer()
        {
            return _initializer();
        }
    }
}