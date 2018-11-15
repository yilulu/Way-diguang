using System;
using System.Collections.Generic;
using System.Web;
using DTcms.DAL;
using System.Text;
using System.Data;

namespace DTcms.Web.tools
{
    /// <summary>
    /// pagAjax 的摘要說明
    /// </summary>
    public class HtmlAjax : IHttpHandler
    {

        AreaDal dalArea = new AreaDal();
        public void ProcessRequest(HttpContext context)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                id = int.Parse(context.Request.Form["id"]);
            }


            BLL.AreaBll cb = new BLL.AreaBll();
            StringBuilder sb = new StringBuilder();
            List<Model.Area> cm = cb.CityList(id);
            sb.Append("[");
            if (cm.Count > 0)
            {
                for (int i = 0; i < cm.Count; i++)
                {
                    Model.Area model = cm[i];
                    sb.Append("{");
                    sb.AppendFormat(@"""c_name"":""{0}"",", model.title);
                    sb.AppendFormat(@"""c_code"":""{0}""", model.id);
                    sb.Append("}");
                    if (i < cm.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
            }
            sb.Append("]");
            // System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
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
