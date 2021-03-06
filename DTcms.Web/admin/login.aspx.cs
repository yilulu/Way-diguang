﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Security.Cryptography;
using System.Text;

namespace DTcms.Web.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUserName.Text = Utils.GetCookie("DTRememberName");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userPwd = txtUserPwd.Text.Trim();
            string code = txtCode.Text.Trim();

            if (userName.Equals("") || userPwd.Equals(""))
            {
                lblTip.Visible = true;
                lblTip.Text = "請輸入用戶名或密碼";
                return;
            }
            if (code.Equals(""))
            {
                lblTip.Visible = true;
                lblTip.Text = "請輸入驗證碼";
                return;
            }
            if (Session[DTKeys.SESSION_CODE] == null)
            {
                lblTip.Visible = true;
                lblTip.Text = "系統找不到驗證碼";
                return;
            }
            if (code.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
            {
                lblTip.Visible = true;
                lblTip.Text = "驗證碼輸入不正確";
                return;
            }
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(userName, DESEncrypt.Encrypt(userPwd));
            if (model == null)
            {
                lblTip.Visible = true;
                lblTip.Text = "用戶名或密碼錯誤";
                return;
            }
            Session[DTKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
            //寫入登入日誌
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            if (siteConfig.logstatus > 0)
            {
                Model.manager_log modelLog = new Model.manager_log();
                modelLog.user_id = model.id;
                modelLog.user_name = model.user_name;
                modelLog.action_type = "login";
                modelLog.note = "用戶登入";
                modelLog.login_ip = DTRequest.GetIP();
                modelLog.login_time = DateTime.Now;
                new BLL.manager_log().Add(modelLog);
            }
            //寫入Cookies
            if (cbRememberId.Checked)
            {
                Utils.WriteCookie("DTRememberName", model.user_name, 14400);
            }
            else
            {
                Utils.WriteCookie("DTRememberName", model.user_name, -14400);
            }
            Utils.WriteCookie("AdminName", "DTcms", model.user_name);
            Utils.WriteCookie("AdminPwd", "DTcms", model.user_pwd);
            Response.Redirect("index.aspx");
            return;
        }
    }
}
