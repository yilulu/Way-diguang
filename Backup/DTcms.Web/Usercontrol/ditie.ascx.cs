using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

namespace DTcms.Web
{
    public partial class ditie : System.Web.UI.UserControl
    {
        public string Htmlquyu = "";
        public string HtmljiaqianQJ = "";
        public string Htmlmianji = "";
        public string Htmlhuxing = "";
        public string Htmlfangshi = "";

        public string Htmlditie = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binnd();
            }
        }

        private void Binnd()
        {
            string url = "productsearch.aspx?mid=2&czid={0}&hxid={1}&zjid={2}";
            string url1 = Request.Url.AbsolutePath;

            if (Request.QueryString["qyid"] != null && Request.QueryString["qyid"].ToString() != "0")
            {
                Htmlquyu += "<span class='cs_huan4'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }
            else
            {
                Htmlquyu += "<span class='cs_huan3'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }

            if (Request.QueryString["zjid"] != null && Request.QueryString["zjid"].ToString() != "0")
            {
                HtmljiaqianQJ += "<span class='cs_huan4'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }
            else
            {
                HtmljiaqianQJ += "<span class='cs_huan3'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }

            if (Request.QueryString["mjid"] != null && Request.QueryString["mjid"].ToString() != "0")
            {
                Htmlmianji += "<span class='cs_huan4'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }
            else
            {
                Htmlmianji += "<span class='cs_huan3'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }

            if (Request.QueryString["hxid"] != null && Request.QueryString["hxid"].ToString() != "0")
            {
                Htmlhuxing += "<span class='cs_huan4'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }
            else
            {
                Htmlhuxing += "<span class='cs_huan3'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }

            if (Request.QueryString["fsid"] != null && Request.QueryString["fsid"].ToString() != "0")
            {
                Htmlfangshi += "<span class='cs_huan4'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }
            else
            {
                Htmlfangshi += "<span class='cs_huan3'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }


            if (Request.QueryString["dtid"] != null && Request.QueryString["dtid"].ToString() != "0")
            {
                Htmlditie += "<span class='cs_huan4'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }
            else
            {
                Htmlditie += "<span class='cs_huan3'><a href='" + url1 + "?mid=" + Request.QueryString["mid"] + "'>不限</a></span>";
            }


            //區域
            var table1 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=1");
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                string param = url1 + "?mid=" + Request.QueryString["mid"] + "&qyid=" + table1.Rows[i]["id"];
                string strclass = "cs_huan4";
                if (Request.QueryString["qyid"] != null && Request.QueryString["qyid"].ToString() == table1.Rows[i]["id"].ToString())
                {
                    strclass = "cs_huan3";
                }

                Htmlquyu += "<span class='" + strclass + "'><a href='" + param + "'>" + table1.Rows[i]["title"] + "</a></span>";
            }

            //租金範圍
            var table2 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=2");
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                string strclass = "cs_huan4";
                if (Request.QueryString["zjid"] != null && Request.QueryString["zjid"].ToString() == table2.Rows[i]["id"].ToString())
                {
                    strclass = "cs_huan3";
                }
                string param = url1 + "?mid=" + Request.QueryString["mid"] + "&zjid=" + table2.Rows[i]["id"];
                HtmljiaqianQJ += "<span  class='" + strclass + "'><a href='" + param + "'>" + table2.Rows[i]["title"] + "</a></span>";
            }

            //面積
            var table3 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=3");
            for (int i = 0; i < table3.Rows.Count; i++)
            {
                string strclass = "cs_huan4";
                if (Request.QueryString["mjid"] != null && Request.QueryString["mjid"].ToString() == table3.Rows[i]["id"].ToString())
                {
                    strclass = "cs_huan3";
                }
                string param = url1 + "?mid=" + Request.QueryString["mid"] + "&mjid=" + table3.Rows[i]["id"];
                Htmlmianji += "<span  class='" + strclass + "'><a href='" + param + "'>" + table3.Rows[i]["title"] + "</a></span>";
            }

            //戶型
            var table4 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=4");
            for (int i = 0; i < table4.Rows.Count; i++)
            {
                string strclass = "cs_huan4";
                if (Request.QueryString["hxid"] != null && Request.QueryString["hxid"].ToString() == table4.Rows[i]["id"].ToString())
                {
                    strclass = "cs_huan3";
                }
                string param = url1 + "?mid=" + Request.QueryString["mid"] + "&hxid=" + table4.Rows[i]["id"];
                Htmlhuxing += "<span  class='" + strclass + "'><a href='" + param + "'>" + table4.Rows[i]["title"] + "</a></span>";
            }

            //方式
            var table5 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=5");
            for (int i = 0; i < table5.Rows.Count; i++)
            {
                string strclass = "cs_huan4";
                if (Request.QueryString["fsid"] != null && Request.QueryString["fsid"].ToString() == table5.Rows[i]["id"].ToString())
                {
                    strclass = "cs_huan3";
                }
                string param = url1 + "?mid=" + Request.QueryString["mid"] + "&fsid=" + table5.Rows[i]["id"];
                Htmlfangshi += "<span  class='" + strclass + "'><a href='" + param + "'>" + table5.Rows[i]["title"] + "</a></span>";
            }

            //地鐵
            var table6 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=6");
            for (int i = 0; i < table6.Rows.Count; i++)
            {
                string strclass = "cs_huan4";
                if (Request.QueryString["dtid"] != null && Request.QueryString["dtid"].ToString() == table6.Rows[i]["id"].ToString())
                {
                    strclass = "cs_huan3";
                }
                string param = url1 + "?mid=" + Request.QueryString["mid"] + "&dtid=" + table6.Rows[i]["id"];
                Htmlditie += "<span  class='" + strclass + "'><a href='" + param + "'>" + table6.Rows[i]["title"] + "</a></span>";
            }

        }


        /// <summary>
        /// 獲取區域戶型
        /// </summary>
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

        /// <summary>
        /// 通過表名，條件獲取資料，返回DataTable
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Where">條件格式: AND 1=1</param>
        /// <returns></returns>
        public DataTable GetDataTableBySql(string TableName, string Where)
        {
            try
            {
                SQLHelper SQlHelper = new SQLHelper();
                string sql = string.Format("SELECT * FROM {0} WHERE 1=1 {1}", TableName, Where);
                var table = SQlHelper.GetDataTable(sql, System.Data.CommandType.Text);
                return table;
            }
            catch (Exception)
            {
                return new DataTable() { };
            }
        }
    }
} 
