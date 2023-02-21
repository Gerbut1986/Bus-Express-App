//namespace BusExpress.PL.Models
//{   
//    using System;
//    using System.IO;
//    using System.Windows;
//    using System.Windows.Input;  
//    using System.Windows.Controls;
//    using System.Collections.Generic;
//    using Microsoft.Office.Interop.Excel;
//    using Application = Microsoft.Office.Interop.Excel.Application;

//    internal class ExcelExport
//    {
//        public void ExportToExcelAndCsv(DataGrid dgDisplay, string path, bool isRewrite = false)
//        {
//            dgDisplay.SelectAllCells();
//            dgDisplay.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
//            ApplicationCommands.Copy.Execute(null, dgDisplay);
//            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
//            String result = (string)Clipboard.GetData(DataFormats.Text);
//            dgDisplay.UnselectAllCells();
//            StreamWriter file1 = new StreamWriter(path, isRewrite);
//            file1.WriteLine(result.Replace(',', ' '));
//            file1.Close();
//            MessageBox.Show(" Exporting DataGrid data to Excel file created.xls");
//        }
//    }

//    static class ExcelExporter<T>
//    {
//        public static void ExportDataToExcel(List<T> result, string path)
//        {
//            var excel = new Application();
//            var excelworkBook = excel.Workbooks.Add();
//            var excelSheet = (Worksheet)excelworkBook.ActiveSheet;
//            excelSheet.Name = "DataSheet";

//            try
//            {
//                //create the column(s) header(s)
//                int col = 1;

//                foreach (var propInfo in result[0].GetType().GetProperties())
//                {
//                    excelSheet.Cells[1, col] = propInfo.Name;
//                    excelSheet.Cells[1, col].Font.Bold = true;
//                    col++;
//                }

//                //put the actual data
//                int k = 0;

//                foreach (var item in result)
//                {
//                    int j = 1;
//                    foreach (var propInfo in item.GetType().GetProperties())
//                    {
//                        try
//                        {
//                            excelSheet.Cells[k + 2, j].Value = propInfo.GetValue(item);
//                            j++;
//                        }
//                        catch (System.Runtime.InteropServices.COMException comex)
//                        {
//                            var err = string.Format("{0}, caused for exporting value - {1}",
//                            comex.Message, propInfo.GetValue(item));
//                            excelSheet.Cells[k + 2, j].Value = $"'{propInfo.GetValue(item)}'";
//                            j++;
//                            continue;
//                        }
//                    }
//                    k++;
//                }
//                //var folderPath = path;
//                //if (!Directory.Exists(folderPath))
//                //    Directory.CreateDirectory(folderPath);
//                //var filePath = $"{folderPath}\\{filename}.xlsx";
//                excelworkBook.Close(true, path);
//                MessageBox.Show($"Exported Successfully to {path}.", "Successfully!", MessageBoxButton.OK);
//            }
//            catch (Exception ex)
//            {
//                excelworkBook.Close(false);
//                Console.WriteLine(ex.Message);
//                Console.WriteLine("Export Failed.");
//            }
//        }
//    }
//}

