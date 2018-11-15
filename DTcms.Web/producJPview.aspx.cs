using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Model;
using Common;
using System.Data;
using DTcms.Common;
using LitJson;

namespace DTcms.Web
{
    public partial class WebForm4 : PageBase
    {
        public string Image1Url = "", HtmlFee = string.Empty;
        public string Images = "";
        protected int id = 0;

        BLL.article bll = new BLL.article();
        public int Status = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"]);

            Bind();
            LoadFee();
        }

        #region 加載商品
        private void Bind()
        {
            Model.article_goods model = bll.GetGoodsModel(id);
            if (model != null)
            {
                lbltitle2.Text = model.title;
                lbltitle.Text = model.title;
                lblContent.Text = model.content;
                Image1Url = model.img_url;
                lblMarkePrice.Text = model.market_price.ToString();
                lblprice.Text = model.sell_price.ToString();
                if (model.shangpinType.ToString().Length >= 75)
                {
                    lblIntroduce.Text = model.shangpinType.ToString().Substring(0, 75);
                }
                else
                {
                    lblIntroduce.Text = model.shangpinType.ToString();
                }
                //lbllianxiren.Text = model.lianxiren;
                //lbldianhua.Text = model.dianhua;
                Status = model.Status;
                int i = 0;
                foreach (var item in model.albums)
                {
                    i++;
                    Images += " <li><a  href='" + item.small_img + "'><img src=\"" + item.small_img + "\" alt=\"" + item.remark + "\" width=\"68\" height=\"50\" rel=\"" + item.big_img + "\"/></a></li>";
                }
            }

            DAL.article dalArticle = new article();
            DataTable dt = dalArticle.GetPageJPList("1", 4, "   and channel_id=" + model.channel_id).Tables[0];
            if (dt.Rows.Count > 0)
            {
                repdatezuixin.DataSource = dt.DefaultView;
                repdatezuixin.DataBind();
            }

            dt = dalArticle.GetPageJPList("3", 5, "  and channel_id=" + model.channel_id).Tables[0];
            if (dt.Rows.Count > 0)
            {
                repdatetemai.DataSource = dt.DefaultView;
                repdatetemai.DataBind();
            }
        }
        #endregion

        #region 加入購物車第一步
        protected void btnAddcart_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AddCat(DTKeys.COOKIE_SHOPPING_CART, id.ToString(), "usercart.aspx");
                #region OLD
                //int intPicCount = int.Parse(Request.Form["text_box"]);
                //DataTable dt = (DataTable)Session["DGCart"];
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    DataRow dr = dt.Rows.Find(id);
                //    if (dr == null)
                //    {
                //        string strName;
                //        string strImage;
                //        decimal decimalPrice;
                //        int intActive;
                //        returnPicInfo(id, out strName, out strImage, out decimalPrice, out intActive);
                //        DataRow new_dr = dt.NewRow();
                //        new_dr["GoodsId"] = id;
                //        new_dr["GoodsName"] = strName;
                //        new_dr["GoodsImage"] = strImage;
                //        new_dr["GoodsPrice"] = decimalPrice;
                //        if (intActive == 2)
                //        {
                //            new_dr["GoodsCount"] = 1;
                //        }
                //        else
                //        {
                //            new_dr["GoodsCount"] = intPicCount;
                //        }
                //        new_dr["GoodsTotal"] = decimalPrice * intPicCount;
                //        new_dr["GoodsActive"] = intActive;
                //        dt.Rows.Add(new_dr);
                //    }

                //    else
                //    {
                //        int intActive = Convert.ToInt16(dr["GoodsActive"]);
                //        if (intActive != 2)
                //        {
                //            int intCount = Convert.ToInt16(dr["GoodsCount"]) + intPicCount;
                //            decimal decimalPrice = Convert.ToDecimal(dr["GoodsPrice"]);
                //            dr["GoodsCount"] = intCount;
                //            dr["GoodsTotal"] = decimalPrice * intCount;
                //        }
                //    }
                //}
                //else
                //{
                //    try
                //    {
                //        string strName;
                //        string strImage;
                //        decimal decimalPrice;
                //        int intActive;
                //        returnPicInfo(id, out strName, out strImage, out decimalPrice, out intActive);
                //        DataRow new_dr = dt.NewRow();
                //        new_dr["GoodsId"] = id;
                //        new_dr["GoodsName"] = strName;
                //        new_dr["GoodsImage"] = strImage;
                //        new_dr["GoodsPrice"] = decimalPrice;
                //        if (intActive == 2)
                //        {
                //            new_dr["GoodsCount"] = 1;
                //        }
                //        else
                //        {
                //            new_dr["GoodsCount"] = intPicCount;
                //        }
                //        new_dr["GoodsTotal"] = decimalPrice * intPicCount;
                //        new_dr["GoodsActive"] = intActive;
                //        dt.Rows.Add(new_dr);
                //    }
                //    catch (Exception ex)
                //    {
                //        DTcms.Common.Utils.MakeFile("else" + ex.ToString(), ex.StackTrace);
                //    }
                //}
                #endregion

            }
            catch (Exception ex)
            {
                DTcms.Common.Utils.MakeFile(ex.ToString(), ex.StackTrace);
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
        private Model.article_goods returnPicInfo(int intPicId, out string strName, out string strImage, out decimal decimalPrice, out int intActive)
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
            return model;
        }
        #endregion

        #region 立即結賬
        protected void btnAddOrder_Click(object sender, ImageClickEventArgs e)
        {
            AddCat(DTKeys.COOKIE_SHOPPING_CART, id.ToString(), "orderadd.aspx");
        }
        #endregion

        #region 顯示運費信息
        void LoadFee()
        {
            Ltf.Model.Freight mdl = new Ltf.Model.Freight();
            Ltf.BLL.Freight bllf = new Ltf.BLL.Freight();

            mdl = bllf.GetModel(1);
            if (mdl != null)
            {
                HtmlFee = "<span style=\"color: #e53333;\">";
                int typeID = mdl.typID;
                if (typeID == 1)
                {
                    HtmlFee += "全店免運費";
                }
                else if (typeID == 2)
                {
                    HtmlFee += mdl.TotalPrice + "元(含)以上免運費," + mdl.spec + "元以下收運費" + mdl.Fee + "元";
                }
                else if (typeID == 3)
                {
                    HtmlFee += "每筆訂單固定收取運費" + mdl.Fee + "元";
                }
                HtmlFee += "</span>";
            }

        }
        #endregion

        #region 加入购物车第二步
        void AddCat(string cart, string Key, string GoUrl)
        {
            //获取当前点击的商品信息
            int intPicCount = DTRequest.GetFormInt("text_box");
            IDictionary<string, string> dic = GetCart(cart);
            if (dic != null)
            {
                if (dic.ContainsKey(Key))
                {

                    if (cart == "dt_cookie_shopping_cart")
                    {
                        dic[Key] = (int.Parse(dic[Key]) + intPicCount).ToString();
                    }
                    AddCookies(cart, JsonMapper.ToJson(dic));
                }
                else
                {
                    if (!dic.ContainsKey(Key))
                    {
                        dic.Add(Key, intPicCount.ToString());
                        AddCookies(cart, JsonMapper.ToJson(dic));
                    }
                }
            }
            else
            {
                dic = new Dictionary<string, string>();
                //不存在的則新增
                if (!dic.ContainsKey(Key))
                {
                    dic.Add(Key, intPicCount.ToString());
                    AddCookies(cart, JsonMapper.ToJson(dic));
                }
            }
            this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功!');window.location.href='" + GoUrl + "'</script>");
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

        #region 獲取對象到cookies
        /// <summary>
        /// 獲取cookies
        /// </summary>
        /// <returns></returns>
        private string GetCookies(string cart)
        {
            return Utils.GetCookie(cart);
        }
        #endregion


    }
}