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
    public partial class UserNeedList : Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int category_id;
        protected string property = string.Empty;
        protected string keywords = string.Empty;

        Ltf.BLL.dt_user_need bll = new Ltf.BLL.dt_user_need();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bing(1);
            }
        }

        #region 加載數據
        private void bing(int p)
        {
            pageSize = 20;
            this.page = DTRequest.GetQueryInt("page", 1);
            DataTable dt = bll.list_pagesWhere(page, pageSize, "", "");

            rptList.DataSource = dt.DefaultView;
            rptList.DataBind();
            totalCount = bll.GetRecordCount("");
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("UserNeedList.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 批量刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //ChkAdminLevel("sys_model", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
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
            JscriptMsg("批次刪除成功啦！", Utils.CombUrlTxt("UserNeedList.aspx", "keywords={0}", ""), "Success", "parent.loadChannelTree");
        }
        #endregion

        #region 設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("download_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("UserNeedList.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }
        #endregion
    }
}