

using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using DotNetCommon.Extension;
using System.IO.Compression;
using System.Net;

namespace DotNetCommon.Web.Compressor
{
    /// <summary>
    /// 静态文件如CSS\JS文件处理程序
    /// 此处理程序可合并、压缩CSS\JS文件
    /// </summary>
    public class AssetHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var assetName = context.Request.QueryString["name"];

            if (!string.IsNullOrEmpty(assetName))
            {
                ResourceSettings setting = new ResourceSettings(context.Server.MapPath("resourceSet.xml"));
                ResourceSet resourceSet = setting[assetName];
                if (resourceSet != null)
                {
                    HandlerCacheItem asset = resourceSet.GetCacheSet(setting.CacheDurationInDays);

                    if (asset != null)
                    {
                        if (setting.GenerateETag)
                        {
                            if (HandleIfNotModified(context, asset.ETag))
                            {
                                return;
                            }
                        }

                        var response = context.Response;
                        response.ContentType = resourceSet.ContentType;

                        if (setting.Compress)
                        {
                            CompressResponse(context);
                        }

                        using (var sw = new StreamWriter(response.OutputStream))
                        {
                            sw.Write(asset.Content);
                        }

                        if (setting.CacheDurationInDays > 0)
                        {
                            if (setting.GenerateETag)
                            {
                                response.Cache.SetETag(asset.ETag);
                            }

                            CacheResponseFor(context, TimeSpan.FromDays(setting.CacheDurationInDays));
                        }
                    }
                }
            }
        }

        public static bool HandleIfNotModified(HttpContext context, string etag)
        {
            bool notModified = false;

            if (!string.IsNullOrEmpty(etag))
            {
                string ifNoneMatch = context.Request.Headers["If-None-Match"];

                if (!string.IsNullOrEmpty(ifNoneMatch) && (string.CompareOrdinal(ifNoneMatch, etag) == 0))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                    notModified = true;
                }
            }

            return notModified;
        }

        /// <summary>
        /// 设置客户端缓存策略
        /// </summary>
        /// <param name="context">参见<see cref="HttpContext"/></param>
        /// <param name="duration">缓存时间，参见<see cref="TimeSpan"/></param>
        public static void CacheResponseFor(HttpContext context, TimeSpan duration)
        {
            var cache = context.Response.Cache;

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetLastModified(context.Timestamp);
            cache.SetExpires(context.Timestamp.Add(duration));
            cache.SetMaxAge(duration);
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }

        /// <summary>
        /// 压缩内容
        /// </summary>
        /// <param name="context"></param>
        public static void CompressResponse(HttpContext context)
        {
            var request = context.Request;

            var acceptEncoding = (request.Headers["Accept-Encoding"] ?? string.Empty).ToUpperInvariant();

            var response = context.Response;

            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}

