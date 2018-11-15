using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DTcms.Model;
using System.Data;
using DTcms.Common;
using LitJson;

namespace DTcms.Web
{
    public partial class usercart : System.Web.UI.Page
    {
        public string Images = "", TotalPrices, SinglePrice, Fee;
        public bool ISb = true; decimal TwoTypeFee = 0;
        protected Model.cart_total cartModel;
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadFee();
            if (!IsPostBack)
            {
                Show_Car();
            }
        }


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
            try
            {
                repddata.DataSource = get_cart_list(string.Empty);
                repddata.DataBind();

                cartModel = GetTotal(DTKeys.COOKIE_SHOPPING_CART);
            }
            catch (Exception ex)
            {
                DTcms.Common.Utils.MakeFile(ex.ToString(), ex.StackTrace);
            }
        }
        #endregion

        #region 删除购物车中的商品
        protected void repddata_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            #region 购物车增加减少===================
            //if (e.CommandName == "jian")
            //{
            //    int intGoodsId = Convert.ToInt32(e.CommandArgument);
            //    DataTable dt = (DataTable)Session["DGCart"];
            //    DataRow dr = dt.Rows.Find(intGoodsId);
            //    if (dr != null)
            //    {
            //        int intGoodsCount = Convert.ToInt32(dr["GoodsCount"]);
            //        if (intGoodsCount > 1)
            //        {
            //            int intCount = intGoodsCount - 1;
            //            dr["GoodsCount"] = intCount;
            //            dr["GoodsTotal"] = Convert.ToDecimal(dr["GoodsPrice"]) * intCount;
            //            Show_Car();
            //        }
            //    }
            //}
            //if (e.CommandName == "jia")
            //{
            //    int intGoodsId = Convert.ToInt32(e.CommandArgument);
            //    DataTable dt = (DataTable)Session["DGCart"];
            //    DataRow dr = dt.Rows.Find(intGoodsId);
            //    if (dr != null)
            //    {

            //        int intGoodsCount = Convert.ToInt32(dr["GoodsCount"]);
            //        if (intGoodsCount < 99)
            //        {
            //            int intCount = intGoodsCount + 1;
            //            dr["GoodsCount"] = intCount;
            //            dr["GoodsTotal"] = Convert.ToDecimal(dr["GoodsPrice"]) * intCount;
            //            Show_Car();
            //        }
            //    }
            //}
            #endregion

            if (e.CommandName == "del")
            {
                string intGoodsId = e.CommandArgument.ToString();
                Clear(DTKeys.COOKIE_SHOPPING_CART, intGoodsId);
                Response.Redirect("usercart.aspx");
            }
        }

        #endregion

        #region 移除購物車
        /// <summary>
        /// 移除購物車
        /// </summary>
        /// <param name="Key">主鍵 0為清理所有的購物車資料</param>
        public void Clear(string cart, string Key)
        {
            if (Key == "0")//為0的時候清理全部購物車cookies
            {
                Utils.WriteCookie(cart, "", -43200);
            }
            else
            {
                IDictionary<string, string> dic = GetCart(cart);
                if (dic != null)
                {
                    dic.Remove(Key);
                    AddCookies(cart, JsonMapper.ToJson(dic));
                }
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

        #region 返回購物車列表
        /// <summary>
        /// 返回購物車列表
        /// </summary>
        /// <returns>IList</returns>
        protected IList<Model.cart_items> get_cart_list(string cart)
        {

            IList<Model.cart_items> ls = GetList(cart);
            if (ls == null)
            {
                ls = new List<Model.cart_items>();
            }
            return ls;
        }
        #endregion

        #region 獲得購物車列表
        public IList<Model.cart_items> GetList(string cart)
        {
            IDictionary<string, string> dic = GetCart(DTKeys.COOKIE_SHOPPING_CART) as Dictionary<string, string>;
            if (dic != null)
            {
                IList<Model.cart_items> iList = new List<Model.cart_items>();
                foreach (var item in dic)
                {
                    try
                    {
                        Model.article_goods model = bll.GetGoodsModel(Utils.StrToInt(item.Key, 0));
                        Model.cart_items modelt = new Model.cart_items();
                        if (model != null)
                        {
                            modelt.title = model.title;
                            modelt.img_url = model.img_url;
                            modelt.price = model.sell_price;
                            modelt.id = model.id;
                            modelt.quantity = Utils.StrToInt(item.Value, 0);
                        }
                        iList.Add(modelt);
                    }
                    catch (Exception eee) { }
                }
                return iList;
            }
            return null;
        }
        #endregion

        #region 擴展方法==========================================
        public Model.cart_total GetTotal(string cart)
        {
            decimal c = 0, k = 0, g = 0;
            Model.cart_total model = new Model.cart_total();
            IList<Model.cart_items> iList = GetList(cart);
            if (iList != null)
            {
                foreach (Model.cart_items modelt in iList)
                {
                    model.total_quantity += modelt.quantity;
                    model.payable_amount += modelt.price * modelt.quantity;
                    model.real_amount += modelt.user_price * modelt.quantity;
                    model.total_point += modelt.point * modelt.quantity;
                }
                model.payable_amount += Utils.StrToDecimal(Fee, 0);
                model.real_amount += Utils.StrToDecimal(Fee, 0);
                model.total_num = int.Parse(c.ToString()) + int.Parse(k.ToString()) + int.Parse(g.ToString());
            }
            return model;
        }
        #endregion

        #region 添加對象到cookies
        /// <summary>
        /// 添加對象到cookies
        /// </summary>
        /// <param name="strValue"></param>
        private void AddCookies(string cart, string strValue)
        {
            Utils.WriteCookie(cart, strValue, 43200); //儲存一個月
        }
        #endregion

        #region 獲取cookies值
        /// <summary>
        /// 獲取cookies值
        /// </summary>
        private IDictionary<string, string> GetCart(string cart)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(GetCookies(cart)))
            {
                return JsonMapper.ToObject<Dictionary<string, string>>(GetCookies(cart));
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 添加對象到cookies
        /// <summary>
        /// 獲取cookies
        /// </summary>
        /// <returns></returns>
        private static string GetCookies(string cart)
        {
            return Utils.GetCookie(cart);
        }
        #endregion


    }
}