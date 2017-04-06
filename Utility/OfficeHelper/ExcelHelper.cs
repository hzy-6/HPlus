using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Odbc;
using System.Data;
using System.Diagnostics; // 命名空间提供特定的类，使您能够与系统进程、事件日志和性能计数器进行交互
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Utility.OfficeHelper
{
    /// <summary>
    /// Excel数据操作类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 导入CVS文件格式
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataSet ConnectCSVFile(string fileName, string path)
        {
            string strConn = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=";
            strConn += path;
            strConn += ";Extensions=asc,csv,tab,txt;HDR=Yes;Persist Security Info=False";
            OdbcConnection objConn = new OdbcConnection(strConn);
            DataSet ds = new DataSet();
            try
            {
                string strSql = "select * from " + fileName;  //fileName, For example: 1.csv
                OdbcDataAdapter odbcCSVDataAdapter = new OdbcDataAdapter(strSql, objConn);
                odbcCSVDataAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                System.GC.Collect();
            }

            return ds;
        }

        /// <summary>
        /// 导入Excel文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="ExcelName"></param>
        /// <returns></returns>
        /// IMEX=1将强制混合数据转换为文本，HDR=NO将第一行作为内容，由于第一行Header都是文本，因此所有列的类型都将转换成文本。

        public static DataSet ExcelToDataSet(string filepath, string ExcelName)
        {
            string strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;IMEX=1;HDR=YES'";
            System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(strCon);
            string strCom = "SELECT * FROM " + "[" + ExcelName + "$]";//读取Excel文件内容
            Conn.Open();
            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, Conn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, ExcelName);
            Conn.Close();
            return ds;
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="tmpDataTable"></param>
        /// <param name="strFileName"></param>
        public static void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFileName)
        {
            //检查进程
            List<Process> excelProcesses = GetExcelProcesses();
            if (excelProcesses.Count > 0)
            {
                KillTheExcel();//杀死进程
            }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
            try
            {
                if (tmpDataTable == null)
                    return;
                int rowNum = tmpDataTable.Rows.Count;
                int columnNum = tmpDataTable.Columns.Count;
                int rowIndex = 1;//如果需要导出列名，设置为1，否则设置为0
                int columnIndex = 0;

                xlApp.DefaultFilePath = "";
                xlApp.DisplayAlerts = true;
                xlApp.SheetsInNewWorkbook = 1;

                Excel.Worksheet ExcelSheet = (Worksheet)xlBook.Worksheets[1];
                ExcelOperate op = new ExcelOperate();//创建样式设置对象
                op.SetColor(ExcelSheet, "A1", "E1", System.Drawing.Color.Red);
                op.SetColumnWidth(ExcelSheet, "B", 20);

                //将DataTable的列名导入Excel表第一行(如果需要可以加上)
                foreach (DataColumn dc in tmpDataTable.Columns)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
                }


                //将DataTable中的数据导入Excel中
                for (int i = 0; i < rowNum; i++)
                {
                    rowIndex++;
                    columnIndex = 0;
                    for (int j = 0; j < columnNum; j++)
                    {
                        columnIndex++;
                        xlApp.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //xlApp.Workbooks.Close();
                xlApp.ActiveWorkbook.SaveAs(strFileName);
                xlApp.Quit();//关闭进程，自动保存
                System.GC.Collect();

            }
        }

        /// <summary>
        /// 获得进程
        /// </summary>
        /// <returns></returns>
        private static List<Process> GetExcelProcesses()
        {
            Process[] processes = Process.GetProcesses();
            List<Process> ListProcess = new List<Process>();

            foreach (Process _pr in processes)
            {
                if (_pr.ProcessName.ToUpper().Equals("EXCEL"))
                {
                    ListProcess.Add(_pr);
                }
            }
            return ListProcess;
        }

        /// <summary>
        /// 销毁所有Excel进程
        /// </summary>
        public static void KillTheExcel()
        {
            List<Process> listProcess = GetExcelProcesses();
            foreach (Process _pr in listProcess)
            {
                _pr.Kill();
            }
        }
    }
}
