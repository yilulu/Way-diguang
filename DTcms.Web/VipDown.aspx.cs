using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DTcms.Common;
using Common;

namespace DTcms.Web
{
    public partial class VipDown : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FilePath = string.Empty; string ee = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                int ID = Utils.StringToNum(Request.QueryString["ID"]);
                Model.dt_download_attach down = new Model.dt_download_attach();
                BLL.article bll = new BLL.article();

                down = bll.GetModelDown(ID);
                if (down != null)
                {
                    FilePath = down.file_path;
                    ee = down.file_ext;
                }


            }
            string fileName = DateTime.Now.ToString("yyyyMMddhhssmm") + "." + ee;  //客户端保存的文件名
            string filePath = Server.MapPath(FilePath);//路径

            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
        }
    }
}
