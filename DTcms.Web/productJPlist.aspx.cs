using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using Common;

namespace DTcms.Web
{
    public partial class WebForm12 : PageBase
    {
        protected int channel_id;
        BLL.article bll = new BLL.article();
        protected string CataName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("mid");
            if (!IsPostBack)
            {
                Bind(1);
            }
        }
        private void Bind(int page)
        {
            int typeID = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                typeID = int.Parse(Request.QueryString["cid"]);
                switch (typeID)
                {
                    case 1:
                        CataName = "最新商品";
                        break;
                    case 2:
                        CataName = "推薦商品";
                        break;
                    case 3:
                        CataName = "特賣商品";
                        break;
                }

            }

            int totalCount;
            int pageSize = 20;

            var table2 = bll.list_pagesWhere(page, pageSize, " and channel_id=" + channel_id + " and  Postid=" + typeID + "  and Status=1 ", " order by sort_id asc");
            this.repdatetemai.DataSource = table2;
            this.repdatetemai.DataBind();
            totalCount = bll.GetTatalNum("  channel_id=" + channel_id + " and  Postid=" + typeID + "  and Status=1");
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = totalCount;
        }

        protected void aspPage_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Bind(e.NewPageIndex);
        }

    }
}