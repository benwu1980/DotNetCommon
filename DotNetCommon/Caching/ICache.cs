using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Caching
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 计数器
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 是否包含一个键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>如果包含则返回<code>true</code>否则返回<code>false</code></returns>
        bool Contains(string key);

        /// <summary>
        /// 从缓存中获得一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">对象缓存的键</param>
        /// <returns>如果存在则返回对象实例</returns>
        T Get<T>(string key);

        /// <summary>
        /// 尝试从缓存中获得一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">对象缓存的键</param>
        /// <param name="value">对象实例，如果缓存中不存在对应的键，则返回<code>Detault(T)</code></param>
        /// <returns>如果成功获得对象实例则返回<code>true</code>，否则返回<code>false</code></returns>
        bool TryGet<T>(string key, out T value);

        /// <summary>
        /// 将对象实例存入缓存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">缓存的键</param>
        /// <param name="value">要存入的值</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// 将对象实例存入缓存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">缓存的键</param>
        /// <param name="value">要存入的值</param>
        /// <param name="absoluteExpiration">缓存的绝对过期时间</param>
        void Set<T>(string key, T value, DateTime absoluteExpiration);

        /// <summary>
        /// 将对象实例存入缓存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">缓存的键</param>
        /// <param name="value">要存入的值</param>
        /// <param name="slidingExpiration">滑动期限</param>
        void Set<T>(string key, T value, TimeSpan slidingExpiration);

        /// <summary>
        /// 删除一个缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        void Remove(string key);
    }
}
