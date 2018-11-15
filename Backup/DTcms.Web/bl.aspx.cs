using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using Common;
using System.Data;
using DTcms.DAL;

namespace DTcms.Web
{
    public partial class bl : PageBase
    {
        protected int channel_id;protected  string FileUpUrl;
        BLL.article bll = new BLL.article();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("ID");
            if (!IsPostBack)
            {
                //Bind(1);
                Bind2(1);
                RptBind();
            }
        }

        #region 加載數據
        private void Bind2(int page)
        {
            BLL.article bll = new BLL.article();
            Model.article_goods model = bll.GetGoodsModel(channel_id);
            if (model != null)
            {
                FileUpUrl = model.link_url;
            }
        }
        #endregion

        #region 搜尋
        protected void aspPage2_PageChanged(object sender, EventArgs e)
        {

        }
        #endregion
        SQLHelper SQlHelper = new SQLHelper();

        #region 獲取區域/地標 顯示名稱
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
        #endregion

        #region 獲取區域戶型
        public string GetTypeWhereString()
        {
            int mid = string.IsNullOrEmpty(Request.QueryString["mid"]) ? 2 : Convert.ToInt32(Request.QueryString["mid"]);
            string Htmlquyu = "";
            string url1 = "/productlist.aspx";
            //區域

            var table1 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=1 and is_sys=1");
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                string param = url1 + "?mid=" + mid + "&qyid=" + table1.Rows[i]["id"];
                if (i < 5)
                {
                    Htmlquyu += "<span class=\"cs_2_bt_1\" ><a style=\"color: #51ab27\" href='" + param + "'>" + table1.Rows[i]["title"] + "</a></span>&nbsp;&nbsp;";
                }
            }
            return Htmlquyu;
        }
        #endregion

        #region 數據綁定
        private void RptBind()
        {
            BLL.category bll = new BLL.category();
            DataTable dt = bll.GetList(0, 6);
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion
    }
}
