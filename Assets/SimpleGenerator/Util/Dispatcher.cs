using System.Collections.Generic;
using UnityEngine;

namespace Assets.SimpleGenerator
{
    public class Dispatcher : MonoBehaviour
    {
        public static List<AsyncTask> A;

        public void Start()
        {
            A = new List<AsyncTask>();
        }

        public void Update()
        {
            for (var i = 0; i < A.Count; i++)
            {
                var task = A[i];
                if (task.IsReady)
                {
                    task.SyncTask();
                    A.Remove(task);
                }
            }
        }

        public static void Queue(AsyncTask asyncTask)
        {
            A.Add(asyncTask);
            asyncTask.Invoke();
        }
    }
}