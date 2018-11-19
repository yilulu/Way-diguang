using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DTcms.Common;
using System.Data;

namespace DTcms.Web
{
    public partial class userorder : PageBase
    {
        public string Images = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WEBUserCurrent.IsLogin)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Bind(1);
                }
            }
        }


        protected int channel_id;
        BLL.orders bll = new BLL.orders();
        string sqlwhere = "";
        private void Bind(int page)
        {
            int totalCount;
            int pageSize = 8;
            //發佈的商品
            if (!string.IsNullOrEmpty(Request.QueryString["status"]))
            {
                sqlwhere = " and status=" + Request.QueryString["status"];
            }
            DataTable dt = bll.list_pagesWhere(page, pageSize, " and user_id=" + WEBUserCurrent.UserID + sqlwhere, "  order by id desc");
            if (dt.Rows.Count > 0)
            {
                this.repddata.DataSource = dt.DefaultView;
                this.repddata.DataBind();
                totalCount = bll.GetTatalNum("  user_id=" + WEBUserCurrent.UserID + sqlwhere);
                aspPage.PageSize = pageSize;
                aspPage.RecordCount = totalCount;
            }

        }
        BLL.article bllp = new BLL.article();
        public string Getdateil(int id)
        {
            var model = bll.GetModel(id);
            string html = "";
            if (model != null)
            {
                foreach (var item in model.order_goods)
                {
                    Model.article_goods modelpro = bllp.GetGoodsModel(item.goods_id);
                    if (modelpro != null)
                    {
                        html += "<img src='" + modelpro.img_url + "'  width=\"40\" height=\"40\"/>&nbsp;&nbsp;";
                    }
                }
            }

            return html;
        }

        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            Bind(aspPage.CurrentPageIndex);
        }

        #region 返回訂單狀態=============================
        protected string GetOrderStatus(string _id)
        {
            string _title = "";
            if (!string.IsNullOrEmpty(_id))
            {
                int ordrerID = Utils.StringToNum(_id);
                Model.orders model = new BLL.orders().GetModel(ordrerID);
                if (model != null)
                {
                    switch (model.status)
                    {
                        case 1:
                            _title = "等待確認";
                            Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
                            if (payModel != null)
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
                }

            }
            return _title;
        }
        #endregion

        #region 生成付款连接
        public string CreateUrl(string payType, string id)
        {
            string htmlStr = string.Empty;
            if (GetOrderStatus(id) == "等待付款")
            {
                //return htmlStr = "<a href=\"pay.aspx?paymenttype=" + payType + "&id=" + id + "\" target=\"_blank\">" + "去付款" + "</a>";
                htmlStr = "<a href=\"payToPay.aspx\" >" + "去付款" + "</a>";
            }
            return htmlStr;
        }
        #endregion
    }
}
