using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class order_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int status;
        protected int payment_status;
        protected int distribution_status;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.status = DTRequest.GetQueryInt("status");
            this.payment_status = DTRequest.GetQueryInt("payment_status");
            this.distribution_status = DTRequest.GetQueryInt("distribution_status");
            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(15); //每頁數量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("orders", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("id>0" + CombSqlTxt(this.status, this.payment_status, this.distribution_status, this.keywords), "  order by status asc,add_time desc,id desc");
            }
        }

        #region 數據綁定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.status > 0)
            {
                this.ddlStatus.SelectedValue = this.status.ToString();
            }
            if (this.payment_status > 0)
            {
                this.ddlPaymentStatus.SelectedValue = this.payment_status.ToString();
            }
            if (this.distribution_status > 0)
            {
                this.ddlDistributionStatus.SelectedValue = this.distribution_status.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            BLL.orders bll = new BLL.orders();
            this.rptList.DataSource = bll.list_pagesWhere(this.page, this.pageSize, " and " + _strWhere, _orderby);
            this.totalCount = bll.GetTatalNum(_strWhere);
            this.rptList.DataBind();

            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}&page={4}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 組合SQL查詢語句==========================
        protected string CombSqlTxt(int _status, int _payment_status, int _distribution_status, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_status > 0)
            {
                strTemp.Append(" and status=" + _status);
            }
            if (_payment_status > 0)
            {
                strTemp.Append(" and payment_status=" + _payment_status);
            }
            if (_distribution_status > 0)
            {
                strTemp.Append(" and distribution_status=" + _distribution_status);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (order_no like '%" + _keywords + "%' or user_name like '%" + _keywords + "%' or accept_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用戶每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("order_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回訂單狀態=============================
        protected string GetOrderStatus(int _id)
        {
            string _title = "";
            Model.orders model = new BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1:
                    _title = "等待確認";
                    Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
                    if (payModel != null && payModel.type == 2)
                    {
                        if (model.payment_status > 1)
                        {
                            _title = "付款成功";
                        }
                        else
                        {
                            _title = "等待付款";
                        }
                    }
                    break;
                case 2:
                    if (model.distribution_status > 1)
                    {
                        _title = "已發貨";
                    }
                    else
                    {
                        _title = "待發貨";
                    }
                    break;
                case 3:
                    _title = "交易完成";
                    break;
                case 4:
                    _title = "訂單取消";
                    break;
                case 5:
                    _title = "訂單作廢";
                    break;
            }

            return _title;
        }
        #endregion

        //關健字查詢
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), txtKeywords.Text));
        }

        //訂單狀態
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                ddlStatus.SelectedValue, this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords));
        }

        //付款狀態
        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), ddlPaymentStatus.SelectedValue, this.distribution_status.ToString(), this.keywords));
        }

        //發貨狀態
        protected void ddlDistributionStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), ddlDistributionStatus.SelectedValue, this.keywords));
        }

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("order_list_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords));
        }

        //取消訂單
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Cancel.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.orders model = bll.GetModel(id);
                    if (model != null && model.status == 1)
                    {
                        bll.UpdateField(id, "status=4");
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


                    }
                }
            }
            JscriptMsg("符合的訂單已取消！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords), "Success");
        }

        //作廢訂單
        protected void btnInvalid_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Invalid.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.orders model = bll.GetModel(id);
                    if (model != null && model.status == 3)
                    {
                        bll.UpdateField(id, "status=5");
                    }
                }
            }
            JscriptMsg("符合的訂單已作廢！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords), "Success");
        }

        //批次刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.orders model = bll.GetModel(id);
                    if (model != null && model.status == 4)
                    {
                        bll.Delete(id);
                    }
                }
            }
            JscriptMsg("符合的訂單已刪除！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords), "Success");
        }


    }


}
