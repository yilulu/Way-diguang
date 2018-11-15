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
    public partial class userlook1 : PageBase
    {
        protected int channel_id; public string Url = string.Empty;
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
            DTcms.DAL.users dalUser = new DAL.users();
            int id = string.IsNullOrEmpty(Request.QueryString["id"]) ? -1 : int.Parse(Request.QueryString["id"]);
            var usermodel = dalUser.GetModel(id);
            if (usermodel != null)
            {
                Images = GetFace(usermodel.avatar);
                lblAdd.Text = usermodel.address;
                username.Text = usermodel.user_name;
                //username2.Text = usermodel.user_name;
                username3.Text = usermodel.nick_name;
                username4.Text = usermodel.user_name;
                email2.Text = usermodel.email;
                mobile2.Text = usermodel.telphone;
                lblUrl.Text = usermodel.gongsi;
                Url = usermodel.gongsi;
                lbl_info.Text = usermodel.jingli;
                //fuwuquyu.Text = usermodel.fuwuquyu;
                //shuxishequ2.Text = usermodel.shuxishequ;
                //fuwutechang2.Text = usermodel.fuwutechang;
                note2.Text = usermodel.note;
            }
        }

        #region 处理头像
        public string GetFace(string FaceUrl)
        {
            string Face = "../../images/default_user_avatar.gi";
            if (!string.IsNullOrEmpty(FaceUrl))
            {
                if (FaceUrl.IndexOf("upload") > 0)
                {
                    Face = FaceUrl;
                }
                else
                {
                    Face = "../../upload/user/" + FaceUrl;
                }
            }
            return Face;
        }
        #endregion

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