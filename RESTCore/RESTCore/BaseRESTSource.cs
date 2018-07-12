using System;
using System.Collections.Generic;

namespace RESTCore
{
    /// <summary>
    /// 基础资源
    /// </summary>
    public class BaseRESTSource { }

    /// <summary>
    /// 基础资源
    /// </summary>
    /// <typeparam name="ViewModel"></typeparam>
    public class BaseRESTSource<ViewModel>
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public ViewModel Get() { return default(ViewModel); }
    }

    /// <summary>
    /// 基础资源
    /// </summary>
    public class BaseRESTSource<ViewModel, EditModel> : BaseRESTSource<ViewModel>
    {
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Put(EditModel model) { return false; }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Post(EditModel model) { return false; }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public bool Delete() { return false; }
    }
}
