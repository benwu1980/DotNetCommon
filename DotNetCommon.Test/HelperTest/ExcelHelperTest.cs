using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DotNetCommon.Data.Helper;

namespace DotNetCommon.Test.HelperTest
{
    class ExcelHelperTest
    {
        public void ToExcel()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("StockNo",typeof(string)));
            dt.Columns.Add(new DataColumn("StockName", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));


            DataRow dr = dt.NewRow();
            dr["StockNo"] = "0000000000001";
            dr["StockName"] = "成科A";
            dr["Price"] = "10.20";
            dr["CreateDate"] = "2012-10-10";
            dt.Rows.Add(dr);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

           // ExcelHelper.DataSetToExcel(ds, @"d:\aa.xls");
           // ExcelHelper.DataTableToExcel(dt, @"d:\aa0.xls", ExcelHelper.ExcelType.Excel2003);
        }
    }
}
