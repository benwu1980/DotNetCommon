using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// 序列化功能辅助工具类
    /// </summary>
    public static class SerializeHelper
    {

        #region 二进制与对象之间的转化

        /// <summary>
        /// 获取对象序列化的二进制
        /// </summary>
        /// <param name="source">要序列化二进制的实体对象</param>
        /// <returns>如果对象为Null，则返回Null。</returns>
        public static byte[] GetBytes(object source)
        {
            if (source == null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                BinaryFormatter bFormtter = new BinaryFormatter();
              
                new BinaryFormatter().Serialize(stream, source);
                stream.Position = 0L;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                return buffer;
            }
        }

        /// <summary>
        /// 从已序列化数据中(byte[])获取对象实体
        /// </summary>
        /// <typeparam name="T">要返回的数据类型</typeparam>
        /// <param name="bytes">二进制数据</param>
        /// <returns>对象实体</returns>
        public static T GetObject<T>(byte[] bytes)
        {
            if (bytes == null)
            {
                return default(T);
            }

            using (var stream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion
        
        #region 将文件xml序列化

        /// <summary>
        /// 将一个对象序列化到一个xml文件中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fileName"></param>
        public static void XmlSerializeToFile(object source, string fileName)
        {
            Guard.IsNotNull(source, "source");
            Guard.IsNotEmpty(fileName, "fileName");

            XmlDocument doc = ObjectToXmlDoc(source);
            doc.Save(fileName);
        }


        /// <summary>
        /// 从一个xml文件中反序列化一个对象 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T XmlDeserializeFromFile<T>(string fileName)
        {
            Guard.IsNotEmpty(fileName, "fileName");

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return ObjectFromXmlDoc<T>(doc);
        }

        /// <summary>
        /// 获取对象序列化的XmlDocument实例
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <returns>如果对象为Null，则返回为Null。</returns>
        public static XmlDocument ObjectToXmlDoc(object source)
        {
            if (source == null)
            {
                return null;
            }

            XmlSerializer serializer = new XmlSerializer(source.GetType());
            StringBuilder sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter)writer, source);
                writer.Close();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sb.ToString());
                return doc;
            }
        }


        /// <summary>
        /// 从已序列化数据(XmlDocument)中获取对象实体
        /// </summary>
        /// <typeparam name="T">要返回的数据类型</typeparam>
        /// <param name="xmlDoc">已序列化的文档对象</param>
        /// <returns>对象实体</returns>
        public static T ObjectFromXmlDoc<T>(XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
            {
                return default(T);
            }
            XmlNodeReader xmlReader = new XmlNodeReader(xmlDoc.DocumentElement);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(xmlReader);
        }
        #endregion


        /// <summary>
        /// 序列化对象到文件
        /// </summary>
        /// <param name="source">要序列化的对象</param>
        /// <param name="fileName">保存到的文件路径</param>
        public static void SerializeToFile(object source, string fileName)
        {
            Guard.IsNotNull(source, "source");
            Guard.IsNotEmpty(fileName, "fileName");

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, source);
                stream.Close();
            }
        }

        /// <summary>
        /// 从一个文件中反序列化一个对象
        /// </summary>
        /// <param name="fileName">要反序列化的文件路径</param>
        public static T DeserializeFromFile<T>(string fileName)
        {
            Guard.IsNotEmpty(fileName, "fileName");

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                object result = bFormatter.Deserialize(stream);
                stream.Close();
                return (T)result;
            }
        }


        #region 对象与json字符串之间的转化

        /// <summary>
        /// 将输入的JSON字符串转化为一个对象
        /// </summary>
        /// <typeparam name="T">要转化为的类型</typeparam>
        /// <param name="json">输入的json字符串</param>
        /// <returns>返回转化后的对象</returns>
        public static T JsonToObj<T>(string json) where T : class
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                return (T)jsonSerializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 将一个对象转化为json格式的字符串
        /// </summary>
        /// <param name="obj">要转化的json字符串的对象</param>
        /// <returns>对象转化后的json字符串</returns>
        public static string JsonToObj(object obj)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        } 

        #endregion

       
    }
}
