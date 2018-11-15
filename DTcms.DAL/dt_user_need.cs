using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;//Please add references
namespace Ltf.DAL
{
    /// <summary>
    /// 数据访问类:dt_user_need
    /// </summary>
    public partial class dt_user_need
    {
        public dt_user_need()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_user_need");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Ltf.Model.dt_user_need model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_user_need(");
            strSql.Append("typeName,Content,AreaName,CataName,AddTime,MianJi,UserID,spec)");
            strSql.Append(" values (");
            strSql.Append("@typeName,@Content,@AreaName,@CataName,@AddTime,@MianJi,@UserID,@spec)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@typeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@CataName", SqlDbType.NVarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@MianJi", SqlDbType.NVarChar,50),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@spec", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.typeName;
            parameters[1].Value = model.Content;
            parameters[2].Value = model.AreaName;
            parameters[3].Value = model.CataName;
            parameters[4].Value = model.AddTime;
            parameters[5].Value = model.MianJi;
            parameters[6].Value = model.UserID;
            parameters[7].Value = model.spec;

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
        public bool Update(Ltf.Model.dt_user_need model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_user_need set ");
            strSql.Append("typeName=@typeName,");
            strSql.Append("Content=@Content,");
            strSql.Append("AreaName=@AreaName,");
            strSql.Append("CataName=@CataName,");
            strSql.Append("MianJi=@MianJi,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("spec=@spec");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@typeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@CataName", SqlDbType.NVarChar,50),
					new SqlParameter("@MianJi", SqlDbType.NVarChar,50),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@spec", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.typeName;
            parameters[1].Value = model.Content;
            parameters[2].Value = model.AreaName;
            parameters[3].Value = model.CataName;
            parameters[4].Value = model.MianJi;
            parameters[5].Value = model.UserID;
            parameters[6].Value = model.spec;
            parameters[7].Value = model.ID;

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
            strSql.Append("delete from dt_user_need ");
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
            strSql.Append("delete from dt_user_need ");
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
        public Ltf.Model.dt_user_need GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,typeName,Content,AreaName,CataName,AddTime,MianJi,UserID,spec from dt_user_need ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Ltf.Model.dt_user_need model = new Ltf.Model.dt_user_need();
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
        public Ltf.Model.dt_user_need DataRowToModel(DataRow row)
        {
            Ltf.Model.dt_user_need model = new Ltf.Model.dt_user_need();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["typeName"] != null)
                {
                    model.typeName = row["typeName"].ToString();
                }
                if (row["Content"] != null)
                {
                    model.Content = row["Content"].ToString();
                }
                if (row["AreaName"] != null)
                {
                    model.AreaName = row["AreaName"].ToString();
                }
                if (row["CataName"] != null)
                {
                    model.CataName = row["CataName"].ToString();
                }
                if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(row["AddTime"].ToString());
                }
                if (row["MianJi"] != null)
                {
                    model.MianJi = row["MianJi"].ToString();
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["spec"] != null)
                {
                    model.spec = row["spec"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,typeName,Content,AreaName,CataName,AddTime,MianJi,UserID,spec ");
            strSql.Append(" FROM dt_user_need ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,typeName,Content,AreaName,CataName,AddTime,MianJi,UserID,spec ");
            strSql.Append(" FROM dt_user_need ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM dt_user_need ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from dt_user_need T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 分頁
        public DataTable list_pagesWhere(int page, int numPerPage, string whereStr, string orderStr)
        {
            string sql = "";
            try
            {
                sql = "select top " + numPerPage + " ID,typeName,Content,AreaName,CataName,AddTime,MianJi,UserID,spec from dt_user_need where 1=1";
                string sql1 = "";
                if (orderStr == "")
                {
                    orderStr = " order by id desc";
                }
                sql += whereStr;
                sql1 += whereStr;
                if (page > 1)
                {
                    sql += " and id not in(select top " + (page - 1) * numPerPage + " id from dt_user_need where 1=1 " + sql1 + orderStr + ")";
                }
                sql += orderStr;

                string s = sql; ;
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

