using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using DTcms.DBUtility;//Please add references
namespace Ltf.DAL
{
    /// <summary>
    /// 数据访问类:Freight
    /// </summary>
    public partial class Freight
    {
        public Freight()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Ltf.Model.Freight model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Freight(");
            strSql.Append("typID,TotalPrice,Fee,spec)");
            strSql.Append(" values (");
            strSql.Append("@typID,@TotalPrice,@Fee,@spec)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@typID", SqlDbType.Int,4),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Fee", SqlDbType.Decimal,9),
					new SqlParameter("@spec", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.typID;
            parameters[1].Value = model.TotalPrice;
            parameters[2].Value = model.Fee;
            parameters[3].Value = model.spec;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Ltf.Model.Freight model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Freight set ");
            strSql.Append("typID=@typID,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Fee=@Fee,");
            strSql.Append("spec=@spec");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@typID", SqlDbType.Int,4),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Fee", SqlDbType.Decimal,9),
					new SqlParameter("@spec", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.typID;
            parameters[1].Value = model.TotalPrice;
            parameters[2].Value = model.Fee;
            parameters[3].Value = model.spec;
            parameters[4].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Freight ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Freight ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Ltf.Model.Freight GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,typID,TotalPrice,Fee,spec from Freight ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Ltf.Model.Freight model = new Ltf.Model.Freight();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Ltf.Model.Freight DataRowToModel(DataRow row)
        {
            Ltf.Model.Freight model = new Ltf.Model.Freight();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["typID"] != null && row["typID"].ToString() != "")
                {
                    model.typID = int.Parse(row["typID"].ToString());
                }
                if (row["TotalPrice"] != null && row["TotalPrice"].ToString() != "")
                {
                    model.TotalPrice = decimal.Parse(row["TotalPrice"].ToString());
                }
                if (row["Fee"] != null && row["Fee"].ToString() != "")
                {
                    model.Fee = decimal.Parse(row["Fee"].ToString());
                }
                if (row["spec"] != null)
                {
                    model.spec = row["spec"].ToString();
                }
            }
            return model;
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

