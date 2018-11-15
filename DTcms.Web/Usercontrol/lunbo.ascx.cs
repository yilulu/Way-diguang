using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class lunbo : System.Web.UI.UserControl
    {
        int channel_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DTRequest.GetQueryInt("mid") != 0)
            {
                this.channel_id = DTRequest.GetQueryInt("mid");
            }
            if (DTRequest.GetUrl().IndexOf("empro-shop.com.tw") > 0)
            {
                this.channel_id = 5;
            }
            if (DTRequest.GetUrl().IndexOf("empro3d.com.tw") > 0)
            {
                this.channel_id = 4;
            }
            channel_id = channel_id == 10 ? 1 : channel_id;
            DTcms.DAL.imagedal dal = new DAL.imagedal();
            int count = 0;
            var table = dal.GetDatalistpage(9999999, 1, "Typeid=" + channel_id + "", "sort", out count).Tables[0];
            repdate.DataSource = table;
            repdate.DataBind();
        }
    }
}