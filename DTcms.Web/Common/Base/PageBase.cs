using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Data;
using Common;
using DTcms.Common;

namespace Common
{
    public class PageBase : Page
    {
        SQLHelper SQlHelper1 = new SQLHelper();


        /// <summary>
        /// 父類的構造函數
        /// </summary>
        public PageBase()
        {
            #region 记录网站访问
            DTcms.BLL.ipAccess bll_ipAccess = new DTcms.BLL.ipAccess();
            DTcms.Model.ipAccess entity = new DTcms.Model.ipAccess();
            entity.iP_Address = DTRequest.GetIP();
            entity.iP_DateTime = DateTime.Now;
            if (!bll_ipAccess.Exists(entity.iP_Address))
            {
                bll_ipAccess.Add(entity);
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetTableBySQL(string sql)
        {
            var table = SQlHelper1.GetDataTable(sql, System.Data.CommandType.Text);
            return table;
        }

        /// <summary>
        /// 通過表名-條件欄位-條件value，獲取顯示的資訊
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="DisplayName">顯示的欄位</param>
        /// <param name="WhereID">條件欄位</param>
        /// <param name="IDvalue">value值</param>
        /// <returns></returns>
        public string GetDisplayName(string TableName, string DisplayName, string WhereID, object IDvalue)
        {
            try
            {
                string sql = string.Format("SELECT * FROM {0} WHERE {1}={2}", TableName, WhereID, IDvalue);
                var table = SQlHelper1.GetDataTable(sql, System.Data.CommandType.Text);
                return table.Rows[0][DisplayName].ToString();
            }
            catch (Exception)
            {
                return "未知";
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
                string sql = string.Format("SELECT * FROM {0} WHERE 1=1 {1} order by sort", TableName, Where);
                var table = SQlHelper1.GetDataTable(sql, System.Data.CommandType.Text);
                return table;
            }
            catch (Exception)
            {
                return new DataTable() { };
            }
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
                var query = SQlHelper1.ExecuteScalar(sql, CommandType.Text);
                title = query == null ? "" : query.ToString();
                return title;
            }
            catch (Exception)
            {
                return "";
            }

        }

        /// <summary>
        /// 截取字串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string ToSubstring(object obj, int Length)
        {
            try
            {
                if (obj.ToString().Length > Length)
                {
                    return obj.ToString().Substring(0, Length) + "...";
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

       

    }
}
