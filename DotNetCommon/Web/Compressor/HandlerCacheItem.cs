using System;
using DotNetCommon.Helper;

namespace DotNetCommon.Web.Compressor
{
    public class HandlerCacheItem
    {
        private string _etag;

        public string Content
        {
            get;
            set;
        }

        public string ETag
        {
            get
            {
                if (string.IsNullOrEmpty(_etag) && !string.IsNullOrEmpty(Content))
                {
                    _etag = CryptHelper.Hash(Content);
                }

                return _etag;
            }
        }
    }
}