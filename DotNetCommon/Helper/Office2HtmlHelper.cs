using System;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// 将office文件(word,excel,ppt)转化为hmtl文件
    /// 要引用Microsoft.Office.Interop.Word， Microsoft.Office.Interop.Excel;， Microsoft.Office.Interop.PowerPoint
    /// 并且属性"嵌入互操作性"改为 false
    /// </summary>
    public class Office2HtmlHelper
    {
        /// <summary>
        /// Word转成Html
        /// </summary>
        /// <param name="wordFile">要转换的word文档名称(包括路径)</param>
        /// <param name="htmlFileName">转换成html的文件名(包括路径)</param>
        public static void Word2Html(string wordFile, string htmlFileName)
        {
            try
            {
                Word.ApplicationClass word = new Word.ApplicationClass();
                Type wordType = word.GetType();
                Word.Documents docs = word.Documents;
                Type docsType = docs.GetType();
                Word.Document doc = (Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { (object)wordFile, true, true });
                Type docType = doc.GetType();
                object saveFileName = (object)htmlFileName;
                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML });
                docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
                wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// Excel转成Html
        /// </summary>
        /// <param name="excelFileName">要转换的excel文档名称(包括路径)</param>
        /// <param name="htmlFileName">转换成html的文件名(包括路径)</param>
        public static void Excel2Html(string excelFileName, string htmlFileName)
        {
            try
            {
                Excel.Application repExcel = new Excel.Application();
                Excel.Workbook workbook = null;
                Excel.Worksheet worksheet = null;
                workbook = repExcel.Application.Workbooks.Open(excelFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                object ofmt = Excel.XlFileFormat.xlHtml;
                workbook.SaveAs(htmlFileName, ofmt, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                object osave = false;
                workbook.Close(osave, Type.Missing, Type.Missing);
                repExcel.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// ppt转成Html
        /// </summary>
        /// <param name="pptFile">要转换的ppt文档名称(包括路径)</param>
        /// <param name="htmlFileName">转换成html的文件名(包括路径)</param>
        public static void PPT2Html(string pptFile, string htmlFileName)
        {
            try
            {
                Microsoft.Office.Interop.PowerPoint.Application ppApp = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Interop.PowerPoint.Presentation prsPres = ppApp.Presentations.Open(pptFile, MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
                prsPres.SaveAs(htmlFileName, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsHTML, MsoTriState.msoTrue);
                prsPres.Close();
                ppApp.Quit();
            }
            catch (Exception)
            {
                throw ex;
            }

        }

    }
}
