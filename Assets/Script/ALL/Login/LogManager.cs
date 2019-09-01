using UnityEngine;
using System.Threading;

namespace MonterChessClient
{
    public static class LogManager
    {
        public static void Log(string format)
        {
            Debug.Log(string.Format("[{0}] {1}", Thread.CurrentThread.ManagedThreadId, format));
        }
    }
}