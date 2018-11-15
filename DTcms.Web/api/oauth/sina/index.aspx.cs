using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth.sina
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //獲得配置資料
            oauth_config config = oauth_helper.get_config("sina");
            if (config == null)
            {
                Response.Write("出錯了，您尚未配置新浪微博的API資料！");
                return;
            }
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["oauth_state"] = state;
            string send_url = "https://api.weibo.com/oauth2/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri);
            //開始發送
            Response.Redirect(send_url);
        }
    }
}
