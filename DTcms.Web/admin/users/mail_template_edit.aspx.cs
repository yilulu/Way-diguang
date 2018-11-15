using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class mail_template_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改類型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("傳輸參數不正確！", "back", "Error");
                    return;
                }
                if (!new BLL.mail_template().Exists(this.id))
                {
                    JscriptMsg("資料不存在或已被刪除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }

            }
        }

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.mail_template bll = new BLL.mail_template();
            Model.mail_template model = bll.GetModel(_id);

            txtCallIndex.Text = model.call_index;
            txtTitle.Text = model.title;
            txtMailTitle.Text = model.maill_title;
            txtContent.Value = model.content;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.mail_template model = new Model.mail_template();
            BLL.mail_template bll = new BLL.mail_template();

            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.maill_title = txtMailTitle.Text.Trim();
            model.content = txtContent.Value;
            if (bll.Add(model) < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = true;
            BLL.mail_template bll = new BLL.mail_template();
            Model.mail_template model = bll.GetModel(_id);

            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.maill_title = txtMailTitle.Text.Trim();
            model.content = txtContent.Value;
            if (!bll.Update(model))
            {
                result = false;
            }
            return result;
        }
        #endregion

        //儲存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("mail_template", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改內容成功！", "mail_template_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("mail_template", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加內容成功！", "mail_template_list.aspx", "Success");
            }
        }

    }
}
