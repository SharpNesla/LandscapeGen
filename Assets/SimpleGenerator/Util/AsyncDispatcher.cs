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
        public static ThreadPool Pool;
        public void Start()
        {
            _a = new List<AsyncTask>();
            Pool = new ThreadPool(Environment.ProcessorCount - 1);
        }

        public void Update()
        {
            for (var i = 0; i < _a.Count; i++)
            {
                var task = _a[i];
                if (task.IsReady)
                {
                    task.SyncAction();
                    _a.Remove(task);
                }
            }
        }

        public static void Queue(AsyncTask asyncTask)
        {
            asyncTask.IsReady = false;
            _a.Add(asyncTask);
            asyncTask.Invoke(Pool);
        }
    }
}