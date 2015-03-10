using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DotNetCommon.Helper
{
    public class ClassHelper
    {
        public static readonly BindingFlags InstanceFlag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

        /// <summary>
        /// 创建一个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(params object[] args)
        {
            return (T)CreateInstance(typeof(T), args);
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            var ts = GetArrayType(args);
            var ci = type.GetConstructor(InstanceFlag, null, ts, null);
            return ci.Invoke(args);
        }

        public static Type[] GetArrayType(params object[] os)
        {
            var ts = new Type[os.Length];
            for (int i = 0; i < os.Length; i++)
            {
                ts[i] = os[i].GetType();
            }
            return ts;
        }
    }
}
