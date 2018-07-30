using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTCore
{
    /// <summary>
    /// 基础资源
    /// </summary>
    public class BaseRESTSource
    {
        public virtual List<string> Options()
        {
            var options = new List<string> { "Options" };
            var type = GetType();

            foreach (var method in type.GetMethods())
            {
                if (BaseRESTSourceEx.Method2Name.Values.Contains(method.Name))
                {
                    options.Add(method.Name);
                }
            }

            return options.Distinct().ToList();
        }
    }
}
