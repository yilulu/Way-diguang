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
    public partial class message_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int type_id;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.type_id = DTRequest.GetQueryInt("type_id");
            this.keywords = DTRequest.GetQueryString("keywords");
            string Url = Request.Url.ToString();
            if (string.IsNullOrEmpty(Utils.GetCookie("UserCheckPwd")))
            {
                if (string.IsNullOrEmpty(DTRequest.GetQueryString("State")))
                {
                    Response.Redirect("RePw.aspx?Url=" + Url + "");
                }
            }
            this.pageSize = GetPageSize(15); //每頁數量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_message", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("id>0" + CombSqlTxt(this.type_id, this.keywords), "post_time desc,id desc");
            }
        }

        #region 數據綁定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.type_id > 0)
            {
                this.ddlType.SelectedValue = this.type_id.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            BLL.user_message bll = new BLL.user_message();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}&page={2}",
                this.type_id.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 組合SQL查詢語句==========================
        protected string CombSqlTxt(int _type_id, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_type_id > 0)
            {
                strTemp.Append(" and type=" + _type_id);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (accept_user_name like '%" + _keywords + "%' or post_user_name like '%" + _keywords + "%' or title like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用戶每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("message_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回訊息類型=============================
        protected string GetMessageType(int _type)
        {
            string result = string.Empty;
            switch (_type)
            {
                case 1:
                    result = "系統消息";
                    break;
                case 2:
                    result = "收件箱";
                    break;
                case 3:
                    result = "寄件匣";
                    break;
            }
            return result;
        }
        #endregion

        //關健字查詢
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}",
                this.type_id.ToString(), txtKeywords.Text));
        }

        //篩選類別
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}",
                ddlType.SelectedValue, this.keywords));
        }

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("message_list_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}",
                this.type_id.ToString(), this.keywords));
        }

        //批次刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_message", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.user_message bll = new BLL.user_message();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}",
                this.type_id.ToString(), this.keywords), "Success");
        }
    }
}
