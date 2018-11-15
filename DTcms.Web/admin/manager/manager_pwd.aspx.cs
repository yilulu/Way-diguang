using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.manager
{
    public partial class manager_pwd : DTcms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Model.manager model = GetAdminInfo();
                ShowInfo(model.id);
            }
        }

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(_id);
            txtUserName.Text = model.user_name;
            txtRealName.Text = model.real_name;
            txtTelephone.Text = model.telephone;
            txtEmail.Text = model.email;
        }
        #endregion

        //儲存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BLL.manager bll = new BLL.manager();
            Model.manager model = GetAdminInfo();

            if (DESEncrypt.Encrypt(txtOldPwd.Text.Trim()) != model.user_pwd)
            {
                JscriptMsg("舊密碼不正確！", "", "Warning");
                return;
            }
            if (txtUserPwd.Text.Trim() != txtUserPwd1.Text.Trim())
            {
                JscriptMsg("兩次密碼不一致！", "", "Warning");
                return;
            }
            model.user_pwd = DESEncrypt.Encrypt(txtUserPwd.Text.Trim());
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();

            if (!bll.Update(model))
            {
                JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                return;
            }
            Session[DTKeys.SESSION_ADMIN_INFO] = null;
            JscriptMsg("密碼修改成功！", "manager_pwd.aspx", "Success");
        }

    }
}