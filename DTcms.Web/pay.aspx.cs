using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using DTcms.Common;
using System.Xml;

namespace DTcms.Web
{
    public partial class pay : System.Web.UI.Page
    {
        protected string paymenttype, id, ptype, webUrl = string.Empty;
        protected Model.orders model = null;
        protected Model.users User = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            webUrl = "http://" + Utils.GetHomeUrl();
            XmlDocument doc = XmlHelper.LoadXmlDoc(Utils.GetXmlMapPath("Configpathofubao"));
            string merchantid1 = doc.SelectSingleNode(@"Root/merID").InnerText;
            string HashKey = doc.SelectSingleNode(@"Root/MerchantID").InnerText;
            string HashIV = doc.SelectSingleNode(@"Root/TerminalID").InnerText;

            id = Request["id"];
            paymenttype = Request["paymenttype"];

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


            if (!string.IsNullOrEmpty(id))
            {
                BLL.orders bll = new BLL.orders();
                model = bll.GetModel(BLL.Function.Instance.StringToNum(id));


                BLL.users bllUser = new BLL.users();
                User = bllUser.GetModel(model.user_id);

                Model.order_goods Order_Info = new Model.order_goods();
                Order_Info = bll.Getorder_goodsModel(model.id); ;
            }
            string ok_urlall = string.Empty;

            ok_urlall = webUrl + "/allpay_ok.aspx"; //返回地址




            StringBuilder sb = new StringBuilder();

            StringBuilder sbHtml = new StringBuilder();

            if (!string.IsNullOrEmpty(ptype))
            {

                string merchantid = "1038690";
                merchantid = merchantid1;
                string url = "";
                //url
                sb.Append("HashKey=" + HashKey + "");//7RswcwBgLcqnMjy5
                if (ptype == "Alipay")
                {
                    sb.Append("&AlipayItemCounts=1");
                    sb.Append("&AlipayItemName=" + model.order_no + "");
                    sb.Append("&AlipayItemPrice=1");
                }
                sb.Append("&ChoosePayment=" + ptype + "");
                if (ptype == "Alipay")
                {
                    sb.Append("&Email=" + User.email + "");
                }
                sb.Append("&ItemName=" + model.order_no + "");
                sb.Append("&MerchantID=" + merchantid + "");
                string time = model.add_time.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                sb.Append("&MerchantTradeDate=" + model.add_time.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/") + "");
                sb.Append("&MerchantTradeNo=" + model.order_no + "");
                sb.Append("&OrderResultURL=" + ok_urlall + "");
                sb.Append("&PaymentType=aio");
                if (ptype == "Alipay")
                {
                    sb.Append("&PhoneNo=" + model.mobile + "");
                }
                sb.Append("&Remark=" + model.order_no + "");
                sb.Append("&ReturnURL=" + ok_urlall + "");
                sb.Append("&TotalAmount=" + model.order_amount + "");
                sb.Append("&TradeDesc=" + model.order_no + "");
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
                sb.Append("&HashIV=" + HashIV + "");//IA0pz3lNuyui52iZ
                url = sb.ToString();
                url = getstr(url).ToLower();
                sb.Append("&CheckMacValue=" + BLL.Function.Instance.MD5(url, 32) + "");
                string CheckMacValue = BLL.Function.Instance.MD5(url, 32);
                //Response.Write("url:" + url);
                //Response.Write(",");
                string savePath = Server.MapPath("PAU/");
                //if (!Directory.Exists(savePath))
                //{
                //    //需要注意的是，需要对这个物理路径有足够的权限，否则会报错 
                //    Directory.CreateDirectory(savePath);
                //    File.CreateText(savePath);
                //    File.WriteAllText(savePath, url);
                //    File.WriteAllText(savePath, "===============================================");
                //    File.WriteAllText(savePath, CheckMacValue);

                //}





                // Response.Write(CheckMacValue);


                //Atm缴费http://payment-stage.allpay.com.tw/Cashier/AioCheckOut

                //sbHtml.Append("<form id='paysubmit' name='ecbanksubmit' action='https://payment.allpay.com.tw/Cashier/AioCheckOut'  method='post'>");

                sbHtml.Append("<form id='paysubmit' name='ecbanksubmit' action=' https://payment-stage.allpay.com.tw/Cashier/AioCheckOut/V2' method='post'>");    //测试URL
                if (ptype == "Alipay")
                {
                    sbHtml.Append("<input type='hidden' name='AlipayItemCounts' value='1'/>");
                    sbHtml.Append("<input type='hidden' name='AlipayItemName' value='" + model.order_no + "'/>");
                    sbHtml.Append("<input type='hidden' name='AlipayItemPrice' value='1'/>");
                }
                sbHtml.Append("<input type='hidden' name='ChoosePayment' value='" + ptype + "'/>");
                if (ptype == "Alipay")
                {
                    sbHtml.Append("<input type='hidden' name='Email' value='" + User.email + "'/>");
                }
                sbHtml.Append("<input type='hidden' name='ItemName' value='" + model.order_no + "'/>");
                sbHtml.Append("<input type='hidden' name='MerchantID' value='" + merchantid + "'/>");
                sbHtml.Append("<input type='hidden' name='MerchantTradeDate' value='" + model.add_time.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/") + "'/>");
                sbHtml.Append("<input type='hidden' name='MerchantTradeNo' value='" + model.order_no + "'/>");
                sbHtml.Append("<input type='hidden' name='OrderResultURL' value='" + ok_urlall + "'/>");
                sbHtml.Append("<input type='hidden' name='PaymentType' value='aio'/>");
                if (ptype == "Alipay")
                {
                    sbHtml.Append("<input type='hidden' name='PhoneNo' value='" + model.mobile + "'/>");
                }
                sbHtml.Append("<input type='hidden' name='Remark' value='" + model.order_no + "'/>");
                sbHtml.Append("<input type='hidden' name='ReturnURL' value='" + ok_urlall + "'/>");
                sbHtml.Append("<input type='hidden' name='TotalAmount' value='" + model.order_amount + "'/>");
                sbHtml.Append("<input type='hidden' name='TradeDesc' value='" + model.order_no + "'/>");
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

        public string getstr(string str)
        {
            return System.Web.HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("UTF-8"));
        }
    }
}
