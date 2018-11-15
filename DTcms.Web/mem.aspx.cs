using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class mem : DTcms.Web.UI.BasePage
    {
        public BLL.user_groups bllUser = new BLL.user_groups();
        public string HostUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}