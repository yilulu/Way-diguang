using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.Tenpay;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpay
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //創建ResponseHandler實例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(TenpayUtil.tenpay_key);

            //判斷簽名
            if (resHandler.isTenpaySign())
            {
                ///通知id
                string notify_id = resHandler.getParameter("notify_id");
                //通過通知ID查詢，確保通知來至財付通
                //創建查詢請求
                RequestHandler queryReq = new RequestHandler(Context);
                queryReq.init();
                queryReq.setKey(TenpayUtil.tenpay_key);
                queryReq.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
                queryReq.setParameter("partner", TenpayUtil.bargainor_id);
                queryReq.setParameter("notify_id", notify_id);

                //通信物件
                TenpayHttpClient httpClient = new TenpayHttpClient();
                httpClient.setTimeOut(5);
                //設置請求內容
                httpClient.setReqContent(queryReq.getRequestURL());
                //後臺調用
                if (httpClient.call())
                {
                    //設置結果參數
                    ClientResponseHandler queryRes = new ClientResponseHandler();
                    queryRes.setContent(httpClient.getResContent());
                    queryRes.setKey(TenpayUtil.tenpay_key);
                    //判斷簽名及結果
                    //只有簽名正確,retcode為0，trade_state為0才是付款成功
                    if (queryRes.isTenpaySign())
                    {
                        //取結果參數做業務處理
                        string out_trade_no = resHandler.getParameter("out_trade_no");
                        //財付通訂單號
                        string transaction_id = resHandler.getParameter("transaction_id");
                        //金額,以分為單位
                        string total_fee = resHandler.getParameter("total_fee");
                        //如果有使用折扣券，discount有值，total_fee+discount=原請求的total_fee
                        string discount = resHandler.getParameter("discount");
                        //訂單類型
                        string order_type = resHandler.getParameter("attach");
                        //付款結果
                        string trade_state = resHandler.getParameter("trade_state");
                        //交易模式，1即時到帳 2仲介擔保
                        string trade_mode = resHandler.getParameter("trade_mode");
                        #region
                        //判斷簽名及結果
                        if ("0".Equals(queryRes.getParameter("retcode")))
                        {
                            //Response.Write("id驗證成功");

                            if ("1".Equals(trade_mode))
                            {       //即時到賬 
                                if ("0".Equals(trade_state))
                                {
                                    //------------------------------
                                    //即時到賬處理業務開始
                                    //------------------------------
                                    //處理資料庫邏輯
                                    //注意交易單不要重複處理
                                    //注意判斷返回金額

                                    //修改付款狀態、時間
                                    if (order_type.ToLower() == DTEnums.AmountTypeEnum.Recharge.ToString().ToLower()) //線上充值
                                    {
                                        BLL.amount_log bll = new BLL.amount_log();
                                        Model.amount_log model = bll.GetModel(out_trade_no);
                                        if (model == null)
                                        {
                                            Response.Write("該訂單號不存在");
                                            return;
                                        }
                                        if (model.value != (decimal.Parse(total_fee) / 100))
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
                                        Model.orders model = bll.GetModel(out_trade_no);
                                        if (model == null)
                                        {
                                            Response.Write("該訂單號不存在");
                                            return;
                                        }
                                        if (model.order_amount != (decimal.Parse(total_fee) / 100))
                                        {
                                            Response.Write("訂單金額和付款金額不相符");
                                            return;
                                        }
                                        bool result = bll.UpdateField(out_trade_no, "payment_status=2,payment_time='" + DateTime.Now + "'");
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

                                    //------------------------------
                                    //即時到賬處理業務完畢
                                    //------------------------------

                                    //給財付通系統發送成功資訊，財付通系統收到此結果後不再進行後續通知
                                    Response.Write("success");
                                }
                                else
                                {
                                    Response.Write("即時到賬付款失敗");
                                }
                            }
                        }
                        else
                        {
                            //錯誤時，返回結果可能沒有簽名，寫日誌trade_state、retcode、retmsg看失敗詳情。
                            //通知財付通處理失敗，需要重新通知
                            Response.Write("查詢驗證簽名失敗或id驗證失敗");
                            Response.Write("retcode:" + queryRes.getParameter("retcode"));
                        }
                        #endregion
                    }
                    else
                    {
                        Response.Write("通知ID查詢簽名驗證失敗");
                    }
                }
                else
                {
                    //通知財付通處理失敗，需要重新通知
                    Response.Write("後臺調用通信失敗");
                    //寫錯誤日誌
                    Response.Write("call err:" + httpClient.getErrInfo() + "<br>" + httpClient.getResponseCode() + "<br>");

                }
            }
            else
            {
                Response.Write("簽名驗證失敗");
            }
            Response.End();
        }
    }
}
