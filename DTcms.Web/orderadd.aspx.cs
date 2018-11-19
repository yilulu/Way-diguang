using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.BLL;
using Common;
using DTcms.Model;
using DTcms.Common;
using LitJson;
using System.Data;

namespace DTcms.Web
{
    public partial class WebForm11 : PageBase
    {
        public string TotalPrice; protected string Fee, point, pointMoney, shengyu, PousePrice; decimal TwoTypeFee = 0;
        string orderNo = string.Empty;
        BLL.users User = new BLL.users();
        protected Model.cart_total cartModel;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!WEBUserCurrent.IsLogin)
            {
                Response.Redirect("login.aspx");
            }

            if (string.IsNullOrEmpty(Request.QueryString["cjid"]))
            {
                if (string.IsNullOrEmpty(GetCookies(DTKeys.COOKIE_SHOPPING_CART)))
                {
                    Response.Redirect("productJPlist.aspx");
                }
            }

            LoadFee();
            if (!IsPostBack)
            {
                orderNo = DateTime.Now.ToString("yyyyMMddhhssmm");
                hideNo.Value = orderNo;
                //CalculationPoint();
                Show_Car();
                GetUser_Info();

            }
        }

        #region 把商品寫入購物車
        private void AddCart()
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


        BLL.article bll = new BLL.article();
        BLL.orders bllorder = new BLL.orders();

        #region 加载个人信息
        private void GetUser_Info()
        {
            Model.users User = new Model.users();
            int UserID = WEBUserCurrent.UserID;
            BLL.users bllUser = new BLL.users();
            User = bllUser.GetModel(UserID);
            if (User != null)
            {
                txtusername.Value = User.user_name;
                txtlianxidianhua.Value = User.telphone;
                txtdizhi.Value = User.address;
                txtphone.Value = User.mobile;
                UserEmail.Value = User.email;
            }
        }
        #endregion

        #region 显示购物车中的产品
        /// <summary>
        /// 显示购物车中的产品
        /// </summary>
        private void Show_Car()
        {
            repddata.DataSource = get_cart_list(string.Empty);
            repddata.DataBind();

            cartModel = GetTotal(DTKeys.COOKIE_SHOPPING_CART);
            decimal decimalTotal = 0;

            //decimalTotal = decimalTotal + decimal.Parse(Fee);
            TotalPrice = decimalTotal.ToString("0.00");
            double zhekouPrice = double.Parse(((decimal.Parse(TotalPrice) * 15) / 100).ToString());
            string result = zhekouPrice.ToString("#0");
            int ZheKoupoints = Utils.StringToNum(result);
            int UID = WEBUserCurrent.UserID;
            if (!string.IsNullOrEmpty(UID.ToString()))
            {
                BLL.users User = new BLL.users();
                Model.users mod = new Model.users();

                mod = User.GetModel(UID);
                if (mod != null)
                {
                    point = mod.point.ToString();
                    pointMoney = point;
                    if (ZheKoupoints >= 150)
                    {
                        ZheKoupoints = 150;
                    }

                    if (mod.point > ZheKoupoints)
                    {
                        pointMoney = ZheKoupoints.ToString();
                    }
                    else
                    {
                        pointMoney = point;
                    }
                    hidePoint.Value = pointMoney;
                    BLL.point_log pointsLog = new BLL.point_log();
                    Model.point_log model = new Model.point_log();
                    model.user_id = UID;
                    model.user_name = orderNo;
                    model.value = Utils.StringToNum(pointMoney);
                    model.remark = "購物時折抵點數";
                    model.add_time = DateTime.Now;
                    model.type = 2;//2標誌點數是減少
                    int m = pointsLog.Add(model);
                }
            }

            shengyu = (cartModel.payable_amount - decimal.Parse(pointMoney) + decimal.Parse(Fee)).ToString();
            PousePrice = (decimal.Parse(TotalPrice) + decimal.Parse(Fee)).ToString();
            if (hideFee.Value == "2")
            {
                decimal FeeLv = TwoTypeFee;
                if (decimal.Parse(TotalPrice) >= FeeLv)
                {
                    PousePrice = TotalPrice;
                    shengyu = (decimal.Parse(TotalPrice) - decimal.Parse(pointMoney)).ToString();
                }

            }


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

        #region 送出訂單
        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {

            IDictionary<string, string> dic = GetCart(DTKeys.COOKIE_SHOPPING_CART) as Dictionary<string, string>;
            cartModel = GetTotal(DTKeys.COOKIE_SHOPPING_CART);
            if (dic == null)
            {
                Response.Redirect("productJPlist.aspx");
            }
            Model.orders model = new Model.orders();
            model.order_no = hideNo.Value;
            model.user_id = WEBUserCurrent.UserID;
            model.user_name = WEBUserCurrent.UserName;
            model.payment_id = string.IsNullOrEmpty(ddlzhifu.SelectedValue) ? 0 : int.Parse(ddlzhifu.SelectedValue);  //付款類型
            model.distribution_id = string.IsNullOrEmpty(ddlpeisong.SelectedValue) ? 0 : int.Parse(ddlpeisong.SelectedValue);  //配送類型
            model.accept_name = txtusername.Value;
            model.post_code = txtyoubian.Value;
            model.telphone = txtlianxidianhua.Value;
            model.mobile = txtphone.Value;
            model.address = txtdizhi.Value;
            model.message = txtliuyan.Value;



            model.payable_freight = decimal.Parse(Fee); //應付運費
            model.real_freight = decimal.Parse(Fee);  //實付運費

            model.payment_fee = 0;  //付款手續費
            model.point = Utils.StringToNum(hidePoint.Value);  //獲得的積分
            model.add_time = DateTime.Now;
            model.order_goods = new List<order_goods>();
            decimal Price = 0m; decimal GoodsPrice = 0, GoodToablPrice = 0;

            if (dic != null)
            {
                IList<Model.cart_items> iList = new List<Model.cart_items>();
                foreach (var item in dic)
                {
                    try
                    {
                        Model.article_goods modelPro = bll.GetGoodsModel(Utils.StrToInt(item.Key, 0));
                        Model.cart_items modelt = new Model.cart_items();
                        if (model != null)
                        {
                            int ProductID = modelPro.id;
                            int ShopNumber = Utils.StrToInt(item.Value, 0);
                            Model.article_goods modelpro = bll.GetGoodsModel(ProductID);
                            Price = modelpro.sell_price;
                            GoodsPrice = ShopNumber * Price;
                            GoodToablPrice += GoodsPrice;


                            Model.order_goods detail = new Model.order_goods();
                            detail.goods_id = ProductID;
                            detail.goods_name = modelPro.title;
                            detail.goods_price = modelPro.sell_price;
                            detail.point = Utils.StringToNum(hidePoint.Value);
                            detail.quantity = ShopNumber;
                            detail.real_price = modelpro.market_price;
                            model.order_goods.Add(detail);
                        }
                    }
                    catch (Exception eee) { }
                }


            }
            #region 算折扣
            string aa = Request.Form["chkPoint"];
            decimal PousePrice = 0, LastPrice = 0;
            if (!string.IsNullOrEmpty(aa))
            {
                PousePrice = GoodToablPrice - decimal.Parse(hidePoint.Value);
                //model.payable_amount = model.order_amount;
                //model.real_amount = model.order_amount;
            }
            else
            {
                PousePrice = GoodToablPrice;
            }
            #endregion

            #region 算运费
            if (hideFee.Value == "2")
            {
                decimal FeeLv = TwoTypeFee;
                if (PousePrice >= FeeLv)
                {
                    LastPrice = PousePrice;
                    model.payable_freight = 0; //應付運費
                    model.real_freight = 0;  //實付運費
                }
                else
                {
                    LastPrice = PousePrice + decimal.Parse(Fee);  //訂單總金額
                }
            }
            else
            {
                LastPrice = PousePrice + decimal.Parse(Fee);  //訂單總金額
            }
            #endregion

            model.order_amount = LastPrice;  //訂單總金額
            model.payable_amount = LastPrice;  //應付商品總金額
            model.real_amount = LastPrice; //實付商品總金額


            #region 清空购物车
            Clear(DTKeys.COOKIE_SHOPPING_CART, "0");
            #endregion 清空购物车

            //普通訂單的情況下修改狀態
            if (!string.IsNullOrEmpty(Request.QueryString["cjid"]))
            {
                int id = Convert.ToInt32(Request.QueryString["cjid"]);
                BLL.article bll = new BLL.article();
                Model.article_goods productmodel = bll.GetGoodsModel(id);
                bll.UpdateField(id, "Status=3");
                model.status = 6;
            }
            int bk = bllorder.Add(model);
            string Url = "pay.aspx?";
            Url += "paymenttype=" + ddlzhifu.SelectedValue + "";
            Url += "&id=" + bk + "";
            // string UserUrl = "userinfo.aspx";
            if (bk > 0)
            {
                //setEmail(model.order_no);
                UpUserPoint(PousePrice);
                int Uid = WEBUserCurrent.UserID;
                User.UpJianPoint(Uid, Utils.StringToNum(hidePoint.Value));
                AddAmount(LastPrice);
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('送出訂單成功，請牢記訂單號：" + model.order_no + "');window.location.href = '" + Url + "';</script>");
                // Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "pay", "window.open('" + Url + "');window.location.href='" + UserUrl + "';", true);

                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "pay", "window.open('" + Url + "');", true);

                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "transfer", "window.location.href='" + UserUrl + "';", true);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('送出失敗，請重新送出');window.location.href = '" + Url + "';</script>");
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

        #region 發送郵件
        private void setEmail(string Nom)
        {
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            string Content = "你購買的商品訂單已經生成" + "請牢記訂單號：" + Nom + "" + "!";
            DTMail.sendMail(model.emailstmp, model.emailport, model.emailfrom, model.emailpassword, model.emailusername, model.emailfrom, UserEmail.Value, "帝光房屋", Content);
        }
        #endregion

        #region 計算點數
        public void CalculationPoint()
        {
            int UID = WEBUserCurrent.UserID;
            if (!string.IsNullOrEmpty(UID.ToString()))
            {
                BLL.users User = new BLL.users();
                Model.users mod = new Model.users();

                mod = User.GetModel(UID);
                if (mod != null)
                {
                    point = mod.point.ToString();
                    pointMoney = point;
                    if (mod.point > 150)
                    {
                        pointMoney = "150";
                    }
                    else
                    {
                        pointMoney = point;
                    }
                    hidePoint.Value = pointMoney;
                    BLL.point_log points = new BLL.point_log();
                    Model.point_log model = new Model.point_log();
                    model.user_id = UID;
                    model.user_name = orderNo;
                    model.value = Utils.StringToNum(pointMoney);
                    model.remark = "購物時折抵點數";
                    model.add_time = DateTime.Now;
                    model.type = 2;//2標誌點數是減少
                    int m = points.Add(model);
                }
            }
        }
        #endregion

        #region 更新點數
        public void UpUserPoint(decimal price)
        {
            int points = 0; int typeValue = 100;
            int UID = WEBUserCurrent.UserID;

            Model.users mod = new Model.users();

            BLL.user_groups BLLGroup = new BLL.user_groups();
            mod = User.GetModel(UID);
            if (mod != null)
            {
                switch (mod.group_id)
                {
                    case 1:
                        typeValue = BLLGroup.GetZheKou(mod.group_id);
                        break;
                    case 2:
                        typeValue = BLLGroup.GetZheKou(mod.group_id);
                        break;
                    case 3:
                        typeValue = BLLGroup.GetZheKou(mod.group_id);
                        break;
                    case 4:
                        typeValue = BLLGroup.GetZheKou(mod.group_id);
                        break;
                }
                double s = double.Parse(((price * typeValue) / 100).ToString());
                string result = s.ToString("#0");
                points = Utils.StringToNum(result);
                User.UpPoint(UID, points);


                BLL.point_log point = new BLL.point_log();
                Model.point_log model = new Model.point_log();
                model.user_id = UID;
                model.user_name = hideNo.Value;
                model.value = points;
                model.remark = "購物回饋點數";
                model.add_time = DateTime.Now;
                model.type = 1;//2標誌點數是減少

                int m = point.Add(model);
            }

        }
        #endregion

        #region 添加消費記錄
        public void AddAmount(decimal price)
        {
            BLL.amount_log bllog = new BLL.amount_log();
            Model.amount_log log = new Model.amount_log();
            int Uid = WEBUserCurrent.UserID;
            log.user_id = Uid;
            log.value = price;
            //log.user_name = orderNo;
            log.order_no = hideNo.Value;
            log.payment_id = Utils.StringToNum(ddlzhifu.SelectedValue);
            log.status = 0;

            int n = bllog.Add(log);
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

        #region 获取對象到cookies
        /// <summary>
        /// 獲取cookies
        /// </summary>
        /// <returns></returns>
        private static string GetCookies(string cart)
        {
            return Utils.GetCookie(cart);
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


    }
}
