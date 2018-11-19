using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class Notebook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WEBUserCurrent.IsLogin)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                LoadNotebook(1);
            }
        }

        #region 加载内容
        public void LoadNotebook(int p)
        {
            int UID = WEBUserCurrent.UserID;
            BLL.dt_feedback bllfeed = new BLL.dt_feedback();
            DataTable dt = bllfeed.list_pagesWhere(1, 20, " and UserID=" + UID + "", "");
            if (dt != null)
            {
                rptList.DataSource = dt.DefaultView;
                rptList.DataBind();
            }
        }
        #endregion

        #region 根据ID获取真实用户名
        public string GetNameByID(string Id)
        {
            string UserName = string.Empty;
            BLL.users bllUser=new BLL.users();
            Model.users User = new Model.users();
            if (!string.IsNullOrEmpty(Id))
            {
                int UID = Utils.StringToNum(Id);
                User = bllUser.GetModel(UID);
                if (User != null)
                {
                    UserName = User.user_name;
                }
            }
            return UserName;
        }
        #endregion
    }
}