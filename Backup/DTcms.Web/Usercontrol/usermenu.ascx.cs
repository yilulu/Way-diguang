using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using Common;

namespace DTcms.Web
{
    public partial class usermenu : System.Web.UI.UserControl
    {
        protected BLL.users bllUser = new BLL.users();
        protected int UID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ids = WEBUserCurrent.UserID.ToString();
            if (!string.IsNullOrEmpty(ids))
            {
                UID = Utils.StringToNum(ids);
            }

        }
    }
}