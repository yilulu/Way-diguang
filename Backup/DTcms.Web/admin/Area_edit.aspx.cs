using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin
{
    public partial class Area_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int id = 0;
        private int Pid = 0;
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

            }
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                {
                    Pid = int.Parse(Request.QueryString["pid"].ToString());
                    //ddlParent.SelectedValue = Request.QueryString["pid"].ToString();
                }
                GetParent();
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        public void GetParent()
        {
            int totalCount = 0;
            DAL.AreaDal aredal = new DAL.AreaDal();
            string where = "parent=" + Pid + "";
            if (this.id > 0)
            {
                where += "and id <>  " + id;
            }
            var table = aredal.GetDatalistpage(99999, 1, where, " sort", out totalCount).Tables[0];
            ddlParent.DataSource = table;
            ddlParent.DataTextField = "title";
            ddlParent.DataValueField = "id";
            ddlParent.DataBind();
            ddlParent.Items.Insert(0, new ListItem() { Text = "請選擇", Value = "0", Selected = true });
        }

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            DAL.AreaDal bll = new DAL.AreaDal();
            Model.Area model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            txtSortId.Text = model.sort;
            ddlParent.SelectedValue = model.parent.ToString();
            txtCode.Text = model.code;

        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.Area model = new Model.Area();
            DAL.AreaDal bll = new DAL.AreaDal();


            model.title = txtTitle.Text.Trim();
            model.sort = txtSortId.Text.Trim();
            model.parent = int.Parse(ddlParent.SelectedValue.Trim());
            model.code = txtCode.Text.Trim();
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
            DAL.AreaDal bll = new DAL.AreaDal();
            Model.Area model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.sort = txtSortId.Text.Trim();
            model.parent = int.Parse(ddlParent.SelectedValue.Trim());
            model.code = txtCode.Text.Trim();
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
                //ChkAdminLevel("sys_model", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改成功！", "Area_list.aspx", "Success", "parent.loadChannelTree");
            }
            else //添加
            {
                //ChkAdminLevel("sys_model", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加成功！", "Area_list.aspx", "Success", "parent.loadChannelTree");
            }
        }
    }
}
