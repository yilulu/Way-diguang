using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;
using System.Text;

namespace DTcms.Web.admin.download
{
    public partial class SetFile : Web.UI.ManagePage
    {
        public string IDList;
        BLL.article blla = new BLL.article();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetValue();
                loadUser();
            }
        }

        #region 加载数据
        private void loadUser()
        {
            BLL.users bllUser = new BLL.users();
            DataTable dt = bllUser.GetList(100000, "", "ID DESC").Tables[0];
            if (dt != null)
            {
                Repeater1.DataSource = dt.DefaultView;
                Repeater1.DataBind();
            }
        }
        #endregion

        #region 保存设置
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string uIDValue = Request.Form["uID"];
            int DownID = DTRequest.GetQueryInt("id");
            int bk = blla.SetFileToMember(uIDValue, DownID);
            if (bk > 0)
            {
                JscriptMsg("指派成功！", "back", "Success");
                return;
            }
        }
        #endregion

        #region 设置状态
        public void SetValue()
        {
            int DownID = DTRequest.GetQueryInt("id");
            IDList = blla.GetUidList(DownID);
            StringBuilder str = new StringBuilder();
            str.Append("<script language='javascript'>");
            str.Append("setCheckBox('" + IDList + "',1);");
            str.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "", str.ToString());

        }
        #endregion

    }
}