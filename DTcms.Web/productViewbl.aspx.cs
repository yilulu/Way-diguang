using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Text;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class productViewbl : PageBase
    {
        public string Image1Url = "", big5Address = string.Empty, X, Y;
        public string Images = "";
        protected int id = 0, channel_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"]);
            this.channel_id = DTRequest.GetQueryInt("mid");
            if (!IsPostBack)
            {
                Bind();
                ListComment();
            }
        }

        #region 加載
        private void Bind()
        {
            BLL.article bll = new BLL.article();
            Model.article_goods model = bll.GetGoodsModel(id);
            if (model != null)
            {
                LabTel.Text = model.dianhua;
                lblTitle.Text = model.title;
                lblContent.Text = model.content;
                Image1Url = model.img_url;
                //lblprice.Text = model.sell_price.ToString();
                //lblyajin.Text = model.yajin.ToString();
                //lblzuoxiang.Text = model.zuoxiang;
                //lblpingshu.Text = GetTypleWhereTilte(model.mianji);
                //lbllouceng.Text = model.louceng;
                //lblxingneng.Text = model.xingneng;
                //lblyongtu.Text = model.yongtu;
                //lblchwei.Text = model.chewei;
                //lblshequ.Text = model.shequ;
                lbldizhi.Text = model.dizhi;
                string Adress = model.dizhi;
                string Values = model.goods_no;
                big5Address = System.Web.HttpUtility.UrlEncode(Adress, Encoding.GetEncoding("UTF-8"));
                if (!string.IsNullOrEmpty(Values))
                {
                    if (Values.IndexOf('|') > 0)
                    {
                        string[] ArrList = Values.Split('|');
                        X = ArrList[0];
                        Y = ArrList[1];
                    }
                }
                //lblquyu.Text = GetTypleWhereTilte(model.quyu);
                //lbljiaqianqj.Text = GetTypleWhereTilte(model.jiaqianQJ);
                //lblmianji.Text = GetTypleWhereTilte(model.mianji);
                //lblhuxing.Text = GetTypleWhereTilte(model.huxing);
                //lblhuxing2.Text = GetTypleWhereTilte(model.huxing);
                //lblfangshi.Text = GetTypleWhereTilte(model.fangshi);

                foreach (var item in model.albums)
                {

                    Images += " <li><a  href='" + item.small_img + "'><img src=\"" + item.small_img + "\" alt=\"" + item.remark + "\" width=\"68\" height=\"50\" rel=\"" + item.big_img + "\"/></a></li>";
                }
                repdateImgae.DataSource = model.albums;
                repdateImgae.DataBind();
            }
        }
        #endregion

        #region  加載評論
        protected void ListComment()
        {
            BLL.article_comment bll = new BLL.article_comment();
            //this.rptList.DataSource = bll.GetList(10, " is_reply=1", " add_time desc");
            //this.rptList.DataBind();
        }
        #endregion

        #region 添加評論
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    Model.article_comment model = new Model.article_comment();
        //    HttpCookie cook = Request.Cookies["VIPIDS"];
        //    if (cook == null)
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('請先登入');window.location.href='login_vip.aspx'</script>");
        //    }
        //    else
        //    {
        //        //ids = cook.Value;
        //    }
        //    string Content = Request.Form["txtContent"];

        //    model.content = DTcms.Common.Utils.ToHtml(Content);
        //    model.article_id = 111;
        //    model.user_ip = DTcms.Common.DTRequest.GetIP();
        //    BLL.article_comment bll = new BLL.article_comment();
        //    bll.Add(model);
        //    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('評論添加成功');window.location.href='productview.aspx?id=" + id + "&mid=2'</script>");
        //}
        #endregion

    }
}