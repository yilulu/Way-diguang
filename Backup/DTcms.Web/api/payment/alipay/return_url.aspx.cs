using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.API.Payment.Alipay;

namespace DTcms.Web.api.payment.alipay
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //讀取網站配置資訊
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判斷是否有帶返回參數
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, DTRequest.GetString("notify_id"), DTRequest.GetString("sign"));

                if (verifyResult)//驗證成功
                {
                    //——請根據您的業務邏輯來編寫程式（以下代碼僅作參考）——
                    //獲取付款寶的通知返回參數，可參考技術文檔中頁面跳轉同步通知參數列表
                    string trade_no = DTRequest.GetString("trade_no");              //付款寶交易號
                    string order_no = DTRequest.GetString("out_trade_no");	        //獲取訂單號
                    string total_fee = DTRequest.GetString("total_fee");            //獲取總金額
                    string subject = DTRequest.GetString("subject");                //商品名稱、訂單名稱
                    string body = DTRequest.GetString("body");                      //商品描述、訂單備註、描述
                    string buyer_email = DTRequest.GetString("buyer_email");        //買家付款寶帳號
                    string trade_status = DTRequest.GetString("trade_status");      //交易狀態
                    string order_type = DTRequest.GetString("extra_common_param");  //訂單交易類別

                    if (DTRequest.GetString("trade_status") == "TRADE_FINISHED" || DTRequest.GetString("trade_status") == "TRADE_SUCCESS")
                    {
                        //成功狀態
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment1", "succeed", order_type, order_no));
                        return;
                    }
                }
            }
            //失敗狀態
            Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
            return;
        }

        /// <summary>
        /// 獲取付款寶GET過來通知消息，並以“參數名=參數值”的形式組成陣列
        /// </summary>
        /// <returns>request回來的資訊組成的陣列</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = Request.QueryString;

            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

    }
}
