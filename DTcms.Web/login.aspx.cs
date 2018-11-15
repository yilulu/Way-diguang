using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //登入
        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {
            BLL.users bll = new BLL.users();
            var model = bll.GetModel(txtusername.Value.Trim(), DESEncrypt.Encrypt(txtpassword.Value.Trim()), 0);
            if (model != null)
            {
                if (model.is_lock == 1)
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('你的帳號還沒有通過審核!');window.location.href='login.aspx'</script>");
                }
                else if (model.is_lock == 2)
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('你的帳號被禁用!');window.location.href='login.aspx'</script>");
                }
                else
                {
                    Utils.WriteCookie("WEBUSERID", model.id.ToString(), 30);
                    Utils.WriteCookie("WEBUserNamecook", model.user_name.ToString(), 30);
                    Utils.WriteCookie("WEBRealNamecook", model.nick_name.ToString(), 30);
                    Utils.WriteCookie("WEBUserTypecook", model.group_id.ToString(), 30);
                    if (model.group_id == 5)
                    {
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('登入成功');window.location.href='userlook.aspx?id=" + model.id + "'</script>");
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('登入成功');window.location.href='userinfo.aspx'</script>");
                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('帳號或密碼錯誤')</script>");
            }
        }

        //找回密碼
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DAL.users bll = new DAL.users();
            var model = bll.ExistsByUsernameAndEmail(txtusername2.Value.Trim(), txtemail.Value);
            if (model.Rows.Count > 0)
            {
                var password = DESEncrypt.Decrypt(model.Rows[0]["password"].ToString());
                BLL.siteconfig bllConfig = new BLL.siteconfig();
                Model.siteconfig config = bllConfig.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));

                string body = model.Rows[0]["user_name"] + "您好,請確認您的密碼:&nbsp;" + password + "&nbsp;請牢記您的密碼";
                // var reWrite = SendMail.Mail(site.emailfrom, site.emailusername, txtemail.Value.Trim(), "會員找回密碼操作", body, site.emailusername, site.emailpassword, site.emailstmp, "");

                var reWrite = DTMail.sendMail(config.emailstmp, config.emailport, config.emailfrom, config.emailpassword, config.emailusername, config.emailfrom, txtemail.Value.Trim(), "會員找回密碼操作", body);
                if (reWrite.Equals("發送失敗"))
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('發送郵件失敗，請重新發送');window.location.href='login.aspx?type=1'</script>");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('發送郵件成功，請查看郵件');window.location.href='login.aspx'</script>");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('用戶名或郵箱不存在');window.location.href='login.aspx?type=1'</script>");
            }

        }
    }
}
