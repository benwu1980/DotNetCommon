
using System;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace DotNetCommon.Caching
{
    /// <summary>
    /// 企业库类库缓存: 配置如需要进行配置下
    ///<configSections>
    ///		<section name="cachingConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Caching.Configuration.CacheManagerSettings, Microsoft.Practices.EnterpriseLibrary.Caching" requirePermission="false"/>
    ///</configSections>
    ///
    ///<cachingConfiguration defaultCacheManager="DefaultCacheManager">
    ///		<cacheManagers>
    ///			<add name="DefaultCacheManager" type="Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager, Microsoft.Practices.EnterpriseLibrary.Caching" expirationPollFrequencyInSeconds="60" maximumElementsInCacheBeforeScavenging="1000" numberToRemoveWhenScavenging="10" backingStoreName="NullBackingStore"/>
    ///		</cacheManagers>
    ///		<backingStores>
    ///			<add type="Microsoft.Practices.EnterpriseLibrary.Caching.BackingStoreImplementations.NullBackingStore, Microsoft.Practices.EnterpriseLibrary.Caching" name="NullBackingStore"/>
    ///		</backingStores>
    ///</cachingConfiguration>
    ///
    /// </summary>
    public class EntLibCache : ICache
    {
        private readonly ICacheManager _manager;

        public EntLibCache(ICacheManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 配置文件结点
        /// </summary>
        /// <param name="cacheManagerName"></param>
        public EntLibCache(string cacheManagerName)
            : this(CacheFactory.GetCacheManager(cacheManagerName))
        {

        }

        public int Count
        {
            get { return _manager.Count; }
        }

        public void Clear()
        {
            _manager.Flush();
        }

        public bool Contains(string key)
        {
            return _manager.Contains(key);
        }

        public T Get<T>(string key)
        {
            return (T)_manager.GetData(key);
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = default(T);

            if (_manager.Contains(key))
            {
                var existingValue = _manager.GetData(key);

                if (existingValue != null)
                {
                    value = (T)existingValue;

                    return true;
                }
            }

            return false;
        }

        public void Set<T>(string key, T value)
        {
            RemoveIfExists(key);
            _manager.Add(key, value);
        }

        private void RemoveIfExists(string key)
        {
            if (_manager.Contains(key))
            {
                Remove(key);
            }
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            RemoveIfExists(key);
            _manager.Add(key, value, CacheItemPriority.Normal, null, new AbsoluteTime(absoluteExpiration.ToLocalTime()));
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            RemoveIfExists(key);
            _manager.Add(key, value, CacheItemPriority.Normal, null, new SlidingTime(slidingExpiration));

        }

        public void Remove(string key)
        {
            _manager.Remove(key);
        }
    }
}