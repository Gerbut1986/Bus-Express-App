namespace Transfer_App.Models
{
    using System.Data;
    using Microsoft.Win32;

    internal class ExcelImport
    {
        public DataView Import()
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            if (choofdlog.ShowDialog() == true)
            {
                string sFileName = choofdlog.FileName;
                string path = System.IO.Path.GetFullPath(choofdlog.FileName);
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                DataSet ds = new DataSet();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(path);
                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Worksheets)
                {
                    var td = new DataTable();
                    td = formofDataTable(ws);
                    ds.Tables.Add(td);// This will give the DataTable from Excel file in Dataset
                }
                var res = ds.Tables[0].DefaultView;
                wb.Close();
                return res;
            }
            return null;
        }
        private DataTable formofDataTable(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            DataTable dt = new DataTable();
            string worksheetName = ws.Name;
            dt.TableName = worksheetName;
            Microsoft.Office.Interop.Excel.Range xlRange = ws.UsedRange;
            object[,] valueArray = (object[,])xlRange.get_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault);
            for (int k = 1; k <= valueArray.GetLength(1); k++)
            {
                dt.Columns.Add((string)valueArray[1, k]);  // Add columns to the data table.
            }
            // Value array first row contains column names. so loop starts from 2 instead of 1:
            object[] singleDValue = new object[valueArray.GetLength(1)]; 
            for (int i = 2; i <= valueArray.GetLength(0); i++)
            {
                for (int j = 0; j < valueArray.GetLength(1); j++)
                {
                    if (valueArray[i, j + 1] != null)
                    {
                        singleDValue[j] = valueArray[i, j + 1].ToString();
                    }
                    else
                    {
                        singleDValue[j] = valueArray[i, j + 1];
                    }
                }
                dt.LoadDataRow(singleDValue, LoadOption.PreserveChanges);
            }

            return dt;
        }
    }
}
