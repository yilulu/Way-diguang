using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Security.Cryptography;
using System.Text;

namespace DTcms.Web.admin.users
{
    public partial class RePw : Web.UI.ManagePage
    {
        string ReferUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ReferUrl = DTRequest.GetQueryString("Url");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string userPwd = TextBox1.Text.Trim();
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));

            if (model.weburl == userPwd)
            {
                Utils.WriteCookie("UserCheckPwd", userPwd, 60);
                Response.Redirect(ReferUrl);
            }
            else
            {
                JscriptMsg("會員管理密碼錯誤！", "RePw.aspx", "Success");
            }
        }


    }
}