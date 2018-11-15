using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

namespace DTcms.Web
{
    public partial class shjdown : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            repdata.DataSource = GetDataTableBySql("dt_users", " and group_id=5");
            repdata.DataBind();
        }
        /// <summary>
        /// 通过表名，条件获取数据，返回DataTable
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Where">条件格式: AND 1=1</param>
        /// <returns></returns>
        public DataTable GetDataTableBySql(string TableName, string Where)
        {
            try
            {
                SQLHelper SQlHelper = new SQLHelper();
                string sql = string.Format("SELECT top 5 * FROM {0} WHERE 1=1 {1} order by {2}", TableName, Where, " exp asc");
                
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