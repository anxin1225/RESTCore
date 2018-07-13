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
    }
}
