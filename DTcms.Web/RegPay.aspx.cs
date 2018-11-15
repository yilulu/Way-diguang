using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Common;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class RegPay : System.Web.UI.Page
    {
        protected string paymenttype, id, ptype, webUrl = string.Empty;
        protected Model.orders model = null;
        protected Model.users User = null; string orderNo = string.Empty;
        DTcms.DAL.users daluser = new DTcms.DAL.users();
        int type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUser();
        }

        #region 激活帳戶
        public void CheckUser()
        {
            if (DTRequest.GetInt("uId", 0) != 0)
            {
                int Uid = DTRequest.GetInt("uId", 0);
                BLL.users bll = new BLL.users();
                Model.users modelUser = bll.GetModel(Uid);
                if (modelUser != null)
                {
                    modelUser.is_lock = 0;
                    if (bll.Update(modelUser))
                    {
                        ToFirstPay(modelUser);
                        //this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='index.aspx'</script>");
                    }

                }
            }

        }
        #endregion

        #region 確認註冊=================================
        protected void reg()
        {
            type = DTRequest.GetFormInt("ctl00$ContentPlaceHolder1$type");
            bool result = true;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();

            //model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.user_name = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtusername");
            model.password = DESEncrypt.Encrypt(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtpassword"));
            model.email = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtemall");
            model.nick_name = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtName");
            //if (fileUpImage.HasFile)
            //{
            //    string extendName = fileUpImage.FileName.Substring(fileUpImage.FileName.LastIndexOf('.'));
            //    string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extendName;
            //    if (!System.IO.Directory.Exists(Server.MapPath("upload/user/")))
            //    {
            //        System.IO.Directory.CreateDirectory(Server.MapPath("upload/user/"));
            //    }

            //    fileUpImage.SaveAs(Server.MapPath("upload/user/" + filename));
            //    model.avatar = filename;
            //}
            //model.sex = rblSex.SelectedValue;
            //DateTime _birthday;
            //if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            //{
            //    model.birthday = _birthday;
            //}
            model.mobile = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtphone");
            //model.qq = "";
            model.address = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtAddress");
            model.amount = 0;
            model.point = 0;
            switch (type)
            {
                case 1:
                    model.amount = 0;
                    break;
                case 2:
                    model.amount = 100;
                    break;
                case 3:
                    model.amount = 200;
                    break;
                case 4:
                    model.amount = 300;
                    break;
            }

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
                setEmail();
                Utils.WriteCookie("LoginUserID", bk.ToString());
                //ToFirstPay(bk);
                //this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功,請登入');window.location.href='login.aspx'</script>");
            }

        }
        #endregion

        #region 發送郵件
        private void setEmail()
        {
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            DTMail.sendMail(model.emailstmp, model.emailport, model.emailfrom, model.emailpassword, model.emailusername, model.emailfrom, DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtemall"), "帝光房屋會員註冊成功通知", "恭喜您成功註冊我們的會員");
        }
        #endregion

        #region 跳转支付页面
        public void ToFirstPay(Model.users model)
        {
            if (model.group_id == 5)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='index.aspx'</script>");

            }
            else
            {
                if (model.group_id == 1)
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='index.aspx'</script>");
                }
                else
                {
                    BLL.user_groups bllusergroup = new BLL.user_groups();
                    //string typeName = string.Empty;

                    //typeName = bllusergroup.GetTitle(model.group_id);


                    //string txt = "恭喜您註冊成功，您註冊的是" + typeName + "會員，若未交費，則您目前的會員級別仍為普通會員";
                    //if (model.group_id == 1)
                    //{
                    //    txt = "恭喜您註冊成功";
                    //}
                    //this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + txt + "');window.location.href='RegPay.aspx?paymenttype=" + DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlzhifu") + "'</script>");
                    ToPay(model.id);
                }

            }
        }
        #endregion

        #region 登錄=====================================
        private void login()
        {
            BLL.users bll = new BLL.users();
            var model = bll.GetModel(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtusername"), DESEncrypt.Encrypt(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$txtpassword")), 0);
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
                    if (DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlGroup") == "1")
                    {
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='userinfo.aspx'</script>");
                    }
                    else
                    {
                        BLL.user_groups bllusergroup = new BLL.user_groups();
                        string typeName = string.Empty;
                        if (!string.IsNullOrEmpty(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlGroup")))
                        {
                            typeName = bllusergroup.GetTitle(Utils.StringToNum(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlGroup")));
                        }

                        string txt = "恭喜您註冊成功，您註冊的是" + typeName + "會員，若未交費，則您目前的會員級別仍為普通會員";
                        if (DTRequest.GetFormString("ddlGroup") == "1")
                        {
                            txt = "恭喜您註冊成功";
                        }
                        //this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + txt + "');window.location.href='RegPay.aspx?paymenttype=" + DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlzhifu") + "'</script>");
                        ToPay(1);
                    }

                }
            }
        }
        #endregion

        #region 跳转支付页面=============================
        void ToPay(int Uid)
        {
            Utils.WriteCookie("LoginUserID", Uid.ToString());
            webUrl = "http://" + Utils.GetHomeUrl();
            paymenttype = "1";

            if (!string.IsNullOrEmpty(paymenttype))
            {

                if (paymenttype == "lian")
                {
                    ptype = "Credit";
                }
                if (paymenttype == "1")
                {
                    ptype = "Credit";
                }
                if (paymenttype == "2")
                {
                    ptype = "WebATM";
                }
                if (paymenttype == "")
                {
                    ptype = "ATM";
                }
                if (paymenttype == "3")
                {
                    ptype = "CVS";
                }
                if (paymenttype == "barcode")
                {
                    ptype = "BARCODE";
                }
                if (paymenttype == "alipay")
                {
                    ptype = "Alipay";
                }
                if (paymenttype == "")
                {
                    ptype = "Tenpay";
                }

            }
            else
            {
                ptype = "ALL";
            }


            string Fee = "0";
            BLL.users bllUser = new BLL.users();
            User = bllUser.GetModel(Uid);
            int Groupid = Utils.StringToNum(DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlGroup"));
            switch (Groupid)
            {
                case 2:
                    Fee = "100";
                    break;
                case 3:
                    Fee = "200";
                    break;
                case 4:
                    Fee = "300";
                    break;
            }
            if (Uid == -1)
                Uid = 1;
            orderNo = DateTime.Now.ToString("yyyyMMddhhssmm") + Uid.ToString() + Groupid.ToString();

            string ok_urlall = webUrl + "/Regpay_ok.aspx"; //返回地址



            StringBuilder sb = new StringBuilder();

            StringBuilder sbHtml = new StringBuilder();

            if (!string.IsNullOrEmpty(ptype) && User != null)
            {

                string merchantid = "1038690";
                string url = "";
                //url
                sb.Append("HashKey=7RswcwBgLcqnMjy5");
                if (ptype == "Alipay")
                {
                    sb.Append("&AlipayItemCounts=1");
                    sb.Append("&AlipayItemName=" + orderNo + "");
                    sb.Append("&AlipayItemPrice=1");
                }
                sb.Append("&ChoosePayment=" + ptype + "");
                if (ptype == "Alipay")
                {
                    sb.Append("&Email=" + User.email + "");
                }
                sb.Append("&ItemName=" + orderNo + "");
                sb.Append("&MerchantID=" + merchantid + "");
                string time = User.reg_time.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                sb.Append("&MerchantTradeDate=" + User.reg_time.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/") + "");
                sb.Append("&MerchantTradeNo=" + orderNo + "");
                sb.Append("&OrderResultURL=" + ok_urlall + "");
                sb.Append("&PaymentType=aio");
                if (ptype == "Alipay")
                {
                    sb.Append("&PhoneNo=" + User.mobile + "");
                }
                sb.Append("&Remark=" + orderNo + "");
                sb.Append("&ReturnURL=" + ok_urlall + "");
                sb.Append("&TotalAmount=" + Fee + "");
                sb.Append("&TradeDesc=" + orderNo + "");
                if (ptype == "Credit")
                {
                    if (paymenttype == "1")
                    {
                        sb.Append("&UnionPay=0");
                    }
                    if (paymenttype == "lian")
                    {
                        sb.Append("&UnionPay=1");
                    }
                }
                if (ptype == "Alipay")
                {
                    sb.Append("&UserName=" + User.user_name + "");
                }
                sb.Append("&HashIV=IA0pz3lNuyui52iZ");
                url = sb.ToString();
                url = getstr(url).ToLower();


                sb.Append("&CheckMacValue=" + BLL.Function.Instance.MD5(url, 32) + "");
                string CheckMacValue = BLL.Function.Instance.MD5(url, 32);



                // Response.Write(CheckMacValue);


                //Atm缴费http://payment-stage.allpay.com.tw/Cashier/AioCheckOut

                sbHtml.Append("<form id='paysubmit' name='ecbanksubmit' action='https://payment.allpay.com.tw/Cashier/AioCheckOut' target='_blank' method='post'>");
                if (ptype == "Alipay")
                {
                    sbHtml.Append("<input type='hidden' name='AlipayItemCounts' value='1'/>");
                    sbHtml.Append("<input type='hidden' name='AlipayItemName' value='" + orderNo + "'/>");
                    sbHtml.Append("<input type='hidden' name='AlipayItemPrice' value='1'/>");
                }
                sbHtml.Append("<input type='hidden' name='ChoosePayment' value='" + ptype + "'/>");
                if (ptype == "Alipay")
                {
                    sbHtml.Append("<input type='hidden' name='Email' value='" + User.email + "'/>");
                }
                sbHtml.Append("<input type='hidden' name='ItemName' value='" + orderNo + "'/>");
                sbHtml.Append("<input type='hidden' name='MerchantID' value='" + merchantid + "'/>");
                sbHtml.Append("<input type='hidden' name='MerchantTradeDate' value='" + User.reg_time.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/") + "'/>");
                sbHtml.Append("<input type='hidden' name='MerchantTradeNo' value='" + orderNo + "'/>");
                sbHtml.Append("<input type='hidden' name='OrderResultURL' value='" + ok_urlall + "'/>");
                sbHtml.Append("<input type='hidden' name='PaymentType' value='aio'/>");
                if (ptype == "Alipay")
                {
                    sbHtml.Append("<input type='hidden' name='PhoneNo' value='" + User.mobile + "'/>");
                }
                sbHtml.Append("<input type='hidden' name='Remark' value='" + orderNo + "'/>");
                sbHtml.Append("<input type='hidden' name='ReturnURL' value='" + ok_urlall + "'/>");
                sbHtml.Append("<input type='hidden' name='TotalAmount' value='" + Fee + "'/>");
                sbHtml.Append("<input type='hidden' name='TradeDesc' value='" + orderNo + "'/>");
                if (ptype == "Credit")
                {
                    if (paymenttype == "1")
                    {
                        sbHtml.Append("<input type='hidden' name='UnionPay' value='0'/>");
                    }
                    if (paymenttype == "lian")
                    {
                        sbHtml.Append("<input type='hidden' name='UnionPay' value='1'/>");
                    }
                }
                if (ptype == "Alipay")
                {
                    sbHtml.Append("<input type='hidden' name='UserName' value='" + User.user_name + "' />");
                }
                sbHtml.Append("<input type='hidden' name='CheckMacValue' value='" + CheckMacValue + "'/>");
                sbHtml.Append("<input type='submit=Submit' style='display:none;'></form>");
                sbHtml.Append("<script>document.forms['paysubmit'].submit();</script>");
                Response.Write(sbHtml);
                Response.End();
            }


            //atm转帐
            if (paymenttype == "atm")
            {
                Response.Redirect("ordershow.aspx?id=" + id);
            }

            //銀行匯款
            if (paymenttype == "hui")
            {
                Response.Redirect("ordershow.aspx?id=" + id);
            }

            //宅配貨到付款
            if (paymenttype == "dao")
            {
                Response.Redirect("ordershow.aspx?id=" + id);
            }

            //到店取货
            if (paymenttype == "dian")
            {
                Response.Redirect("ordershow.aspx?id=" + id);
            }

        }
        #endregion

        public string getstr(string str)
        {
            return System.Web.HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("UTF-8"));
        }
    }
}
