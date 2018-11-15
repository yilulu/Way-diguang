using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin
{
    public partial class center : Web.UI.ManagePage
    {
        protected Model.manager admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo(); //管理員資料
                //登入資料
                if (admin_info != null)
                {
                    BLL.manager_log bll = new BLL.manager_log();
                    Model.manager_log model1 = bll.GetModel(admin_info.user_name, 1, "login");
                    if (model1 != null)
                    {
                        //本次登入
                        litIP.Text = bll.GetModel(admin_info.user_name, 1, "login").login_ip;
                    }
                    Model.manager_log model2 = bll.GetModel(admin_info.user_name, 2, "login");
                    if (model2 != null)
                    {
                        //上一次登入
                        litBackIP.Text = model2.login_ip;
                        litBackTime.Text = model2.login_time.ToString();
                    }
                }
               
               
                
            }
        }
    }
}