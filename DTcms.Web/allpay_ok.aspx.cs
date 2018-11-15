using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using DTcms.Common;
using System.Xml;

namespace DTcms.Web
{
    public partial class allpay_ok : DTcms.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            webUrl = "http://" + Utils.GetHomeUrl();
            XmlDocument doc = XmlHelper.LoadXmlDoc(Utils.GetXmlMapPath("Configpathofubao"));
            string merchantid1 = doc.SelectSingleNode(@"Root/merID").InnerText;
            string HashKey = doc.SelectSingleNode(@"Root/MerchantID").InnerText;
            string HashIV = doc.SelectSingleNode(@"Root/TerminalID").InnerText;
            string url = "";
            string mid = merchantid1;//1038690


            string MerchantID = Request["MerchantID"];
            string MerchantTradeNo = Request["MerchantTradeNo"];
            string PaymentDate = Request["PaymentDate"];
            string PaymentType = Request["PaymentType"];
            string PaymentTypeChargeFee = Request["PaymentTypeChargeFee"];
            string RtnCode = Request["RtnCode"];
            string RtnMsg = Request["RtnMsg"];
            string SimulatePaid = Request["SimulatePaid"];
            string TradeAmt = Request["TradeAmt"];
            string TradeDate = Request["TradeDate"];
            string TradeNo = Request["TradeNo"];

            string CheckMacValue = Request["CheckMacValue"];


            StringBuilder sb = new StringBuilder();
            sb.Append("HashKey=" + HashKey + "");//7RswcwBgLcqnMjy5
            sb.Append("&MerchantID=" + Request["MerchantID"] + "");
            sb.Append("&MerchantTradeNo=" + Request["MerchantTradeNo"] + "");
            sb.Append("&PaymentDate=" + Request["PaymentDate"] + "");
            sb.Append("&PaymentType=" + Request["PaymentType"] + "");
            sb.Append("&PaymentTypeChargeFee=" + Request["PaymentTypeChargeFee"] + "");
            sb.Append("&RtnCode=" + Request["RtnCode"] + "");
            sb.Append("&RtnMsg=" + Request["RtnMsg"] + "");
            sb.Append("&SimulatePaid=" + Request["SimulatePaid"] + "");
            sb.Append("&TradeAmt=" + Request["TradeAmt"] + "");
            sb.Append("&TradeDate=" + Request["TradeDate"] + "");
            sb.Append("&TradeNo=" + Request["TradeNo"] + "");
            sb.Append("&HashIV=" + HashIV + "");//IA0pz3lNuyui52iZ
            url = sb.ToString();
            url = getstr(url).ToLower();


            string CMValue = DTcms.BLL.Function.Instance.MD5(url, 32);

            // Response.Write(CMValue);
            //  Response.End();
            //  && CheckMacValue == CMValue

            if (MerchantID == mid && RtnCode == "1" && !string.IsNullOrEmpty(MerchantTradeNo) && CheckMacValue == CMValue)
            {
                BLL.orders bll_order = new BLL.orders();
                DataSet ds = bll_order.GetList(1, "order_no='" + MerchantTradeNo + "'", " id desc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int bk = bll_order.UpOrderState(MerchantTradeNo);
                    if (bk > 0)
                    {
                        setEmail();
                        //Response.Write("1|OK");
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('付款成功，將跳回首頁');window.location.href='/'</script>");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("发生错误");
                        Response.End();
                    }

                }
                else
                {
                    Response.Write("0|ErrorMessage");
                    Response.End();
                }
            }
        }
        public string getstr(string str)
        {
            return System.Web.HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("UTF-8"));
        }

        #region 發送郵件
        private void setEmail()
        {
            if (IsUserLogin())
            {
                Model.users modelUser = HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] as Model.users;
                BLL.siteconfig bll = new BLL.siteconfig();
                Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
                DTMail.sendMail(model.emailstmp, model.emailport, model.emailfrom, model.emailpassword, model.emailusername, model.emailfrom, modelUser.email, "帝光房屋精品物件購買成功通知", "恭喜您購買成功");
            }
        }
        #endregion

    }
}