using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.attribute
{
    public partial class edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int channel_id;
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            if (this.channel_id == 0)
            {
                JscriptMsg("頻道參數不正確！", "back", "Error");
                return;
            }
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改類型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("傳輸參數不正確！", "back", "Error");
                    return;
                }
                if (!new BLL.attributes().Exists(this.id))
                {
                    JscriptMsg("記錄不存在或已被刪除！", "back", "Error");
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
            BLL.attributes bll = new BLL.attributes();
            Model.attributes model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            txtRemark.Text = model.remark;
            ddlType.SelectedValue = model.type.ToString();
            txtDefaultValue.Text = model.default_value;
            txtSortId.Text = model.sort_id.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.attributes model = new Model.attributes();
            BLL.attributes bll = new BLL.attributes();
            model.channel_id = this.channel_id;
            model.title = txtTitle.Text.Trim();
            model.remark = txtRemark.Text.Trim();
            model.type = int.Parse(ddlType.SelectedValue);
            model.default_value = txtDefaultValue.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
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
            BLL.attributes bll = new BLL.attributes();
            Model.attributes model = bll.GetModel(_id);
            model.title = txtTitle.Text.Trim();
            model.remark = txtRemark.Text.Trim();
            model.type = int.Parse(ddlType.SelectedValue);
            model.default_value = txtDefaultValue.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
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
                ChkAdminLevel(channel_id, DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改屬性成功！", "list.aspx?channel_id=" + channel_id, "Success");
            }
            else //添加
            {
                ChkAdminLevel(channel_id, DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加屬性成功！", "list.aspx?channel_id=" + channel_id, "Success");
            }
        }


    }
}
