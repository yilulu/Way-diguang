using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace DTcms.Web
{
    public partial class WebForm6 : PageBase
    {
        public string Images = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WEBUserCurrent.IsLogin)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                if (WEBUserCurrent.UserType == "5")
                {
                    Response.Redirect("userSJ.aspx");
                }
                if (!IsPostBack)
                {
                    Bindinfo();
                }
            }
        }

        private void Bindinfo()
        {
            DTcms.DAL.users dalUser = new DAL.users();
            var usermodel = dalUser.GetModel(WEBUserCurrent.UserID);
            Images = string.IsNullOrEmpty(usermodel.avatar) ? "images/vip_touxiang.jpg" : "upload/user/" + usermodel.avatar;
            lblUsername.Text = usermodel.nick_name;
            lblbUsertype.Text = new DAL.user_groups().GetTitle(usermodel.group_id);

            //是商家會員就獲取資訊
            if (usermodel.group_id == 5)
            {
                BindJp(1);
            }
            
        }
        protected int channel_id;
        BLL.article bll = new BLL.article();
        string sqlwhere = "";
        private void BindJp(int page)
        {
            int totalCount;
            int pageSize = 8;
            //發佈的商品
            if (!string.IsNullOrEmpty(Request.QueryString["status"]))
            {
                sqlwhere = " and Status=" + Request.QueryString["status"];
            }
            this.repddata.DataSource = bll.GetGoodsList(pageSize, page, " user_id=" + WEBUserCurrent.UserID + sqlwhere, "sort_id asc,add_time desc", out totalCount);
            this.repddata.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = totalCount;
        }

        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            BindJp(aspPage.CurrentPageIndex);
        }
    }
}
