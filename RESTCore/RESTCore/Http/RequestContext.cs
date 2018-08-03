using System;
using System.Net;

namespace RESTCore
{
    public class HttpContext
    {
        public static HttpListenerRequest Request
        {
            get { return ThreadCurrentData.GetInstance().Get<HttpListenerRequest>(); }
            set { ThreadCurrentData.GetInstance().Set(value); }
        }

        public static HttpListenerResponse Response
        {
            get { return ThreadCurrentData.GetInstance().Get<HttpListenerResponse>(); }
            set { ThreadCurrentData.GetInstance().Set(value); }
        }
    }
}
