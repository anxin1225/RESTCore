using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace RESTCore
{
    /// <summary>
    /// REST style service
    /// </summary>
    public class RESTHttpService
    {
        public bool IsRuning { get; set; }

        public int Port { get; set; }

        private HttpListener _HttpListener = null;

        public void Start()
        {
            _HttpListener.Prefixes.Add($"http://+:{Port}/");

            _HttpListener.Start();

            new Thread(() =>
            {
                while (IsRuning)
                {
                    var result = _HttpListener.BeginGetContext(new AsyncCallback(HttpAsyncCallback), _HttpListener);
                    result.AsyncWaitHandle.WaitOne();
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        public void Stop()
        {
            IsRuning = false;
            _HttpListener?.Close();
        }

        protected void HttpAsyncCallback(IAsyncResult result)
        {
            if (!IsRuning)
                return;

            if (!result.IsCompleted)
                return;

            var listener = (HttpListener)result.AsyncState;
            var context = listener.EndGetContext(result);

            ThreadCurrentData.GetInstance().Set(context.Request);
            ThreadCurrentData.GetInstance().Set(context.Response);


        }
    }
}
