using System;
using System.Collections.Generic;
using System.Web;
using DTcms.Common;
using Common;

namespace DTcms.Web.Common
{
    /// <summary>
    /// Vip_Down 的摘要说明
    /// </summary>
    public class Vip_Down : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            delSummary();
        }

        #region 删除檔案
        public void delSummary()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Sid"]))
            {
                int ID = Utils.StringToNum(HttpContext.Current.Request.Form["Sid"]);
                BLL.article bll = new BLL.article();
                if (bll.Delete(ID))
                {
                    HttpContext.Current.Response.Write("1");
                }
                else {
                    HttpContext.Current.Response.Write("0");
                }
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}