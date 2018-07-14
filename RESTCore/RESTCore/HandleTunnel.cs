using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RESTCore
{
    /// <summary>
    /// 处理隧道
    /// </summary>
    public class HandleTunnel
    {
        public HandleTunnel ChildTunnel { get; set; }

        /// <summary>
        /// 处理数据
        /// </summary>
        public virtual bool Handle()
        {
            return ChildTunnel?.Handle() ?? false;
        }
    }

    /// <summary>
    /// Http协议的隧道
    /// </summary>
    public class HttpHandleTunnel : HandleTunnel
    {
        /// <summary>
        /// 请求数据
        /// </summary>
        public HttpListenerRequest Request
        {
            get { return ThreadCurrentData.GetInstance().Get<HttpListenerRequest>(); }
        }

        /// <summary>
        /// 响应
        /// </summary>
        public HttpListenerResponse Response
        {
            get { return ThreadCurrentData.GetInstance().Get<HttpListenerResponse>(); }
        }
    }

    /// <summary>
    /// 处理通道 错误
    /// </summary>
    public class ErrorHandleTunnel : HttpHandleTunnel
    {
        public override bool Handle()
        {
            try
            {
                return ChildTunnel.Handle();
            }
            catch (Exception ex)
            {
                try
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var list = GetError(ex);

                    using (StreamWriter sw = new StreamWriter(Response.OutputStream, Encoding.UTF8))
                    {
                        foreach (var item in list)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        private List<string> GetError(Exception ex)
        {
            List<string> list = new List<string>();
            GetError(ex, list);
            return list;
        }

        private void GetError(Exception ex, List<string> errors)
        {
            errors.Insert(0, ex.StackTrace);
            errors.Insert(0, ex.Message);

            if (ex.InnerException != null)
                GetError(ex.InnerException, errors);
        }
    }

    /// <summary>
    /// 404找不到的隧道
    /// </summary>
    public class Notfound404HandleTunnel : HttpHandleTunnel
    {
        public override bool Handle()
        {
            if (Response == null)
                return false;

            Response.StatusCode = (int)HttpStatusCode.NotFound;

            using (StreamWriter sw = new StreamWriter(Response.OutputStream, Encoding.UTF8))
            {
                sw.WriteLine("404 not found");
            }

            return true;
        }
    }

    /// <summary>
    /// 路由规则
    /// </summary>
    public class APIHandleTunnel : HttpHandleTunnel
    {
        private Dictionary<string, string> HttpMethod2Method = new Dictionary<string, string>
        {
            { "OPTIONS", "Options" },
            { "GET", "Get" },
            //{ "HEAD", "Head" },
            { "POST", "Post" },
            { "PUT", "Put" },
            { "DELETE", "Delete" },
        };

        public Route<BaseRESTSource> Route { get; set; }

        public override bool Handle()
        {
            var source = Route.Matching(GetRouteKey(), out var routedata);

            if (source == null)
                throw new Exception($"not find Source for {GetRouteKey()}");

            // find parser
            var parser = HttpDataParserHelper.GetInstance().FindParser(Request.ContentType);
            if (parser == null)
                throw new Exception($"not find HttpDataParser for {Request.ContentType}");

            ThreadCurrentData.GetInstance().Set(parser);

            // find the method by HttpMethod
            if (!HttpMethod2Method.ContainsKey(Request.HttpMethod?.ToUpper()))
                throw new Exception($"not find Method to Mapping {Request.HttpMethod}");

            var method_name = HttpMethod2Method[Request.HttpMethod?.ToUpper()];
            var method = source.FindMethod(method_name);
            if (method == null)
                throw new Exception($"not find Method({method_name}) on {source.GetType().Name} ");

            object[] parameters = parser.GetParameters(method);
            var back_data = method.Invoke(source, parameters);

            // write back data to client
            if (method.ReturnType == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                Response.OutputStream.Close();
            }
            else
            {
                IHttpDataGenerator generator = null;

                if (Request.AcceptTypes == null || Request.AcceptTypes.Length == 0)
                {
                    generator = HttpDataGeneratorHelper.GetInstance().DefaultGenerator();
                }
                else
                {
                    foreach (var accept_type in Request.AcceptTypes)
                    {
                        generator = HttpDataGeneratorHelper.GetInstance().FindGenerator(accept_type);
                        if (generator != null)
                            break;
                    }
                }

                if (generator == null)
                    throw new Exception($"not find DataGenerator for {string.Join(";", Request.AcceptTypes ?? new string[0])}");

                var back_string = generator.Generator(back_data);

                using (Response.OutputStream)
                {
                    var bytes = Encoding.UTF8.GetBytes(back_string);
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                }
            }

            return true;
        }

        /// <summary>
        /// 获取路由Key
        /// </summary>
        /// <returns></returns>
        private string GetRouteKey()
        {
            return Request?.RawUrl;
        }
    }
}
