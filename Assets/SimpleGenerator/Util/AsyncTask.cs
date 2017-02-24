using System;
using System.Threading;

namespace SimpleGenerator.Util
{
    public enum TaskState
    {
        Prepared,
        Handling,
        Ready
    }

    public class AsyncTask
    {
        public readonly Action AsyncAction;
        public readonly Action SyncAction;
        public Thread Executor;

        public TaskState State;

        public AsyncTask(Action asyncAction, Action syncAction)
        {
            State = TaskState.Prepared;
            AsyncAction = () =>
            {
                State = TaskState.Handling;
                asyncAction();
                State = TaskState.Ready;
            };
            SyncAction = () =>
            {
                syncAction();
                State = TaskState.Prepared;
            };
        }



        public void Invoke(ThreadPool pool)
        {
            pool.QueueTask(this);
        }
    }
}