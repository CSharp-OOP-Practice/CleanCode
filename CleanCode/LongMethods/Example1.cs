using System;
using System.Web;
using CleanCode.LongMethods;

namespace FooFoo
{
    public partial class Download : System.Web.UI.Page
    {
        private readonly DataTabletoCsvMapper _dataTableToCsvMapper = new DataTabletoCsvMapper();
        private readonly TableReaderClass _tableReader = new TableReaderClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearResponse();

            SetCacheability();

            WriteContentToResponse(GetCsv());
        }

        private byte[] GetCsv()
        {
            System.IO.MemoryStream ms = _dataTableToCsvMapper.Map(_tableReader.GetDataTable());
            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteArray;
        }

        private void WriteContentToResponse(byte[] byteArray)
        {
            Response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
            Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
            Response.ContentType = "text/comma-separated-values";
            Response.AddHeader("Content-Disposition", "attachment; filename=FooFoo.csv");
            Response.AddHeader("Content-Length", byteArray.Length.ToString());
            Response.BinaryWrite(byteArray);
        }

        private void SetCacheability()
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.CacheControl = "private";
            Response.AppendHeader("Pragma", "cache");
            Response.AppendHeader("Expires", "60");
        }

        private void ClearResponse()
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();
        }

    }
}