using System;
using System.Threading;

namespace Assets.SimpleGenerator
{
    public class AsyncTask
    {
        public volatile bool IsReady;
        public Action Parallel,SyncTask;
        private AsyncTask(Action parallelTask, Action SyncTask)
        {
            this.Parallel = parallelTask;
            this.SyncTask = SyncTask;

        }

        public void Invoke()
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                Parallel();
                IsReady = true;
            });
        }

        public static void Queue(Action parallelTask, Action SyncTask)
        {
            var i = new AsyncTask(parallelTask,SyncTask);
            Dispatcher.Queue(i);
        }
    }
}