using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 下载模型
    /// </summary>
    public partial class article
    {
        #region Method
        /// <summary>
        /// 修改下载副表一列数据
        /// </summary>
        public void UpdateDownloadField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_article_download set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.article_download model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_article(");
            strSql.Append("channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time)");
            strSql.Append(" values (");
            strSql.Append("@channel_id,@category_id,@title,@link_url,@img_url,@seo_title,@seo_keywords,@seo_description,@content,@sort_id,@click,@is_lock,@user_id,@add_time)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@category_id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@click", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.channel_id;
            parameters[1].Value = model.category_id;
            parameters[2].Value = model.title;
            parameters[3].Value = model.link_url;
            parameters[4].Value = model.img_url;
            parameters[5].Value = model.seo_title;
            parameters[6].Value = model.seo_keywords;
            parameters[7].Value = model.seo_description;
            parameters[8].Value = model.content;
            parameters[9].Value = model.sort_id;
            parameters[10].Value = model.click;
            parameters[11].Value = model.is_lock;
            parameters[12].Value = model.user_id;
            parameters[13].Value = model.add_time;
            parameters[14].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //附表信息
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into dt_article_download(");
            strSql2.Append("id,is_msg,is_red)");
            strSql2.Append(" values (");
            strSql2.Append("@id,@is_msg,@is_red)");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					new SqlParameter("@is_red", SqlDbType.TinyInt,1)};
            parameters2[0].Direction = ParameterDirection.InputOutput;
            parameters2[1].Value = model.is_msg;
            parameters2[2].Value = model.is_red;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //顶和踩
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into dt_article_diggs(");
            strSql3.Append("id,digg_good,digg_bad)");
            strSql3.Append(" values (");
            strSql3.Append("@id,@digg_good,@digg_bad)");
            SqlParameter[] parameters3 = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@digg_good", SqlDbType.Int,4),
					new SqlParameter("@digg_bad", SqlDbType.Int,4)};
            parameters3[0].Direction = ParameterDirection.InputOutput;
            parameters3[1].Value = model.digg_good;
            parameters3[2].Value = model.digg_bad;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //下载附件
            if (model.download_attachs != null)
            {
                StringBuilder strSql4;
                foreach (Model.download_attach models in model.download_attachs)
                {
                    strSql4 = new StringBuilder();
                    strSql4.Append("insert into dt_download_attach(");
                    strSql4.Append("article_id,title,file_path,file_ext,file_size,down_num,point)");
                    strSql4.Append(" values (");
                    strSql4.Append("@article_id,@title,@file_path,@file_ext,@file_size,@down_num,@point)");
                    SqlParameter[] parameters4 = {
					        new SqlParameter("@article_id", SqlDbType.Int,4),
					        new SqlParameter("@title", SqlDbType.NVarChar,255),
					        new SqlParameter("@file_path", SqlDbType.NVarChar,255),
					        new SqlParameter("@file_ext", SqlDbType.NVarChar,100),
					        new SqlParameter("@file_size", SqlDbType.Int,4),
					        new SqlParameter("@down_num", SqlDbType.Int,4),
					        new SqlParameter("@point", SqlDbType.Int,4)};
                    parameters4[0].Direction = ParameterDirection.InputOutput;
                    parameters4[1].Value = models.title;
                    parameters4[2].Value = models.file_path;
                    parameters4[3].Value = models.file_ext;
                    parameters4[4].Value = models.file_size;
                    parameters4[5].Value = models.down_num;
                    parameters4[6].Value = models.point;
                    cmd = new CommandInfo(strSql4.ToString(), parameters4);
                    sqllist.Add(cmd);
                }
            }

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[14].Value;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_download model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update dt_article set ");
                        strSql.Append("channel_id=@channel_id,");
                        strSql.Append("category_id=@category_id,");
                        strSql.Append("title=@title,");
                        strSql.Append("link_url=@link_url,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("seo_title=@seo_title,");
                        strSql.Append("seo_keywords=@seo_keywords,");
                        strSql.Append("seo_description=@seo_description,");
                        strSql.Append("content=@content,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("click=@click,");
                        strSql.Append("is_lock=@is_lock,");
                        strSql.Append("user_id=@user_id,");
                        strSql.Append("add_time=@add_time");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4),
					            new SqlParameter("@category_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@click", SqlDbType.Int,4),
					            new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					            new SqlParameter("@user_id", SqlDbType.Int,4),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.channel_id;
                        parameters[1].Value = model.category_id;
                        parameters[2].Value = model.title;
                        parameters[3].Value = model.link_url;
                        parameters[4].Value = model.img_url;
                        parameters[5].Value = model.seo_title;
                        parameters[6].Value = model.seo_keywords;
                        parameters[7].Value = model.seo_description;
                        parameters[8].Value = model.content;
                        parameters[9].Value = model.sort_id;
                        parameters[10].Value = model.click;
                        parameters[11].Value = model.is_lock;
                        parameters[12].Value = model.user_id;
                        parameters[13].Value = model.add_time;
                        parameters[14].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //修改副表
                        StringBuilder strSql21 = new StringBuilder();
                        strSql21.Append("update dt_article_download set ");
                        strSql21.Append("id=@id,");
                        strSql21.Append("is_msg=@is_msg,");
                        strSql21.Append("is_red=@is_red");
                        strSql21.Append(" where id=@id ");
                        SqlParameter[] parameters21 = {
					            new SqlParameter("@id", SqlDbType.Int,4),
					            new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_red", SqlDbType.TinyInt,1)};
                        parameters21[0].Value = model.id;
                        parameters21[1].Value = model.is_msg;
                        parameters21[2].Value = model.is_red;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql21.ToString(), parameters21);

                        //修改顶和踩
                        StringBuilder strSql22 = new StringBuilder();
                        strSql22.Append("update dt_article_diggs set ");
                        strSql22.Append("digg_good=@digg_good,");
                        strSql22.Append("digg_bad=@digg_bad");
                        strSql22.Append(" where id=@id ");
                        SqlParameter[] parameters22 = {
					            new SqlParameter("@digg_good", SqlDbType.Int,4),
					            new SqlParameter("@digg_bad", SqlDbType.Int,4),
                                new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters22[0].Value = model.digg_good;
                        parameters22[1].Value = model.digg_bad;
                        parameters22[2].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql22.ToString(), parameters22);

                        //删除已删除的附件
                        new download_attach().DeleteList(conn, trans, model.download_attachs, model.id);
                        // 添加/修改附件
                        if (model.download_attachs != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.download_attach models in model.download_attachs)
                            {
                                strSql2 = new StringBuilder();
                                if (models.id > 0)
                                {
                                    strSql2.Append("update dt_download_attach set ");
                                    strSql2.Append("article_id=@article_id,");
                                    strSql2.Append("title=@title,");
                                    strSql2.Append("file_path=@file_path,");
                                    strSql2.Append("file_ext=@file_ext,");
                                    strSql2.Append("file_size=@file_size,");
                                    strSql2.Append("down_num=@down_num,");
                                    strSql2.Append("point=@point");
                                    strSql2.Append(" where id=@id");
                                    SqlParameter[] parameters2 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@title", SqlDbType.NVarChar,255),
					                        new SqlParameter("@file_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@file_ext", SqlDbType.NVarChar,100),
					                        new SqlParameter("@file_size", SqlDbType.Int,4),
					                        new SqlParameter("@down_num", SqlDbType.Int,4),
					                        new SqlParameter("@point", SqlDbType.Int,4),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters2[0].Value = models.article_id;
                                    parameters2[1].Value = models.title;
                                    parameters2[2].Value = models.file_path;
                                    parameters2[3].Value = models.file_ext;
                                    parameters2[4].Value = models.file_size;
                                    parameters2[5].Value = models.down_num;
                                    parameters2[6].Value = models.point;
                                    parameters2[7].Value = models.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                                else
                                {
                                    strSql2.Append("insert into dt_download_attach(");
                                    strSql2.Append("article_id,title,file_path,file_ext,file_size,down_num,point)");
                                    strSql2.Append(" values (");
                                    strSql2.Append("@article_id,@title,@file_path,@file_ext,@file_size,@down_num,@point)");
                                    SqlParameter[] parameters2 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@title", SqlDbType.NVarChar,255),
					                        new SqlParameter("@file_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@file_ext", SqlDbType.NVarChar,100),
					                        new SqlParameter("@file_size", SqlDbType.Int,4),
					                        new SqlParameter("@down_num", SqlDbType.Int,4),
					                        new SqlParameter("@point", SqlDbType.Int,4)};
                                    parameters2[0].Value = models.article_id;
                                    parameters2[1].Value = models.title;
                                    parameters2[2].Value = models.file_path;
                                    parameters2[3].Value = models.file_ext;
                                    parameters2[4].Value = models.file_size;
                                    parameters2[5].Value = models.down_num;
                                    parameters2[6].Value = models.point;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                            }
                        }

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
        /// 得到一个对象实体
        /// </summary>
        public Model.article_download GetDownloadModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time,is_msg,is_red,digg_good,digg_bad from view_article_download ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article_download model = new Model.article_download();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["channel_id"] != null && ds.Tables[0].Rows[0]["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["category_id"] != null && ds.Tables[0].Rows[0]["category_id"].ToString() != "")
                {
                    model.category_id = int.Parse(ds.Tables[0].Rows[0]["category_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["link_url"] != null && ds.Tables[0].Rows[0]["link_url"].ToString() != "")
                {
                    model.link_url = ds.Tables[0].Rows[0]["link_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_title"] != null && ds.Tables[0].Rows[0]["seo_title"].ToString() != "")
                {
                    model.seo_title = ds.Tables[0].Rows[0]["seo_title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_keywords"] != null && ds.Tables[0].Rows[0]["seo_keywords"].ToString() != "")
                {
                    model.seo_keywords = ds.Tables[0].Rows[0]["seo_keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_description"] != null && ds.Tables[0].Rows[0]["seo_description"].ToString() != "")
                {
                    model.seo_description = ds.Tables[0].Rows[0]["seo_description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["content"] != null && ds.Tables[0].Rows[0]["content"].ToString() != "")
                {
                    model.content = ds.Tables[0].Rows[0]["content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["click"] != null && ds.Tables[0].Rows[0]["click"].ToString() != "")
                {
                    model.click = int.Parse(ds.Tables[0].Rows[0]["click"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_msg"] != null && ds.Tables[0].Rows[0]["is_msg"].ToString() != "")
                {
                    model.is_msg = int.Parse(ds.Tables[0].Rows[0]["is_msg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_red"] != null && ds.Tables[0].Rows[0]["is_red"].ToString() != "")
                {
                    model.is_red = int.Parse(ds.Tables[0].Rows[0]["is_red"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_good"] != null && ds.Tables[0].Rows[0]["digg_good"].ToString() != "")
                {
                    model.digg_good = int.Parse(ds.Tables[0].Rows[0]["digg_good"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_bad"] != null && ds.Tables[0].Rows[0]["digg_bad"].ToString() != "")
                {
                    model.digg_bad = int.Parse(ds.Tables[0].Rows[0]["digg_bad"].ToString());
                }
                #endregion  父表信息end

                model.download_attachs = new download_attach().GetList(id); //附件列表

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
        public DataSet GetDownloadList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time,is_msg,is_red,digg_good,digg_bad ");
            strSql.Append(" FROM view_article_download ");
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
        public DataSet GetDownloadList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_article_download");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #region 分頁
        public DataTable listDown_page(int page, int numPerPage, string whereStr, string orderStr)
        {
            string sql = "";
            try
            {
                sql = "select top " + numPerPage + " * from view_article_download where 1=1";
                string sql1 = "";
                if (orderStr == "")
                {
                    orderStr = " order by sort_id asc";
                }
                sql += whereStr;
                sql1 += whereStr;
                if (page > 1)
                {
                    sql += " and id not in(select top " + (page - 1) * numPerPage + " id from view_article_download where 1=1 " + sql1 + orderStr + ")";
                }
                sql += orderStr;

                string s = sql; ;
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #region 獲取總數
        public int GetDownTatalNum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(id) FROM view_article_download");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere + "");
            }
            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }
        #endregion

        #region 分頁
        public DataTable list_pagesWheres(int page, int numPerPage, string whereStr, string orderStr)
        {
            string sql = "";
            try
            {
                sql = "select top " + numPerPage + " b.ID,b.title,b.user_id,a.file_size,a.id as vipid from dt_download_attach a,dt_article b where 1=1 and b.id=a.article_id";
                string sql1 = "";
                if (orderStr == "")
                {
                    orderStr = " order by b.id asc";
                }
                sql += whereStr;
                sql1 += whereStr;
                if (page > 1)
                {
                    sql += " and b.id not in(select top " + (page - 1) * numPerPage + " b.id from dt_article where 1=1 " + sql1 + orderStr + ")";
                }
                sql += orderStr;

                string s = sql; ;
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #region 獲取總數
        public int GetTatalNums(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(b.id) from dt_download_attach a,dt_article b where 1=1 and b.id=a.article_id");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }
        #endregion

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_download_attach GetModelDown(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,article_id,title,file_path,file_ext,file_size,down_num,point from dt_download_attach ");
            strSql.Append(" where article_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            DTcms.Model.dt_download_attach model = new DTcms.Model.dt_download_attach();
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
        public DTcms.Model.dt_download_attach DataRowToModel(DataRow row)
        {
            DTcms.Model.dt_download_attach model = new DTcms.Model.dt_download_attach();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["article_id"] != null && row["article_id"].ToString() != "")
                {
                    model.article_id = int.Parse(row["article_id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["file_path"] != null)
                {
                    model.file_path = row["file_path"].ToString();
                }
                if (row["file_ext"] != null)
                {
                    model.file_ext = row["file_ext"].ToString();
                }
                if (row["file_size"] != null && row["file_size"].ToString() != "")
                {
                    model.file_size = int.Parse(row["file_size"].ToString());
                }
                if (row["down_num"] != null && row["down_num"].ToString() != "")
                {
                    model.down_num = int.Parse(row["down_num"].ToString());
                }
                if (row["point"] != null && row["point"].ToString() != "")
                {
                    model.point = int.Parse(row["point"].ToString());
                }
            }
            return model;
        }

        #region 把上传文件指派给会员
        public int SetFileToMember(string IDList, int DownID)
        {
            int bk = 0;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("UPDATE dt_article SET xiajialiyou='" + IDList + "'");
            if (DownID != 0)
            {
                strSQL.Append(" WHERE ID=" + DownID);
            }
            bk = DbHelperSQL.ExecuteSql(strSQL.ToString());
            return bk;
        }
        #endregion


        #region 得到集合
        public string GetUidList(int DownID)
        {
            string idListValue = string.Empty;
            StringBuilder strSQL = new StringBuilder();
            if (DownID != 0)
            {
                strSQL.Append("SELECT xiajialiyou FROM dt_article WHERE ID=" + DownID);
                object obj = DbHelperSQL.GetSingle(strSQL.ToString());
                if (obj != null)
                {
                    idListValue = obj.ToString();
                }
            }
            return idListValue;
        }
        #endregion

        #endregion  Method
    }
}