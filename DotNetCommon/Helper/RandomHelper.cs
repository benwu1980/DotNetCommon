using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper
{
    public class RandomHelper
    {
        #region Constants

        /// <summary>
        /// 小写字母
        /// </summary>
        public const string LowerChars = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 小写字母和数字
        /// </summary>
        public const string LowerNumericChars = "abcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// 数字列表
        /// </summary>
        public const string NumericChars = "0123456789";

        /// <summary>
        /// 大写字母列表
        /// </summary>
        public const string UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        ///大写字母和小写字母列表
        /// </summary>
        public const string UpperLowerChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        ///     The upper lower numeric chars
        /// </summary>
        public const string UpperLowerNumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// 大写字母和数字列表
        /// </summary>
        public const string UpperNumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        #endregion Constants

        private static readonly Random rand = new Random();

        /// <summary>
        /// /生成一个指定长度的随机字串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="inputChars">可选择的字串列表</param>
        /// <returns>System.String</returns>
        public static string RandomString(int length, string inputChars = UpperLowerNumericChars)
        {
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = inputChars[rand.Next(inputChars.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// 随机生成一个整数，最大不能超过 maxValue
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int Next(int maxValue)
        {
            lock (rand)
            {
                return rand.Next(maxValue);
            }
        }

        public static int Next(int minValue, int maxValue)
        {
            lock (rand)
            {
                return rand.Next(minValue, maxValue);
            }
        }

        public static void NextBytes(byte[] buffer)
        {
            lock (rand)
            {
                rand.NextBytes(buffer);
            }
        }

        public static double NextDouble()
        {
            lock (rand)
            {
                return rand.NextDouble();
            }
        }
    }
}
