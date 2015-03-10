using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DotNetCommon.Helper;
using System.Globalization;

namespace DotNetCommon.Data.Extension
{
    /// <summary>
    /// DataReader的扩展方法
    /// </summary>
    public static class DataRecordExtensions
    {
        /// <summary>
        /// 是否包含所给的列
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            if (dr == null || string.IsNullOrWhiteSpace(columnName))
                return false;

            for (var i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static byte GetByte(this IDataRecord dr, string columnName, byte defaultValue = 0)
        {
            return !dr.HasColumn(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<byte>(dr[columnName], defaultValue);
        }

        public static decimal GetDecimal(this IDataRecord dr, string columnName, decimal defaultValue = 0)
        {
            return !dr.HasColumn(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<decimal>(dr[columnName], defaultValue);
        }

        public static double GetDouble(this IDataRecord dr, string columnName, double defaultValue = 0)
        {
            return !dr.HasColumn(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<double>(dr[columnName], defaultValue);
        }

        public static float GetFloat(this IDataRecord dr, string columnName, float defaultValue = 0)
        {
            return !dr.HasColumn(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<float>(dr[columnName], defaultValue);
        }

        public static short GetInt16(this IDataRecord dr, string columnName, short defaultValue = 0)
        {
            return !dr.HasColumn(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<short>(dr[columnName], defaultValue);
        }


        public static int GetInt(this IDataRecord dr, string columnName, int defaultValue = 0)
        {
            return !dr.HasColumn(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<int>(dr[columnName], defaultValue);
        }

        public static long GetInt64(this IDataRecord dr, string columnName, long defaultValue = 0)
        {
            return !dr.HasColumn(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<long>(dr[columnName], defaultValue);
        }

        public static DateTime GetDateTime(this IDataRecord dr, string columnName, DateTime defaultValue = default(DateTime))
        {
            return !dr.HasColumn(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<DateTime>(dr[columnName], defaultValue);
        }

        public static DateTime? GetNullDateTime(this IDataRecord dr, string columnName, DateTime? defaultValue = default(DateTime?))
        {
            return !dr.HasColumn(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<DateTime?>(dr[columnName], defaultValue);
        }

        public static string GetString(this IDataRecord dr, string columnName, string defaultValue = null)
        {
            return !dr.HasColumn(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<string>(dr[columnName], defaultValue);
        }

        public static bool GetBool(this IDataRecord dr, string columnName, bool defaultValue = false)
        {
            return !dr.HasColumn(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<bool>(dr[columnName], defaultValue);
        }
    }
}
