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
    public partial class productview : PageBase
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
                p_1.Visible = false;
                p_2_3.Visible = true;
                p_2.Visible = false;
                if (model.category_id == 298)
                {
                    p_1.Visible = true;
                    p_2_3.Visible = false;
                    p_2.Visible = false;
                }
                if (model.category_id == 299)
                {
                    pingshu.Visible = false;
                    p_1.Visible = false;
                    p_2_3.Visible = false;
                    p_2.Visible = true;
                }

                LabTel.Text = model.dianhua;
                lblConnet.Text = model.lianxiren;
                lblTitle.Text = model.title;
                lblSingPrice.Text = model.yongtu;
                lblContent.Text = model.content;
                Image1Url = model.img_url;
                lblprice.Text = model.sell_price.ToString();
                //lblyajin.Text = model.yajin.ToString();
                lblZjprice.Text = model.sell_price.ToString();
                lblzuoxiang.Text = model.zuoxiang;
                lblpingshu.Text = model.mianji.ToString();
                BLL.category bllCata = new BLL.category();
                Model.category Cata = bllCata.GetModel(model.xianlu);
                if (Cata != null)
                {
                    int Pid = Cata.parent_id;
                    string FirstStaton = bllCata.GetModel(Pid).title;
                    lblStation.Text = FirstStaton + "-" + Cata.title;
                }
                lblhuxing.Text = GetTypleWhereTilte(model.huxing, null);
                Cata = bllCata.GetModel(model.category_id);
                lblxingneng.Text = model.xingneng;
                lblAge.Text = model.digg_good.ToString();
                if (!string.IsNullOrEmpty(model.louceng))
                {
                    lbllouceng.Text = model.louceng.ToString();
                }
                if (Cata != null)
                {
                    lblyongtu.Text = Cata.title;
                }

                lblchwei.Text = model.chewei;
                lblPort.Text = model.chewei;
                lblshequ.Text = model.shequ;
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

                foreach (var item in model.albums)
                {

                    Images += " <li><a  href='" + item.small_img + "'><img src=\"" + item.small_img + "\" alt=\"" + item.remark + "\" width=\"68\" height=\"50\" rel=\"" + item.big_img + "\"/></a></li>";
                }

                #region 宴會廳
                lblDestTable.Text = model.link_url;  //桌數
                lblkictich.Text = "無";
                if (model.stock_quantity == 1)
                {
                    lblkictich.Text = "有";   //廚房
                }
                lblStag.Text = "無";
                if (model.fangshi == 1)
                {
                    lblStag.Text = "有"; //舞台
                }
                lblMuisu.Text = "無";
                if (model.quyu == 1)
                {
                    lblMuisu.Text = "無";    //音響
                }
                lbllou1.Text = model.louceng;

                #endregion

                #region 戶外廣告
                lblForm.Text = model.shangpinType;
                lblLou2.Text = model.louceng;
                lblChiCun.Text = model.fuwuxiangju;

                #endregion

                repdateImgae.DataSource = model.albums;
                repdateImgae.DataBind();
            }
        }
        #endregion

        #region  加載評論
        protected void ListComment()
        {
            BLL.article_comment bll = new BLL.article_comment();
            this.rptList.DataSource = bll.GetList(10, " is_reply=1 and article_id=" + id + "", " add_time desc");
            this.rptList.DataBind();
        }
        #endregion

        #region 添加評論
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.article_comment model = new Model.article_comment();
            //HttpCookie cook = Request.Cookies["WEBUSERID"];
            string cook = Utils.GetCookie("WEBUSERID").ToString();
            if (cook == null)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('請先登入');window.location.href='login_vip.aspx'</script>");
            }
            else
            {
                //ids = cook.Value;
            }
            string Content = Request.Form["txtContent"];

            model.content = DTcms.Common.Utils.ToHtml(Content);
            model.article_id = id;
            model.user_ip = DTcms.Common.DTRequest.GetIP();
            BLL.article_comment bll = new BLL.article_comment();
            bll.Add(model);
            this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('送出成功');window.location.href='productview.aspx?id=" + id + "&mid=2'</script>");
        }
        #endregion

    }
}