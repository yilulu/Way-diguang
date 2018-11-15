using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DTcms.Model;
using System.Data;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class usercart : System.Web.UI.Page
    {
        public string Images = "", TotalPrices, SinglePrice, Fee;
        public bool ISb = true; decimal TwoTypeFee = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadFee();
            if (!IsPostBack)
            {
                //AddCart();
                Show_Car();
            }
        }

        #region 加入購物車
        protected void AddCart()
        {
            int id = Utils.StringToNum(Request.QueryString["id"]);

            int intPicCount = Utils.StringToNum(Request.QueryString["Num"]);
            DataTable dt = (DataTable)Session["DGCart"];
            DataRow dr = dt.Rows.Find(id);
            if (dr == null)
            {
                string strName;
                string strImage;
                decimal decimalPrice;
                int intActive;
                returnPicInfo(id, out strName, out strImage, out decimalPrice, out intActive);
                DataRow new_dr = dt.NewRow();
                new_dr["GoodsId"] = id;
                new_dr["GoodsName"] = strName;
                new_dr["GoodsImage"] = strImage;
                new_dr["GoodsPrice"] = decimalPrice;
                if (intActive == 2)
                {
                    new_dr["GoodsCount"] = 1;
                }
                else
                {
                    new_dr["GoodsCount"] = intPicCount;
                }
                new_dr["GoodsTotal"] = decimalPrice * intPicCount;
                new_dr["GoodsActive"] = intActive;
                dt.Rows.Add(new_dr);
            }
            else
            {
                int intActive = Convert.ToInt16(dr["GoodsActive"]);
                if (intActive != 2)
                {
                    int intCount = Convert.ToInt16(dr["GoodsCount"]) + intPicCount;
                    decimal decimalPrice = Convert.ToDecimal(dr["GoodsPrice"]);
                    dr["GoodsCount"] = intCount;
                    dr["GoodsTotal"] = decimalPrice * intCount;
                }
            }
        }
        #endregion

        BLL.article bll = new BLL.article();
        BLL.orders bllorder = new BLL.orders();

        #region 返回产品基本信息
        /// <summary>
        /// 返回产品基本信息
        /// </summary>
        /// <param name="intPicId">产品编号</param>
        /// <param name="strName">产品名称</param>
        /// <param name="strImage">产品图片</param>
        /// <param name="decimalPrice">产品价格</param>
        /// <param name="intActive">销售方式</param>
        private void returnPicInfo(int intPicId, out string strName, out string strImage, out decimal decimalPrice, out int intActive)
        {
            Model.article_goods model = bll.GetGoodsModel(intPicId);
            //DataSet ds = bllpro.GetList(" id=" + intPicId + "");
            if (model != null)
            {
                //DataRow dr = ds.Tables[0].Rows[0];
                strName = model.title;

                string strPath = model.img_url;

                strImage = strPath;
                decimalPrice = model.sell_price;
                intActive = 0;
            }
            else
            {
                strName = string.Empty;
                strImage = string.Empty;
                decimalPrice = 0;
                intActive = 0;
                Response.Redirect("Default.aspx");
                Response.End();
            }
        }
        #endregion

        #region 显示购物车中的产品
        /// <summary>
        /// 显示购物车中的产品
        /// </summary>
        private void Show_Car()
        {
            DataTable dt = (DataTable)Session["DGCart"];
            if (dt != null && dt.Rows.Count > 0)
            {
                repddata.DataSource = dt.DefaultView;
                repddata.DataBind();
            }
            decimal decimalTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimalTotal += Convert.ToDecimal(dt.Rows[i]["GoodsTotal"]);
            }
            SinglePrice = decimalTotal.ToString();
            TotalPrices = (decimalTotal + decimal.Parse(Fee)).ToString();
            if (hideFee.Value == "2")
            {
                decimal FeeLv = TwoTypeFee;
                if (decimalTotal >= FeeLv)
                {
                    TotalPrices = decimalTotal.ToString();
                    Fee = "0";
                }

            }
        }
        #endregion

        #region 删除购物车中的商品
        protected void repddata_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "jian")
            {
                int intGoodsId = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)Session["DGCart"];
                DataRow dr = dt.Rows.Find(intGoodsId);
                if (dr != null)
                {
                    int intGoodsCount = Convert.ToInt32(dr["GoodsCount"]);
                    if (intGoodsCount > 1)
                    {
                        int intCount = intGoodsCount - 1;
                        dr["GoodsCount"] = intCount;
                        dr["GoodsTotal"] = Convert.ToDecimal(dr["GoodsPrice"]) * intCount;
                        Show_Car();
                    }
                }
            }
            if (e.CommandName == "jia")
            {
                int intGoodsId = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)Session["DGCart"];
                DataRow dr = dt.Rows.Find(intGoodsId);
                if (dr != null)
                {

                    int intGoodsCount = Convert.ToInt32(dr["GoodsCount"]);
                    if (intGoodsCount < 99)
                    {
                        int intCount = intGoodsCount + 1;
                        dr["GoodsCount"] = intCount;
                        dr["GoodsTotal"] = Convert.ToDecimal(dr["GoodsPrice"]) * intCount;
                        Show_Car();
                    }
                }
            }
            if (e.CommandName == "del")
            {
                int intGoodsId = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)Session["DGCart"];
                DataRow dr = dt.Rows.Find(intGoodsId);
                dr.Delete();
                //Show_Car();
                Response.Redirect("usercart.aspx");
            }
        }

        #endregion

        #region 获取总价格
        public decimal GetTotalPrice(string price, string num)
        {
            decimal totalPrice = 0M;
            if ((!string.IsNullOrEmpty(price)) && !string.IsNullOrEmpty(num))
            {
                decimal dc = decimal.Parse(price);
                int nums = int.Parse(num);
                totalPrice = dc * nums;
            }
            SinglePrice = totalPrice.ToString();
            TotalPrices = (totalPrice + decimal.Parse(Fee)).ToString();
            if (hideFee.Value == "2")
            {
                decimal FeeLv = TwoTypeFee;
                if (totalPrice >= FeeLv)
                {
                    TotalPrices = totalPrice.ToString();
                    Fee = "0";
                }
                else
                {
                    TotalPrices = (totalPrice + decimal.Parse(Fee)).ToString();
                }
            }

            return totalPrice;
        }
        #endregion

        #region 顯示運費信息
        void LoadFee()
        {
            decimal HtmlFee = 0M;
            Ltf.Model.Freight mdl = new Ltf.Model.Freight();
            Ltf.BLL.Freight bllf = new Ltf.BLL.Freight();

            mdl = bllf.GetModel(1);
            if (mdl != null)
            {
                int typeID = mdl.typID;
                if (typeID == 1)
                {
                    HtmlFee = 0;
                }
                else if (typeID == 2)
                {
                    HtmlFee = mdl.Fee;
                    TwoTypeFee = mdl.TotalPrice;
                }
                else if (typeID == 3)
                {
                    HtmlFee = mdl.Fee;
                }
                Fee = HtmlFee.ToString();
                hideFee.Value = typeID.ToString();
            }

        }
        #endregion


    }
}