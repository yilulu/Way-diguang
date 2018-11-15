using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.manager
{
    public partial class role_list : DTcms.Web.UI.ManagePage
    {
        public string keywords = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["keywords"]))
            {
                this.keywords = Request.QueryString["keywords"].Trim();
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_role", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                //取得管理員資料
                Model.manager model = GetAdminInfo();
                RptBind("role_type>=" + model.role_type + CombSqlTxt(this.keywords));
            }
        }

        #region 數據綁定
        private void RptBind(string _strWhere)
        {
            this.txtKeywords.Text = this.keywords;
            BLL.manager_role bll = new BLL.manager_role();
            this.rptList.DataSource = bll.GetList(_strWhere);
            this.rptList.DataBind();
        }
        #endregion

        #region 組合SQL查詢語句
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and role_name like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回角色類型名稱
        protected string GetTypeName(int role_type)
        {
            string str = "";
            switch (role_type)
            {
                case 1:
                    str = "超級用戶";
                    break;
                default:
                    str = "系統用戶";
                    break;
            }
            return str;
        }
        #endregion

        //刪除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_role", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.manager_role bll = new BLL.manager_role();
            //批次刪除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("role_list.aspx", "keywords={0}", this.keywords), "Success");
        }

        //查詢操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("role_list.aspx", "keywords={0}", txtKeywords.Text.Trim()));
        }
    }
}
