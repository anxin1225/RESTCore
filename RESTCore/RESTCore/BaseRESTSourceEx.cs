using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RESTCore
{
    public static class BaseRESTSourceEx
    {
        public static MethodInfo FindMethod(this BaseRESTSource source, string method)
        {
            return source.GetType().GetMethod(method);
        }

        public static Dictionary<RESTMethod, string> Method2Name = new Dictionary<RESTMethod, string>
        {
            { RESTMethod.ADD, "Put" },
            { RESTMethod.DELETE, "Delete" },
            { RESTMethod.UPDATA, "Post" },
            { RESTMethod.GET, "Get" },
            { RESTMethod.OPTION, "Options" },
        };

        public static MethodInfo FindMethod(this BaseRESTSource source, RESTMethod method)
        {
            return source.FindMethod(Method2Name.TryGetValue(method, out var value) ? value : null);
        }
    }
}
