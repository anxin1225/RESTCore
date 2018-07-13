using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    /// <summary>
    /// 路由项信息
    /// </summary>
    public class RouteItemInfo<T>
    {
        private List<string> RuleItems = new List<string>();

        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName { get; set; }

        private string _RouteRule;

        /// <summary>
        /// 路由规则
        /// </summary>
        public string RouteRule
        {
            get { return _RouteRule; }
            set
            {
                _RouteRule = value;

                RuleItems.Clear();
                RuleItems.AddRange(value?.Split(new char[] { '/' }, StringSplitOptions.None));
            }
        }

        /// <summary>
        /// Route Target
        /// </summary>
        public T Target { get; set; }

        /// <summary>
        /// 默认路由信息
        /// </summary>
        public dynamic DefaultRouteData { get; set; }

        /// <summary>
        /// try to maching route
        /// </summary>
        /// <param name="key"></param>
        /// <param name="routedata"></param>
        /// <returns></returns>
        public bool Matching(string key, out Dictionary<string, object> routedata)
        {
            routedata = null;

            if (RuleItems.Count == 0)
                return false;

            var key_items = key?.Split(new char[] { '/' }, StringSplitOptions.None);
            if (key_items.Length != RuleItems.Count)
                return false;

            Dictionary<string, object> temp = new Dictionary<string, object>();

            for (int i = 0; i < key_items.Length; i++)
            {
                var rule_item = RuleItems[i];
                var key_item = key_items[i];

                if (rule_item.IndexOfAny(new char[] { '{', '}' }) < 0)
                {
                    if (string.Compare(rule_item, key_item, false) != 0)
                        return false;
                }
                else if (rule_item.StartsWith('{') && rule_item.EndsWith('}'))
                {
                    temp[rule_item.Substring(1, rule_item.Length - 2)] = key_item;
                    continue;
                }
                else
                {
                    throw new NotSupportedException(rule_item);
                }
            }

            routedata = temp;
            return true;
        }
    }

    public class Route<T>
        where T : class
    {
        private bool _InitFinsh = false;

        private List<RouteItemInfo<T>> _RouteItemInfo = new List<RouteItemInfo<T>>();

        public void Register(RouteItemInfo<T> routeinfo)
        {
            if (_InitFinsh)
                throw new Exception("init is finshed can not add route item");

            _RouteItemInfo.Add(routeinfo);
        }

        public T Matching(string key, out Dictionary<string, object> routedata)
        {
            routedata = null;

            if (!_InitFinsh)
                _InitFinsh = true;

            RouteItemInfo<T> _RouteItem = null;
            foreach (var item in _RouteItemInfo)
            {
                if (item.Matching(key, out var rdata))
                {
                    _RouteItem = item;
                    routedata = rdata;
                    break;
                }
            }

            return _RouteItem?.Target;
        }
    }
}
