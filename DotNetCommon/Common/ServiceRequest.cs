using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DotNetCommon.Common
{
    /// <summary>
    /// 此方法自博客园，
    /// wcf服务访问通用类，如：
    /// ServiceRequester<ICalculateService>.Request("endPointName", delegate(ICalculateService proxy) 
   ///  { 
   ///    proxy.Add(1, 4,1); 
   ///  }); 
    /// </summary>
    /// <typeparam name="T">服务接口类型</typeparam>
    public class ServiceRequest<T>
    {
        private static IDictionary<string, ChannelFactory<T>> _channelPool = new Dictionary<string, ChannelFactory<T>>();

        private static object ayncLock = new object();
        static private ChannelFactory<T> GetChannelFactory(string endPointName)
        {
            lock (ayncLock)
            {
                string cacheName = endPointName;
                if (string.IsNullOrEmpty(endPointName))
                {
                    cacheName = typeof(T).Name;
                }
                ChannelFactory<T> _factory = null;
                _channelPool.TryGetValue(cacheName, out _factory);
                if (_factory == null)
                {
                    _factory = new ChannelFactory<T>(endPointName);
                    _channelPool.Add(endPointName, _factory);
                }
                return _factory;
            }
        }
        static public void Request(string endPointName, Action<T> action)
        {
            IClientChannel proxy = null;
            try
            {
                lock (ayncLock)
                {
                    proxy = GetChannelFactory(endPointName).CreateChannel() as IClientChannel;
                    proxy.Open();
                    action((T)proxy);
                    proxy.Close();
                }
            }
            catch (CommunicationException communicationException)
            {
                if (proxy != null)
                {
                    proxy.Abort();
                }
                throw communicationException;
            }
            catch (TimeoutException timeoutException)
            {
                if (proxy != null)
                {
                    proxy.Abort();
                }
                throw timeoutException;
            }
            catch (Exception ex)
            {
                if (proxy != null)
                {
                    proxy.Abort();
                }
                throw ex;
            }
        }
    }
}
