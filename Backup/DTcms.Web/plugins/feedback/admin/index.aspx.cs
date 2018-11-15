using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DTcms.Web.plugins.feedback.admin
{
    public partial class index : System.Web.UI.Page
    {

        BLL.dt_feedback bllNot = new BLL.dt_feedback();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData(1);
            }
        }

        private void LoadData(int page)
        {

            DataTable dt = bllNot.list_pagesWhere(page, AspNetPager1.PageSize, "", " order by  add_time desc");
            if (dt != null)
            {
                rptList.DataSource = dt.DefaultView;
                rptList.DataBind();
                AspNetPager1.RecordCount = bllNot.GetRecordCount("");
            }
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            LoadData(e.NewPageIndex);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnUnLock_Click(object sender, EventArgs e)
        {

        }
    }
}