using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class group_list : Web.UI.ManagePage
    {
        public string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string Url = Request.Url.ToString();
            if (string.IsNullOrEmpty(Utils.GetCookie("UserCheckPwd")))
            {
                if (string.IsNullOrEmpty(DTRequest.GetQueryString("State")))
                {
                    Response.Redirect("RePw.aspx?Url=" + Url + "");
                }
            }
            this.keywords = DTRequest.GetQueryString("keywords");
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_groups", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("id>0" + CombSqlTxt(this.keywords));
            }
        }

        #region 數據綁定
        private void RptBind(string _strWhere)
        {
            this.txtKeywords.Text = this.keywords;
            BLL.user_groups bll = new BLL.user_groups();
            this.rptList.DataSource = bll.GetList(0, _strWhere, "grade asc,id asc");
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
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        //查詢操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("group_list.aspx", "keywords={0}", txtKeywords.Text.Trim()));
        }

        //刪除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_groups", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.user_groups bll = new BLL.user_groups();
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
            JscriptMsg("批次刪除成功啦！", Utils.CombUrlTxt("group_list.aspx", "keywords={0}", txtKeywords.Text.Trim()), "Success");
        }

    }
}
