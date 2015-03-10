using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DotNetCommon.Web
{
    /// <summary>
    /// 请求url的帮助类
    /// </summary>
    public class HttpHelper
    {
        #region 事件
        public delegate void RequestTimeOutHandler(string url, RequestInfo request, Exception ex);
        /// <summary>
        /// 请求超时事件
        /// </summary>
        public event RequestTimeOutHandler RequestTimeOut;

        public delegate void RequestFinishHandler(ResponseInfo response);
        /// <summary>
        ///  请求完成触发事件
        /// </summary>
        public event RequestFinishHandler RequestFinish;
        #endregion


        private CookieContainer _cookies = new CookieContainer();
        public CookieContainer Cookies
        {
            get { return _cookies; }
            set { _cookies = value; }
        }

        #region 设置发送头信息
        /// <summary>
        /// 设置发送头信息
        /// </summary>
        private void SetRequestHeader(HttpWebRequest request)
        {
            request.Timeout = 10 * 1000;
            request.Accept = "*/*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.104 Safari/537.36";
            request.Headers["Accept-Encoding"] = "gzip,deflate";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8,en;q=0.6";
            request.Headers["Accept-Charset"] = "utf-8;q=0.7,*;q=0.7";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;
        }
        #endregion


        #region 同步请求url

        public ResponseInfo HttpRequest(string url)
        {
            return HttpRequest(url, new RequestInfo(), new Uri(url).Host, Encoding.UTF8);
        }

        public ResponseInfo HttpRequest(string url, Encoding coding)
        {
            return HttpRequest(url, new RequestInfo(), new Uri(url).Host, coding);
        }

        public ResponseInfo HttpRequest(string url, RequestInfo request)
        {
            return HttpRequest(url, request, null, Encoding.UTF8);
        }

        public ResponseInfo HttpRequest(string url, RequestInfo request, string referer)
        {
            return HttpRequest(url, request, referer, Encoding.UTF8);
        }

        /// <summary>
        /// 获取HTML页面内容
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="bAllowAutoRedirect">是否自动跳转</param>
        /// <param name="request">查询信息</param>
        /// <param name="referer">引用页</param>
        /// <param name="coding">编码格式</param>
        /// <returns></returns>
        public ResponseInfo HttpRequest(string url, RequestInfo request, string referer, Encoding coding)
        {
            ResponseInfo result = new ResponseInfo();

            if (request.Method == Method.POST)
            {
                webRequest.Method = "POST";
                using (Stream strem = webRequest.GetRequestStream())
                {
                    byte[] bs = request.ToBytes();
                    strem.Write(bs, 0, bs.Length);
                    strem.Close();
                }
            }
            else
            {
                string query = request.ToString();
                if (query.Length > 0)
                {
                    url = string.Format("{0}?{1}", url, query);
                }
            }

            webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Referer = referer.Length == 0 ? new Uri(url).Host : referer;

            #region SSL方式
            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                X509Certificate objx509 = new X509Certificate();//创建证书文件
                webRequest.ClientCertificates.Add(objx509);
            }
            #endregion

            SetRequestHeader(webRequest);  // 设置发送头信息

            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = 60 * 1000;
            webRequest.CookieContainer = this.Cookies;

            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
                Stream responseStream = httpResponse.GetResponseStream();

                result.HtmlText = System.Web.HttpUtility.HtmlDecode(UnZipStream(httpResponse.ContentEncoding, responseStream, coding));
                result.RemoteDateTime = Convert.ToDateTime(httpResponse.GetResponseHeader("Date"));
                result.Url = httpResponse.ResponseUri.OriginalString;

                this.Cookies.Add(httpResponse.Cookies);

                result.Cookies = this.Cookies;
                result.StatusCode = httpResponse.StatusCode;

                responseStream.Dispose();

                if (RequestFinish != null)
                {
                    RequestFinish(result);
                }
            }
            catch (Exception ex)
            {
                if (RequestTimeOut != null)
                {
                    RequestTimeOut(url, request, ex);
                }
            }
            return result;
        }

        #endregion

        #region 异步获取远程HTML
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl)
        {
            BeginGetHTML(sUrl, false, new RequestInfo() { });
        }
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="send">是否自动重定向URL</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl, bool bAllowAutoRedirect)
        {
            BeginGetHTML(sUrl, bAllowAutoRedirect, new RequestInfo() { });
        }
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl, RequestInfo request)
        {
            BeginGetHTML(sUrl, false, request);
        }
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="bAllowAutoRedirect">是否自动跳转</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl, bool bAllowAutoRedirect, RequestInfo request)
        {
            ResponseInfo result = new ResponseInfo();

            if (request.Method == Method.GET)
            {
                string sData = request.ToString();
                if (sData.Length > 0) sUrl = string.Format("{0}?{1}", sUrl, sData);
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(sUrl);
            // 设置发送头信息
            SetRequestHeader(httpRequest);
            // 自动重定向
            httpRequest.AllowAutoRedirect = bAllowAutoRedirect;

            if (request.Method == Method.POST)
            {
                // 设置发送方式
                httpRequest.Method = request.Method.ToString();

                // 获取发送数据流
                Stream stream = httpRequest.GetRequestStream();

                byte[] bs = request.ToBytes();
                stream.Write(bs, 0, bs.Length);
                stream.Close();
            }
            AsyncCallback callback = new AsyncCallback(GetResponseStreamCallback);
            try
            {
                httpRequest.BeginGetResponse(callback, httpRequest);
            }
            catch (WebException ex)
            {
                if (RequestTimeOut != null)
                    RequestTimeOut.Invoke(sUrl, request, ex);
            }
        }

        private void GetResponseStreamCallback(IAsyncResult ar)
        {
            HttpWebRequest httpRequest = ar.AsyncState as HttpWebRequest;
            HttpWebResponse httpResponse = httpRequest.EndGetResponse(ar) as HttpWebResponse;

            Stream responseStream = httpResponse.GetResponseStream();

            ResponseInfo response = new ResponseInfo();

            response.HtmlText = UnGzipStream(httpResponse.ContentEncoding, responseStream);
            response.RemoteDateTime = Convert.ToDateTime(httpResponse.GetResponseHeader("Date"));
            if (RequestFinish != null)
            {
                RequestFinish(response);
            }
        }
        #endregion

        public HttpWebRequest webRequest { get; set; }

        #region 从响应流中获取文本内容
        private string UnGzipStream(string ecoding, Stream resonseStream)
        {
            return UnZipStream(ecoding, resonseStream, Encoding.UTF8);
        }

        private string UnZipStream(string ecoding, Stream responseStream, Encoding coding)
        {
            Stream stream = null;
            ecoding = ecoding.ToUpperInvariant();
            if (ecoding.Contains("GZIP"))
            {
                stream = new GZipStream(responseStream, CompressionMode.Decompress);
            }
            else if (ecoding.Contains("DEFLATE"))
            {
                stream = new DeflateStream(responseStream, CompressionMode.Decompress);
            }

            string result = string.Empty;
            using (var sr = new StreamReader(stream, coding))
            {
                result = sr.ReadToEnd();
            }
            stream.Dispose();

            return result;
        }

        #endregion

    }

    #region 返回内容
    /// <summary>
    /// 返回请求的内容
    /// </summary>
    public class ResponseInfo
    {
        /// <summary>
        /// 当前访问的URL地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public string HtmlText { get; set; }

        /// <summary>
        /// 返回的HTTP状态代码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        private DateTime _RemoteDateTime = Convert.ToDateTime("1900-01-01");
        /// <summary>
        /// 远程服务器时间
        /// </summary>
        public DateTime RemoteDateTime
        {
            get { return _RemoteDateTime; }
            set { _RemoteDateTime = value; }
        }

        private CookieContainer _Cookies = new CookieContainer();
        /// <summary>
        /// 返回的Cookies
        /// </summary>
        public CookieContainer Cookies
        {
            get { return _Cookies; }
            set { _Cookies = value; }
        }
    }
    #endregion

    #region 请求的数据信息
    /// <summary>
    /// 请求的数据信息
    /// </summary>
    [Serializable]
    public class RequestInfo
    {
        private Dictionary<string, string> _parameters = new Dictionary<string, string>();
        /// <summary>
        /// 请求参数字典
        /// </summary>
        public Dictionary<string, string> Parameters
        {
            set { this._parameters = value; }
            get { return this._parameters; }
        }


        private Method _sendMethod = Method.GET;
        /// <summary>
        /// 请求数据方式
        /// </summary>
        public Method Method
        {
            get { return _sendMethod; }
            set { _sendMethod = value; }
        }

        private CookieContainer _cookies = new CookieContainer();
        /// <summary>
        /// 请求数据是要带上的cookie
        /// </summary>
        public CookieContainer Cookies
        {
            get { return this._cookies; }
            set { this._cookies = value; }
        }

        /// <summary>
        /// 清除参数
        /// </summary>
        public void Clear()
        {
            this._parameters.Clear();
        }

        /// <summary>
        /// 增加一个键值
        /// </summary>
        public void Add(string key, string value)
        {
            _parameters.Add(key, value);
        }

        /// <summary>
        /// 返回参数的请求格式：如：a=1&b=2
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("&", _parameters.Select(c => string.Format("{0}={1}", c.Key, c.Value)).ToArray());
        }

        public byte[] ToBytes()
        {
            return Encoding.ASCII.GetBytes(this.ToString());
        }

        public string GetValue(string key)
        {
            if (_parameters.ContainsKey(key))
            {
                return _parameters[key];
            }
            return string.Empty;
        }

        /// <summary>
        /// 更新某个键值
        /// </summary>
        public void SetValue(string key, string value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key] = value;
            }
        }
    }
    #endregion

    public enum Method
    {
        GET, POST
    }
}
