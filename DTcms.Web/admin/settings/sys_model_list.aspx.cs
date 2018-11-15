using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class sys_model_list : Web.UI.ManagePage
    {
        public string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_model", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("id>0" + CombSqlTxt(this.keywords));
            }
        }

        #region 數據綁定
        private void RptBind(string _strWhere)
        {
            this.txtKeywords.Text = this.keywords;
            BLL.sys_model bll = new DTcms.BLL.sys_model();
            _strWhere += string.Format(" and inherit_index='{0}'", ddlTypleID.SelectedValue);
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
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        //刪除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_model", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            DTcms.BLL.sys_model bll = new DTcms.BLL.sys_model();
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
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("sys_model_list.aspx", "keywords={0}", txtKeywords.Text.Trim()), "Success", "parent.loadChannelTree");
        }

        //查詢操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RptBind("id>0" + CombSqlTxt(this.keywords));
        }

        public string GetTypeName(object typeid)
        {
            string obj = typeid == null ? "" : typeid.ToString();
            string typename = "";
            switch (obj)
            {
                case "1":
                    typename = "區域";
                    break;
                case "2":
                    typename = "總價";
                    break;
                case "3":
                    typename = "面積";
                    break;
                case "4":
                    typename = "戶型";
                    break;
                case "5":
                    typename = "方式";
                    break;
                case "6":
                    typename = "捷運沿線";
                    break;
            }
            return typename;
        }
    }
}
