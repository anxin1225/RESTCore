using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RESTCore
{
    /// <summary>
    /// Http data parser
    /// </summary>
    public interface IHttpDataParser
    {
        bool CanHandle(string contenttype);

        T GetFromQueryString<T>();

        T GetFromForm<T>();

        object[] GetParameters(MethodInfo method);
    }
}
