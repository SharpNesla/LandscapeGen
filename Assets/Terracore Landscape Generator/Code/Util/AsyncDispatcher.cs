using System;
using System.Collections.Generic;
using Code.Core;
using SimpleGenerator.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    [CustomEditor(typeof(TerracoreGenerator))]
    public class AsyncDispatcher : UnityEditor.Editor
    {

        private static List<AsyncTask> _a;
        public int ThreadCount;

        private void Start()
        {
            _a = new List<AsyncTask>();
            System.Threading.ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
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
                System.Threading.ThreadPool.QueueUserWorkItem(asyncTask.AsyncAction);
            }
        }
    }
}