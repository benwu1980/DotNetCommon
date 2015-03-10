using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using DotNetCommon.Data.Helper;
using System.IO.Compression;

namespace DotNetCommon.Data.Extension
{

    /// <summary>
    /// 数据表的扩展方法
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 将DataTable转化为Excel文件
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fileName"></param>
        public static void ExportToExcelFile(this DataTable table, string fileName)
        {
          //  ExcelHelper.DataSetToExcel(table, fileName);
        }

        /// <summary>
        /// 将DataTable转化为实体列表
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table)
        {
            if (table == null)
                return new List<T>();

            List<T> list = new List<T>();
            Func<DataRow, T> readRow = ClassHelper.DynamicDataRow<T>();

            foreach (DataRow dr in table.Rows)
            {
                list.Add(readRow(dr));
            }
            return list;
        }

        /// <summary>
        /// 将DataTable转换成压缩的字节数组
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns> byte[]</returns>
        public static byte[] ToCompressedByte(this DataTable dataTable)
        {
            if (dataTable == null)
                return null;

            using (var ms = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(ms, CompressionMode.Compress))
                {
                    dataTable.WriteXml(gZipStream);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 将指定的字节数组转化为DataTable
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>DataTable</returns>
        public static DataTable FromCompressedByte(byte[] bytes)
        {
            DataTable dataTable = new DataTable();

            using (var ms = new MemoryStream(bytes))
            {
                using (var gzs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    dataTable.ReadXml(gzs);
                }
            }

            return dataTable;
        }


    }
}
