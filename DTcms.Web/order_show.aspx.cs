using Common;
using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class order_show : PageBase
    {
        private int id = 0;
        protected Model.orders model = new Model.orders();
        protected Model.users userModel = new Model.users();
        protected Model.user_groups groupModel = new Model.user_groups();
        protected Model.payment payModel = new Model.payment();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryInt("id");
        
            if (!Page.IsPostBack)
            {
                ShowInfo(this.id);
            }
        }

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(_id);
            payModel = new BLL.payment().GetModel(model.payment_id);
            userModel = new BLL.users().GetModel(model.user_id);
            if (userModel != null)
            {
                groupModel = new BLL.user_groups().GetModel(userModel.group_id);
            }
            if (payModel == null)
            {
                payModel = new Model.payment();
            }
            this.rptList.DataSource = model.order_goods;
            this.rptList.DataBind();
            //訂單狀態
            if (model.status == 1)
            {
                if (payModel != null && payModel.type == 1)
                {
                    if (model.payment_status > 1)
                    {
                        this.lbtnConfirm.Enabled = true;
                    }
                }
                else
                {
                    this.lbtnConfirm.Enabled = true;
                }
            }
            else if (model.status == 2 && model.distribution_status == 1)
            {
                this.lbtnSend.Enabled = true;
            }
            else if (model.status == 2 && model.distribution_status == 2)
            {
                this.lbtnComplete.Enabled = true;
            }
         
        }
        #endregion

        #region  確認訂單
        protected void lbtnConfirm_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region  商家發貨
        protected void lbtnSend_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region  完成訂單
        protected void lbtnComplete_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region  取消訂單
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region  作廢訂單
        protected void btnInvalid_Click(object sender, EventArgs e)
        {
        }
        #endregion

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}