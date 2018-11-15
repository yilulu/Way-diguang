using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Data;
using Common;
using System.Text;

namespace DTcms.Web
{
    public partial class userfabuedit : System.Web.UI.Page
    {

        protected internal Model.siteconfig siteConfig;
        int channel_id = 0;
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath("Configpath"));
            channel_id = Convert.ToInt32(ddlmodel.SelectedValue);
            this.id = DTRequest.GetQueryInt("id");
            if (id > 0)
            {
                trid.Visible = true;
                channel_id = DTRequest.GetQueryInt("mid");
            }
            else
            {
                trid.Visible = false;
                txtTime.Text = DateTime.Now.ToString();
            }
            if (!IsPostBack)
            {
                GetArea();
                GetCategory();

                if (id > 0)
                {
                    ShowInfo(id);
                }
            }

        }

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.article bll = new BLL.article();
            Model.article_goods model = bll.GetGoodsModel(_id);

            ddlCategoryId.SelectedValue = model.category_id.ToString();
            ddlmodel.SelectedValue = model.channel_id.ToString();
            channel_id = model.channel_id;
            GetCategory();
            txtTitle.Text = model.title;
            txtGoodsNo.Text = model.goods_no;
            txtStockQuantity.Text = model.stock_quantity.ToString();
            txtMarketPrice.Text = model.market_price.ToString();
            txtSellPrice.Text = model.sell_price.ToString();
            //txtPoint.Text = model.point.ToString();
            //txtLinkUrl.Text = model.link_url;
            txtTime.Text = model.add_time.ToString();
            //if (model.is_msg == 1)
            //{
            //    cblItem.Items[0].Selected = true;
            //}
            //if (model.is_top == 1)
            //{
            //    cblItem.Items[1].Selected = true;
            //}
            //if (model.is_red == 1)
            //{
            //    cblItem.Items[2].Selected = true;
            //}
            //if (model.is_hot == 1)
            //{
            //    cblItem.Items[3].Selected = true;
            //}
            //if (model.is_slide == 1)
            //{
            //    cblItem.Items[4].Selected = true;
            //}
            //if (model.is_lock == 1)
            //{
            //    cblItem.Items[5].Selected = true;
            //}
            //txtSortId.Text = model.sort_id.ToString();
            //txtClick.Text = model.click.ToString();
            //txtDiggGood.Text = model.digg_good.ToString();
            //txtDiggBad.Text = model.digg_bad.ToString();
            txtContent.Value = model.content;
            //txtSeoTitle.Text = model.seo_title;
            //txtSeoKeywords.Text = model.seo_keywords;
            //txtSeoDescription.Text = model.seo_description;
            //賦值用戶組價格
            if (model.goods_group_prices != null)
            {
                for (int i = 0; i < this.rptPrice.Items.Count; i++)
                {
                    int hideId = Convert.ToInt32(((HiddenField)this.rptPrice.Items[i].FindControl("hideGroupId")).Value);
                    foreach (Model.goods_group_price modelt in model.goods_group_prices)
                    {
                        if (hideId == modelt.group_id)
                        {
                            ((HiddenField)this.rptPrice.Items[i].FindControl("hidePriceId")).Value = modelt.id.ToString();
                            ((TextBox)this.rptPrice.Items[i].FindControl("txtGroupPrice")).Text = modelt.price.ToString();
                        }
                    }
                }
            }
            //賦值上傳的相冊
            focus_photo.Value = model.img_url; //封面圖片
            LitAlbumList.Text = GetAlbumHtml(model.albums, model.img_url);

            ddlquyu.SelectedValue = model.quyu.ToString();
            ddljiaqian.SelectedValue = model.jiaqianQJ.ToString();
            ddlmianji.SelectedValue = model.mianji.ToString();
            ddlhuxing.SelectedValue = model.huxing.ToString();
            ddlfangshi.SelectedValue = model.fangshi.ToString();
            ddlditie.SelectedValue = model.xianlu.ToString();

            txtyajin.Text = model.yajin.ToString();
            txtzuoxiang.Text = model.zuoxiang;
            txtlouceng.Text = model.louceng;
            txtxingneng.Text = model.xingneng;
            txtyongtu.Text = model.yongtu;
            txtchewei.Text = model.chewei;
            txtshequ.Text = model.shequ;
            txtdizhi.Text = model.dizhi;

            txtgongsi.Text = model.gongsi;
            txtfuwxiangmu.Text = model.fuwuxiangju;
            txtdianhua.Text = model.dianhua;
            txtuser.Text = model.lianxiren;
            txtshangpintype.Text = model.shangpinType;

        }
        #endregion


        #region 返回相簿清單HMTL=========================
        private string GetAlbumHtml(List<Model.article_albums> models, string focus_photo)
        {
            StringBuilder strTxt = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    strTxt.Append("<li>\n");
                    strTxt.Append("<input type=\"hidden\" name=\"hide_photo_name\" value=\"" + modelt.id + "|" + modelt.big_img + "|" + modelt.small_img + "\" />\n");
                    strTxt.Append("<input type=\"hidden\" name=\"hide_photo_remark\" value=\"" + modelt.remark + "\" />\n");
                    strTxt.Append("<div onclick=\"focus_img(this);\" class=\"img_box");
                    if (focus_photo == modelt.small_img)
                    {
                        strTxt.Append(" current");
                    }
                    strTxt.Append("\">\n");
                    strTxt.Append("<img bigsrc=\"" + modelt.big_img + "\" src=\"" + modelt.small_img + "\" />");
                    strTxt.Append("<span class=\"remark\"><i>");
                    if (!string.IsNullOrEmpty(modelt.remark))
                    {
                        strTxt.Append(modelt.remark);
                    }
                    else
                    {
                        strTxt.Append("暫無描述...");
                    }
                    strTxt.Append("</i></span></div>\n");
                    strTxt.Append("<a onclick=\"show_remark(this);\" href=\"javascript:;\">描述</a><a onclick=\"del_img(this);\" href=\"javascript:;\">刪除</a>\n");
                    strTxt.Append("</li>\n");
                }
            }
            return strTxt.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.article_goods model = new Model.article_goods();
            BLL.article bll = new BLL.article();

            model.channel_id = this.channel_id;
            model.title = txtTitle.Text.Trim();
            model.category_id = int.Parse(ddlCategoryId.SelectedValue);
            model.goods_no = txtGoodsNo.Text;
            model.stock_quantity = int.Parse(txtStockQuantity.Text);
            model.market_price = decimal.Parse(txtMarketPrice.Text);
            model.sell_price = decimal.Parse(txtSellPrice.Text);
            //model.point = int.Parse(txtPoint.Text);
            //model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = focus_photo.Value;
            model.content = txtContent.Value;
            //model.seo_title = txtSeoTitle.Text.Trim();
            //model.seo_keywords = txtSeoKeywords.Text.Trim();
            // model.seo_description = txtSeoDescription.Text.Trim();
            //model.sort_id = int.Parse(txtSortId.Text.Trim());
            //model.click = int.Parse(txtClick.Text.Trim());
            //model.digg_good = int.Parse(txtDiggGood.Text.Trim());
            // model.digg_bad = int.Parse(txtDiggBad.Text.Trim());
            model.add_time = DateTime.Parse(txtTime.Text.ToString());
            model.is_msg = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            model.is_lock = 0;
            //if (cblItem.Items[0].Selected == true)
            //{
            //    model.is_msg = 1;
            //}
            //if (cblItem.Items[1].Selected == true)
            //{
            //    model.is_top = 1;
            //}
            //if (cblItem.Items[2].Selected == true)
            //{
            //    model.is_red = 1;
            //}
            //if (cblItem.Items[3].Selected == true)
            //{
            //    model.is_hot = 1;
            //}
            //if (cblItem.Items[4].Selected == true)
            //{
            //    model.is_slide = 1;
            //}
            //if (cblItem.Items[5].Selected == true)
            //{
            //    model.is_lock = 1;
            //}
            //會員組價格
            //List<Model.goods_group_price> priceList = new List<Model.goods_group_price>();
            //for (int i = 0; i < rptPrice.Items.Count; i++)
            //{
            //    int _groupid = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
            //    decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
            //    priceList.Add(new Model.goods_group_price { group_id = _groupid, price = _price });
            //}
            //model.goods_group_prices = priceList;
            //保存相冊
            string[] albumArr = Request.Form.GetValues("hide_photo_name");
            string[] remarkArr = Request.Form.GetValues("hide_photo_remark");
            if (albumArr != null && albumArr.Length > 0)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { big_img = imgArr[1], small_img = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { big_img = imgArr[1], small_img = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }

            //擴展屬性
            BLL.attributes bll2 = new BLL.attributes();
            DataSet ds2 = bll2.GetList("channel_id=" + this.channel_id);

            List<Model.attribute_value> attrls = new List<Model.attribute_value>();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                int attr_id = int.Parse(dr["id"].ToString());
                string attr_title = dr["title"].ToString();
                string attr_value = Request.Form["content_" + attr_id];
                if (!string.IsNullOrEmpty(attr_value))
                {
                    attrls.Add(new Model.attribute_value { attribute_id = attr_id, title = attr_title, content = attr_value });
                }
            }
            model.attribute_values = attrls;

            //model.Postid = CheckBoxList1.SelectedValue;
            //model.Type = int.Parse(cblItem.SelectedValue);
            model.AddType = 1;
            model.Status = 0;

            if (!string.IsNullOrEmpty(ddlquyu.SelectedValue))
            {
                model.quyu = Convert.ToInt32(ddlquyu.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddljiaqian.SelectedValue))
            {
                model.jiaqianQJ = Convert.ToInt32(ddljiaqian.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlmianji.SelectedValue))
            {
                model.mianji = Convert.ToInt32(ddlmianji.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlhuxing.SelectedValue))
            {
                model.huxing = Convert.ToInt32(ddlhuxing.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlfangshi.SelectedValue))
            {
                model.fangshi = Convert.ToInt32(ddlfangshi.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlditie.SelectedValue))
            {
                model.xianlu = Convert.ToInt32(ddlditie.SelectedValue);
            }

            model.yajin = string.IsNullOrEmpty(txtyajin.Text) ? 0 : Convert.ToInt32(txtyajin.Text);
            model.zuoxiang = txtzuoxiang.Text;
            model.louceng = txtlouceng.Text;
            model.xingneng = txtxingneng.Text;
            model.yongtu = txtyongtu.Text;
            model.chewei = txtchewei.Text;
            model.shequ = txtshequ.Text;
            model.dizhi = txtdizhi.Text;
            model.gongsi = txtgongsi.Text;
            model.fuwuxiangju = txtfuwxiangmu.Text;
            model.dianhua = txtdianhua.Text;
            model.lianxiren = txtuser.Text;
            model.user_id = WEBUserCurrent.UserID;
            model.shangpinType = txtshangpintype.Text;
            //if (!bll.Update(model))
            //{
            //    result = false;
            //}


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
            BLL.article bll = new BLL.article();
            Model.article_goods model = bll.GetGoodsModel(_id);

            model.channel_id = this.channel_id;
            model.title = txtTitle.Text.Trim();
            model.category_id = int.Parse(ddlCategoryId.SelectedValue);
            model.goods_no = txtGoodsNo.Text;
            model.stock_quantity = int.Parse(txtStockQuantity.Text);
            model.market_price = decimal.Parse(txtMarketPrice.Text);
            model.sell_price = decimal.Parse(txtSellPrice.Text);
            //model.point = int.Parse(txtPoint.Text);
            //model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = focus_photo.Value;
            model.content = txtContent.Value;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.click = int.Parse(txtClick.Text.Trim());
            model.digg_good = int.Parse(txtDiggGood.Text.Trim());
            model.digg_bad = int.Parse(txtDiggBad.Text.Trim());
            model.add_time = DateTime.Parse(txtTime.Text.ToString());
            model.is_msg = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            model.is_lock = 0;
            //if (cblItem.Items[0].Selected == true)
            //{
            //    model.is_msg = 1;
            //}
            //if (cblItem.Items[1].Selected == true)
            //{
            //    model.is_top = 1;
            //}
            //if (cblItem.Items[2].Selected == true)
            //{
            //    model.is_red = 1;
            //}
            //if (cblItem.Items[3].Selected == true)
            //{
            //    model.is_hot = 1;
            //}
            //if (cblItem.Items[4].Selected == true)
            //{
            //    model.is_slide = 1;
            //}
            //if (cblItem.Items[5].Selected == true)
            //{
            //    model.is_lock = 1;
            //}
            //用戶組價格
            List<Model.goods_group_price> priceList = new List<Model.goods_group_price>();
            for (int i = 0; i < rptPrice.Items.Count; i++)
            {
                int hidPriceId = 0;
                if (!string.IsNullOrEmpty(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value))
                {
                    hidPriceId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value);
                }
                int hidGroupId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
                decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
                priceList.Add(new Model.goods_group_price { id = hidPriceId, article_id = _id, group_id = hidGroupId, price = _price });
            }
            model.goods_group_prices = priceList;
            //儲存相冊
            if (model.albums != null)
                model.albums.Clear();
            string[] albumArr = Request.Form.GetValues("hide_photo_name");
            string[] remarkArr = Request.Form.GetValues("hide_photo_remark");
            if (albumArr != null)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    int img_id = int.Parse(imgArr[0]);
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { id = img_id, article_id = _id, big_img = imgArr[1], small_img = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { id = img_id, article_id = _id, big_img = imgArr[1], small_img = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }

            //擴展屬性
            BLL.attributes bll2 = new BLL.attributes();
            DataSet ds2 = bll2.GetList("channel_id=" + this.channel_id);

            List<Model.attribute_value> attrls = new List<Model.attribute_value>();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                int attr_id = int.Parse(dr["id"].ToString());
                string attr_title = dr["title"].ToString();
                string attr_value_id = Request.Form["value_" + attr_id];
                string attr_value_content = Request.Form["content_" + attr_id];
                if (!string.IsNullOrEmpty(attr_value_id) && !string.IsNullOrEmpty(attr_value_content))
                {
                    attrls.Add(new Model.attribute_value { id = Convert.ToInt32(attr_value_id), article_id = _id, attribute_id = attr_id, title = attr_title, content = attr_value_content });
                }
            }
            model.attribute_values = attrls;



            //model.Postid =0;
            model.Type = 0;

            if (!string.IsNullOrEmpty(ddlquyu.SelectedValue))
            {
                model.quyu = Convert.ToInt32(ddlquyu.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddljiaqian.SelectedValue))
            {
                model.jiaqianQJ = Convert.ToInt32(ddljiaqian.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlmianji.SelectedValue))
            {
                model.mianji = Convert.ToInt32(ddlmianji.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlhuxing.SelectedValue))
            {
                model.huxing = Convert.ToInt32(ddlhuxing.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlfangshi.SelectedValue))
            {
                model.fangshi = Convert.ToInt32(ddlfangshi.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlditie.SelectedValue))
            {
                model.xianlu = Convert.ToInt32(ddlditie.SelectedValue);
            }

            model.yajin = string.IsNullOrEmpty(txtyajin.Text) ? 0 : Convert.ToInt32(txtyajin.Text);
            model.zuoxiang = txtzuoxiang.Text;
            model.louceng = txtlouceng.Text;
            model.xingneng = txtxingneng.Text;
            model.yongtu = txtyongtu.Text;
            model.chewei = txtchewei.Text;
            model.shequ = txtshequ.Text;
            model.dizhi = txtdizhi.Text;
            model.gongsi = txtgongsi.Text;
            model.fuwuxiangju = txtfuwxiangmu.Text;
            model.dianhua = txtdianhua.Text;
            model.lianxiren = txtuser.Text;
            model.shangpinType = txtshangpintype.Text;
            if (!bll.Update(model))
            {
                result = false;
            }
            return result;
        }
        #endregion



        //獲取對應的類別
        protected void ddlmodel_SelectedIndexChanged(object sender, EventArgs e)
        {
            channel_id = Convert.ToInt32(ddlmodel.SelectedValue);
            GetCategory();
        }

        /// <summary>
        /// 綁定類別
        /// </summary>
        private void GetCategory()
        {
            BLL.category bll = new BLL.category();
            DataTable dt = bll.GetList(0, channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("請選擇類別...", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }

        /// <summary>
        /// 綁定市區
        /// </summary>
        private void GetArea()
        {
            DAL.AreaDal dalare = new DAL.AreaDal();
            int count = 0;
            var table = dalare.GetDatalistpage(99999, 1, " parent=0", " sort", out count);
            if (table.Tables.Count > 0)
            {
                foreach (DataRow dr in table.Tables[0].Rows)
                {
                    string Id = dr["id"].ToString();
                    string Title = dr["title"].ToString().Trim();
                    ddlAreaid.Items.Add(new ListItem(Title, Id));

                    var table2 = dalare.GetDatalistpage(99999, 1, " parent=" + Id, " sort", out count);
                    if (table2.Tables.Count > 0)
                    {
                        foreach (DataRow dr2 in table2.Tables[0].Rows)
                        {
                            string Id2 = dr2["id"].ToString();
                            string Title2 = dr2["title"].ToString().Trim();
                            Title2 = "├ " + Title2;
                            Title2 = Utils.StringOfChar(2 - 1, "　") + Title2;
                            ddlAreaid.Items.Add(new ListItem(Title2, Id2));
                        }
                    }
                }
            }
        }

        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool bl = false;
            if (id > 0)
            {
                bl = DoEdit(id);
            }
            else
            {
                bl = DoAdd();
            }

            if (bl)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "<script>alert('送出成功!');window.location.href='userSJ.aspx';</script>", "");
                Response.Redirect("userSJ.aspx");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "<script>alert('送出失敗!')</script>", "");
            }
        }
    }
}
