using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.Alipay;
using DTcms.Common;

namespace DTcms.Web.api.payment.alipay
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判斷是否有帶返回參數
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, DTRequest.GetString("notify_id"), DTRequest.GetString("sign"));

                if (verifyResult)//驗證成功
                {
                    string trade_no = DTRequest.GetString("trade_no");         //支付寶交易號
                    string order_no = DTRequest.GetString("out_trade_no");     //獲取訂單號
                    string total_fee = DTRequest.GetString("total_fee");       //獲取總金額
                    string subject = DTRequest.GetString("subject");           //商品名稱、訂單名稱
                    string body = DTRequest.GetString("body");                 //商品描述、訂單備註、描述
                    string buyer_email = DTRequest.GetString("buyer_email");   //買家支付寶帳號
                    string trade_status = DTRequest.GetString("trade_status"); //交易狀態
                    string order_type = DTRequest.GetString("extra_common_param"); //訂單交易類別

                    if (DTRequest.GetString("trade_status") == "TRADE_FINISHED" || DTRequest.GetString("trade_status") == "TRADE_SUCCESS")
                    {
                        //修改付款狀態、時間
                        if (order_type.ToLower() == DTEnums.AmountTypeEnum.Recharge.ToString().ToLower()) //線上儲值
                        {
                            BLL.amount_log bll = new BLL.amount_log();
                            Model.amount_log model = bll.GetModel(order_no);
                            if (model == null)
                            {
                                Response.Write("該訂單號不存在");
                                return;
                            }
                            if (model.value != decimal.Parse(total_fee))
                            {
                                Response.Write("訂單金額和付款金額不相符");
                                return;
                            }
                            model.status = 1;
                            model.complete_time = DateTime.Now;
                            bool result = bll.Update(model);
                            if (!result)
                            {
                                Response.Write("修改訂單狀態失敗");
                                return;
                            }
                        }
                        else if (order_type.ToLower() == DTEnums.AmountTypeEnum.BuyGoods.ToString().ToLower()) //購買商品
                        {
                            BLL.orders bll = new BLL.orders();
                            Model.orders model = bll.GetModel(order_no);
                            if (model == null)
                            {
                                Response.Write("該訂單號不存在");
                                return;
                            }
                            if (model.order_amount != decimal.Parse(total_fee))
                            {
                                Response.Write("訂單金額和付款金額不相符");
                                return;
                            }
                            bool result = bll.UpdateField(order_no, "payment_status=2,payment_time='" + DateTime.Now + "'");
                            if (!result)
                            {
                                Response.Write("修改訂單狀態失敗");
                                return;
                            }
                            //扣除積分
                            if (model.point < 0)
                            {
                                new BLL.point_log().Add(model.user_id, model.user_name, model.point, "換購扣除積分，訂單號：" + model.order_no);
                            }
                        }
                    }

                    Response.Write("success");  //請不要修改或刪除
                }
                else//驗證失敗
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("無通知參數");
            }
        }

        /// <summary>
        /// 獲取支付寶POST過來通知消息，並以“參數名=參數值”的形式組成陣列
        /// </summary>
        /// <returns>request回來的資訊組成的陣列</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = Request.Form;

            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

    }
}
