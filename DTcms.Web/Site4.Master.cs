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
    public partial class Site4 : MasterPage
    {
        public string TitleHtml1 = "";
        public string TitleHtml2 = "";
        public string TitleHtml3 = "";
        public string HostUrl = string.Empty;
        protected int TodayCount, TotalCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            HostUrl = "http://" + Utils.GetHomeUrl();
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
            //城鎮
            var table1 = this.GetDataTableBySql("dt_Area", " and parent=0");
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                TitleHtml1 += "<li class=\"lis\"><a href='javascript:void(0)' data='" + table1.Rows[i]["id"] + "' onclick='gets_value(this,1," + table1.Rows[i]["id"] + ")'>" + table1.Rows[i]["title"] + "</a></li>";
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
