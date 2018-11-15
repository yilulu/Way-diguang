using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.Tenpay;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpay
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //創建ResponseHandler實例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(TenpayUtil.tenpay_key);

            //判斷簽名
            if (resHandler.isTenpaySign())
            {
                //通知id
                string notify_id = resHandler.getParameter("notify_id");
                //商戶訂單號
                string out_trade_no = resHandler.getParameter("out_trade_no");
                //財付通訂單號
                string transaction_id = resHandler.getParameter("transaction_id");
                //金額,以分為單位
                string total_fee = resHandler.getParameter("total_fee");
                //如果有使用折扣券，discount有值，total_fee+discount=原請求的total_fee
                string discount = resHandler.getParameter("discount");
                //付款結果
                string trade_state = resHandler.getParameter("trade_state");
                //交易模式，1即時到賬，2仲介擔保
                string trade_mode = resHandler.getParameter("trade_mode");
                //訂單類型
                string order_type = resHandler.getParameter("attach");

                if ("1".Equals(trade_mode))
                {       //即時到賬 
                    if ("0".Equals(trade_state))
                    {
                        //給財付通系統發送成功資訊，財付通系統收到此結果後不再進行後續通知
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment1", "succeed", order_type, out_trade_no));
                        return;
                    }
                    else
                    {
                        //失敗狀態
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
                        return;
                    }
                }
            }
            else
            {
                //認證簽名失敗
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
            }

        }
    }
}
