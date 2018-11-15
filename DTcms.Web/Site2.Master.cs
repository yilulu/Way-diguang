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
    public partial class Site2 : System.Web.UI.MasterPage
    {
        public string TitleHtml1 = "";
        public string TitleHtml2 = "";
        public string TitleHtml3 = "";
        int PartentID = 0;
        DTcms.DAL.category dalCategory = new DAL.category();
        public List<DTcms.Model.category> ListCategory = new List<Model.category>();
        public string HostUrl = string.Empty;
        protected int TodayCount, TotalCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            HostUrl = "http://" + Utils.GetHomeUrl();
            this.btnSearch.UseSubmitBehavior = false;
            if (!IsPostBack)
            {
                BindCategory();
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
                TitleHtml1 += "<li class=\"lis\"><a href='javascript:void(0)' data='" + table1.Rows[i]["id"] + "' onclick='GetArea(" + table1.Rows[i]["id"] + ")'>" + table1.Rows[i]["title"] + "</a></li>";
            }

            //用途
            var table2 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=4");
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                TitleHtml2 += "<span data='" + table2.Rows[i]["id"] + "' onclick='gets_value(this,2)' class=\"cur\">" + table2.Rows[i]["title"] + "</span>";
            }

            //租金範圍
            var table3 = this.GetDataTableBySql("dt_sys_model", " and inherit_index=2");
            for (int i = 0; i < table3.Rows.Count; i++)
            {
                TitleHtml3 += "<span data='" + table3.Rows[i]["id"] + "' onclick='HideValue(3)' class=\"cur\">" + table3.Rows[i]["title"] + "</span>";
            }
        }

        private void BindCategory()
        {
            ListCategory = dalCategory.GetCategAndChildList(0, 5);

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
            this.PartentID = DTRequest.GetQueryInt("Pcid");
            int mid = 5;
            string key = sousuo_22.Value;
            string url = string.Format("Search.aspx?mid={0}&key={1}&Pcid={2}", mid, key,this.PartentID);
            Response.Redirect(url);
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
