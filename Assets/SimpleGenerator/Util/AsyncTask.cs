using System;
using System.Threading;

namespace Test
{
    public class AsyncTask
    {
        public readonly Action AsyncAction;
        public readonly Action SyncAction;
        public Thread Executor;

        public bool IsReady;
        public AsyncTask(Action asyncAction, Action syncAction)
        {
            IsReady = false;
            AsyncAction = () =>
            {
                asyncAction();
                IsReady = true;
            };
            SyncAction = syncAction;
        }

        public void StopTaskExecuting()
        {
            Executor.Abort();
        }

        public void Invoke(ThreadPool pool)
        {
            pool.QueueTask(this);
        }
    }
}