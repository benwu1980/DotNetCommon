using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Extension;
using System.Diagnostics;

namespace DotNetCommon
{
    public static class Guard
    {
        /// <summary>
        /// 非空
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotEmpty(Guid argument, string argumentName)
        {
            if (argument == Guid.Empty)
            {
                throw new ArgumentException("\"{0}\" 不能为空 guid。".FormatWith(argumentName), argumentName);
            }
        }

        /// <summary>
        /// 非空
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty((argument ?? string.Empty).Trim()))
            {
                throw new ArgumentException("\"{0}\" 不能为空。".FormatWith(argumentName), argumentName);
            }
        }

        /// <summary>
        /// 未超过长度
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="length"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotOutOfLength(string argument, int length, string argumentName)
        {
            if (argument.Trim().Length > length)
            {
                throw new ArgumentException("\"{0}\" 不能多于 {1} 个字符。".FormatWith(argumentName, length), argumentName);
            }
        }

        /// <summary>
        /// 检测是否为非空引用
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// 非负数
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegative(int argument, string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 数字应该大于0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(int argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 数字应该大于或者等于0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegative(long argument, string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 数字应该大于0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(long argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 数字应该大于等于0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegative(float argument, string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 数字应该大于0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(float argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 数字应该大于等于0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegative(decimal argument, string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 数字应该大于0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(decimal argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }


        /// <summary>
        /// 不是过去时间
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotInPast(DateTime argument, string argumentName)
        {
            if (argument < DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 不是未来时间
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotInFuture(DateTime argument, string argumentName)
        {
            if (argument > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 非负数
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegative(TimeSpan argument, string argumentName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 非负数或0
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(TimeSpan argument, string argumentName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// 非空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotEmpty<T>(ICollection<T> argument, string argumentName)
        {
            IsNotNull(argument, argumentName);

            if (argument.Count == 0)
            {
                throw new ArgumentException("集合不能为空。", argumentName);
            }
        }

        /// <summary>
        /// 数值没有超出范围
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotOutOfRange(int argument, int min, int max, string argumentName)
        {
            if ((argument < min) || (argument > max))
            {
                throw new ArgumentOutOfRangeException(argumentName, "{0} 必须在 \"{1}\"-\"{2}\" 之间。".FormatWith(argumentName, min, max));
            }
        }

        /// <summary>
        /// 正确的电子邮件地址
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotInvalidEmail(string argument, string argumentName)
        {
            IsNotEmpty(argument, argumentName);

            if (!argument.IsEmail())
            {
                throw new ArgumentException("\"{0}\" 不是正确的电子邮件地址。".FormatWith(argumentName), argumentName);
            }
        }

        /// <summary>
        /// 正确的网址
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        [DebuggerStepThrough]
        public static void IsNotInvalidWebUrl(string argument, string argumentName)
        {
            IsNotEmpty(argument, argumentName);

            if (!argument.IsWebUrl())
            {
                throw new ArgumentException("\"{0}\" 不是正确的网址。".FormatWith(argumentName), argumentName);
            }
        }

         [DebuggerStepThrough]
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(string.Format("\"{0}\"不能为空", argumentName));
            }
        }
    }
}
