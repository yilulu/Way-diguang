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
    public partial class TdView : PageBase
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
                lblConnet.Text = model.lianxiren;
                lblTitle.Text = model.title;
                lblContent.Text = model.content;
                Image1Url = model.img_url;
                lblSellPrice.Text = model.single_price.ToString();
                lblTotalPrice.Text = model.sell_price.ToString();
                lblyajin.Text = model.shangpinType.ToString();
                lblZjprice.Text = model.market_price.ToString();
                //lblzuoxiang.Text = model.zuoxiang;
                lblpingshu.Text = model.mianji.ToString();
                BLL.category bllCata = new BLL.category();
                Model.category Cata = bllCata.GetModel(model.xianlu);
                lblNo.Text = model.shequ;
                switch (model.category_id)
                {
                    case 302:
                        hire.Visible = true;
                        sell.Visible = false;
                        break;
                    case 303:
                        hire.Visible = false;
                        sell.Visible = true;
                        break;
                    case 328:
                        hire.Visible = true;
                        sell.Visible = true;
                        break;
                }
                //if (Cata != null)
                //{
                //    int Pid = Cata.parent_id;
                //    string FirstStaton = bllCata.GetModel(Pid).title;
                if (model.quyu == 0 || string.IsNullOrEmpty(model.quyu.ToString()))
                {
                    lblStation.Text = "無資料";
                }
                else
                {
                    lblStation.Text = model.quyu.ToString() + "%";
                }

                // }
                lblhuxing.Text = model.stock_quantity == 1 ? "有" : "無";
                Cata = bllCata.GetModel(model.category_id);
                lblxingneng.Text = model.xingneng;
                lblAge.Text = model.fuwuxiangju.ToString();
                if (!string.IsNullOrEmpty(model.link_url))
                {
                    lbllouceng.Text = model.link_url.ToString();
                }
                //if (Cata != null)
                //{
                //    lblyongtu.Text = Cata.title;
                //}
                lblFenQu.Text = GetUserArea(model.point);
                if (model.jiaqianQJ == 0 || string.IsNullOrEmpty(model.jiaqianQJ.ToString()))
                {
                    lblchwei.Text = "無資料";
                }
                else
                {
                    lblchwei.Text = model.jiaqianQJ.ToString() + "%";
                }
                //lblshequ.Text = model.shequ;

                if (model.huxing == 0 || string.IsNullOrEmpty(model.huxing.ToString()))
                {
                    lbldizhi.Text = "無資料";
                }
                else
                {
                    lbldizhi.Text = model.huxing.ToString() + "米";
                }
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
            //BLL.article_comment bll = new BLL.article_comment();
            //this.rptList.DataSource = bll.GetList(10, " is_reply=1 and article_id=" + id + "", " add_time desc");
            //this.rptList.DataBind();
        }
        #endregion

        #region 添加評論
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    Model.article_comment model = new Model.article_comment();
        //    //HttpCookie cook = Request.Cookies["WEBUSERID"];
        //    string cook = Utils.GetCookie("WEBUSERID").ToString();
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
        //    model.article_id = id;
        //    model.user_ip = DTcms.Common.DTRequest.GetIP();
        //    BLL.article_comment bll = new BLL.article_comment();
        //    bll.Add(model);
        //    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('送出成功');window.location.href='productview.aspx?id=" + id + "&mid=2'</script>");
        //}
        #endregion


        #region 獲取分區標題
        public string GetUserArea(int ID)
        {
            string Name = string.Empty;
            if (ID != 0)
            {
                int CataID = ID;
                BLL.category CATA = new BLL.category();
                Name = CATA.GetTitle(CataID);

            }
            return Name;
        }
        #endregion
    }
}