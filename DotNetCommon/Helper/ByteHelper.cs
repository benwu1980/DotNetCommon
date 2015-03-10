using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper
{
    public class ByteUtility
    {
        /// <summary>
        /// 比较两个byte数组是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ByteArrayCompare(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            bool flag = true;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    flag = false; break;
                }
            }
            return flag;
        }


        /// <summary>
        /// 获取取第index是否为1
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetBit(byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }

        /// <summary>
        /// 将第index位设为1
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte SetBit(byte b, int index)
        {
            b |= (byte)(1 << index);
            return b;
        }

        /// <summary>
        /// 将第index位设为0
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte ClearBit(byte b, int index)
        {
            b &= (byte)((1 << 8) - 1 - (1 << index));
            return b;
        }
        /// <summary>
        /// 将第index位取反
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte ReverseBit(byte b, int index)
        {
            b ^= (byte)(1 << index);
            return b;
        }

        public static byte[] ToBytes(int value)
        {
            byte[] b = new byte[4];
            b[0] = (byte)(value);
            b[1] = (byte)(value >> 8);
            b[2] = (byte)(value >> 16);
            b[3] = (byte)(value >> 24);
            return b;
        }

        /// <summary>
        /// 将元素为四个的数据转化为一个无符号整数
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static uint ToUint(byte[] b)
        {
           return (uint)(b[0] | b[1] << 8 | b[2] << 16 | b[3] << 24);
        }
    }
}
