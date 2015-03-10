using System;
using System.Reflection;

namespace DotNetCommon.Common
{

    /// <summary>
    /// 生成一个类的单一实例
    /// </summary>
    /// <typeparam name="T">实例类型</typeparam>
    public static class Singleton<T> where T : class
    {
        static volatile T _instance;
        static object _lock = new object();

        static Singleton() { }

        /// <summary>
        /// 获取一个单一类的实体
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            ConstructorInfo constructor = null;
                            try
                            {
                                constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                            }
                            catch (Exception exception)
                            {
                                throw new InvalidOperationException(exception.Message, exception);
                            }

                            if (constructor == null || constructor.IsAssembly)
                            {
                                throw new InvalidOperationException(string.Format("创建类 '{0}'出错。", typeof(T).Name));
                            }

                            _instance = (T)constructor.Invoke(null);
                        }
                    }
                }

                return _instance;
            }
        }

        /*
        //这种方式也可以
         
        private static readonly Lazy<T> current = new Lazy<T>(() => Activator.CreateInstance<T>(), true);

        public static T Instance
        {
            get { return current.Value; }
        }
        
         * 
         */

    }
}
