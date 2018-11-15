using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Common;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class Regpay_ok : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "";
            string mid = "1038690";


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
            sb.Append("HashKey=7RswcwBgLcqnMjy5");
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
            sb.Append("&HashIV=IA0pz3lNuyui52iZ");
            url = sb.ToString();
            url = getstr(url).ToLower();


            string CMValue = DTcms.BLL.Function.Instance.MD5(url, 32);

            // Response.Write(CMValue);
            //  Response.End();
            //  && CheckMacValue == CMValue

            if (MerchantID == mid && RtnCode == "1" && !string.IsNullOrEmpty(MerchantTradeNo) && CheckMacValue == CMValue)
            {
                int Uid = WEBUserCurrent.UserID;

                BLL.users bllUser = new BLL.users();
                int bk = bllUser.UpFee(Uid, 1);
                if (bk > 0)
                {
                    Model.users User = new Model.users();
                    User = bllUser.GetModel(Uid);
                    if (User != null)
                    {

                        int point = 0;
                        int Groupid = User.group_id;
                        string Introduce_UserName = User.dianming;
                        if (Session["GroupName"] != null && Session["Type"] != null)
                        {
                            string type = Session["Type"].ToString();
                            Groupid = Utils.StringToNum(Session["GroupName"].ToString());

                            bllUser.UpdateField(Uid, " group_id=" + Groupid + " ,endTime=" + User.endtime.AddYears(2) + " ");
                        }
                        switch (Groupid)
                        {
                            case 1:
                                point = 0;
                                bllUser.UpPoint(Introduce_UserName, point);
                                break;
                            case 2:
                                point = 50;
                                bllUser.UpPoint(Introduce_UserName, point);
                                break;
                            case 3:
                                point = 100;
                                bllUser.UpPoint(Introduce_UserName, point);
                                break;
                            case 4:
                                point = 150;
                                bllUser.UpPoint(Introduce_UserName, point);
                                break;
                        }
                    }
                    Response.Write("1|OK");
                    Response.End();
                }
                else
                {
                    Response.Write("0|ErrorMessage");
                    Response.End();
                }
            }
            else
            {
                Response.Write("0|ErrorMessage");

                Response.End();
            }
        }

        public string getstr(string str)
        {
            return System.Web.HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("UTF-8"));
        }
    }
}
