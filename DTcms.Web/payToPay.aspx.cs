using Common;
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class payToPay : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Url = "pay.aspx?";
            Url += "paymenttype=1";
            Url += "&id=255";
            this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('確定付款嗎');window.location.href = '" + Url + "';</script>");
        }
    }
}