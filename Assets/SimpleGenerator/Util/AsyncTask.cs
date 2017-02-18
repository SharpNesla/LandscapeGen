using System;
using System.Threading;

namespace SimpleGenerator.Util
{
    public class AsyncTask
    {
        public readonly Action AsyncAction;
        public readonly Action SyncAction;
        public Thread Executor;

        public bool IsReady;
        public AsyncTask(Action asyncAction, Action syncAction)
        {
            IsReady = true;
            AsyncAction = () =>
            {
                asyncAction();
                IsReady = true;
            };
            SyncAction = syncAction;
        }

        public void StopTaskExecuting()
        {
            if (!IsReady)
            {
                Executor.Abort();
            }
        }

        public void Invoke(ThreadPool pool)
        {
            pool.QueueTask(this);
        }
    }
}