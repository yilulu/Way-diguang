using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;

/// <summary>
///SQLHelper 的摘要說明
/// </summary>
namespace Common
{
    public class SQLHelper
    {
        public SQLHelper()
        {
            //
            //TODO: 在此處添加構造函數邏輯
            //
        } /// <summary>
        /// 資料庫連接字串
        /// </summary>
        private readonly string connstr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        //protected static string connstr = string.Format(ConfigurationSettings.AppSettings["ConnectionString"], System.Web.HttpContext.Current.Server.MapPath("site.mdb"));
        //string connstr = System.Configuration.ConfigurationSettings.AppSettings["connstr"] + System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["dbpath"]);

        //string connstr = "Provider= " + "Microsoft.Jet.Sql.4.0;" + "DataSource= " + System.Web.HttpContext.Current.Server.MapPath("dbpath");

        //  private static readonly string StrCn = @"Data Source=T403-033\HB_SQLSERVER;Initial Catalog=F:\INTERHB\WEBWORKMANAGE\WEBAPPCODE\APP_DATA\DATABASE.MDF;Integrated Security=True";
        //Provider=MSDASQL.1;Persist Security Info=False;Data Source=MS Access Database;Initial Catalog=Club";
        /// <summary>
        /// 功能描述：執行SQL語句,返回影響的行數
        /// </summary>
        /// <param name="CmdText">命令參數內容</param>
        /// <returns>返回所影響的行數</returns>
        public  int ExecuteNonQuery(string CmdText, CommandType CmdType)
        {

            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cn.Open();
                    cmd.CommandType = CmdType;
                    cmd.CommandText = CmdText;
                    return cmd.ExecuteNonQuery();
                }

                catch (SqlException ex)
                {

                    throw new Exception(CmdText);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        /// <summary>
        ///  功能描述：根據參數類型和參數返回所受影響行數
        /// </summary>
        /// <param name="CmdType">命令參數類型</param>
        /// <param name="CmdText">命令參數內容</param>
        /// <param name="Parameter">命令參數列表</param>
        /// <returns>返回所受影響行數</returns>
        public int ExecuteNonQuery(string CmdText, CommandType CmdType, SqlParameter[] Parameter)
        {

            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cn.Open();
                    cmd.CommandType = CmdType;
                    cmd.CommandText = CmdText;
                    foreach (SqlParameter par in Parameter)
                    {
                        cmd.Parameters.Add(par);
                    }
                    return cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 功能描述：根據命令類型和命令文本返回SqlDataReader物件
        /// </summary>
        /// <param name="CmdType">命令類型</param>
        /// <param name="CmdText">命令文本</param>
        /// <returns>返回SqlDataReader對象</returns>
        public SqlDataReader SqlDataReader(string SqlString, CommandType CmdType)
        {
            SqlConnection cn = new SqlConnection(connstr);

            SqlCommand cmd = cn.CreateCommand();
            try
            {
                cn.Open();
                cmd.CommandType = CmdType;
                cmd.CommandText = SqlString;

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 功能描述：根據命令類型和命令文本及命令參數返回SqlDataReader對象
        /// </summary>
        /// <param name="CmdType">命令類型</param>
        /// <param name="CmdText">命令文本</param>
        /// <param name="Parameter">命令參數</param>
        /// <returns>返回SqlDataReader對象</returns>
        public SqlDataReader SqlDataReader(string SqlString, CommandType CmdType, params SqlParameter[] Parameter)
        {
            SqlConnection cn = new SqlConnection(connstr);

            SqlCommand cmd = cn.CreateCommand();
            try
            {
                cn.Open();
                cmd.CommandType = CmdType;
                cmd.CommandText = SqlString;
                foreach (SqlParameter par in Parameter)
                {
                    cmd.Parameters.Add(par);
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 功能描述：根據命令CmdText獲取DataTable物件
        /// </summary>
        /// <param name="CmdType">命令類型</param>
        /// <param name="CmdText">命令內容</param>
        /// <returns></returns>
        public  DataTable GetDataTable(string SqlString, CommandType CmdType)
        {
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cmd.CommandType = CmdType;
                    cmd.CommandText = SqlString;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "emp");
                    return ds.Tables["emp"];
                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        /// <summary>
        /// 功能描述：根據命令CmdText獲取DataTable物件
        /// </summary>
        /// <param name="CmdType">命令類型</param>
        /// <param name="CmdText">命令內容</param>
        /// <returns></returns>
        public DataTable GetDataTable(string SqlString, CommandType CmdType, params SqlParameter[] Parameter)
        {
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cmd.CommandType = CmdType;
                    cmd.CommandText = SqlString;
                    foreach (SqlParameter par in Parameter)
                    {
                        cmd.Parameters.Add(par);
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "emp");
                    return ds.Tables["emp"];

                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }



        public DataSet SqlDataAdapter(string SqlString, CommandType CmdType)
        {
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cmd.CommandType = CmdType;
                    cmd.CommandText = SqlString;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;

                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        /// <summary>
        /// 功能描述：根據命令CmdText獲取DataSet物件
        /// </summary>
        /// <param name="CmdType">命令類型</param>
        /// <param name="CmdText">命令內容</param>
        /// <returns></returns>
        public DataSet SqlDataAdapter(string SqlString, CommandType CmdType, params SqlParameter[] Parameter)
        {
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cmd.CommandType = CmdType;
                    cmd.CommandText = SqlString;
                    foreach (SqlParameter par in Parameter)
                    {
                        cmd.Parameters.Add(par);
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;

                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 功能描述：根據命令CmdText獲取DataSet物件
        /// </summary>
        /// <param name="CmdType">命令類型</param>
        /// <param name="CmdText">命令內容</param>
        /// <returns></returns>
        public  DataSet SqlDataAdapter_Page(string SqlString, CommandType CmdType, int sart, int Pagecount)  //分頁
        {
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cmd.CommandType = CmdType;
                    cmd.CommandText = SqlString;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, sart, Pagecount, "ds");
                    return ds;

                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 功能描述：執行一條計算查詢結果語句，返回查詢結果（object）。
        /// </summary>
        /// <param name="CmdType">命令類型</param>
        /// <param name="SqlString">命名內容</param>
        /// <returns>返回查詢結果</returns>
        public  object ExecuteScalar(string SqlString, CommandType CmdType)
        {
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cn.Open();
                    cmd.CommandType = CmdType;
                    cmd.CommandText = SqlString;
                    object obj = cmd.ExecuteScalar();
                    if (Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        public object ExecuteScalar(string SqlString, CommandType CmdType, params SqlParameter[] Parameter)
        {
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = cn.CreateCommand();
                try
                {
                    cn.Open();
                    cmd.CommandType = CmdType;
                    cmd.CommandText = SqlString;
                    foreach (SqlParameter par in Parameter)
                    {
                        cmd.Parameters.Add(par);
                    }
                    object obj = cmd.ExecuteScalar();
                    if (Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }

    }
}
  
