using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DotNetCommon.Helper;

namespace DotNetCommon.Web.Compressor
{
    public class ResourceSettings
    {
        public string Version { get; set; }
        public bool GenerateETag { get; set; }
        public int CacheDurationInDays { get; set; }
        public bool Compress { get; set; }

        public List<ResourceSet> ResourceSets = new List<ResourceSet>();

        public ResourceSettings(string configName)
        {
            XDocument xDoc = XDocument.Load(configName);
            var element = xDoc.Root.Elements().First();

            Version = ObjectHelper.ChangeType<string>(element.Attribute("version").Value);
            GenerateETag = ObjectHelper.ChangeType<bool>(element.Attribute("generateETag").Value);
            CacheDurationInDays = ObjectHelper.ChangeType<int>(element.Attribute("cacheDurationInDays").Value, 1);
            Compress = ObjectHelper.ChangeType<bool>(element.Attribute("compress").Value);

            foreach (var item in element.Elements())
            {
                ResourceSets.Add(new ResourceSet(item));
            }
        }

        public ResourceSet this[string keyName]
        {
            get
            {
                return ResourceSets.FirstOrDefault(resource => resource.KeyName == keyName);
            }
        }
    }
}
