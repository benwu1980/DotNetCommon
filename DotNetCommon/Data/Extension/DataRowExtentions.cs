using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DotNetCommon.Helper;

namespace DotNetCommon.Data.Extension
{
    /// <summary>
    /// DataRow扩展方法
    /// </summary>
    public static class DataRowExtensions
    {
        public static byte GetByte(this DataRow dr, string columnName, byte defaultValue = 0)
        {
            return !dr.Table.Columns.Contains(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<byte>(dr[columnName], defaultValue);
        }

        public static decimal GetDecimal(this DataRow dr, string columnName, decimal defaultValue = 0)
        {
            return !dr.Table.Columns.Contains(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<decimal>(dr[columnName], defaultValue);
        }

        public static double GetDouble(this DataRow dr, string columnName, double defaultValue = 0)
        {
            return !dr.Table.Columns.Contains(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<double>(dr[columnName], defaultValue);
        }

        public static float GetFloat(this DataRow dr, string columnName, float defaultValue = 0)
        {
            return !dr.Table.Columns.Contains(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<float>(dr[columnName], defaultValue);
        }

        public static short GetInt16(this DataRow dr, string columnName, short defaultValue = 0)
        {
            return !dr.Table.Columns.Contains(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<short>(dr[columnName], defaultValue);
        }


        public static int GetInt(this DataRow dr, string columnName, int defaultValue = 0)
        {
            return !dr.Table.Columns.Contains(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<int>(dr[columnName], defaultValue);
        }

        public static long GetInt64(this DataRow dr, string columnName, long defaultValue = 0)
        {
            return !dr.Table.Columns.Contains(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<long>(dr[columnName], defaultValue);
        }

        public static DateTime GetDateTime(this DataRow dr, string columnName, DateTime defaultValue = default(DateTime))
        {
            return !dr.Table.Columns.Contains(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<DateTime>(dr[columnName], defaultValue);
        }

        public static DateTime? GetNullDateTime(this DataRow dr, string columnName, DateTime? defaultValue = default(DateTime?))
        {
            return !dr.Table.Columns.Contains(columnName)
                         ? defaultValue
                         : ObjectHelper.ChangeType<DateTime?>(dr[columnName], defaultValue);
        }

        public static string GetString(this DataRow dr, string columnName, string defaultValue = null)
        {
            return !dr.Table.Columns.Contains(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<string>(dr[columnName], defaultValue);
        }

        public static bool GetBool(this DataRow dr, string columnName, bool defaultValue = false)
        {
            return !dr.Table.Columns.Contains(columnName)
                ? defaultValue
                : ObjectHelper.ChangeType<bool>(dr[columnName], defaultValue);
        }

    }
}
