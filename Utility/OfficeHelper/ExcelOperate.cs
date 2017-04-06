using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Utility.OfficeHelper
{
    /// <summary>
    /// Excel单元格控制类
    /// </summary>
    public class ExcelOperate
    {
        //定义变量的缺省值
        private object mValue = System.Reflection.Missing.Value;

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void Merge(Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Merge(mValue);
        }

        /// <summary>
        /// 设置连续区域的字体大小
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="strStartCell">开始单元格</param>
        /// <param name="strEndCell">结束单元格</param>
        /// <param name="intFontSize">字体大小</param>
        public void SetFontSize(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, int intFontSize)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Font.Size = intFontSize.ToString();
        }

        /// <summary>
        /// 在指定单元格插入指定的值
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="Cell">单元格 如Cells[1,1]</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteCell(Excel._Worksheet CurSheet, object objCell, object objValue)
        {
            CurSheet.get_Range(objCell, mValue).Value2 = objValue;

        }

        /// <summary>
        /// 在指定Range中插入指定的值
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="StartCell">开始单元格</param>
        /// <param name="EndCell">结束单元格</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteRange(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, object objValue)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Value2 = objValue;
        }

        /// <summary>
        /// 合并单元格，并在合并后的单元格中插入指定的值
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteAfterMerge(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, object objValue)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Merge(mValue);
            CurSheet.get_Range(objStartCell, mValue).Value2 = objValue;
        }

        /// <summary>
        /// 设置整个连续区域的字体颜色
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="clrColor">颜色</param>
        public void SetColor(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, System.Drawing.Color clrColor)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /// <summary>
        /// 设置整个连续区域的单元格背景色
        /// </summary>
        /// <param name="CurSheet"></param>
        /// <param name="objStartCell"></param>
        /// <param name="objEndCell"></param>
        /// <param name="clrColor"></param>
        public void SetBgColor(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, System.Drawing.Color clrColor)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Interior.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }


        /// <summary>
        /// 设置连续区域的字体名称
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="fontname">字体名称 隶书、仿宋_GB2312等</param>
        public void SetFontName(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, string fontname)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Font.Name = fontname;
        }

        /// <summary>
        /// 设置连续区域的字体为黑体
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetBold(Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Font.Bold = true;
        }

        /// <summary>
        /// 设置连续区域的边框：上下左右都为黑色连续边框
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetBorderAll(Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

        }

        /// <summary>
        /// 设置连续区域水平居中
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetHAlignCenter(Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        }

        /// <summary>
        /// 设置连续区域水平居左
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetHAlignLeft(Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        }

        /// <summary>
        /// 设置连续区域水平居右
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetHAlignRight(Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        /// <summary>
        /// 为单元格设置公式
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objCell">单元格</param>
        /// <param name="strFormula">公式</param>
        public void SetFormula(Excel._Worksheet CurSheet, object objCell, string strFormula)
        {
            CurSheet.get_Range(objCell, mValue).Formula = strFormula;
        }


        /// <summary>
        /// 单元格自动换行
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void AutoWrapText(Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).WrapText = true;
        }

        /// <summary>
        /// 设置指定列列宽
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="strColID">列标识，如A代表第一列</param>
        /// <param name="dblWidth">宽度</param>
        public void SetColumnWidth(Excel._Worksheet CurSheet, string strColID, double dblWidth)
        {
            ((Excel.Range)CurSheet.Columns.GetType().InvokeMember("Item", System.Reflection.BindingFlags.GetProperty, null, CurSheet.Columns, new object[] { (strColID + ":" + strColID).ToString() })).ColumnWidth = dblWidth;
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="dblWidth">宽度</param>
        public void SetColumnWidth(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, double dblWidth)
        {
            CurSheet.get_Range(objStartCell, objEndCell).ColumnWidth = dblWidth;
        }


        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="dblHeight">行高</param>
        public void SetRowHeight(Excel._Worksheet CurSheet, object objStartCell, object objEndCell, double dblHeight)
        {
            CurSheet.get_Range(objStartCell, objEndCell).RowHeight = dblHeight;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="CurBook">Workbook</param>
        /// <param name="strFilePath">文件路径</param>
        public void Save(Excel._Workbook CurBook, string strFilePath)
        {
            CurBook.SaveCopyAs(strFilePath);
        }

        /// <summary>
        /// 另存为文件
        /// </summary>
        /// <param name="CurBook">Workbook</param>
        /// <param name="strFilePath">文件路径</param>
        public void SaveAs(Excel._Workbook CurBook, string strFilePath)
        {
            CurBook.SaveAs(strFilePath, mValue, mValue, mValue, mValue, mValue, Excel.XlSaveAsAccessMode.xlNoChange, mValue, mValue, mValue, mValue, mValue);
        }

        /// <summary>
        /// 释放内存
        /// </summary>
        public void Dispose(Excel._Worksheet CurSheet, Excel._Workbook CurBook, Excel._Application CurExcel)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurSheet);
                CurSheet = null;
                CurBook.Close(false, mValue, mValue);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurBook);
                CurBook = null;

                CurExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurExcel);
                CurExcel = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcessesByName("Excel"))
                    //if (pro.StartTime < DateTime.Now)
                    pro.Kill();
            }
            System.GC.SuppressFinalize(this);

        }


    }
}
