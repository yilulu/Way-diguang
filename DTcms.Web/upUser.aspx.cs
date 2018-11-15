using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class upUser : System.Web.UI.Page
    {
        protected string paymenttype, id, ptype, webUrl = string.Empty;
        protected Model.orders model = null;
        protected Model.users User = null; string orderNo = string.Empty;
        DTcms.DAL.users daluser = new DTcms.DAL.users();
        protected void Page_Load(object sender, EventArgs e)
        {
            ToPay();
        }

        #region 跳转支付页面=============================
        void ToPay()
        {
            webUrl = "http://" + Utils.GetHomeUrl();
            paymenttype = DTRequest.GetFormString("ctl00$ContentPlaceHolder1$ddlzhifu");

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
            int Uid = WEBUserCurrent.UserID;
            string Fee = "0";
            BLL.users bllUser = new BLL.users();
            User = bllUser.GetModel(Uid);
            string Groupid = string.Empty;
            if (!string.IsNullOrEmpty(DTRequest.GetQueryString("gid")))
            {
                Groupid = DTRequest.GetQueryString("gid");
            }
            switch (Utils.StrToInt(Groupid, 0))
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
            orderNo = DateTime.Now.ToString("yyyyMMddhhssmm") + Uid.ToString() + Groupid;

            string ok_urlall = webUrl + "/Regpay_ok.aspx"; //返回地址



            StringBuilder sb = new StringBuilder();

            StringBuilder sbHtml = new StringBuilder();

            if (!string.IsNullOrEmpty(ptype))
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