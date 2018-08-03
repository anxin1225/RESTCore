using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RESTCore
{
    public class ThreadCurrentData : DataBox
    {
        private static ThreadLocal<ThreadCurrentData> _ThreadCurrentData = new ThreadLocal<ThreadCurrentData>(() => new ThreadCurrentData());

        private ThreadCurrentData() { }

        public static ThreadCurrentData GetInstance()
        {
            return _ThreadCurrentData.Value;
        }
    }
}
