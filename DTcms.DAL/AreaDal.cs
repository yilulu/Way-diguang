using System;
using System.Collections.Generic;
using System.Text;
using DTcms.DBUtility;
using System.Data;
using System.Data.SqlClient;
using DTcms.Common;

namespace DTcms.DAL
{
    public class AreaDal
    {
        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.Area model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_Area(");
            strSql.Append("title,parent,code,sort)");
            strSql.Append(" values (");
            strSql.Append("@title,@parent,@code,@sort)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@parent", SqlDbType.Int,4),
					new SqlParameter("@code", SqlDbType.NVarChar,50),
                    new SqlParameter("@sort", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.parent;
            parameters[2].Value = model.code;
            parameters[3].Value = model.sort;
            parameters[4].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[4].Value;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Area model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update dt_Area set ");
                        strSql.Append("title=@title,");
                        strSql.Append("parent=@parent,");
                        strSql.Append("code=@code,");
                        strSql.Append("sort=@sort");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@parent", SqlDbType.Int,4),
					            new SqlParameter("@code", SqlDbType.NVarChar,50),
					            new SqlParameter("@sort", SqlDbType.NVarChar,50),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.parent;
                        parameters[2].Value = model.code;
                        parameters[3].Value = model.sort;
                        parameters[4].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);



                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql2 = new StringBuilder();
            //strSql2.Append("delete from dt_Area_nav ");
            //strSql2.Append(" where id=@id ");
            //SqlParameter[] parameters2 = {
            //        new SqlParameter("@id", SqlDbType.Int,4)};
            //parameters2[0].Value = id;

            //CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            //sqllist.Add(cmd);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_Area ");
            strSql.Append(" where id=" + id);
            //SqlParameter[] parameters = {
            //        new SqlParameter("@id", SqlDbType.Int,4)};
            //parameters[0].Value = id;

            //cmd = new CommandInfo(strSql.ToString(), parameters);
            //sqllist.Add(cmd);
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
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
        public Model.Area GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from dt_Area ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.Area model = new Model.Area();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["parent"] != null && ds.Tables[0].Rows[0]["parent"].ToString() != "")
                {
                    model.parent = int.Parse(ds.Tables[0].Rows[0]["parent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["code"] != null && ds.Tables[0].Rows[0]["code"].ToString() != "")
                {
                    model.code = ds.Tables[0].Rows[0]["code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort"] != null && ds.Tables[0].Rows[0]["sort"].ToString() != "")
                {
                    model.sort = ds.Tables[0].Rows[0]["sort"].ToString();
                }

                #endregion  父表信息end



                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetDatalistpage(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_Area");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetDatalistpageByParentID(int ParentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_Area");
            if (!string.IsNullOrEmpty(ParentID.ToString()))
            {
                strSql.Append(" where parent=" + ParentID);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 分頁
        public DataTable list_page(int page, int numPerPage, string whereStr, string orderStr)
        {
            string sql = "";
            try
            {
                sql = "select top " + numPerPage + " * from dt_Area where 1=1";
                string sql1 = "";
                if (orderStr == "")
                {
                    orderStr = "";
                }
                sql += whereStr;
                sql1 += whereStr;
                if (page > 1)
                {
                    sql += " and id not in(select top " + (page - 1) * numPerPage + " id from dt_Area where 1=1 " + sql1 + orderStr + ")";
                }
                sql += orderStr;

                string s = sql; ;
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #region 獲取總數
        public int GetNewsTatalNum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(id) FROM dt_Area");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere + "");
            }
            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }
        #endregion
    }
}
