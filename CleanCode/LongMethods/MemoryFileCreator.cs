using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CleanCode.LongMethods
{
    public class DataTabletoCsvMapper
    {
        public System.IO.MemoryStream Map(DataTable dataTable)
        {
            MemoryStream ReturnStream = new MemoryStream();
            //Create a streamwriter to write to the memory stream
            StreamWriter sw = new StreamWriter(ReturnStream);
            WriteColumnNames(dataTable, sw);
            WriteRows(dataTable, sw);
            sw.Flush();
            sw.Close();
            
            return ReturnStream;
        }

        private static void WriteRows(DataTable dt, StreamWriter sw)
        {
            foreach (DataRow dr in dt.Rows)
            {
                WriteRow(dt, sw, dr);
                sw.WriteLine();
            }
        }

        private static void WriteRow(DataTable dt, StreamWriter sw, DataRow dr)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                WriteCell(dr, i, sw);

                WriteSeperatorIfRequired(dt, i, sw);
            }
        }

        private static void WriteSeperatorIfRequired(DataTable dt, int i, StreamWriter sw)
        {
            if (i < dt.Columns.Count - 1)
            {
                sw.Write(",");
            }
        }

        private static void WriteCell(DataRow dr, int i, StreamWriter sw)
        {
            if (!Convert.IsDBNull(dr[i]))
            {
                string str = String.Format("\"{0:c}\"", dr[i].ToString()).Replace("\r\n", " ");
                sw.Write(str);
            }
            else
            {
                sw.Write("");
            }
        }

        private static void WriteColumnNames(DataTable dt, StreamWriter sw)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < dt.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.WriteLine();
        }
    }
}
