using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using Common;

namespace DTcms.Web
{
    public partial class productview1 : PageBase
    {
        public string Image1Url = "";
        public string Images = "";
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id =Convert.ToInt32(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {
            BLL.article bll = new BLL.article();
            Model.article_goods model = bll.GetGoodsModel(id);
            if (model != null)
            {
                lblTitle.Text = model.title;
                lblContent.Text = model.content;
                Image1Url = model.img_url;
                lblprice.Text = model.sell_price.ToString();
                lblyajin.Text = model.yajin.ToString();
                lblzuoxiang.Text = model.zuoxiang;
                lblpingshu.Text = model.mianji.ToString();
                lbllouceng.Text = model.louceng;
                lblxingneng.Text = model.xingneng;
                lblyongtu.Text = model.yongtu;
                lblchwei.Text = model.chewei;
                lblshequ.Text = model.shequ;
                lbldizhi.Text = model.dizhi;

                lblquyu.Text = GetTypleWhereTilte(model.quyu);
                lbljiaqianqj.Text = GetTypleWhereTilte(model.jiaqianQJ);
                lblmianji.Text = model.mianji.ToString();
                lblhuxing.Text = GetTypleWhereTilte(model.huxing);
                lblhuxing2.Text = GetTypleWhereTilte(model.huxing);
                lblfangshi.Text = GetTypleWhereTilte(model.fangshi);

                foreach (var item in model.albums)
                {
                    Images += " <img src=\"" + item.small_img + "\" alt=\"" + item.remark + "\" width=\"68\" height=\"50\" rel=\"" + item.big_img + "\"/>";
                }
                repdateImgae.DataSource = model.albums;
                repdateImgae.DataBind();
            }
        }
    }
}