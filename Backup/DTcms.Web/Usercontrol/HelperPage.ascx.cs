using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.BLL;
using System.Data;

namespace DTcms.Web
{
    public partial class HelperPage : System.Web.UI.UserControl
    {
        DTcms.DAL.category dalcateg = new DAL.category();
        DAL.article dalArticle = new DAL.article();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetBind();
        }
        public string HtmlSte = "";
        private void GetBind()
        {
            BLL.article bll = new BLL.article();
            DataTable table = dalcateg.GetChildList(0, 100);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                HtmlSte += "<li><span class=\"xiaotu\"><img src=\"" + table.Rows[i]["img_url"] + "\" name=\"xgj_tu\" id=\"xgj_tu\" /></span><span class=\"help-zi\"><h2>" + table.Rows[i]["title"] + "</h2>";
                var table2 = bll.list_pagesWhere(1, 10000, " and channel_id =100 and category_id=" + table.Rows[i]["id"], " order by sort_id asc");
                if (table2 != null && table2.Rows.Count > 0)
                {
                    for (int j = 0; j < table2.Rows.Count; j++)
                    {
                        HtmlSte += "<span class=\"xgj_ms\"><a href='newsview.aspx?type=1&id=" + table2.Rows[j]["id"] + "' target='_blank'>" + table2.Rows[j]["title"] + "</a>";
                        HtmlSte += "</span>";
                    }
                }
                HtmlSte += "</span></li>";
                string SS = HtmlSte.Replace("newsview.aspx?type=1&id=372", "http://lvr.land.moi.gov.tw/N11/changemenu.action");
                string SS1 = SS.Replace("newsview.aspx?type=1&id=371", "http://lvr.land.moi.gov.tw/N11/changemenu.action");
                string SS2 = SS1.Replace("newsview.aspx?type=1&id=161", "http://lvr.land.moi.gov.tw/N11/changemenu.action");
                HtmlSte = SS2;

            }
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string ToSubstring(string obj, int Length)
        {
            try
            {
                if (obj.Length > Length)
                {
                    return obj.Substring(0, Length) + "...";
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}