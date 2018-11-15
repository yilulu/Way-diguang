using System;
using System.Collections.Generic;
using System.Web;

namespace DTcms.Web.tools
{
    /// <summary>
    /// regChk 的摘要说明
    /// </summary>
    public class regChk : IHttpHandler
    {
        BLL.users bllUSER = new BLL.users();
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];

            context.Response.ContentType = "text/plain";
            switch (action)
            {
                case "userName":
                    chkUserNmae();
                    break;
                case "Email":
                    chkEmail();
                    break;
            }


        }
        public void chkUserNmae()
        {
           
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["name"]))
            {
                string UserName = HttpContext.Current.Request.QueryString["name"];
                if (bllUSER.Exists(UserName))
                {
                    HttpContext.Current.Response.Write("1");
                }
                else
                {
                    HttpContext.Current.Response.Write("0");
                }
            }
            else
            {
                HttpContext.Current.Response.Write("1");
            }

        }
        public void chkEmail()
        {
            string UserName = HttpContext.Current.Request.QueryString["name"];
            if (bllUSER.ExistsEmail(UserName))
            {
                HttpContext.Current.Response.Write("1");
            }
            else
            {
                HttpContext.Current.Response.Write("0");
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}