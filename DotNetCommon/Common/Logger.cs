using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;

namespace DotNetCommon.Common
{
    /// <summary>
    /// 日志方法 需要引用log4net.dll及文件log4net.config配置文件，以及配置节点名称log
    /// </summary>
    public class Logger
    {
        private static readonly ILog _log = LogManager.GetLogger("log"); //配置文件 <logger name='log'>

        static Logger()
        {
            string configFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config";
            var fi = new FileInfo(configFilePath);
            XmlConfigurator.Configure(fi);
            XmlConfigurator.ConfigureAndWatch(fi);
        }

        public static void Debug(string message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }

        public static void Info(string message)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(message);
            }
        }

        public static void Warn(string message)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(message);
            }
        }

        public static void Error(string message)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(message);
            }
        }

        public static void Fatal(string message)
        {
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(message);
            }
        }
    }
}
