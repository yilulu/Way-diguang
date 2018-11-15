using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

namespace DTcms.Web
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        SQLHelper SQlHelper = new SQLHelper();
        BLL.article bll = new BLL.article();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind(1);
            }
        }

        private void Bind(int page)
        {
            BLL.article bll = new BLL.article();
            int totalCount;
            int pageSize = 10;
            //最新商品
            this.repdate.DataSource = bll.GetGoodsList(pageSize, page, " Status=3", "sort_id asc,add_time desc", out totalCount);
            this.repdate.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = totalCount;
        }

        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            Bind(aspPage.CurrentPageIndex);
        }

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

        /// <summary>
        /// 獲取成交時間
        /// </summary>
        /// <returns></returns>
        public string GetChengjiaodate(object id)
        {
            try
            {
                string sql = "select add_time from dt_orders o join dt_order_goods d on o.id=d.order_id  where d.goods_id=" + id;
                var query = SQlHelper.ExecuteScalar(sql, CommandType.Text);
                return query == null ? "" : Convert.ToDateTime(query).ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                return "";
            }

        }

        /// <summary>
        /// 獲取區域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAreaName(object id)
        {
            string sql = "select (p2.title)Title1,(p1.title)Title2 from dt_Area p1 left join dt_Area p2 on p1.parent=p2.id where p1.id=5";  //市/區
            var query = SQlHelper.GetDataTable(sql, CommandType.Text);
            if (query.Rows.Count > 0)
            {
                return (query.Rows[0]["Title1"] + "/" + query.Rows[0]["Title2"]).ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
