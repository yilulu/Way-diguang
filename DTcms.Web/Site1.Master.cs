using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using DTcms.Common;
using System.Xml;

namespace DTcms.Web
{
    public partial class Site1 : MasterPage
    {
        protected int channel_id;
        public string TitleHtml1 = "";
        public string TitleHtml2 = "";
        public string TitleHtml3 = "";
        public string HostUrl = string.Empty;
        protected int TodayCount, TotalCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSearch.UseSubmitBehavior = false;
            this.channel_id = DTRequest.GetQueryInt("mid");
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
            int parent_id = 22;
            if (this.channel_id == 3)
            {
                parent_id = 55;
            }
            var table2 = GetDataTableBySql("dt_category", " and parent_id=" + parent_id + "");
            if (table2.Rows.Count > 0)
            {
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    TitleHtml2 += "<li class=\"lis\"><a href='javascript:void(0)' data='" + table2.Rows[i]["id"] + "' onclick='SetValue(this,4," + table2.Rows[i]["id"] + ")'>" + table2.Rows[i]["title"] + "</a></li>";
                }
            }

            BLL.sys_model bll = new DTcms.BLL.sys_model();
            string _strWhere = string.Format(" 1=1 and inherit_index='{0}'", 4);
            var table3 = bll.GetList(_strWhere).Tables[0];
            if (table3.Rows.Count > 0)
            {
                for (int i = 0; i < table3.Rows.Count; i++)
                {
                    TitleHtml3 += "<li class=\"lis\"><a href='javascript:void(0)' data='" + table3.Rows[i]["id"] + "' onclick='Sethuxing(this,5," + table3.Rows[i]["id"] + ")'>" + table3.Rows[i]["title"] + "</a></li>";
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

        #region 搜尋
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int czid = 0;
            int hxid = 0;
            int zjid = 0;
            int CataID = 0;
            int HuXingID = 0;
            czid = string.IsNullOrEmpty(hdf1.Value) ? 0 : int.Parse(hdf1.Value);
            hxid = string.IsNullOrEmpty(hdf2.Value) ? 0 : int.Parse(hdf2.Value);
            //zjid = string.IsNullOrEmpty(hdf3.Value) ? 0 : int.Parse(hdf3.Value);
            CataID = string.IsNullOrEmpty(hdf4.Value) ? 0 : int.Parse(hdf4.Value);
            HuXingID = string.IsNullOrEmpty(hdf5.Value) ? 0 : int.Parse(hdf5.Value);
            string key = string.Empty;
            if (sousuo_22.Value != "請輸入社區、街道、學校、商圈" && !string.IsNullOrEmpty(sousuo_22.Value))
            {
                key = sousuo_22.Value;
            }

            if (hdf0.Value == "2")
            {
                string url = string.Format("productsearch.aspx?mid=2&czid={0}&hxid={1}&key={2}&CataID={3}&hx={4}", czid, hxid, key, CataID, HuXingID);
                Response.Redirect(url);
            }
            else
            {
                string url = string.Format("productsearch.aspx?mid=3&czid={0}&hxid={1}&key={2}&CataID={3}&hx={4}", czid, hxid, key, CataID, HuXingID);
                Response.Redirect(url);
            }
        }
        #endregion
    }
}
