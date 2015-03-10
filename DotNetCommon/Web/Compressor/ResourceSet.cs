

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;
using System.ComponentModel;
using System.Web.Hosting;
using System.IO;

namespace DotNetCommon.Web.Compressor
{
    /// <summary>
    /// 资源文件的组合
    /// </summary>
    public sealed class ResourceSet
    {
        /// <summary>
        /// 资源组的名称
        /// </summary>
        public string KeyName { get; private set; }

        /// <summary>
        /// 资源类型 (js 或者 css).
        /// </summary>
        public string ContentType { get; private set; }

        private List<Resource> Resources = new List<Resource>();
        public ResourceSet(XElement xElement)
        {
            KeyName = xElement.Attribute("keyName").Value;
            ContentType = xElement.Attribute("type").Value;
            foreach (var element in xElement.Elements())
            {
                Resources.Add(new Resource() { FileName = element.Attribute("name").Value });
            }
        }

        public HandlerCacheItem GetCacheSet(int durationInDays)
        {
            var key = string.Format("ResourceCache_{0}", KeyName);

            HandlerCacheItem asset = WebCache.Get(key) as HandlerCacheItem;

            if (asset == null)
            {
                if (Resources.Count > 0)
                {
                    var contentBuilder = new StringBuilder();

                    foreach (var resource in Resources)
                    {
                        string fileName = HostingEnvironment.MapPath(resource.FileName);
                        var fileContent = File.ReadAllText(fileName);

                        if (!string.IsNullOrEmpty(fileContent))
                        {
                            contentBuilder.AppendLine(fileContent);
                            contentBuilder.AppendLine();
                        }
                    }

                    var content = contentBuilder.ToString();

                    if (!string.IsNullOrEmpty(content))
                    {
                        asset = new HandlerCacheItem { Content = content };

                        var cacheData = WebCache.Get(key);
                        if (durationInDays > 0 && cacheData == null)
                        {
                            WebCache.Add(key, asset, DateTime.Now.AddDays(durationInDays) - DateTime.Now);
                        }
                    }
                }
            }

            return asset;

        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Resource Set '{0}' (Type: {1})", KeyName, ContentType);
        }
    }

    /// <summary>
    /// 资源信息
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// 资源路径
        /// </summary>
        public string FileName { get; set; }
    }
}
