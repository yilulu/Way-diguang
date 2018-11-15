using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Model;
using DTcms.Common;
using System.Data;
using System.Text;
using DTcms.Web.UI;

namespace DTcms.Web
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        DTcms.DAL.users daluser = new DTcms.DAL.users();
        int type = 0; public string address = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 防止表单重复提交
            //sb保存的是JavaScript脚本代码,点击提交按钮时执行该脚本
            StringBuilder sb = new StringBuilder();
            //保证验证函数的执行 
            sb.Append("if (typeof(Page_ClientValidate) == 'function') { if (Page_ClientValidate() == false) { return false; }};");
            //点击提交按钮后设置Button的disable属性防止用户再次点击,注意这里的this是JavaScript代码
            sb.Append("this.disabled  = true;");
            //用__doPostBack来提交，保证按钮的服务器端click事件执行 
            sb.Append(this.ClientScript.GetPostBackEventReference(this.btnlogin, ""));
            sb.Append(";");
            //SetUIStyle()是JavaScript函数,点击提交按钮后执行,如可以显示动画效果提示后台处理进度
            //注意SetUIStyle()定义在aspx页面中
            sb.Append("SetUIStyle();");
            //给提交按钮增加OnClick属性
            this.btnlogin.Attributes.Add("onclick", sb.ToString());
            #endregion
            type = DTRequest.GetQueryInt("type");
            if (!IsPostBack)
            {
                TreeBind("is_lock=0"); //綁定類別
            }

            ddlGroup.SelectedValue = type.ToString();

            note.Value = "帝光會員優惠:";
        }

        #region 綁定類別=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "grade asc,id asc").Tables[0];

            this.ddlGroup.Items.Clear();
            this.ddlGroup.Items.Add(new ListItem("請選擇組別...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroup.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 確認註冊=================================
        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {
            bool result = true;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();

            model.user_name = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtusername");
            model.password = DESEncrypt.Encrypt(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtpassword"));
            model.email = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtemall");
            model.nick_name = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtName");
            if (fileUpImage.HasFile)
            {
                string extendName = fileUpImage.FileName.Substring(fileUpImage.FileName.LastIndexOf('.'));
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extendName;
                if (!System.IO.Directory.Exists(Server.MapPath("upload/user/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("upload/user/"));
                }

                fileUpImage.SaveAs(Server.MapPath("upload/user/" + filename));
                model.avatar = filename;
            }
            //model.sex = rblSex.SelectedValue;
            //DateTime _birthday;
            //if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            //{
            //    model.birthday = _birthday;
            //}
            model.telphone = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtTel");
            model.mobile = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtphone");
            //model.qq = "";
            model.address = model.address = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtcity") + "|" + DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtcity1") + "|" + txtZip.Text + "|" + DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtAddress"); ;
            model.amount = 0;
            model.point = 0;
            //switch (type)
            //{
            //    case 1:
            //        model.amount = 0;
            //        break;
            //    case 2:
            //        model.amount = 100;
            //        break;
            //    case 3:
            //        model.amount = 200;
            //        break;
            //    case 4:
            //        model.amount = 300;
            //        break;
            //}

            model.exp = 0;
            model.reg_time = DateTime.Now;
            model.reg_ip = DTRequest.GetIP();

            //if (ddlGroup.SelectedValue == "0")
            //{
            //    model.group_id = 1;
            //}
            //else
            //{
            model.group_id = Utils.StringToNum(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlGroup"));
            model.dianming = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtIntroduce");
            //model.dianmiaoshu = dianmiaoshu.Value;
            //model.congye = congye.Value;
            model.gongsi = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$gongsi");
            model.fuwuquyu = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$CompanyName");
            //model.fuwuquyu = fuwuquyu.Value;
            //model.shuxishequ = shuxishequ.Value;
            //model.fuwutechang = fuwutechang.Value;
            //model.jingli = jingli.Value;
            //model.zhengshu = zhengshu.Value;
            model.note = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$note");
            model.is_lock = 1;
            //}
            int bk = bll.Add(model);
            if (bk < 1)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('網路異常，請重試')</script>");
            }
            else
            {
                setEmail(bk);
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊完成，請查收電子郵件並依照步驟完成帳戶啟動');window.location.href='index.aspx'</script>");
            }

        }
        #endregion

        #region 登錄=====================================
        private void login()
        {
            BLL.users bll = new BLL.users();
            var model = bll.GetModel(txtusername.Value.Trim(), DESEncrypt.Encrypt(txtpassword.Value.Trim()), 0);
            if (model != null)
            {
                HttpCookie ccookie1 = new HttpCookie("WEBUSERID", model.id.ToString());
                HttpCookie ccookie2 = new HttpCookie("WEBUserNamecook", model.user_name.ToString());
                HttpCookie ccookie3 = new HttpCookie("WEBRealNamecook", model.nick_name.ToString());
                HttpCookie ccookie4 = new HttpCookie("WEBUserTypecook", model.group_id.ToString());
                Response.Cookies.Add(ccookie1);
                Response.Cookies.Add(ccookie2);
                Response.Cookies.Add(ccookie3);
                Response.Cookies.Add(ccookie4);
                if (model.group_id == 5)
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='userSJ.aspx'</script>");

                }
                else
                {
                    if (ddlGroup.SelectedValue == "1")
                    {
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='userinfo.aspx'</script>");
                    }
                    else
                    {
                        BLL.user_groups bllusergroup = new BLL.user_groups();
                        string typeName = string.Empty;
                        if (!string.IsNullOrEmpty(ddlGroup.SelectedValue))
                        {
                            typeName = bllusergroup.GetTitle(Utils.StringToNum(ddlGroup.SelectedValue));
                        }

                        string txt = "恭喜您註冊成功，您註冊的是" + typeName + "會員，若未交費，則您目前的會員級別仍為普通會員";
                        if (ddlGroup.SelectedValue == "1")
                        {
                            txt = "恭喜您註冊成功";
                        }
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + txt + "');window.location.href='RegPay.aspx?paymenttype=" + ddlzhifu.SelectedValue + "'</script>");
                    }

                }
            }
        }
        #endregion

        #region 返回级联数据
        public string getcity(int i, string adr)
        {
            string str = "";
            try
            {
                str = adr.Split('|')[i].ToString();
            }
            catch (Exception eee) { }
            return str;

        }
        #endregion

        #region 發送郵件
        private void setEmail(int UserID)
        {
            //生成隨機碼
            string strcode = Utils.GetCheckCode(20);
            //獲得郵件內容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("regverify");
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", model.webname);
            titletxt = titletxt.Replace("{username}", DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtusername"));
            bodytxt = bodytxt.Replace("{webname}", model.webname);
            bodytxt = bodytxt.Replace("{username}", DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtusername"));
            bodytxt = bodytxt.Replace("{linkurl}", "<a href=" + "http://" + Utils.GetHomeUrl() + "/RegPay.aspx?uId=" + UserID + ">請點擊連接激活帳戶" + "</a>"); //此處需要修改
            DTMail.sendMail(model.emailstmp, model.emailport, model.emailfrom, model.emailpassword, model.emailusername, model.emailfrom, DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtemall"), "帝光房屋會員註冊成功通知", bodytxt);
        }
        #endregion

    }
}
