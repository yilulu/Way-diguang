using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public partial class ipAccess
    {
        public ipAccess()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string IP_Address)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_IpAccess");
            strSql.Append(" where IP_DateTime>= dateadd(ms,0,DATEADD(dd, DATEDIFF(dd,0,getdate()), 0)) and IP_DateTime <= dateadd(ms,-3,DATEADD(dd, DATEDIFF(dd,-1,getdate()), 0))");
            strSql.Append(" and IP_Address=@IP_Address");
            SqlParameter[] parameters = {
					new SqlParameter("@IP_Address", SqlDbType.NVarChar,50)};
            parameters[0].Value = IP_Address;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

     

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ipAccess model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_IpAccess(");
            strSql.Append("IP_Address,IP_DateTime)");
            strSql.Append(" values (");
            strSql.Append("@IP_Address,@IP_DateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@IP_Address", SqlDbType.NVarChar,100),
					new SqlParameter("@IP_DateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.iP_Address;
            parameters[1].Value = model.iP_DateTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        
  
        /// <summary>
        /// 获得全部数据
        /// </summary>
        public int GetAllCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select IP_Address from dt_IpAccess");
         
            return DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 获得当天数据
        /// </summary>
        public int GetTodayCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(IP_Address)  from dt_IpAccess");
            strSql.Append(" where IP_DateTime>= dateadd(ms,0,DATEADD(dd, DATEDIFF(dd,0,getdate()), 0)) and IP_DateTime <= dateadd(ms,-3,DATEADD(dd, DATEDIFF(dd,-1,getdate()), 0))");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows.Count;
        }

        #endregion  Method
    }
}