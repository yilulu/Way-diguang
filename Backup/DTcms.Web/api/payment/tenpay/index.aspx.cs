using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.Tenpay;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpay
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //讀取網站配置資訊
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);
            //獲得訂單資訊
            string order_type = DTRequest.GetFormString("pay_order_type"); //訂單類型
            string order_no = DTRequest.GetFormString("pay_order_no"); //訂單號
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0); //訂單金額
            string user_name = DTRequest.GetFormString("pay_user_name"); //付款用戶名
            string subject = DTRequest.GetFormString("pay_subject"); //備註說明

            if (order_type == "" || order_no == "" || order_amount == 0 || user_name == "")
            {
                Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("對不起，您提交的參數有誤！"));
                return;
            }

            ////////////////////////////////////////////請求參數////////////////////////////////////////////
            //創建RequestHandler實例
            RequestHandler reqHandler = new RequestHandler(Context);
            //初始化
            reqHandler.init();
            //設置金鑰
            reqHandler.setKey(TenpayUtil.tenpay_key);
            reqHandler.setGateUrl("https://gw.tenpay.com/gateway/pay.htm");
            //-----------------------------
            //設置付款參數
            //-----------------------------
            reqHandler.setParameter("partner", TenpayUtil.bargainor_id);		        //商戶號
            reqHandler.setParameter("out_trade_no", order_no);		//商家訂單號
            reqHandler.setParameter("total_fee", (Convert.ToDouble(order_amount) * 100).ToString());			        //商品金額,以分為單位
            reqHandler.setParameter("return_url", TenpayUtil.tenpay_return);		    //交易完成後跳轉的URL
            reqHandler.setParameter("notify_url", TenpayUtil.tenpay_notify);		    //接收財付通通知的URL
            reqHandler.setParameter("body", "付款會員：" + user_name);	                    //商品描述
            reqHandler.setParameter("bank_type", "DEFAULT");		    //銀行類型(仲介擔保時此參數無效)
            reqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress);   //用戶的公網ip，不是商戶伺服器IP
            reqHandler.setParameter("fee_type", "1");                    //幣種，1人民幣
            reqHandler.setParameter("subject", siteConfig.webname + "-" + subject);              //商品名稱(仲介交易時必填)


            //系統可選參數
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("service_version", "1.0");
            reqHandler.setParameter("input_charset", "UTF-8");
            reqHandler.setParameter("sign_key_index", "1");

            //業務可選參數

            reqHandler.setParameter("attach", order_type);               //附加資料，原樣返回
            reqHandler.setParameter("product_fee", "0");                 //商品費用，必須保證transport_fee + product_fee=total_fee
            reqHandler.setParameter("transport_fee", "0");               //物流費用，必須保證transport_fee + product_fee=total_fee
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));            //訂單生成時間，格式為yyyymmddhhmmss
            reqHandler.setParameter("time_expire", "");                 //訂單失效時間，格式為yyyymmddhhmmss
            reqHandler.setParameter("buyer_id", "");                    //買方財付通帳號
            reqHandler.setParameter("goods_tag", "");                   //商品標記
            reqHandler.setParameter("trade_mode", "1");                 //交易模式，1即時到賬(默認)，2仲介擔保，3後臺選擇（買家進付款中心列表選擇）
            reqHandler.setParameter("transport_desc", "");              //物流說明
            reqHandler.setParameter("trans_type", "1");                  //交易類型，1實物交易，2虛擬交易
            reqHandler.setParameter("agentid", "");                     //平臺ID
            reqHandler.setParameter("agent_type", "");                  //代理模式，0無代理(預設)，1表示卡易售模式，2表示網店模式
            reqHandler.setParameter("seller_id", "");                   //賣家商戶號，為空則等同於partner

            //獲取請求帶參數的url
            string requestUrl = reqHandler.getRequestURL();

            /*Get的實現方式
            string a_link = "<a target=\"_blank\" href=\"" + requestUrl + "\">" + "財付通付款" + "</a>";
            Response.Write(a_link);*/

            //實現自動跳轉===============================
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='tenpaysubmit' name='tenpaysubmit' action='" + reqHandler.getGateUrl() + "' method='get'>");
            Hashtable ht = reqHandler.getAllParameters();
            foreach (DictionaryEntry de in ht)
            {
                sbHtml.Append("<input type=\"hidden\" name=\"" + de.Key + "\" value=\"" + de.Value + "\" >\n");
            }
            //submit按鈕控制項請不要含有name屬性
            sbHtml.Append("<input type='submit' value='確認' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['tenpaysubmit'].submit();</script>");

            Response.Write(sbHtml.ToString());


        }
    }
}
