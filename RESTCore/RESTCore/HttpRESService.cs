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
    public class HttpRESTService
    {
        public bool IsRuning { get; set; }

        public int Port { get; set; }

        public HandleTunnel HandleTunnel { get; set; }

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

            HttpContext.Request = context.Request;
            HttpContext.Response = context.Response;

            try { OnHttpRequest(); } catch { }
        }

        protected virtual void OnHttpRequest()
        {
            if (HandleTunnel == null)
            {
                using (HttpContext.Response.OutputStream)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    var bytes = Encoding.UTF8.GetBytes($"not find {nameof(HandleTunnel)}");
                    HttpContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
                }
            }
            else if (!HandleTunnel.Handle())
            {
                using (HttpContext.Response.OutputStream)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var bytes = Encoding.UTF8.GetBytes($"HandleTunnel can not hanlde");
                    HttpContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
