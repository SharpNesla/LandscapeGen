using System.Collections.Generic;
using Test;
using UnityEngine;
using ThreadPool = Test.ThreadPool;

namespace Assets.SimpleGenerator
{
    public class AsyncDispatcher : MonoBehaviour
    {
        public static List<AsyncTask> A;
        public static ThreadPool Pool;
        public void Start()
        {
            A = new List<AsyncTask>();
            Pool = new ThreadPool(4);
        }

        public void Update()
        {
            for (var i = 0; i < A.Count; i++)
            {
                var task = A[i];
                if (task.IsReady)
                {
                    task.SyncAction();
                    A.Remove(task);
                }
            }
        }

        public static void Queue(AsyncTask asyncTask)
        {
            A.Add(asyncTask);

            asyncTask.Invoke(Pool);
            Debug.Log(A.Count);
        }
    }
}