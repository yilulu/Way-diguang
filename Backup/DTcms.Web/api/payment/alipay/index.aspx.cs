using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.API.Payment.Alipay;

namespace DTcms.Web.api.payment.alipay
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //讀取網站配置資料
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);

            //獲得訂單資料
            string order_type = DTRequest.GetFormString("pay_order_type"); //訂單類型
            string order_no = DTRequest.GetFormString("pay_order_no");
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0);
            string user_name = DTRequest.GetFormString("pay_user_name");
            string subject = DTRequest.GetFormString("pay_subject");
            if (order_type == "" || order_no == "" || order_amount == 0 || user_name == "")
            {
                Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("對不起，您送出的參數有誤！"));
                return;
            }


            ////////////////////////////////////////////請求參數////////////////////////////////////////////

            //必填參數//

            //請與貴網站訂單系統中的唯一訂單號匹配
            string out_trade_no = order_no;
            //訂單名稱，顯示在支付寶收銀台裡的“商品名稱”裡，顯示在支付寶的交易管理的“商品名稱”的列表裡。
            string _subject = siteConfig.webname + "-" + subject;
            //訂單描述、訂單詳細、訂單備註，顯示在支付寶收銀台裡的“商品描述”裡
            string body = "支付會員：" + user_name;
            //訂單總金額，顯示在支付寶收銀台裡的“應付總額”裡
            string total_fee = order_amount.ToString();


            //擴展功能參數——預設支付方式//

            //預設支付方式，代碼見“即時到帳介面”技術文檔
            string paymethod = "";
            //默認網銀代號，代號列表見“即時到帳介面”技術文檔“附錄”→“銀行列表”
            string defaultbank = "";

            //擴展功能參數——防釣魚//

            //防釣魚時間戳記
            string anti_phishing_key = "";
            //獲取用戶端的IP地址，建議：編寫獲取用戶端IP位址的程式
            string exter_invoke_ip = "";

            //擴展功能參數——其他//

            //商品展示地址，要用http:// 格式的完整路徑，不允許加?id=123這類自訂參數
            string show_url = siteConfig.weburl;
            //自訂參數，可存放任何內容（除=、&等特殊字元外），不會顯示在頁面上
            string extra_common_param = order_type;
            //默認買家支付寶帳號
            string buyer_email = "";
            string royalty_type = "";
            string royalty_parameters = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把請求參數打包成陣列
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", _subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("extra_common_param", extra_common_param);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            //構造即時到帳介面表單送出HTML資料，無需修改
            Service ali = new Service();
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
            Response.Write(sHtmlText);
        }
    }
}
