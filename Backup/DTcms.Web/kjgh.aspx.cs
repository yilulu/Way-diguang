using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Common;
using System.Text;
using System.Data;
using Common;

namespace DTcms.Web
{
    public partial class kjgh : PageBase
    {
        //裝潢設計
        public string strHtmlPost4_left = "";
        public string strHtmlPost4_zhongjian = "";
        public string strHtmlPost4_right = "";
        protected int channel_id;
        BLL.article bll = new BLL.article();
        string sqlwhere = ""; protected string title = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = 4;
            if (!IsPostBack)
            {
                Bind(1);
            }
        }
        public string GetTitle()
        {

            switch (channel_id)
            {
                case 2:
                    title = "<a href=\"productlist.aspx?mid=" + channel_id + "\">" + "最新出售" + "</a>";
                    break;
                case 3:
                    title = "<a href=\"productlist.aspx?mid=" + channel_id + "\">" + "最新出租" + "</a>";
                    break;
                case 4:
                    title = "<a href=\"productlist.aspx?mid=" + channel_id + "\">" + "空間規劃" + "</a>";
                    break;
            }
            return title;
        }

        /// <summary>
        /// 裝潢設計
        /// </summary>
        private void Bind(int page)
        {
            int pageSize = 13;
            //最新商品
            var table = bll.list_pagesWhere(page, pageSize, " and channel_id=" + channel_id + GetWhere() + " and Status=1", " order by sort_id asc");
            //this.repdate1.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = bll.GetTatalNum(" channel_id=" + channel_id + GetWhere() + " and Status=1"); ;


            // var table = dalArticle.GetPageindexList("4", 13).Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i < 6)
                {
                    strHtmlPost4_left += "<div class=\"bdlxt\">" +
                 "<div class=\"huiyoubt\">" +
                     "<a href=\"User_zh.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\">" +
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
               "<a href=\"User_zh.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\" >" +
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
                       "<a href=\"User_zh.aspx?id=" + table.Rows[i]["id"] + "&mid=" + table.Rows[i]["channel_id"] + "\">" +
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

        private string GetWhere()
        {
            StringBuilder str = new StringBuilder("");
            if (Request.QueryString["czid"] != null && Request.QueryString["czid"].ToString() != "0")
            {
                str.Append(" and Areaid=" + Request.QueryString["czid"]);
            }
            if (Request.QueryString["qyid"] != null && Request.QueryString["qyid"].ToString() != "0")
            {
                str.Append(" and quyu=" + Request.QueryString["qyid"]);
            }
            if (Request.QueryString["zjid"] != null && Request.QueryString["zjid"].ToString() != "0")
            {
                str.Append(" and jiaqianQJ=" + Request.QueryString["zjid"]);
            }
            if (Request.QueryString["mjid"] != null && Request.QueryString["mjid"].ToString() != "0")
            {
                str.Append(" and mianji=" + Request.QueryString["mjid"]);
            }
            if (Request.QueryString["hxid"] != null && Request.QueryString["hxid"].ToString() != "0")
            {
                str.Append(" and huxing=" + Request.QueryString["hxid"]);
            }

            if (Request.QueryString["fsid"] != null && Request.QueryString["fsid"].ToString() != "0")
            {
                str.Append(" and fangshi=" + Request.QueryString["fsid"]);
            }
            if (Request.QueryString["dtid"] != null && Request.QueryString["dtid"].ToString() != "0")
            {
                str.Append(" and xianlu=" + Request.QueryString["dtid"]);
            }
            return str.ToString();
        }

        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            Bind(aspPage.CurrentPageIndex);
        }

        SQLHelper SQlHelper = new SQLHelper();
        /// <summary>
        /// 獲取區域/地標 顯示名稱
        /// </summary>
        /// <returns></returns>
        public string GetTypleWhereTilte(int id, int? inherit_index = null)
        {
            try
            {
                string title = "";
                string sql = "select title from dt_sys_model where id=" + id;
                if (inherit_index != null)
                {
                    sql += " and inherit_index='" + inherit_index + "'";
                }
                var query = SQlHelper.ExecuteScalar(sql, CommandType.Text);
                title = query == null ? "" : query.ToString();
                return title;
            }
            catch (Exception)
            {
                return "";
            }

        }


    }
}