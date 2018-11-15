using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Common;
using Common;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

namespace DTcms.Web
{
    public partial class viplist : PageBase
    {
        public int channel_id;
        protected int category_id;
        string ids = ""; protected int totalCount = 0;
        public string Images = "";
        BLL.users bllUser = new BLL.users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WEBUserCurrent.IsLogin)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('請先登入');window.location.href='login_vip.aspx'</script>");
            }
            else
            {

                ids = WEBUserCurrent.UserID.ToString();
                if (!bllUser.isVip(Utils.StringToNum(ids)))
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('你不是VIP會員，無法訪問次頁');window.location.href='userinfo.aspx'</script>");
                }
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

            Model.users User = new Model.users();

            var usermodel = bllUser.GetModel(BLL.Function.Instance.StringToNum(ids));
            Images = "images/vip_touxiang.jpg";
            lblUsername.Text = usermodel.user_name;
            lblbUsertype.Text = new DAL.user_groups().GetTitle(usermodel.group_id);

            //是商家會員就獲取資訊

            BindJp(1);

        }
        BLL.article bll = new BLL.article();
        string sqlwhere = "";
        private void BindJp(int page)
        {
            int pageSize = 8;
            //發佈的商品
            if (!string.IsNullOrEmpty(Request.QueryString["status"]))
            {
                sqlwhere = " and Status=" + Request.QueryString["status"];
            }
            //this.repddata.DataSource = bll.GetGoodsList(pageSize, page, " user_id=" + ids + sqlwhere, "sort_id asc,add_time desc", out totalCount);
            DataTable dt = bll.list_pagesWheres(page, pageSize, sqlwhere + " and (xiajialiyou like '%" + WEBUserCurrent.UserID + "%' or user_id=" + WEBUserCurrent.UserID + ")", " ");
            string PrintHtml = string.Empty;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Title = dt.Rows[i]["title"].ToString();
                    string FileSize = dt.Rows[i]["file_size"].ToString();
                    string vipid = dt.Rows[i]["file_size"].ToString();
                    int ID = Utils.StringToNum(dt.Rows[i]["ID"].ToString());
                    int UserID = Utils.StringToNum(dt.Rows[i]["user_id"].ToString());

                    PrintHtml += " <tr><td></td>";
                    PrintHtml += "<td>" + Title + "</td>";
                    PrintHtml += "<td>" + FileSize + "</td>";
                    PrintHtml += "<td><a href=\"VipDown.aspx?ID=" + ID + "\">點擊下載</a>";
                    if (UserID == WEBUserCurrent.UserID)
                    {
                        PrintHtml += " | <a href=\"javascript:;\" onclick=\"Del(" + ID + ")\">刪除</a>";
                    }
                    PrintHtml += " </td></tr>";
                }
            }
            else
            {
                PrintHtml = "暫無內容";
            }
            VIPList.Text = PrintHtml;
            if (!string.IsNullOrEmpty(Request.QueryString["status"]))
            {
                sqlwhere = " and Status=" + Request.QueryString["status"];
            }
            this.totalCount = bll.GetTatalNums(sqlwhere + " and (xiajialiyou like '%" + WEBUserCurrent.UserID + "%' or user_id=" + WEBUserCurrent.UserID + ")");
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = this.totalCount;
        }

        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            BindJp(aspPage.CurrentPageIndex);
        }
    }
}
