using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Text;
using DTcms.Common;
using System.Data;

namespace DTcms.Web
{
    public partial class UserNeed : PageBase
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int category_id;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }

        }

        private void LoadData()
        {
            Ltf.BLL.dt_user_need bll = new Ltf.BLL.dt_user_need();
            pageSize = 15;
            this.page = DTRequest.GetQueryInt("page", 1);
            DataTable dt = bll.list_pagesWhere(page, pageSize, "", "");

            rptList.DataSource = dt.DefaultView;
            rptList.DataBind();
            totalCount = bll.GetRecordCount("");
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("UserNeed.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }


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
            Response.Redirect(Utils.CombUrlTxt("UserNeed.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }
        #endregion
    }
}