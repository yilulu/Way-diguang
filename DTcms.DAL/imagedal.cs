using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
using System.Data.SqlClient;

namespace DTcms.DAL
{
    public class imagedal
    {
        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.image model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_image(");
            strSql.Append("title,sort,img_url,link_url,Typeid,Vipids)");
            strSql.Append(" values (");
            strSql.Append("@title,@sort,@img_url,@link_url,@Typeid,@Vipids)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@img_url", SqlDbType.NVarChar,200),
                    new SqlParameter("@link_url", SqlDbType.NVarChar,100),
                    new SqlParameter("@Typeid", SqlDbType.Int),
                    new SqlParameter("@Vipids", SqlDbType.NVarChar),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.sort;
            parameters[2].Value = model.img_url;
            parameters[3].Value = model.link_url;
            parameters[4].Value = model.Typeid;
            parameters[5].Value = model.Vipids;
            parameters[6].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[6].Value;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.image model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update dt_image set ");
                        strSql.Append("title=@title,");
                        strSql.Append("sort=@sort,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("link_url=@link_url,");
                        strSql.Append("Typeid=@Typeid,");
                        strSql.Append("Vipids=@Vipids");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@sort", SqlDbType.Int,4),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,200),
					            new SqlParameter("@link_url", SqlDbType.NVarChar,100),
                                new SqlParameter("@Typeid", SqlDbType.Int),
                                new SqlParameter("@Vipids", SqlDbType.NVarChar),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.sort;
                        parameters[2].Value = model.img_url;
                        parameters[3].Value = model.link_url;
                        parameters[4].Value = model.Typeid;
                        parameters[5].Value = model.Vipids;
                        parameters[6].Value = model.id;
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
            strSql2.Append("delete from dt_image ");
            strSql2.Append(" where id=@id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = id;

            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);
            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
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
        public Model.image GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from dt_image ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.image model = new Model.image();
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
                if (ds.Tables[0].Rows[0]["sort"] != null && ds.Tables[0].Rows[0]["sort"].ToString() != "")
                {
                    model.sort = int.Parse(ds.Tables[0].Rows[0]["sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["link_url"] != null && ds.Tables[0].Rows[0]["link_url"].ToString() != "")
                {
                    model.link_url = ds.Tables[0].Rows[0]["link_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Typeid"] != null && ds.Tables[0].Rows[0]["Typeid"].ToString() != "")
                {
                    model.Typeid = int.Parse(ds.Tables[0].Rows[0]["Typeid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Vipids"] != null && ds.Tables[0].Rows[0]["Vipids"].ToString() != "")
                {
                    model.Vipids = ds.Tables[0].Rows[0]["Vipids"].ToString();
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
            strSql.Append("select * FROM dt_image");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
    }
}
