using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.manager
{
    public partial class manager_list : DTcms.Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string keywords;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords").Trim();
            this.pageSize = GetPageSize(15); //每頁數量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_manager", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                //取得管理員資料
                Model.manager model = GetAdminInfo();
                RptBind("role_type>=" + model.role_type + CombSqlTxt(this.keywords), "add_time desc");
            }
        }

        #region 數據綁定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            this.txtKeywords.Text = this.keywords;
            BLL.manager bll = new BLL.manager();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();
            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("manager_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 組合SQL查詢語句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or real_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("manager_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //關健字查詢
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", txtKeywords.Text));
        }

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("manager_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", this.keywords));
        }

        //批次刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_manager", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.manager bll = new BLL.manager();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked && GetAdminInfo().id != id)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", this.keywords), "Success");
        }

    }
}
