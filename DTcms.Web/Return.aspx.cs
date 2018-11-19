using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using Common;

namespace DTcms.Web
{
    public partial class Return : PageBase
    {
        protected string paymenttype, id, ptype;
        protected Model.orders model = null;
        protected Model.users User = null;
        protected void Page_Load(object sender, EventArgs e)
        {
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

            ok_urlall = "http://diguang.myflysoft.com/allpay_ok.aspx"; //返回地址




            StringBuilder sb = new StringBuilder();

            StringBuilder sbHtml = new StringBuilder();

            if (!string.IsNullOrEmpty(ptype))
            {

                string merchantid = "2000132";
                string url = "";
                //url
                sb.Append("HashKey=5294y06JbISpM5x9");
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
                sb.Append("&HashIV=v77hoKGq4kWxNNIS");
                url = sb.ToString();
                url = getstr(url).ToLower();
                sb.Append("&CheckMacValue=" + BLL.Function.Instance.MD5(url, 32) + "");
                string CheckMacValue = BLL.Function.Instance.MD5(url, 32);
                Response.Write("url:" + url);
                Response.Write(",");
                Response.Write(CheckMacValue);



                //Atm缴费http://payment-stage.allpay.com.tw/Cashier/AioCheckOut



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
        }
        public string getstr(string str)
        {
            return System.Web.HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("UTF-8"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("pay.aspx?paymenttype=" + paymenttype + "&id=" + id + "");
        }
    }
}
