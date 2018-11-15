using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class distribution_edit : Web.UI.ManagePage
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
                    JscriptMsg("輸入參數不正確！", "back", "Error");
                    return;
                }
                if (!new BLL.distribution().Exists(this.id))
                {
                    JscriptMsg("配送方式不存在或已刪除！", "back", "Error");
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
            BLL.distribution bll = new BLL.distribution();
            Model.distribution model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            rblType.SelectedValue = model.type.ToString();
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtAmount.Text = model.amount.ToString();
            txtRemark.Text = model.remark;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.distribution model = new Model.distribution();
            BLL.distribution bll = new BLL.distribution();

            model.title = txtTitle.Text.Trim();
            model.type = int.Parse(rblType.SelectedValue);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            model.remark = txtRemark.Text;
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
            BLL.distribution bll = new BLL.distribution();
            Model.distribution model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.type = int.Parse(rblType.SelectedValue);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            model.remark = txtRemark.Text;
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
                ChkAdminLevel("distribution", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改成功！", "distribution_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("distribution", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加成功！", "distribution_list.aspx", "Success");
            }
        }

    }
}
