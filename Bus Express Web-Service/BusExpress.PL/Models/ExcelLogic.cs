namespace BusExpress.PL.Models
{
    using System;
    using System.Linq;
    using OfficeOpenXml;
    using System.Collections.Generic;

    public class ExcelLogic
    {
        public IEnumerable<T> GetList<T>(ExcelWorksheet sheet)
        {
            var list = new List<T>();
            // First row is for knowing the properties of object:
            var columnInfo = Enumerable.Range(1, sheet.Dimension.Columns).ToList().Select(n =>

                new { Index = n, ColumnName = sheet.Cells[1, n].Value.ToString() }
            );

            for (int row = 2; row < sheet.Dimension.Rows; row++)
            {
                T obj = (T)Activator.CreateInstance(typeof(T)); // Generic object
                foreach (var prop in typeof(T).GetProperties())
                {
                    var col = columnInfo.SingleOrDefault(c => c.ColumnName == prop.Name).Index;
                    var val = sheet.Cells[row, col].Value;
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(val, propType));
                }
                list.Add(obj);
            }

            return list;
        }
    }
}