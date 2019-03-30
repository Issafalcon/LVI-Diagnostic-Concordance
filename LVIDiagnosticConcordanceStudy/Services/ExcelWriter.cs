using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services
{

    public class ExcelWriter : IExcelWriter
    {

        public Byte[] WriteToExcel<T>(List<T> items)
        {
            DataTable data = ListToDataTable(items);
            Byte[] fileBytes;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Study Data");

                worksheet.Cells["A1"].LoadFromDataTable(data, true, TableStyles.Medium15);

                fileBytes = package.GetAsByteArray();
            }

            return fileBytes;

        }

        private DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dt = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in props)
            {
                dt.Columns.Add(property.Name);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }

                dt.Rows.Add(values);
            }

            return dt;
        }
    }
}
