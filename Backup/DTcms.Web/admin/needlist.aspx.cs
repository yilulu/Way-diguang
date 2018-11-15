using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Data;

namespace DTcms.Web.admin
{
    public partial class needlist : Web.UI.ManagePage
    {
        string strWhere = "", type = "add"; protected string UserID;
        Ltf.BLL.dt_user_need bll = new Ltf.BLL.dt_user_need();
        Ltf.Model.dt_user_need need = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["typ"]))
            {
                type = Request.QueryString["typ"];
            }
            if (type == "mod")
            {
                if (!IsPostBack)
                {
                    LoadData();
                }

            }

        }

        #region 加載
        private void LoadData()
        {
            int id = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                id = int.Parse(Request.QueryString["ID"]);
            }
            need = bll.GetModel(id);
            if (need != null)
            {
                txtType.Text = need.typeName;
                txtArea.Text = need.AreaName;
                txtCata.Text = need.CataName;
                txtMianJi.Text = need.MianJi;
                txtSpec.Text = need.spec;
            }


        }
        #endregion

        #region 添加/編輯
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            need = new Ltf.Model.dt_user_need();
            need.typeName = txtType.Text.Trim();
            need.AreaName = txtArea.Text.Trim();
            need.CataName = txtCata.Text.Trim();
            need.MianJi = txtMianJi.Text.Trim();
            need.spec = txtSpec.Text.Trim();
            need.AddTime = DateTime.Now;
            need.UserID = 1;
            need.Content = "aaaaa";
            if (type == "mod")
            {
                need.ID = int.Parse(Request.QueryString["ID"]);
                if (!bll.Update(need))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改成功！", "UserNeedList.aspx", "Success");

            }
            else
            {
                int bk = bll.Add(need);
                if (bk < 0)
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加成功！", "UserNeedList.aspx", "Success");
            }
        }
        #endregion
    }
}