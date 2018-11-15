using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class sys_channel_list : DTcms.Web.UI.ManagePage
    {
        public string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_channel", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("id>0" + CombSqlTxt(this.keywords));
            }
        }

        #region 數據綁定
        private void RptBind(string _strWhere)
        {
            this.txtKeywords.Text = this.keywords;
            DTcms.BLL.sys_channel bll = new DTcms.BLL.sys_channel();
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
            ChkAdminLevel("sys_channel", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            DTcms.BLL.sys_channel bll = new DTcms.BLL.sys_channel();
            BLL.url_rewrite bll2 = new BLL.url_rewrite();
            //批次刪除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                    //刪除URL映射表
                    bll2.Remove("channel", id.ToString());
                }
            }
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("sys_channel_list.aspx", "keywords={0}", txtKeywords.Text.Trim()), "Success", "parent.loadChannelTree");
        }

        //查詢操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("sys_channel_list.aspx", "keywords={0}", txtKeywords.Text.Trim()));
        }
    }
}
