using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class templet_file_edit : DTcms.Web.UI.ManagePage
    {
        protected string filePath; //檔路徑
        protected string pathName; //範本目錄
        protected string fileName; //檔案名稱

        protected void Page_Load(object sender, EventArgs e)
        {
            pathName = DTRequest.GetQueryString("path");
            fileName = DTRequest.GetQueryString("filename");
            if (string.IsNullOrEmpty(pathName) || string.IsNullOrEmpty(fileName))
            {
                JscriptMsg("傳輸參數不正確！", "back", "Error");
                return;
            }
            filePath = Utils.GetMapPath(@"../../templates/" + pathName + "/" + fileName);
            if (!File.Exists(filePath))
            {
                JscriptMsg("該文件不存在！", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_templet", DTEnums.ActionEnum.All.ToString()); //檢查許可權
                ShowInfo(filePath);
            }
        }

        #region 賦值操作=================================
        private void ShowInfo(string _path)
        {
            using (StreamReader objReader = new StreamReader(_path, Encoding.UTF8))
            {
                txtContent.Text = objReader.ReadToEnd();
                objReader.Close();
            }
        }
        #endregion

        //儲存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(this.filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                Byte[] info = Encoding.UTF8.GetBytes(txtContent.Text);
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
            JscriptMsg("範本儲存成功！", Utils.CombUrlTxt("templet_file_list.aspx", "skin={0}", this.pathName), "Success");
        }

    }
}
