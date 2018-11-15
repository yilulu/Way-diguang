using System;
using System.Collections.Generic;
using System.Text;
using DTcms.DBUtility;
using DTcms.Common;
using System.Data;
using System.Data.SqlClient;

namespace DTcms.DAL
{
    /// <summary>
    /// 导航模型
    /// </summary>
 public partial   class article_nav_DAL
    {
     public article_nav_DAL() { }
     #region  Method
     /// <summary>
     /// 是否存在该记录
     /// </summary>
     public bool Exists(int id)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("select count(1) from dt_article_nav");
         strSql.Append(" where n_id=@n_id");
         SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)};
         parameters[0].Value = id;

         return DbHelperSQL.Exists(strSql.ToString(), parameters);
     }

     /// <summary>
     /// 返回记录总数
     /// </summary>
     public int GetCount(string strWhere)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("select count(*) as H ");
         strSql.Append(" from dt_article_nav ");
         if (strWhere.Trim() != "")
         {
             strSql.Append(" where " + strWhere);
         }
         return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
     }

     /// <summary>
     /// 增加一条数据
     /// </summary>
     public int Add(Model.article_nav model)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("insert into dt_article_nav(");
         strSql.Append("n_title,n_url,n_state,n_sequence,n_desc)");
         strSql.Append(" values (");
         strSql.Append("@n_title,@n_url,@n_state,@n_sequence,@n_desc)");
         strSql.Append(";select @@IDENTITY");
         SqlParameter[] parameters = {
				
					new SqlParameter("@n_title", SqlDbType.NVarChar,100),
					new SqlParameter("@n_url", SqlDbType.NVarChar,100),
					new SqlParameter("@n_state", SqlDbType.TinyInt,1),
					new SqlParameter("@n_sequence", SqlDbType.Int,4),
					new SqlParameter("@n_desc", SqlDbType.NText)};
         parameters[0].Value = model.n_title;
         parameters[1].Value = model.n_url;
         parameters[2].Value = model.n_state;
         parameters[3].Value = model.n_sequence;
         parameters[4].Value = model.n_desc;
     

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
     /// 修改一列数据
     /// </summary>
     public void UpdateField(int id, string strValue)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("update dt_article_nav set " + strValue);
         strSql.Append(" where id=" + id);
         DbHelperSQL.ExecuteSql(strSql.ToString());
     }

     /// <summary>
     /// 更新一条数据
     /// </summary>
     public bool Update(Model.article_nav model)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("update dt_article_nav set ");
         strSql.Append("n_title=@n_title,");
         strSql.Append("n_url=@n_url,");
         strSql.Append("n_state=@n_state,");
         strSql.Append("n_sequence=@n_sequence,");
         strSql.Append("n_desc=@n_desc");

         strSql.Append(" where n_id=@n_id");
         SqlParameter[] parameters = {
					
					new SqlParameter("@n_title", SqlDbType.NVarChar,100),
					new SqlParameter("@n_url", SqlDbType.NVarChar,100),
					new SqlParameter("@n_state", SqlDbType.TinyInt,1),
					new SqlParameter("@n_sequence", SqlDbType.Int,4),
					new SqlParameter("@n_desc", SqlDbType.NText),
				
					new SqlParameter("@n_id", SqlDbType.Int,4)};
         parameters[0].Value = model.n_title;
         parameters[1].Value = model.n_url;
         parameters[2].Value = model.n_state;
         parameters[3].Value = model.n_sequence;
         parameters[4].Value = model.n_desc;
         parameters[5].Value = model.n_id;
       
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
     public bool Delete(int id)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("delete from dt_article_nav ");
         strSql.Append(" where n_id=@n_id");
         SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)};
         parameters[0].Value = id;

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
     /// 得到一个对象实体
     /// </summary>
     public Model.article_nav GetModel(int id)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("select  top 1 * from dt_article_nav ");
         strSql.Append(" where n_id=@n_id");
         SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)};
         parameters[0].Value = id;

         DTcms.Model.article_nav model = new DTcms.Model.article_nav();
         DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
         if (ds.Tables[0].Rows.Count > 0)
         {
             if (ds.Tables[0].Rows[0]["n_id"] != null && ds.Tables[0].Rows[0]["n_id"].ToString() != "")
             {
                 model.n_id = int.Parse(ds.Tables[0].Rows[0]["n_id"].ToString());
             }
             if (ds.Tables[0].Rows[0]["n_title"] != null && ds.Tables[0].Rows[0]["n_title"].ToString() != "")
             {
                 model.n_title = ds.Tables[0].Rows[0]["n_title"].ToString();
             }
             if (ds.Tables[0].Rows[0]["n_url"] != null && ds.Tables[0].Rows[0]["n_url"].ToString() != "")
             {
                 model.n_url = ds.Tables[0].Rows[0]["n_url"].ToString();
             }

             if (ds.Tables[0].Rows[0]["n_state"] != null && ds.Tables[0].Rows[0]["n_state"].ToString() != "")
             {
                 model.n_state = int.Parse(ds.Tables[0].Rows[0]["n_state"].ToString());
             }
             if (ds.Tables[0].Rows[0]["n_sequence"] != null && ds.Tables[0].Rows[0]["n_sequence"].ToString() != "")
             {
                 model.n_sequence =  int.Parse(ds.Tables[0].Rows[0]["n_sequence"].ToString());
             }
             if (ds.Tables[0].Rows[0]["n_desc"] != null && ds.Tables[0].Rows[0]["n_desc"].ToString() != "")
             {
                 model.n_desc = ds.Tables[0].Rows[0]["n_desc"].ToString();
             }
            
             return model;
         }
         else
         {
             return null;
         }
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
         strSql.Append(" n_id,n_title,n_url,n_state,n_sequence,n_desc ");
         strSql.Append(" FROM dt_article_nav ");
         if (strWhere.Trim() != "")
         {
             strSql.Append(" where " + strWhere);
         }
         strSql.Append(" order by " + filedOrder);
         return DbHelperSQL.Query(strSql.ToString());
     }

     /// <summary>
     /// 获得查询分页数据
     /// </summary>
     public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
     {
         StringBuilder strSql = new StringBuilder();
         strSql.Append("select * FROM dt_article_nav");
         if (strWhere.Trim() != "")
         {
             strSql.Append(" where " + strWhere);
         }
         recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
         return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
     }

     #endregion  Method
    }
}
