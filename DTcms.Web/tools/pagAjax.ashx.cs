using System;
using System.Collections.Generic;
using System.Web;
using DTcms.DAL;
using System.Text;

namespace DTcms.Web.tools
{
    /// <summary>
    /// pagAjax 的摘要說明
    /// </summary>
    public class pagAjax : IHttpHandler
    {

        AreaDal dalAre = new AreaDal();
        public void ProcessRequest(HttpContext context)
        {
            int id = int.Parse(context.Request.QueryString["id"]);
            int count = 0;
            var table = dalAre.GetDatalistpage(999999999, 1, " parent=" + id + "", "id", out count).Tables[0];
            StringBuilder html = new StringBuilder();
            html.Append("<div class=\"qingxuanze\"><span onclick=\"HideValue(2)\" class=\"close\"><a href=\"#\">關閉</a></span>區域選擇</div>");
            html.Append("<ul>");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                html.Append("<li class=\"lis\"><a href='javascript:void(0)' data='" + table.Rows[i]["id"] + "' onclick='getsCounry(this,2," + table.Rows[i]["id"] + ")'>" + table.Rows[i]["title"] + "</a></li>");
            }
            html.Append("</ul>");
            context.Response.ContentType = "text/plain";
            context.Response.Write(html.ToString());
            context.Response.End();
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
