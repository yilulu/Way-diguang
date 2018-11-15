using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.manager
{
    public partial class nav_edit : DTcms.Web.UI.ManagePage
    {
       
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改類型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("傳輸參數不正確！", "back", "Error");
                    return;
                }
                if (!new BLL.article_nav_BLL().Exists(this.id))
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
            BLL.article_nav_BLL bll = new BLL.article_nav_BLL();
            Model.article_nav model = bll.GetModel(_id);
            txtNavTitle.Text = model.n_title;
           rblIsState.SelectedValue = model.n_state.ToString();
           txtNavSequence.Text= model.n_sequence.ToString();
           txtNavUrl.Text = model.n_url;
           
           txtNavDesc.Text = model.n_desc;
          
        }
        #endregion

      

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.article_nav model = new Model.article_nav();
            BLL.article_nav_BLL bll = new BLL.article_nav_BLL();
           
            model.n_state = int.Parse(rblIsState.SelectedValue);
            model.n_title =txtNavTitle.Text.Trim();
            model.n_sequence = int.Parse(txtNavSequence.Text.Trim());
            model.n_url = txtNavUrl.Text.Trim();
            model.n_desc = txtNavDesc.Text.Trim();
          
            
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
            BLL.article_nav_BLL bll = new BLL.article_nav_BLL();
            Model.article_nav model = bll.GetModel(_id);

         
           
            model.n_state = int.Parse(rblIsState.SelectedValue);
           
            model.n_title =txtNavTitle.Text.Trim();
            model.n_url =txtNavUrl.Text.Trim();
            model.n_sequence =int.Parse(txtNavSequence.Text.Trim());
            model.n_desc = txtNavDesc.Text;
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
                ChkAdminLevel("sys_nav", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改導航成功！", "nav_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("sys_nav", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加導航成功！", "nav_list.aspx", "Success");
            }
        }

    }
}
