using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DTcms.Common;
using System.Text;
using System.Data;

namespace DTcms.Web
{
    public partial class User_zh : PageBase
    {
        protected int channel_id;
        BLL.article bll = new BLL.article();
        string sqlwhere = "";
        public int chushouCount = 0;
        public int chuzuCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindinfo();

            }
        }

        public string Images = "";
        private void Bindinfo()
        {
            int id = string.IsNullOrEmpty(Request.QueryString["id"]) ? -1 : int.Parse(Request.QueryString["id"]);
            BLL.article bll = new BLL.article();
            Model.article_goods usermodel = bll.GetGoodsModel(id);
            if (usermodel != null)
            {
                Images = string.IsNullOrEmpty(usermodel.img_url) ? "images/vip_touxiang.jpg" : usermodel.img_url;
                //lblAdd.Text = usermodel.dizhi;
                username.Text = usermodel.title;
                username2.Text = usermodel.title;
                //username3.Text = usermodel.title;
                username4.Text = usermodel.title;
                email2.Text = usermodel.gongsi;
                mobile2.Text = usermodel.dianhua;
                lblLianXiRen.Text = usermodel.lianxiren;
                shuxishequ2.Text = usermodel.fuwuxiangju;
                fuwutechang2.Text = usermodel.shequ;
                note2.Text = usermodel.content;
            }
        }


        protected void lbtnout_Click(object sender, EventArgs e)
        {
            HttpCookie ccookie1 = Response.Cookies["WEBUSERID"];
            HttpCookie ccookie2 = Response.Cookies["WEBUserNamecook"];
            HttpCookie ccookie3 = Response.Cookies["WEBRealNamecook"];
            HttpCookie ccookie4 = Response.Cookies["WEBUserTypecook"];
            if (ccookie1 != null)
            {
                ccookie1.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie1);
            }
            if (ccookie2 != null)
            {
                ccookie2.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie2);
            }
            if (ccookie3 != null)
            {
                ccookie3.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie3);
            }
            if (ccookie4 != null)
            {
                ccookie4.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie4);
            }

            Response.Redirect("index.aspx");
        }
    }
}