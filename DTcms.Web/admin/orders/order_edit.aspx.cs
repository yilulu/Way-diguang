using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class order_edit : Web.UI.ManagePage
    {
        private int id = 0;
        protected Model.orders model = new Model.orders();
        protected Model.users userModel = new Model.users();
        protected Model.user_groups groupModel = new Model.user_groups();
        protected Model.payment payModel = new Model.payment();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryInt("id");
            if (this.id == 0)
            {
                JscriptMsg("傳輸參數不正確！", "back", "Error");
                return;
            }
            if (!new BLL.orders().Exists(this.id))
            {
                JscriptMsg("資料不存在或已被刪除！", "back", "Error");
                return;
            }
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
            if (model.status < 3)
            {
                this.btnCancel.Visible = true;
            }
            //如果訂單為已完成時，不能取消訂單
            if (model.status == 3)
            {
                this.btnInvalid.Visible = true;
            }
        }
        #endregion

        #region  確認訂單
        protected void lbtnConfirm_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            //檢查訂單狀態
            if (model == null || model.status > 1)
            {
                JscriptMsg("訂單不符合要求，無法確認！", "", "Error");
                return;
            }
            //檢查付款方式
            Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                JscriptMsg("付款方式不存在，無法確認！", "", "Error");
                return;
            }
            //如果付款方式為線上付款，則檢查付款狀態
            if (payModel.type == 1)
            {
                if (model.payment_status != 2)
                {
                    JscriptMsg("訂單未付款，無法確認！", "", "Error");
                    return;
                }
            }
            bll.UpdateField(this.id, "status=2,confirm_time='" + DateTime.Now + "'");
            JscriptMsg("訂單確認成功！", "order_edit.aspx?id=" + this.id, "Success");
        }
        #endregion

        #region  商家發貨
        protected void lbtnSend_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            //檢查訂單狀態
            if (model == null || model.status != 2)
            {
                JscriptMsg("訂單不符合要求，無法發貨！", "", "Error");
                return;
            }
            //檢查付款方式
            Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                JscriptMsg("付款方式不存在，無法發貨！", "", "Error");
                return;
            }
            //如果付款方式為線上付款，則檢查付款狀態
            if (payModel.type == 1)
            {
                if (model.payment_status != 2)
                {
                    JscriptMsg("訂單未付款，無法發貨！", "", "Error");
                    return;
                }
            }
            bll.UpdateField(this.id, "distribution_status=2,distribution_time='" + DateTime.Now + "'");
            JscriptMsg("訂單發貨成功！", "order_edit.aspx?id=" + this.id, "Success");
        }
        #endregion

        #region  完成訂單
        protected void lbtnComplete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            //檢查訂單狀態
            if (model == null || model.status != 2 || model.distribution_status != 2)
            {
                JscriptMsg("訂單不符合要求，無法發貨！", "", "Error");
                return;
            }
            //檢查付款方式
            Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                JscriptMsg("付款方式不存在，無法完成訂單！", "", "Error");
                return;
            }
            //增加積分/經驗值
            if (model.point > 0)
            {
                new BLL.point_log().Add(model.user_id, model.user_name, model.point, "購物獲得積分，訂單號：" + model.order_no);
            }
            //如果配送方式為先款後貨，則檢查付款狀態
            if (payModel.type == 2)
            {
                bll.UpdateField(this.id, "status=3,complete_time='" + DateTime.Now + "'," + "payment_status=2,payment_time='" + DateTime.Now + "'");
            }
            else
            {
                bll.UpdateField(this.id, "status=3,complete_time='" + DateTime.Now + "'");
            }
            JscriptMsg("訂單已經完成！", "order_edit.aspx?id=" + this.id, "Success");
        }
        #endregion

        #region  取消訂單
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Cancel.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            if (model == null && model.status > 2)
            {
                JscriptMsg("訂單不符合要求，無法取消！", "", "Error");
                return;
            }
            bll.UpdateField(this.id, "status=4");
            int UID = 0, Point = 0;
            string orNo = string.Empty;

            #region 根据ID获取订单参数
            UID = model.user_id;
            // Point = model.point;
            orNo = model.order_no;
            #endregion


            #region 更新点数
            BLL.users BLLUser = new BLL.users();
            BLL.point_log points = new BLL.point_log();
            DataTable dtOrder = points.GetList(100, " user_name='" + orNo + "' ", " add_time desc").Tables[0];
            if (dtOrder != null)
            {
                for (int k = 0; k < dtOrder.Rows.Count; k++)
                {

                    Model.point_log modelPoint = new Model.point_log();
                    int type = Utils.StringToNum(dtOrder.Rows[k]["type"].ToString());
                    Point = Utils.StringToNum(dtOrder.Rows[k]["value"].ToString());
                    if (type == 1)
                    {
                        BLLUser.UpJianPoint(UID, Point);

                        #region 记录点数消费

                        modelPoint.user_id = UID;
                        modelPoint.user_name = orNo;
                        modelPoint.value = Point;
                        modelPoint.remark = "訂單取消購物回饋點數";
                        modelPoint.add_time = DateTime.Now;
                        modelPoint.type = 2;
                        int m = points.Add(modelPoint);
                        #endregion
                    }
                    if (type == 2)
                    {
                        BLLUser.UpPoint(UID, Point);

                        #region 记录点数消费

                        modelPoint.user_id = UID;
                        modelPoint.user_name = orNo;
                        modelPoint.value = Point;
                        modelPoint.remark = "訂單取消返還購物時折抵點數";
                        modelPoint.add_time = DateTime.Now;
                        modelPoint.type = 1;
                        int m = points.Add(modelPoint);
                        #endregion
                    }

                }
            }


            #endregion
            JscriptMsg("訂單取消成功！", "order_edit.aspx?id=" + this.id, "Success");
        }
        #endregion

        #region  作廢訂單
        protected void btnInvalid_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Invalid.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            if (model == null && model.status != 3)
            {
                JscriptMsg("訂單未完成，無法作廢！", "", "Error");
                return;
            }
            bll.UpdateField(this.id, "status=5");
            JscriptMsg("訂單取消成功！", "order_edit.aspx?id=" + this.id, "Success");
        }
        #endregion

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
