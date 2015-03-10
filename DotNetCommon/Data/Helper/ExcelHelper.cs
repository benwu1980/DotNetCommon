using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.Data.OleDb;
using ExcelApp = Microsoft.Office.Interop.Excel;
using DotNetCommon.Helper;


namespace DotNetCommon.Data.Helper
{
    /// <summary>
    /// excel操作帮助类
    /// 备注：  "HDR=yes;"表示Excel文件的第一行是列名而不是数据;"HDR=No;"就表示第一行也是数据。
    ///         "IMEX=1 "如果列中的数据类型不一致，使用"IMEX=1"可必免数据类型冲突。 
    /// 此帮助类中的设置为 HDR=yes,IMEX=1
    /// </summary>
    public class ExcelHelper
    {
        public const string Excel2003ConnStringFormat = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        public const string Excel2007ConnStringFormat = "Provider=Microsoft.Ace.OleDb.12.0;Data Source='{0}';Extended Properties='Excel 12.0;HDR=YES;IMEX=1'";

        public enum ExcelType
        {
            Excel2003,
            Excel2007
        }

        /// <summary>
        ///将DataTable导出到一个EXECL文件
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        private static string DataTableToExcel(System.Data.DataTable dataTable, string excelPath, ExcelType excelType = ExcelType.Excel2003)
        {
            Guard.IsNotNull(dataTable, "数据源DataTable不能为空");

            string connString = excelType == ExcelType.Excel2003 ? Excel2003ConnStringFormat : Excel2007ConnStringFormat;
            connString = string.Format(connString, excelPath);


            if (dataTable.Rows.Count == 0)
            {
                return "没有数据";
            }

            int rows = dataTable.Rows.Count;
            int cols = dataTable.Columns.Count;

            StringBuilder sb = new StringBuilder();
            //生成创建表的脚本
            sb.Append("CREATE TABLE ");
            sb.Append(dataTable.TableName + " ( ");

            for (int i = 0; i < cols; i++)
            {
                if (i < cols - 1)
                    sb.Append(string.Format("{0} nvarchar,", dataTable.Columns[i].ColumnName));
                else
                    sb.Append(string.Format("{0} nvarchar)", dataTable.Columns[i].ColumnName));
            }

            using (OleDbConnection objConn = new OleDbConnection(connString))
            {
                OleDbCommand objCmd = new OleDbCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = sb.ToString();

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return "在Excel中创建表失败，错误信息：" + e.Message;
                }

                #region 生成插入数据脚本
                sb.Remove(0, sb.Length);
                sb.Append("INSERT INTO ");
                sb.Append(dataTable.TableName + " ( ");

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append(dataTable.Columns[i].ColumnName + ",");
                    else
                        sb.Append(dataTable.Columns[i].ColumnName + ") values (");
                }

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append("@" + dataTable.Columns[i].ColumnName + ",");
                    else
                        sb.Append("@" + dataTable.Columns[i].ColumnName + ")");
                }
                #endregion


                //建立插入动作的Command
                objCmd.CommandText = sb.ToString();
                OleDbParameterCollection param = objCmd.Parameters;

                for (int i = 0; i < cols; i++)
                {
                    param.Add(new OleDbParameter("@" + dataTable.Columns[i].ColumnName, OleDbType.VarChar));
                }

                //遍历DataTable将数据插入新建的Excel文件中
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        param[i].Value = row[i];
                    }

                    objCmd.ExecuteNonQuery();
                }

                return "数据已成功导入Excel";
            }//end using
        }


        /// <summary> 
        /// 将excel文档导出到datatable(OleDb方法)
        /// </summary> 
        /// <param name="excelPath">文件名称</param> 
        /// <returns>返回一个数据集</returns> 
        public DataSet ExcelToDataSet(string excelPath, ExcelType excelType = ExcelType.Excel2003)
        {
            string connString = excelType == ExcelType.Excel2003 ? Excel2003ConnStringFormat : Excel2007ConnStringFormat;
            try
            {
                System.IO.File.Delete(excelPath);
            }
            catch (Exception)
            {
                throw new Exception("该文件已经存在，删除文件时出错！");
            }

            DataSet excelDS = new DataSet();
            using (OleDbConnection excelConnection = new OleDbConnection(connString))
            {
                excelConnection.Open();
                DataTable dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string[] excelSheet = new String[dt.Rows.Count];
                int sheet = 0;
                foreach (DataRow row in dt.Rows)
                {
                    excelSheet[sheet] = row["Table_Name"].ToString();
                    sheet++;
                }

                for (int i = 0; i < excelSheet.Length; i++)
                {
                    using (OleDbCommand command = new OleDbCommand("Select  * FROM [" + excelSheet[i] + "]", excelConnection))
                    {
                        using (OleDbDataAdapter excelAdapter = new OleDbDataAdapter())
                        {
                            DataTable sheetDT = new DataTable();
                            excelAdapter.SelectCommand = command;
                            excelAdapter.Fill(sheetDT);
                            excelDS.Tables.Add(sheetDT);
                        }
                    }
                }
                excelConnection.Close();
            }
            return excelDS;
        }

        /// <summary>
        ///  用Microsoft.Office.Interop.Excel将dataset导出到Excel
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="excelFileName"></param>
        public static void DataSetToExcel(DataSet ds, string excelFileName)
        {
            if (ds == null) return;

            KillProcess("Excel");

            ExcelApp.Application xlApp = new ExcelApp.Application();
            object missing = Missing.Value;

            if (xlApp == null)
            {
                throw new Exception("无法创建Excel对象，检查是否安装Excel。");
            }

            ExcelApp.Workbooks workbooks = xlApp.Workbooks;
            ExcelApp.Workbook workbook = workbooks.Add(ExcelApp.XlWBATemplate.xlWBATWorksheet);
            ExcelApp.Sheets sheets = workbook.Worksheets;
            ExcelApp.Worksheet worksheet = null;
            ExcelApp.Range range;

            try
            {
                xlApp.DisplayAlerts = false;

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable table = ds.Tables[i];
                    worksheet = (ExcelApp.Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                    worksheet.Name = ds.Tables[i].TableName.Trim();

                    int rowIndex = 1;
                    int colIndex = 0;

                    //导入字段
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        worksheet.Cells[1, j + 1] = table.Columns[j].ColumnName.Trim();
                        colIndex++;
                    }
                    string maxHeaderCell = NumberToChar(colIndex) + "1"; //最长的列号

                    //导入sheet数据
                    for (int r = 0; r < table.Rows.Count; r++)
                    {
                        for (int t = 0; t < table.Columns.Count; t++)
                        {
                            if (table.Columns[t].DataType == typeof(string))
                            {
                                //前面加个' ，要不然如果前面是0，则显示不出 ,也可以先判断前面是否为0，
                                worksheet.Cells[r + 2, t + 1] = "'" + table.Rows[r][table.Columns[t].ColumnName.Trim()];
                            }
                            else
                            {
                                worksheet.Cells[r + 2, t + 1] = table.Rows[r][table.Columns[t].ColumnName.Trim()];
                            }
                        }
                        rowIndex++;
                    }

                    range = worksheet.get_Range("A1", maxHeaderCell);
                    range.VerticalAlignment = ExcelApp.XlVAlign.xlVAlignCenter;
                    range.EntireColumn.AutoFit(); //所有的列宽度自动
                }
                ((ExcelApp.Worksheet)workbook.Worksheets[ds.Tables.Count + 1]).Delete();

                string ext = Path.GetExtension(excelFileName);
                //保存文件      
                if (ext == ".xls")
                {
                    worksheet.SaveAs(excelFileName, 56, missing, missing, missing, missing, missing, missing, missing);
                }
                else
                {
                    worksheet.SaveAs(excelFileName, missing, missing, missing, missing, missing, missing, missing, missing);
                }

                workbook.Close(ExcelApp.XlSaveAction.xlSaveChanges, missing, missing);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            releaseObject(xlApp);
            releaseObject(workbook);
            releaseObject(worksheet);
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private static void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程
            try
            {
                foreach (Process process in Process.GetProcessesByName(processName))
                {
                    if (!process.CloseMainWindow())
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        private static string NumberToChar(int number)
        {
            if (1 <= number && 26 >= number)
            {
                int num = number + 64;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] btNumber = new byte[] { (byte)num };
                return asciiEncoding.GetString(btNumber);
            }
            else if (number > 26)
            {
                int NewNum = number % 26;
                int count = number / 26;
                string ss = NumberToChar(count);
                int num = NewNum + 64;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] btNumber = new byte[] { (byte)num };
                return ss + asciiEncoding.GetString(btNumber);
            }

            return "数字不在转换范围内";
        }
    }

}
