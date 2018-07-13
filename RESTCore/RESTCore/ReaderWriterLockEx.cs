using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading
{
    public static class ReaderWriterLockEx
    {
        /// <summary>
        /// 在读锁中运行
        /// </summary>
        /// <param name="action"></param>
        public static void RunOnReaderLock(this ReaderWriterLock _SceneSetLock, Action action)
        {
            _SceneSetLock.AcquireReaderLock(Timeout.Infinite);
            //Console.WriteLine("AcquireReaderLock");
            try { action(); } catch { }
            //Console.WriteLine("ReleaseReaderLock");
            _SceneSetLock.ReleaseReaderLock();
        }

        /// <summary>
        /// 在读锁中运行
        /// </summary>
        /// <param name="action"></param>
        public static T RunOnReaderLock<T>(this ReaderWriterLock _SceneSetLock, Func<T> action)
        {
            T obj = default(T);

            _SceneSetLock.AcquireReaderLock(Timeout.Infinite);
            //Console.WriteLine("AcquireReaderLock");
            try { obj = action(); } catch { }
            //Console.WriteLine("ReleaseReaderLock");
            _SceneSetLock.ReleaseReaderLock();

            return obj;
        }

        /// <summary>
        /// 在写锁中运行
        /// </summary>
        /// <param name="action"></param>
        public static void RunOnWriterLock(this ReaderWriterLock _SceneSetLock, Action action)
        {
            _SceneSetLock.AcquireWriterLock(Timeout.Infinite);
            //Console.WriteLine("AcquireReaderLock");
            try { action(); } catch { }
            //Console.WriteLine("ReleaseReaderLock");
            _SceneSetLock.ReleaseWriterLock();
        }

        /// <summary>
        /// 在写锁中运行
        /// </summary>
        /// <param name="action"></param>
        public static T RunOnWriterLock<T>(this ReaderWriterLock _SceneSetLock, Func<T> action)
        {
            T obj = default(T);

            _SceneSetLock.AcquireWriterLock(Timeout.Infinite);
            //Console.WriteLine("AcquireReaderLock");
            try { obj = action(); } catch { }
            //Console.WriteLine("ReleaseReaderLock");
            _SceneSetLock.ReleaseWriterLock();

            return obj;
        }
    }
}
