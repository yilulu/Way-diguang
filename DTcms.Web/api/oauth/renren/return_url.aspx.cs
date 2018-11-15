using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;
using LitJson;

namespace DTcms.Web.api.oauth.renren
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得返回參數
            string state = DTRequest.GetQueryString("state");
            string code = DTRequest.GetQueryString("code");

            string access_token = string.Empty;
            string expires_in = string.Empty;
            string openid = string.Empty;

            if (Session["oauth_state"] == null || Session["oauth_state"].ToString() == "" || state != Session["oauth_state"].ToString())
            {
                Response.Write("出錯啦，state未初始化！");
                return;
            }
            if (string.IsNullOrEmpty(code))
            {
                Response.Write("授權被取消，相關資料：" + DTRequest.GetQueryString("error"));
                return;
            }
            
            //獲取Access Token
            JsonData jd = renren_helper.get_access_token(code);
            if (jd == null)
            {
                Response.Write("錯誤代碼：，無法獲取Access Token，請檢查App Key是否正確！");
            }

            access_token = jd["access_token"].ToString();
            expires_in = jd["expires_in"].ToString();
            openid = jd["user"]["id"].ToString();
            //儲存獲取資料用到的資料
            Session["oauth_name"] = "renren";
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //跳轉到指定頁面
            Response.Redirect(new Web.UI.BasePage().linkurl("oauth_login"));
            return;

        }
    }
}
