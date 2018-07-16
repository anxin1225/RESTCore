using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    /// <summary>
    /// 资源注册器
    /// </summary>
    public interface IRESTSourceRegistrar
    {
        void Register(Route<BaseRESTSource> source);
    }
}
