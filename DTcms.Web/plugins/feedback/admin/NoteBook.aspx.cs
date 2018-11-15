using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.plugins.feedback.admin
{

    public partial class NoteBook : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected string keywords = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            this.pageSize = GetPageSize(15); //每頁數量
            if (!IsPostBack)
            {
                RptBind();
            }
        }

        #region 數據綁定=================================
        private void RptBind()
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            this.txtKeywords.Text = this.keywords;
            BLL.dt_feedback bllfeed = new BLL.dt_feedback();
            DataTable dt = bllfeed.list_pagesWhere(this.page, this.pageSize, "", " order by id desc");
            this.totalCount = bllfeed.GetRecordCount("");
            txtPageNum.Text = this.pageSize.ToString();
            if (dt != null)
            {
                rptList.DataSource = dt.DefaultView;
                rptList.DataBind();
            }

            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("NoteBook.aspx", "group_id={0}&keywords={1}&page={2}", "", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 返回用戶每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("regFee_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion
        protected void lbtnUnLock_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.dt_feedback bll = new BLL.dt_feedback();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.dt_feedback model = bll.GetModel(id);
                    if (model != null)
                    {
                        bll.Delete(id);
                    }
                }
            }
            JscriptMsg("留言已刪除！", Utils.CombUrlTxt("NoteBook.aspx", "keywords={0}", this.keywords), "Success");
        }



        #region 設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("regFee_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("NoteBook.aspx", "group_id={0}&keywords={1}",
                "", this.keywords));
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("NoteBook.aspx", "keywords={0}",
               txtKeywords.Text));
        }

    }
}