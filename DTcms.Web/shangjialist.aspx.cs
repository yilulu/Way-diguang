using Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class WebForm13 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind(1);
            }
        }

        private void Bind(int pageindex)
        {
            DTcms.BLL.users userbll = new BLL.users();
            int count = 0;
            repdata.DataSource = userbll.GetList(10, pageindex, " group_id=5 ", " id", out count);
            repdata.DataBind();
            aspPage.RecordCount = count;
            aspPage.PageSize = 10;
        }

        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            Bind(aspPage.CurrentPageIndex);
        }
    }
}