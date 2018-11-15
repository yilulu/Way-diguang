using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Data;
using Common;

namespace DTcms.Web
{
    public partial class WebForm3 : PageBase
    {
        protected int channel_id;
        BLL.article bll = new BLL.article();
        public string HtmlTuijian = "";
        public string HtmlTemai = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = 5;
            if (!IsPostBack)
            {
                Bind(1);
            }
        }

        private void Bind(int page)
        {
            int totalCount;
            int pageSize = 6;
            //最新商品
            string where = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Pcid"]))
            {
                where = " and category_id=" + Request.QueryString["Pcid"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                where += " and title like '%" + Request.QueryString["key"] + "%'";
            }
            this.repdatenew.DataSource = bll.list_pagesWhere(page, pageSize, " and channel_id=" + channel_id + " and Postid=1 and Status=1" + where, " order by sort_id asc ");
            this.repdatenew.DataBind();
            totalCount = bll.GetTatalNum("  channel_id=" + channel_id + " and Postid=1 and Status=1" + where);


            //推薦商品
            int pageSize2 = 9;
            var table = bll.list_pagesWhere(page, pageSize2, " and channel_id=" + channel_id + " and  Postid=2  and Status=1", " order by sort_id asc");
            this.repdateTuijian.DataSource = table;
            this.repdateTuijian.DataBind();
            if (table.Rows.Count > 0)
            {
                if (table.Rows.Count > 0)
                {
                    HtmlTuijian = "<a  href=\"producJPview.aspx?id=" + table.Rows[0]["id"] + "&mid=" + table.Rows[0]["channel_id"] + "\" >" +
                        "<img src=\"" + table.Rows[0]["img_url"] + "\" width='354' height='444' border=\"0\" /></a>" +
                    "<div class=\"yingcanbt2\">" +
                        "<div class=\"tmc2\">" +
                        "</div>" +
                        "<div class=\"zi2\">" +
                           " <div class=\"ycbianhao2\">" +
                               " 1</div>" +
                            "<div class=\"btjs2\">" +
                                "<h1>" +
                                    "" + table.Rows[0]["title"] + "</h1>" +
                                "<h2>" +
                                    "" + table.Rows[0]["sell_price"] + "元</span></h2>" +
                            "</div>" +
                        "</div>" +
                    "</div>";
                }
            }


            //特賣商品
            var table2 = bll.list_pagesWhere(page, pageSize2, " and channel_id=" + channel_id + " and  Postid=3  and Status=1 " + where, " order by sort_id asc");
            if (table2.Rows.Count > 0)
            {
                if (table2.Rows.Count > 0)
                {
                    HtmlTemai = "<a href=\"producJPview.aspx?id=" + table2.Rows[0]["id"] + "&mid=" + table2.Rows[0]["channel_id"] + "\" >" +
                            "<img src=\"" + table2.Rows[0]["img_url"] + "\" width='354' height='444' border=\"0\" /></a>" +
                        "<div class=\"yingcanbt2\">" +
                            "<div class=\"tmc2\">" +
                            "</div>" +
                            "<div class=\"zi2\">" +
                               " <div class=\"ycbianhao2\">" +
                                   " 1</div>" +
                                "<div class=\"btjs2\">" +
                                    "<h1>" +
                                        "" + table2.Rows[0]["title"] + "</h1>" +
                                    "<h2>" +
                                        "" + table2.Rows[0]["sell_price"] + "元 <span class=\"pings\"></span></h2>" +
                                "</div>" +
                            "</div>" +
                        "</div>";
                }
            }
            this.repdatetemai.DataSource = table2;
            this.repdatetemai.DataBind();

        }
    }
}
