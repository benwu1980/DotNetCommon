using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// 加密工具，密文可解密
    /// </summary>
    public class CryptHelper
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public const string KEY_64 = "p*@~\\|/?>t<'\",^$nR]D|\\{qK";

        /// <summary>
        /// 带有密钥的加密,用Decrypt来解密
        /// </summary>
        /// <param name="data">要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string data)
        {
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(data);
                provider.Key = ASCIIEncoding.ASCII.GetBytes(KEY_64.Substring(0, 8));
                provider.IV = ASCIIEncoding.ASCII.GetBytes(KEY_64);

                MemoryStream ms = new MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        
        /// <summary>
        /// 带有密钥的解密 用Encrypt来解密
        /// </summary>
        /// <param name="data">要解密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string data)
        {
            byte[] inputByteArray = Convert.FromBase64String(data);
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {

                provider.Key = ASCIIEncoding.ASCII.GetBytes(KEY_64.Substring(0, 8));
                provider.IV = ASCIIEncoding.ASCII.GetBytes(KEY_64);

                MemoryStream ms = new MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, provider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <summary>
        /// 原始base64编码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] Base64Encode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            ToBase64Transform tb64 = new ToBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 3 < source.Length)
            {
                buff = tb64.TransformFinalBlock(source, pos, 3);
                stm.Write(buff, 0, buff.Length);
                pos += 3;
            }

            buff = tb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);

            return stm.ToArray();

        }

        /// <summary>
        /// 原始base64解码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] Base64Decode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            FromBase64Transform fb64 = new FromBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 4 < source.Length)
            {
                buff = fb64.TransformFinalBlock(source, pos, 4);
                stm.Write(buff, 0, buff.Length);
                pos += 4;
            }

            buff = fb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);
            return stm.ToArray();

        }

        /// <summary>
        /// 比较两个byte数组是否相同
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool CompareByteArrays(byte[] source, byte[] dest)
        {
            if ((source == null) || (dest == null))
                throw new ArgumentException("source or dest is not valid");

            bool ret = true;

            if (source.Length != dest.Length)
                return false;
            else
                if (source.Length == 0)
                    return true;

            for (int i = 0; i < source.Length; i++)
                if (source[i] != dest[i])
                {
                    ret = false;
                    break;
                }
            return ret;
        }

        /// <summary>
        /// 密码加密码算法Hash
        /// </summary>
        /// <param name="source">密码明文</param>
        /// <returns>加密后的密码</returns>
        public static string Hash(string source)
        {
            for (int i = 0; i < 3; i++)
            {
                source = MD5Hash(source);
            }
            return source;
        }

        private static string MD5Hash(string source)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.Default.GetBytes(source));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }
    }
}
