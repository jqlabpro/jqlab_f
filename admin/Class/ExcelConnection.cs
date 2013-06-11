using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace ExcelUpload.Class
{
    public class ExcelConnection
    {
        static OleDbConnection oc;
        static OleDbCommand oco;
        static OleDbDataAdapter oda;

        public static string filename;
        private static string _filename
        {
            get { return filename; }
            set { filename = value; }
        }

        private ExcelConnection()
        {
            Connect();
        }

        private static void Connect()
        {
            oc = new OleDbConnection();
            string ext = Path.GetExtension(filename);
            if (ext == ".xls")
            {
                oc.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ System.Web.HttpContext.Current.Server.MapPath("~/Files/" + filename) + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
            }
            else if (ext == ".xlsx")
            {
                oc.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.Web.HttpContext.Current.Server.MapPath("~/Files/" + filename) + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
            }
            oc.Open();
        }

        private static void Disconnect()
        {
            oc.Close();
        }

        public static DataTable GetAllData(string sqlString)
        {
            Connect();

            DataTable dt = new DataTable();
            oco = new OleDbCommand();
            oco.CommandText = sqlString;
            oco.CommandType = CommandType.Text;
            oco.Connection = oc;

            oda = new OleDbDataAdapter(oco.CommandText, oc.ConnectionString);
            oda.Fill(dt);

            Disconnect();
            return dt;
        }

    }
}