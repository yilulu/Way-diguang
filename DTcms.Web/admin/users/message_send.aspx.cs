using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class message_send : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.user_message model = new Model.user_message();
            BLL.user_message bll = new BLL.user_message();

            model.title = txtTitle.Text.Trim();
            model.content = txtContent.Value;

            string[] arrUserName = txtUserName.Text.Trim().Split(',');
            if (arrUserName.Length > 0)
            {
                foreach (string username in arrUserName)
                {
                    if (new BLL.users().Exists(username))
                    {
                        model.accept_user_name = username;
                        if (bll.Add(model) < 1)
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        //儲存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_message", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
            if (!DoAdd())
            {
                JscriptMsg("發送過程中發生錯誤！", "", "Error");
                return;
            }
            JscriptMsg("發送訊息成功！", "message_list.aspx", "Success");
        }

    }
}
