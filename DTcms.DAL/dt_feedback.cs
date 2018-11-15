using System;
using System.Collections.Generic;
using System.Text;
using DTcms.DBUtility;
using System.Data;
using System.Data.SqlClient;
using DTcms.Common;
namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:dt_feedback
    /// </summary>
    public partial class dt_feedback
    {
        public dt_feedback()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.dt_feedback model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_feedback(");
            strSql.Append("title,content,user_name,user_tel,user_qq,user_email,user_Address,user_Money,user_MianJi,user_Class,user_Function,add_time,reply_content,is_lock,zhuzhi,UserID)");
            strSql.Append(" values (");
            strSql.Append("@title,@content,@user_name,@user_tel,@user_qq,@user_email,@user_Address,@user_Money,@user_MianJi,@user_Class,@user_Function,@add_time,@reply_content,@is_lock,@zhuzhi,@UserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@user_name", SqlDbType.NVarChar,50),
					new SqlParameter("@user_tel", SqlDbType.NVarChar,30),
					new SqlParameter("@user_qq", SqlDbType.NVarChar,30),
					new SqlParameter("@user_email", SqlDbType.NVarChar,100),
					new SqlParameter("@user_Address", SqlDbType.NVarChar,100),
					new SqlParameter("@user_Money", SqlDbType.NVarChar,100),
					new SqlParameter("@user_MianJi", SqlDbType.NVarChar,100),
					new SqlParameter("@user_Class", SqlDbType.NVarChar,100),
					new SqlParameter("@user_Function", SqlDbType.NVarChar,100),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@reply_content", SqlDbType.NText),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
                    new SqlParameter("@zhuzhi", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.content;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.user_tel;
            parameters[4].Value = model.user_qq;
            parameters[5].Value = model.user_email;
            parameters[6].Value = model.user_Address;
            parameters[7].Value = model.user_Money;
            parameters[8].Value = model.user_MianJi;
            parameters[9].Value = model.user_Class;
            parameters[10].Value = model.user_Function;
            parameters[11].Value = model.add_time;
            parameters[12].Value = model.reply_content;
            parameters[13].Value = model.is_lock;
            parameters[14].Value = model.zhuzhi;
            parameters[15].Value = model.UserID;

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
        public bool Update(DTcms.Model.dt_feedback model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_feedback set ");
            strSql.Append("reply_content=@reply_content,");
            strSql.Append("reply_time=@reply_time,");
            strSql.Append("user_Function=@user_Function");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {				
					new SqlParameter("@reply_content", SqlDbType.NText),
					new SqlParameter("@reply_time", SqlDbType.DateTime),
                    new SqlParameter("@user_Function", SqlDbType.NChar),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.reply_content;
            parameters[1].Value = model.reply_time;
            parameters[2].Value = model.user_Function;
            parameters[3].Value = model.id;
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
            strSql.Append("delete from dt_feedback ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_feedback ");
            strSql.Append(" where id in (" + idlist + ")  ");
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
        public DTcms.Model.dt_feedback GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,UserID,title,zhuzhi,content,user_name,user_tel,user_qq,user_email,user_Address,user_Money,user_MianJi,user_Class,user_Function,add_time,reply_content,reply_time,is_lock from dt_feedback ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            DTcms.Model.dt_feedback model = new DTcms.Model.dt_feedback();
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
        public DTcms.Model.dt_feedback DataRowToModel(DataRow row)
        {
            DTcms.Model.dt_feedback model = new DTcms.Model.dt_feedback();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["user_tel"] != null)
                {
                    model.user_tel = row["user_tel"].ToString();
                }
                if (row["user_qq"] != null)
                {
                    model.user_qq = row["user_qq"].ToString();
                }
                if (row["user_email"] != null)
                {
                    model.user_email = row["user_email"].ToString();
                }
                if (row["user_Address"] != null)
                {
                    model.user_Address = row["user_Address"].ToString();
                }
                if (row["user_Money"] != null)
                {
                    model.user_Money = row["user_Money"].ToString();
                }
                if (row["user_MianJi"] != null)
                {
                    model.user_MianJi = row["user_MianJi"].ToString();
                }
                if (row["user_Class"] != null)
                {
                    model.user_Class = row["user_Class"].ToString();
                }
                if (row["user_Function"] != null)
                {
                    model.user_Function = row["user_Function"].ToString();
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
                if (row["zhuzhi"] != null)
                {
                    model.zhuzhi = row["zhuzhi"].ToString();
                }
                if (row["reply_content"] != null)
                {
                    model.reply_content = row["reply_content"].ToString();
                }
                if (row["reply_time"] != null && row["reply_time"].ToString() != "")
                {
                    model.reply_time = DateTime.Parse(row["reply_time"].ToString());
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
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
            strSql.Append("select id,title,content,user_name,user_tel,user_qq,user_email,user_Address,user_Money,user_MianJi,user_Class,user_Function,add_time,reply_content,reply_time,is_lock ");
            strSql.Append(" FROM dt_feedback ");
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
            strSql.Append(" id,title,content,user_name,user_tel,user_qq,user_email,user_Address,user_Money,user_MianJi,user_Class,user_Function,add_time,reply_content,reply_time,is_lock ");
            strSql.Append(" FROM dt_feedback ");
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
            strSql.Append("select count(1) FROM dt_feedback ");
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
        #region 分頁
        public DataTable list_pagesWhere(int page, int numPerPage, string whereStr, string orderStr)
        {
            string sql = "";
            try
            {
                sql = "select top " + numPerPage + " * from dt_feedback where 1=1";
                string sql1 = "";
                if (orderStr == "")
                {
                    orderStr = " order by id desc";
                }
                sql += whereStr;
                sql1 += whereStr;
                if (page > 1)
                {
                    sql += " and id not in(select top " + (page - 1) * numPerPage + " id from dt_feedback where 1=1 " + sql1 + orderStr + ")";
                }
                sql += orderStr;

                string s = sql; ;
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #endregion  BasicMethod

    }
}

