using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace RESTCore
{
    public class RouteOption<T>
        where T : class
    {
        private List<T> _ItemList = new List<T>();
        private List<string> _RouteFormatKeys = new List<string>();
        private List<RouteItemInfo<T>> _RouteItemInfos = new List<RouteItemInfo<T>>();

        private Func<T, Dictionary<string, string>> _GetRouteValueFunc = null;

        public RouteOption<T> AddRouteFormatKey(string key)
        {
            _RouteFormatKeys.Add(key);

            return this;
        }

        public RouteOption<T> AddRangeRouteFormatKey(IEnumerable<string> keys)
        {
            _RouteFormatKeys.AddRange(keys);

            return this;
        }

        public RouteOption<T> AddRouteItemInfo(RouteItemInfo<T> routeitem)
        {
            _RouteItemInfos.Add(routeitem);

            return this;
        }

        public RouteOption<T> Register(T t)
        {
            _ItemList.Add(t);

            return this;
        }

        public RouteOption<T> Register(Assembly assembly)
        {
            if (assembly == null)
                return this;

            var ttype = typeof(T);

            foreach (var type in assembly.GetTypes())
            {
                if (ttype.IsAssignableFrom(type))
                {
                    try
                    {
                        var item = (T)Activator.CreateInstance(type);
                        _ItemList.Add(item);
                    }
                    catch
                    {

                    }
                }
            }

            return this;
        }

        public Route<T> GetRoute()
        {
            List<RouteItemInfo<T>> item_list = new List<RouteItemInfo<T>>(_RouteItemInfos);

            Regex regex_subkey = new Regex("^\\{(?<RouteKey>[\\S]+?)\\}$");

            if (_GetRouteValueFunc != null)
            {
                foreach (var item in _ItemList)
                {
                    foreach (var format_key in _RouteFormatKeys)
                    {
                        var keys = format_key.Split(new char[] { '/' }, StringSplitOptions.None);
                        if (keys == null || keys.Length == 0)
                        {
                            item_list.Add(new RouteItemInfo<T>
                            {
                                RouteName = $"{format_key}_{Guid.NewGuid().ToString("N")}",
                                RouteRule = format_key,
                                Target = item,
                                DefaultRouteData = null,
                            });

                            continue;
                        }

                        foreach (var key in keys)
                        {
                            if (key.StartsWith("{") && key.EndsWith("}"))
                            {
                                var match = regex_subkey.Match(key);
                                if (match.Success)
                                {

                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // Duplicate removal

            var route = new Route<T>();
            route.Register(item_list);
            return route;
        }
    }
}
