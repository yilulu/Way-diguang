using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using Common;
using System.Data;

namespace DTcms.Web
{
    public partial class index : PageBase
    {
        public string strHtmlPost1 = "";
        public string strHtmlPost1_left = "";
        public string strHtmlPost1_top = "";
        public string strHtmlPost1_bottom = "";

        //裝潢設計
        public string strHtmlPost4_left = "";
        public string strHtmlPost4_zhongjian = "";
        public string strHtmlPost4_right = "";

        //帝光精品
        public string strHtmlPost5_left = "";
        public string strHtmlPost5_right = "";

        public string strAdvImage1 = "";
        public string strAdvImage2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind1();
                Bind2();
                Bind3();
                Bind4();
                LoadPicList();

                //獲取首頁廣告
                DTcms.DAL.imagedal dal = new DAL.imagedal();
                int count = 0;
                var table = dal.GetDatalistpage(9999999, 1, " Typeid=22", "sort", out count).Tables[0];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strAdvImage1 = "<a href='" + table.Rows[i]["link_url"] + "' ><img src='" + table.Rows[i]["img_url"].ToString() + "' name=\"guanggao1_1\" id=\"guanggao1_1\" /></a>";
                    }
                    if (i == 1)
                    {
                        strAdvImage2 = "<a href='" + table.Rows[i]["link_url"] + "' ><img src='" + table.Rows[i]["img_url"].ToString() + "' name=\"guanggao1_1\" id=\"guanggao1_1\" /></a>";
                    }
                }
            }
        }

        #region 成交
        public string GetChengjiao(int mid)
        {

            string html = "";
            string sql = @"select * from dt_orders where id in
(select order_id from dt_order_goods where goods_id in 
(select id from dt_article where channel_id=" + mid + " and Status=3))";
            var table = GetTableBySQL(sql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var day = DateTime.Now.Day - (Convert.ToDateTime(table.Rows[i]["add_time"]).Day);
                html += "  <li><span class=\"wuzhu\">" + table.Rows[i]["user_name"] + "</span><span class=\"chengjiao\">" + day + "天前成交</span></li><li class=\"chengjiao1\">" + ToSubstring(table.Rows[i]["message"], 15) + "</li>";
            }
            return html;
        }
        #endregion

        #region 獲取區域戶型
        /// <summary>
        /// 獲取區域戶型
        /// </summary>
        public string GetTypeWhereString(int mid, int type)
        {
            string Htmlquyu = "";
            string Htmlhuxing = "";
            string url1 = "/productlist.aspx";
            //區域
            if (type == 1)
            {
                var table1 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=1");
                for (int i = 0; i < table1.Rows.Count; i++)
                {
                    string param = url1 + "?mid=" + mid + "&qyid=" + table1.Rows[i]["id"];
                    if (i < 7)
                    {
                        Htmlquyu += "<span class=\"1l_tishi2\" ><a style=\"color: #51ab27\" href='" + param + "'>" + table1.Rows[i]["title"] + "</a></span>&nbsp;&nbsp;";
                    }
                }
                return Htmlquyu;
            }
            else
            {
                //戶型
                var table4 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=4");
                for (int i = 0; i < table4.Rows.Count; i++)
                {
                    string param = url1 + "?mid=" + mid + "&hxid=" + table4.Rows[i]["id"];
                    if (i < 7)
                    {
                        Htmlhuxing += "<span class=\"1l_tishi2\"><a style=\"color: #51ab27\" href='" + param + "'>" + table4.Rows[i]["title"] + "</a></span>&nbsp;&nbsp;";
                    }
                }
                return Htmlhuxing;
            }
        }
        #endregion

        DAL.article dalArticle = new article();

        #region 新房屋推薦
        /// <summary>
        /// 新房屋推薦
        /// </summary>
        public void Bind1()
        {
            repdateNew.DataSource = dalArticle.GetPageindexList("", 2, "  and Postid=6");  //兩條新房推薦數據
            repdateNew.DataBind();

            var table1 = dalArticle.GetPageindexList("1", 8, " and channel_id=3 or  channel_id=2").Tables[0];
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                if (i < 6)
                {
                    if (i == 0)
                    {
                        strHtmlPost1 = string.Format("<div class=\"img\" id=\"bigImg\">" +
                                "<a href=\"productview.aspx?id=" + table1.Rows[i]["id"] + "&mid=" + table1.Rows[i]["channel_id"] + "\"  >" +
                                    "<img height=\"444\" width=\"275\" alt=\"" + table1.Rows[i]["title"] + "\" src=\"" + table1.Rows[i]["img_url"] + "\"></a>" +
                                "<div class=\"img_mask\">" +
                                "</div>" +
                                "<p>" +
                                    "<a href=\"productview.aspx?id=" + table1.Rows[i]["id"] + "&mid=" + table1.Rows[i]["channel_id"] + "\"  >" + table1.Rows[i]["title"] + "</a></p>" +
                            "</div>");
                    }
                    strHtmlPost1_left += "<div class=\"img\">" +
                                "<a href=\"productview.aspx?id=" + table1.Rows[i]["id"] + "&mid=" + table1.Rows[i]["channel_id"] + "\"  >" +
                                    "<img height=\"147\" width=\"172\" alt=\"" + table1.Rows[i]["title"] + "\" src=\"" + table1.Rows[i]["img_url"] + "\"" +
                                        "rel=\"" + table1.Rows[i]["img_url"] + "\"></a>" +
                                "<div class=\"mask\" style=\"width: 172px; height: 147px; display: block;\">" +
                                "</div>" +
                            "</div>";
                }
                else if (i >= 6 && i < 8)
                {
                    if (i == 3)
                    {
                        strHtmlPost1_top += "<div class=\"img\">" +
                                        "<a href=\"productview.aspx?id=" + table1.Rows[i]["id"] + "&mid=" + table1.Rows[i]["channel_id"] + "\"  >" +
                                            "<img height=\"200\" width=\"235\" alt=\"" + table1.Rows[i]["title"] + "\" src=\"" + table1.Rows[i]["img_url"] + "\" rel=\"" + table1.Rows[i]["img_url"] + "\"></a>" +
                                        "<div class=\"mask\" style=\"width: 235px; height: 200px; display: block;\">" +
                                        "</div>" +
                                    "</div>";
                    }
                    else
                    {
                        strHtmlPost1_top += "<div class=\"img\">" +
                                         "<a href=\"productview.aspx?id=" + table1.Rows[i]["id"] + "&mid=" + table1.Rows[i]["channel_id"] + "\"  >" +
                                             "<img height=\"200\" width=\"150\" alt=\"" + table1.Rows[i]["title"] + "\" src=\"" + table1.Rows[i]["img_url"] + "\" rel=\"" + table1.Rows[i]["img_url"] + "\"></a>" +
                                         "<div class=\"mask\" style=\"width: 150px; height: 200px; display: block;\">" +
                                         "</div>" +
                                     "</div>";
                    }
                }
                else
                {
                    strHtmlPost1_bottom += "<div class=\"img\">" +
                                    "<a href=\"productview.aspx?id=" + table1.Rows[i]["id"] + "&mid=" + table1.Rows[i]["channel_id"] + "\"  >" +
                                        "<img height=\"243\" width=\"127\" alt=\"" + table1.Rows[i]["title"] + "\" src=\"" + table1.Rows[i]["img_url"] + "\" rel=\"" + table1.Rows[i]["img_url"] + "\"></a>" +
                                    "<div class=\"mask\" style=\"width: 127px; height: 243px; display: none;\">" +
                                    "</div>" +
                                "</div>";
                }
            }
        }
        #endregion

        #region 不動產出租  不動產出售
        /// <summary>
        /// 不動產出租  不動產出售
        /// </summary>
        public void Bind2()
        {
            //不動產出租
            repdatepost2.DataSource = dalArticle.GetPageindexList("", 5, " and channel_id=3");
            repdatepost2.DataBind();

            //不動產出售
            repdatepost3.DataSource = dalArticle.GetPageindexList("", 5, " and channel_id=2");
            repdatepost3.DataBind();
        }
        #endregion

        #region 裝潢設計
        public void Bind3()
        {
            var table = dalArticle.GetPageindexList("", 13, " and channel_id=4").Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i < 6)
                {
                    strHtmlPost4_left += "<div class=\"bdlxt\">" +
                 "<div class=\"huiyoubt\">" +
                     "<a href=\"User_zh.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\"  >" +
                         "<img src=\"" + table.Rows[i]["img_url"] + "\" name=\"bdccz_xtu1\" height=\"155\" width=\"135\" border=\"0\" /></a>" +
                     "<div class=\"yingcanbt\">" +
                         "<div class=\"tmc\">" +
                         "</div>" +
                         "<div class=\"zi\">" +
                             "<div class=\"ycbianhao\">" +
                              "" + (i + 1) + "</div>" +
                             "<div class=\"btjs\">" +
                                 "" + this.ToSubstring(table.Rows[i]["title"].ToString(), 10) + "</div>" +
                         "</div>" +
                     "</div>" +
                 "</div>" +
             "</div>";
                }
                else if (i == 6)
                {
                    strHtmlPost4_zhongjian = "<div class=\"huiyoubt1\">" +
               "<a href=\"User_zh.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\"  >" +
                   "<img src=\"" + table.Rows[i]["img_url"] + "\" width=\"460\" height=\"495\" border=\"0\" /></a>" +
               "<div class=\"yingcanbt1\">" +
                   "<div class=\"tmc1\">" +
                   "</div>" +
                   "<div class=\"zi1\">" +
                       "<div class=\"ycbianhao1\">" +
                           "" + (i + 1) + "</div>" +
                       "<div class=\"btjs1\">" +
                           "<h1>" +
                               "" + this.ToSubstring(table.Rows[i]["title"].ToString(), 9) + "</h1>" +
                           "<h2>" +
                               "" + this.ToSubstring(table.Rows[i]["title"].ToString(), 9) + " <span class=\"pings\">" + table.Rows[i]["mianji"] + "</span></h2>" +
                       "</div>" +
                   "</div>" +
               "</div>" +
           "</div>";
                }
                else
                {
                    strHtmlPost4_right += "<div class=\"bdlxt\">" +
                   "<div class=\"huiyoubt\">" +
                       "<a href=\"User_zh.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\"  >" +
                           "<img src=\"" + table.Rows[i]["img_url"] + "\" name=\"bdccz_xtu1\" height=\"155\" width=\"135\" border=\"0\" /></a>" +
                       "<div class=\"yingcanbt\">" +
                           "<div class=\"tmc\">" +
                           "</div>" +
                           "<div class=\"zi\">" +
                               "<div class=\"ycbianhao\">" +
                                "" + (i + 1) + "</div>" +
                               "<div class=\"btjs\">" +
                                   "" + this.ToSubstring(table.Rows[i]["title"].ToString(), 10) + "</div>" +
                           "</div>" +
                       "</div>" +
                   "</div>" +
               "</div>";
                }
            }
        }
        #endregion

        #region 帝光精品
        public void Bind4()
        {
            var table = dalArticle.GetPageindexList("", 9, "and channel_id=5").Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i == 0)
                {
                    strHtmlPost5_left = " <a href=\"producJPview.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\"  >" +
                    "<img width='170' height='150' src=\"" + table.Rows[i]["img_url"] + "\" border=\"0\" /></a>" +
                "<div class=\"yingcanbt2\">" +
                    "<div class=\"tmc2\">" +
                    "</div>" +
                    "<div class=\"zi2\">" +
                        "<div class=\"ycbianhao2\">" +
                            "1</div>" +
                        "<div class=\"btjs2\">" +
                            "<h1>" +
                                "" + this.ToSubstring(table.Rows[i]["title"].ToString(), 30) + "</h1>" +
                            "<h2>" +
                                "" + table.Rows[i]["sell_price"] + "元 </h2>" +
                        "</div>" +
                    "</div>" +
                "</div>";
                }
                else
                {
                    strHtmlPost5_right += "<ul>" +
                "<li class=\"4l_xiaotu\"><a href=\"producJPview.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\"  >" +
                    "<img src=\"" + table.Rows[i]["img_url"] + "\" width='170' height='150' border=\"0\" /></a></li>" +
                "<a href=\"producJPview.aspx?id=" + table.Rows[i]["id"] + "\"><span class=\"l_jp_wenzi_tu\">" + this.ToSubstring(table.Rows[i]["title"].ToString(), 30) + "</span></a>" +
                "<li class=\"l_jp_wenzi_tu2\"><span class=\"miaoshu1\">" + table.Rows[i]["sell_price"] + "元</span></li>" +
            "</ul>";
                }
            }
        }
        #endregion

        #region 加载轮转幻灯片
        public void LoadPicList()
        {
            string HtmlPrint = string.Empty;
            string HtmlNum = string.Empty;
            int channel_id = 11;
            DTcms.DAL.imagedal dal = new DAL.imagedal();
            int count = 0;
            DataTable dt = dal.GetDatalistpage(9999999, 1, "Typeid=" + channel_id + "", "sort", out count).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int Num = i + 1;
                    string LinkUrl = dt.Rows[i]["link_url"].ToString();
                    string imgUrl = dt.Rows[i]["img_url"].ToString();
                    HtmlPrint += "<div class=\"fcon\" style=\"display: none;\">";
                    HtmlPrint += "<a target=\"_blank\" href=\"" + LinkUrl + "\">";
                    HtmlPrint += "<img src=\"" + imgUrl + "\" style=\"opacity: 1;\"></a>";
                    HtmlPrint += "</div>";


                    HtmlNum += "<a href=\"javascript:void(0)\" hidefocus=\"true\" target=\"_self\" class=\"\"><i>" + Num + "</i></a>";
                }
                lblList.Text = HtmlPrint;
                lblNum.Text = HtmlNum;
            }
        }
        #endregion

        #region 返回字符串
        public string GetNameByID(string ItemId)
        {
            string HtmlValue = "20";
            if (!string.IsNullOrEmpty(ItemId))
            {
                int id = Convert.ToInt32(ItemId);
                BLL.sys_model bll = new BLL.sys_model();
                Model.sys_model model = bll.GetModel(id);
                if (model != null)
                {
                    HtmlValue = model.title;
                }
            }
            return HtmlValue;
        }
        #endregion

    }
}
