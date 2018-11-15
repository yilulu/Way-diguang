using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class mem : System.Web.UI.Page
    {
        public BLL.user_groups bllUser = new BLL.user_groups();
        public string HostUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            HostUrl = HttpContext.Current.Request.Url.Host;
        }
    }
}