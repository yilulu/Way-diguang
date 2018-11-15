using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class point_log : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string keywords = string.Empty;

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

            this.pageSize = GetPageSize(15); //每頁數量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("point_log", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("id>0" + CombSqlTxt(this.keywords), "add_time desc,id desc");
            }
        }

        #region 數據綁定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            this.txtKeywords.Text = this.keywords;
            BLL.point_log bll = new BLL.point_log();
            this.rptList.DataSource = bll.list_pagesWhere(this.page, this.pageSize, " and " + _strWhere, " order by " + _orderby);
            this.rptList.DataBind();
            this.totalCount = bll.GetTatalNum(_strWhere);
            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("point_log.aspx", "keywords={0}&page={1}",
                this.keywords, "__id__");
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
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or remark like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用戶每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("point_log_page_size"), out _pagesize))
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
            Response.Redirect(Utils.CombUrlTxt("point_log.aspx", "keywords={0}", txtKeywords.Text));
        }

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("point_log_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("point_log.aspx", "keywords={0}", this.keywords));
        }

        //批次刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("point_log", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.point_log bll = new BLL.point_log();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("point_log.aspx", "keywords={0}", this.keywords), "Success");
        }

        #region 根据ID返回用户名
        public string GetNameByID(string id)
        {
            string UserName = "未知";
            if (!string.IsNullOrEmpty(id))
            {
                int UID = Utils.StringToNum(id);
                BLL.users bllUser = new BLL.users();
                Model.users User = new Model.users();
                User = bllUser.GetModel(UID);
                if (User != null)
                {
                    UserName = User.user_name;
                }
            }
            return UserName;
        }
        #endregion

    }
}
