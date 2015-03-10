

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
    /// ��̬�ļ���CSS\JS�ļ��������
    /// �˴������ɺϲ���ѹ��CSS\JS�ļ�
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
        /// ���ÿͻ��˻������
        /// </summary>
        /// <param name="context">�μ�<see cref="HttpContext"/></param>
        /// <param name="duration">����ʱ�䣬�μ�<see cref="TimeSpan"/></param>
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
        /// ѹ������
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

