using System;
using System.Collections.Generic;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    /// <summary>
    /// chkManagePwd 的摘要说明
    /// </summary>
    public class chkManagePwd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["name"]))
            {
                string UserName = HttpContext.Current.Request.QueryString["name"];
                BLL.siteconfig bll = new BLL.siteconfig();
                Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
                if (model.weburl == DESEncrypt.Encrypt(UserName))
                {
                    context.Response.Write(1);
                }
                else
                {
                    context.Response.Write("0");
                }
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