using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DotNetCommon.Helper
{
    public class NetHelper
    {
        /// <summary>
        /// 获取本机的计算机名
        /// </summary>
        public static string LocalHostName
        {
            get { return Dns.GetHostName(); }
        }

        /// <summary>
        /// 获取本机的局域网IP LocalIp
        /// </summary>
        public static string LocalIp
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表为空，则返回空字符串
                if (addressList.Length < 1)
                {
                    return "";
                }

                //返回本机的局域网IP
                return addressList[0].ToString();
            }
        }

        public static string WANIP
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表小于2，则返回空字符串
                if (addressList.Length < 2)
                {
                    return "";
                }

                //返回本机的广域网IP
                return addressList[1].ToString();
            }
        }
    }
}
