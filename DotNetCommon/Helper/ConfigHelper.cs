using System.Configuration;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// 获取配置文件的帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获取配置节点的值（主要是值类型）
        /// </summary>
        /// <typeparam name="T">要返回的类型</typeparam>
        /// <param name="key">节点name</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T Get<T>(string key, T defaultValue = default(T))
        {
            var obj = ConfigurationManager.AppSettings[key];
            if (obj == null)
                return defaultValue;

            return ObjectHelper.ChangeType<T>(obj, defaultValue);
        }

        /// <summary>
        /// 检索当前应用程序默认配置的指定配置节,转化为对象
        /// </summary>
        /// <typeparam name="T">要转化为的对象</typeparam>
        /// <param name="sectionName">配置节点名称</param>
        /// <param name="defaultValue">转化不成功默认的值</param>
        /// <returns></returns>
        public static T GetSection<T>(string sectionName, T defaultValue = default(T))
        {
            var obj = ConfigurationManager.GetSection(sectionName);
            return (obj != null && obj is T) ? (T)obj : defaultValue;
        }
    }
}
