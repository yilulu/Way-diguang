using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

namespace DTcms.Web
{
    public partial class WebForm9 : PageBase
    {
        BLL.article bll = new BLL.article();
        public int chushouCount = 0;
        public int chuzuCount = 0;
        int userid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userid = Convert.ToInt32(Request.QueryString["userid"]);
            }
            catch (Exception)
            {

                throw;
            }
            if (!IsPostBack)
            {
                Bind(1);
                Bind2(1);
            }
        }

        private void Bind(int page)
        {
            int totalCount;
            int pageSize = 10;

            //出租
            this.repdate1.DataSource = bll.GetGoodsList(pageSize, page, " channel_id=3 and user_id=" + userid, "sort_id asc,add_time desc", out totalCount);
            this.repdate1.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = totalCount;
            chuzuCount = totalCount;

        }
        private void Bind2(int page)
        {
            int totalCount;
            int pageSize2 = 10;

            //出售
            var table = bll.GetGoodsList(pageSize2, page, " channel_id=2 and user_id=" + userid, "sort_id asc,add_time desc", out totalCount);
            this.repdate2.DataSource = table;
            this.repdate2.DataBind();
            aspPage2.PageSize = pageSize2;
            aspPage2.RecordCount = totalCount;
            chushouCount = totalCount;
        }


        protected void aspPage_PageChanged(object sender, EventArgs e)
        {
            Bind(aspPage.CurrentPageIndex);
        }
        protected void aspPage2_PageChanged(object sender, EventArgs e)
        {
            Bind2(aspPage2.CurrentPageIndex);
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