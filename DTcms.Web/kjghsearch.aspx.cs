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
    public partial class kjghsearch : System.Web.UI.Page
    {
        protected int channel_id;
        BLL.article bll = new BLL.article();
        string sqlwhere = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("mid");
            if (!IsPostBack)
            {
                Bind(1);
            }
        }
        public string GetTitle()
        {
            string title = "";
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
        private void Bind(int page)
        {
            int totalCount;
            int pageSize = 10;
            //最新商品
            this.repdate1.DataSource = bll.GetGoodsList(pageSize, page, " channel_id=" + channel_id + GetWhere() + "", "sort_id asc,add_time desc", out totalCount);
            this.repdate1.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = totalCount;
        }

        private string GetWhere()
        {
            StringBuilder str = new StringBuilder("");

            if (Request.QueryString["czid"] != null && Request.QueryString["czid"] != "0")
            {
                str.Append(" and category_id=" + Request.QueryString["czid"]);
            }
            if (Request.QueryString["key"] != "請輸入設計風格名稱" && !string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                str.AppendFormat(" and (title like '%{0}%' or content like '%{0}%' or shequ like '%{0}%' or dizhi like '%{0}%')", Request.QueryString["key"]);
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