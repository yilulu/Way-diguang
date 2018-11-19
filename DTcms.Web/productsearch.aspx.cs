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
    public partial class productsearch : PageBase
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
            this.repdate1.DataSource = bll.list_pagesWhere(page, pageSize, "  and channel_id=" + channel_id + GetWhere() + "", " order by add_time desc");
            this.repdate1.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = bll.GetTatalNum("   channel_id=" + channel_id + GetWhere() + "");
        }

        private string GetWhere()
        {
            StringBuilder str = new StringBuilder("");
            str.Append(" and Status=1 ");
            if (Request.QueryString["hxid"] != null && Request.QueryString["hxid"] != "0")
            {
                str.Append(" and Areaid=" + Request.QueryString["hxid"]);
            }
            if (Request.QueryString["CataID"] != null && Request.QueryString["CataID"] != "0")
            {
                str.Append(" and category_id=" + Request.QueryString["CataID"]);
            }
            if (Request.QueryString["hx"] != null && Request.QueryString["hx"] != "0")
            {
                str.Append(" and huxing=" + Request.QueryString["hx"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["key"]))
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

        #region 獲取租或賣
        public string getSellOrHire(string price)
        {
            string ReturnHtml = string.Empty;
            if (this.channel_id == 2)
            {
                ReturnHtml = "總價:" + price + "萬";
            }
            if (channel_id == 3)
            {
                ReturnHtml = "租金:" + price + "元/月";
            }
            return ReturnHtml;
        }
        #endregion

        #region 獲取區域
        public string GetA(string id)
        {
            string AreaName = string.Empty;
            string cityName = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                AreaDal dal = new AreaDal();
                Model.Area model = new Model.Area();
                model = dal.GetModel(int.Parse(id));
                if (model != null)
                {
                    AreaName = model.title;
                    int partentID = model.parent;
                    Model.Area City = dal.GetModel(partentID);
                    if (City != null)
                    {
                        cityName = City.title;
                    }
                }
            }

            return cityName + "-" + AreaName;
        }
        #endregion

    }
}