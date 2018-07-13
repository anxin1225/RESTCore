using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    /// <summary>
    /// 数据盒子
    /// </summary>
    public class DataBox
    {
        private Dictionary<string, object> _Mapping = new Dictionary<string, object>();

        public const string DEFAULT = "Default";

        /// <summary>
        /// get value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return Get<T>(DEFAULT);
        }

        /// <summary>
        /// get value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return Get<T>(default(T), key);
        }

        /// <summary>
        /// get value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="def"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(T def, string key)
        {
            var fkey = FormatKey(typeof(T), key);
            if (_Mapping.TryGetValue(fkey, out object obj) && obj is T)
                return (T)obj;

            return def;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(Type type, string key)
        {
            var fkey = FormatKey(type, key);
            if (_Mapping.TryGetValue(fkey, out object obj) && type.IsAssignableFrom(obj?.GetType()))
                return obj;

            return (type?.IsValueType ?? false) ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// set value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Set<T>(T obj)
        {
            Set<T>(obj, DEFAULT);
        }

        /// <summary>
        /// set value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        public void Set<T>(T obj, string key)
        {
            Set(typeof(T), obj, key);
        }

        /// <summary>
        /// set value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        public void Set(Type type, object obj, string key)
        {
            var fkey = FormatKey(type, key);

            if (obj != null)
                _Mapping[fkey] = obj;
            else
                _Mapping.Remove(fkey);
        }

        /// <summary>
        /// get format key
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string FormatKey(Type type, string key)
        {
            return $"{type.FullName}::{key}";
        }
    }
}
