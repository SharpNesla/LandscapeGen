using System;
using System.Collections.Generic;
using SimpleGenerator.Util;
using UnityEngine;
using ThreadPool = SimpleGenerator.Util.ThreadPool;

namespace Assets.SimpleGenerator
{
    public class AsyncDispatcher : MonoBehaviour
    {
        private static List<AsyncTask> _a;
        private static ThreadPool _pool;
        private void Start()
        {
            _a = new List<AsyncTask>();
            _pool = new ThreadPool(Environment.ProcessorCount - 1);
        }

        private void Update()
        {
            for (var i = 0; i < _a.Count; i++)
            {
                var task = _a[i];
                if (task.State == TaskState.Ready)
                {
                    task.SyncAction();
                    _a.Remove(task);
                }
            }
        }

        public static void Abort(AsyncTask asyncTask)
        {
            if (asyncTask != null && asyncTask.State == TaskState.Handling)
            {
                asyncTask.Executor.Abort();
                asyncTask.State = TaskState.Prepared;
                _a.Remove(asyncTask);
            }

        }

        public static void Queue(AsyncTask asyncTask)
        {
            if (asyncTask != null && asyncTask.State == TaskState.Prepared)
            {
                _a.Add(asyncTask);
                asyncTask.Invoke(_pool);
            }
        }
    }
}