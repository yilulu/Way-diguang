using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.api.payment.balance
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //讀取網站配置資料
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);

            string order_type = DTRequest.GetFormString("pay_order_type"); //訂單類型
            string order_no = DTRequest.GetFormString("pay_order_no");
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0);
            string subject = DTRequest.GetFormString("pay_subject");
            if (order_no == "" || order_amount == 0 )
            {
                Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("對不起，您送出的參數有誤！"));
                return;
            }
            //檢查是否已登入
            Model.users userModel = new Web.UI.BasePage().GetUserInfo();
            if (userModel == null)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "login")); //尚未登入
                return;
            }
            if (userModel.amount < order_amount)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "recharge")); //帳戶的餘額不足
                return;
            }

            if (order_type.ToLower() == DTEnums.AmountTypeEnum.BuyGoods.ToString().ToLower()) //購買商品
            {
                BLL.orders bll = new BLL.orders();
                Model.orders model = bll.GetModel(order_no);
                if (model == null)
                {
                    Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("對不起，商品訂單號不存在！"));
                    return;
                }
                if (model.payment_status == 1)
                {
                    //執行扣取帳戶金額
                    int result = new BLL.amount_log().Add(userModel.id, userModel.user_name, DTEnums.AmountTypeEnum.BuyGoods.ToString(), order_no, model.payment_id, -1 * order_amount, subject, 1);
                    if (result > 0)
                    {
                        //更改訂單狀態
                        bool result1 = bll.UpdateField(order_no, "payment_status=2,payment_time='" + DateTime.Now + "'");
                        if (!result1)
                        {
                            Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
                            return;
                        }
                        //扣除積分
                        if (model.point < 0)
                        {
                            new BLL.point_log().Add(model.user_id, model.user_name, model.point, "換購扣除積分，訂單號：" + model.order_no);
                        }
                    }
                    else
                    {
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
                        return;
                    }
                }
                //付款成功
                Response.Redirect(new Web.UI.BasePage().linkurl("payment1", "succeed", order_type, order_no));
                return;
            }
            Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("對不起，找不到需要付款的訂單類型！"));
            return;
        }
    }
}
