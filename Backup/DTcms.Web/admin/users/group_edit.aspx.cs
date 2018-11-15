using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class group_edit : Web.UI.ManagePage
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
                if (!new BLL.user_groups().Exists(this.id))
                {
                    JscriptMsg("用戶組不存在或已被刪除！", "back", "Error");
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
            BLL.user_groups bll = new BLL.user_groups();
            Model.user_groups model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            rblIsLock.SelectedValue = model.is_lock.ToString();
            rblIsDefault.SelectedValue = model.is_default.ToString();
            rblIsUpgrade.SelectedValue = model.is_upgrade.ToString();
            txtGrade.Text = model.grade.ToString();
            //txtUpgradeExp.Text = model.upgrade_exp.ToString();
            //txtAmount.Text = model.amount.ToString();
            //txtPoint.Text = model.point.ToString();
            txtDiscount.Text = model.discount.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.user_groups model = new Model.user_groups();
            BLL.user_groups bll = new BLL.user_groups();

            model.title = txtTitle.Text.Trim();
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.is_default = int.Parse(rblIsDefault.SelectedValue);
            model.is_upgrade = int.Parse(rblIsUpgrade.SelectedValue);
            model.grade = int.Parse(txtGrade.Text.Trim());
            //model.upgrade_exp = int.Parse(txtUpgradeExp.Text.Trim());
            //model.amount = decimal.Parse(txtAmount.Text.Trim());
            //model.point = int.Parse(txtPoint.Text.Trim());
            model.discount = int.Parse(txtDiscount.Text.Trim());
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
            BLL.user_groups bll = new BLL.user_groups();
            Model.user_groups model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.is_default = int.Parse(rblIsDefault.SelectedValue);
            model.is_upgrade = int.Parse(rblIsUpgrade.SelectedValue);
            model.grade = int.Parse(txtGrade.Text.Trim());
            //model.upgrade_exp = int.Parse(txtUpgradeExp.Text.Trim());
            //model.amount = decimal.Parse(txtAmount.Text.Trim());
            //model.point = int.Parse(txtPoint.Text.Trim());
            model.discount = int.Parse(txtDiscount.Text.Trim());
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
                ChkAdminLevel("user_groups", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改會員組成功！", "group_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("user_groups", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加會員組成功！", "group_list.aspx", "Success");
            }
        }

    }
}
