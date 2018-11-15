using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class Site3 : MasterPage
    {
        public string TitleHtml1 = "";
        public string TitleHtml2 = "";
        public string TitleHtml3 = "";
        public string HostUrl = string.Empty;
        protected int TodayCount, TotalCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            HostUrl = "http://" + Utils.GetHomeUrl();
            this.btnSearch.UseSubmitBehavior = false;
            if (!IsPostBack)
            {
                Binddata();
            }
            LoadAccess();
        }

        private void LoadAccess()
        {
            BLL.ipAccess bll = new BLL.ipAccess();

            TotalCount = bll.GetAllCount();
            TodayCount = bll.GetTodayCount();
        }

        private void Binddata()
        {

            var table2 = GetDataTableBySql("dt_category", " and parent_id=24");
            if (table2.Rows.Count > 0)
            {
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    TitleHtml2 += "<li class=\"lis\"><a href='javascript:void(0)' data='" + table2.Rows[i]["id"] + "' onclick='SetValue(this,4," + table2.Rows[i]["id"] + ")'>" + table2.Rows[i]["title"] + "</a></li>";
                }
            }
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

        #region 搜尋
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int czid = 0;
            int hxid = 0;
            int zjid = 0;
            czid = string.IsNullOrEmpty(hdf1.Value) ? 0 : int.Parse(hdf1.Value);

            string key = string.IsNullOrEmpty(sousuo_22.Value) ? "" : sousuo_22.Value;
            if (hdf0.Value == "2")
            {
                string url = string.Format("kjghsearch.aspx?mid=4&czid={0}", czid);
                Response.Redirect(url);
            }
            else
            {
                string url = string.Format("kjghsearch.aspx?mid=4&czid={0}", czid);
                Response.Redirect(url);
            }
        }
        #endregion

        protected void lbtnout_Click(object sender, EventArgs e)
        {
            HttpCookie ccookie1 = Response.Cookies["WEBUSERID"];
            HttpCookie ccookie2 = Response.Cookies["WEBUserNamecook"];
            HttpCookie ccookie3 = Response.Cookies["WEBRealNamecook"];
            HttpCookie ccookie4 = Response.Cookies["WEBUserTypecook"];
            if (ccookie1 != null)
            {
                ccookie1.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie1);
            }
            if (ccookie2 != null)
            {
                ccookie2.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie2);
            }
            if (ccookie3 != null)
            {
                ccookie3.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie3);
            }
            if (ccookie4 != null)
            {
                ccookie4.Expires = DateTime.Now.AddDays(-999);
                Request.Cookies.Add(ccookie4);
            }

            Response.Redirect("/");
        }
    }
}
