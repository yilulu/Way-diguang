using System;
using System.Collections.Generic;
using System.Web;
using Common;
using System.Data;

namespace DTcms.Web.tools
{
    /// <summary>
    /// UserNeed 的摘要说明
    /// </summary>
    public class UserNeed : IHttpHandler
    {
        Ltf.DAL.dt_user_need dal = new Ltf.DAL.dt_user_need();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = HttpContext.Current.Request.QueryString["action"];
            switch (action)
            {
                case "add":
                    Add();
                    break;
                case "List":
                    List();
                    break;
            }

        }
        protected void List()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["id"]))
            {
                string UserId = HttpContext.Current.Request.QueryString["id"];

                DataTable dt = dal.GetList(" UserID=" + UserId + "").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string HtmlValue = dt.Rows[0]["content"].ToString();
                    HttpContext.Current.Response.Write(HtmlValue);
                }
            }
        }

        protected void Add()
        {
            string Data = HttpContext.Current.Request.QueryString["options"];
            DateTime dtTime = DateTime.Now;
            int UserID = WEBUserCurrent.UserID;

            Ltf.Model.dt_user_need mod = new Ltf.Model.dt_user_need();
            mod.Content = Data;
            mod.UserID = UserID;
            mod.AddTime = dtTime;



            int bk = dal.Add(mod);
            if (bk > 0)
            {
                HttpContext.Current.Response.Write(bk);
            }
            else
            {
                HttpContext.Current.Response.Write(0);
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