using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class login_vip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //登入
        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {
            //DAL.manager bll = new DAL.manager();
            //Model.manager model = bll.GetModelVIP(txtusername.Value.Trim(), DESEncrypt.Encrypt(txtpassword.Value.Trim()));
            //if (model != null)
            //{

            //    HttpCookie ccookie1 = new HttpCookie("VIPIDS", model.id.ToString());
            //    Response.Cookies.Add(ccookie1);

            //    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('登入成功');window.location.href='viplist.aspx'</script>");
            //}
            //else
            //{
            //    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('帳號或密碼錯誤')</script>");
            //}

            BLL.users bll = new BLL.users();
            var model = bll.GetModel(txtusername.Value.Trim(), DESEncrypt.Encrypt(txtpassword.Value.Trim()), 0, " and is_lock=0 AND isVip=1");
            if (model != null)
            {         
                if (model.is_lock == 1)
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('你的帳號還沒有通過審核!');window.location.href='login_vip.aspx'</script>");
                }
                else if (model.is_lock == 2)
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('你的帳號被禁用!');window.location.href='login_vip.aspx'</script>");
                }
                else
                {
                    Utils.WriteCookie("WEBUSERID", model.id.ToString(), 30);
                    Utils.WriteCookie("WEBUserNamecook", model.user_name.ToString(), 30);
                    Utils.WriteCookie("WEBRealNamecook", model.nick_name.ToString(), 30);
                    Utils.WriteCookie("WEBUserTypecook", model.group_id.ToString(), 30);
                    if (model.group_id == 5)
                    {
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('登入成功');window.location.href='viplist.aspx'</script>");
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('登入成功');window.location.href='viplist.aspx'</script>");
                    }
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('帳號或密碼錯誤')</script>");
            }
        }
    }
}
