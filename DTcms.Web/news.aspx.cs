using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Common;
using Common;
using System.Text.RegularExpressions;
using System.Data;

namespace DTcms.Web
{
    public partial class news : PageBase
    {
        public int channel_id;
        protected int category_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("mid");

            this.category_id = DTRequest.GetQueryInt("category_id");
            RptBind();
            if (!IsPostBack)
            {
                Bind(1);
            }
        }

        private void Bind(int pageindex)
        {
            BLL.article bll = new BLL.article();
            int pageSize = 10;
            string strWhere = "and channel_id=" + channel_id + "";
            if (this.category_id != 0)
            {
                strWhere += " and category_id=" + this.category_id + "";
            }
            this.repdata.DataSource = bll.list_page(pageindex, pageSize, strWhere, " order by sort_id asc");
            this.repdata.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = bll.GetNewsTatalNum(" 1=1" + strWhere);
        }
        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            Bind(aspPage.CurrentPageIndex);
        }

        public string HtmlSubstring(object ob)
        {
            string str = NoHTML(ob.ToString());
            str = ToSubstring(str, 120);
            return str;
        }

        #region 去除HTML
        public string NoHTML(string Htmlstring)
        {
            //删除腳本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }
        #endregion

        #region 數據綁定
        private void RptBind()
        {
            BLL.category bll = new BLL.category();
            int partentID = 0;
            DataTable dt = bll.GetList(partentID, this.channel_id);
            if (dt.Rows.Count > 0)
            {
                rptList.DataSource = dt.DefaultView;
                rptList.DataBind();
            }

        }
        #endregion
    }
}