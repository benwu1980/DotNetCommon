using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Extension;
using System.Web;

namespace DotNetCommon.Web
{
    /// <summary>
    /// 网址组合的方法
    /// </summary>
    public class QueryBuilder
    {
        public Uri Uri { get; private set; }

        private string target = string.Empty;

        public QueryBuilder(Uri uri)
        {
            Uri = uri;

            UriBuilder ub = new UriBuilder(uri);

            var query = ub.Uri.Query;
            if (!string.IsNullOrEmpty(query))
            {
                var queryArray = query.Substring(1).ToStringArray("&");
                foreach (var item in queryArray)
                {
                    if (item.Contains("="))
                    {
                        var s = item.ToStringArray("=");
                        AddQuery(s[0], s[1]);
                    }

                }
            }
        }

        public QueryBuilder(string uriString)
            : this(new Uri(uriString))
        { }


        private string QueryString
        {
            get { return querys.Select(c => c.Key + "=" + c.Value).ToArray().ToLinkString("&"); }
        }

        /// <summary>
        /// 完整的地址
        /// </summary>
        public string URL
        {
            get
            {
                if (Uri != null)
                {
                    string path = Uri.AbsoluteUri.IndexOf("?") > -1
                        ? Uri.AbsoluteUri.Substring(0, Uri.AbsoluteUri.IndexOf("?"))
                        : Uri.AbsoluteUri;
                    return querys.Count > 0 ? (path + "?" + QueryString) : path;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 相对地址
        /// </summary>
        public string PathAndQuery
        {
            get
            {
                if (Uri != null)
                {
                    string path = Uri.AbsolutePath.IndexOf("?") > -1
                        ? Uri.AbsolutePath.Substring(0, Uri.AbsolutePath.IndexOf("?"))
                        : Uri.AbsolutePath;
                    return querys.Count > 0 ? (path + "?" + QueryString) : path;
                }
                return string.Empty;
            }
        }

        private Dictionary<string, string> querys = new Dictionary<string, string>();

        public void AddQuery(string key, string value)
        {
            if (querys.Keys.Contains(key.ToLower()))
            {
                return;
            }

            querys.Add(key.ToLower(), value);
        }

        public void RemoveQuery(string key)
        {
            if (querys.Keys.Contains(key.ToLower()))
            {
                querys.Remove(key);
            }
        }
    }
}
