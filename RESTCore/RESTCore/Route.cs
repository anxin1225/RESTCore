using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    /// <summary>
    /// 路由项信息
    /// </summary>
    public class RouteItemInfo
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
        /// 默认路由信息
        /// </summary>
        public dynamic DefaultRouteData { get; set; }

        /// <summary>
        /// 尝试匹配这条路由
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Matching(string key)
        {
            if (RuleItems.Count == 0)
                return false;

            var key_items = key?.Split(new char[] { '/' }, StringSplitOptions.None);
            if (key_items.Length != RuleItems.Count)
                return false;

            for (int i = 0; i < key_items.Length; i++)
            {
                var item = RuleItems[i];
                
                if (item.IndexOfAny(new char[] { '{', '}' }) < 0)
                {

                }
                else
                {

                }
            }

            //foreach (var item in RuleItems)
            //{
            //    if (item.IndexOfAny(new char[] { '{', '}' }) < 0)
            //    {
            //        if (item.Equals())
            //        {

            //        }
            //    }
            //}
        }
    }

    /// <summary>
    /// 路由目标信息
    /// </summary>
    public class RouteTargetInfo<T>
    {
        protected Dictionary<string, string> FormatDic = new Dictionary<string, string>();

        public RouteTargetInfo() { }

        public RouteTargetInfo(Dictionary<string, string> dic)
        {
            if (dic != null)
                FormatDic = dic;
        }

        /// <summary>
        /// 获取路由格式化信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string GetRouteFormatValue(string key)
        {
            return FormatDic.TryGetValue(key, out string value) ? value : null;
        }

        /// <summary>
        /// 目标信息
        /// </summary>
        public T Target { get; set; }
    }

    /// <summary>
    /// 路由导向
    /// </summary>
    public class Route<T>
        where T : class
    {
        /// <summary>
        /// 路由对象信息
        /// </summary>
        private List<RouteItemInfo> _RouteItemInfo = new List<RouteItemInfo>();

        /// <summary>
        /// 路由目标信息
        /// </summary>
        private List<RouteTargetInfo<T>> _RouteTargetInfo = new List<RouteTargetInfo<T>>();

        /// <summary>
        /// 注册路由信息
        /// </summary>
        /// <param name="routeinfo"></param>
        public void Register(RouteItemInfo routeinfo)
        {
            _RouteItemInfo.Add(routeinfo);
        }

        /// <summary>
        /// 尝试匹配内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Matching(string key)
        {
            foreach (var item in _RouteItemInfo)
            {

            }

            return null;
        }
    }
}
