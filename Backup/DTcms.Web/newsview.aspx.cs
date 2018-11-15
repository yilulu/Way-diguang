using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using Common;

namespace DTcms.Web
{
    public partial class newsview : PageBase
    {
        public string Image1Url = "";
        public string Images = "";
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"]);
            if (id == 410)
            {
                Response.Redirect("down.aspx");
            }
            if (!IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {
            BLL.article bll = new BLL.article();
            Model.article_news model;
            if (Request.QueryString["type"] != null)
            {
                model = new DAL.article().GetModel(id);
                lblDatetime.Visible = false;
            }
            else
            {
                model = bll.GetNewsModel(id);
            }
            if (model != null)
            {
                lblTitle.Text = model.title;
                lblDatetime.Text = "發佈日期: " + model.add_time.ToString("yyyy-MM-dd");
                lblFrom.Text = "來源：" + model.from;
                lblContent.Text = model.content;
            }
        }
    }
}