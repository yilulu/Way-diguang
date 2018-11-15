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
    /// 商品模型
    /// </summary>
    public partial class article
    {
        #region  Method
        /// <summary>
        /// 修改商品副表一列数据
        /// </summary>
        public void UpdateGoodsField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_article_goods set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 修改表字段信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="strValue"></param>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_article set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 批量下架
        /// </summary>
        public bool UpdateList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_article set Status=0");
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
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.article_goods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_article(");
            strSql.Append("channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time,Postid,Type,AddType,Status,quyu,jiaqianQJ,mianji,huxing,fangshi,xianlu,yajin,zuoxiang,louceng,xingneng,yongtu,chewei,shequ,dizhi,gongsi,fuwuxiangju,dianhua,Areaid,lianxiren,shangpinType,xiajialiyou,isFront)");
            strSql.Append(" values (");
            strSql.Append("@channel_id,@category_id,@title,@link_url,@img_url,@seo_title,@seo_keywords,@seo_description,@content,@sort_id,@click,@is_lock,@user_id,@add_time,@Postid,@Type,@AddType,@Status,@quyu,@jiaqianQJ,@mianji,@huxing,@fangshi,@xianlu,@yajin,@zuoxiang,@louceng,@xingneng,@yongtu,@chewei,@shequ,@dizhi,@gongsi,@fuwuxiangju,@dianhua,@Areaid,@lianxiren,@shangpinType,@xiajialiyou,@isFront)");
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
					new SqlParameter("@user_id", SqlDbType.NVarChar,100),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@Postid", SqlDbType.NVarChar,100),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@AddType", SqlDbType.Int),
                    new SqlParameter("@Status", SqlDbType.Int),

                    new SqlParameter("@quyu", SqlDbType.Int),
                    new SqlParameter("@jiaqianQJ", SqlDbType.Int),
                    new SqlParameter("@mianji", SqlDbType.Int),
                    new SqlParameter("@huxing", SqlDbType.Int),
                    new SqlParameter("@fangshi", SqlDbType.Int),
                    new SqlParameter("@xianlu", SqlDbType.Int),
                    new SqlParameter("@yajin", SqlDbType.NVarChar,100),
                    new SqlParameter("@zuoxiang", SqlDbType.NVarChar,100),
                    new SqlParameter("@louceng", SqlDbType.NVarChar,100),
                    new SqlParameter("@xingneng", SqlDbType.NVarChar,100),
                    new SqlParameter("@yongtu", SqlDbType.NVarChar,100),
                    new SqlParameter("@chewei", SqlDbType.NVarChar,100),
                    new SqlParameter("@shequ", SqlDbType.NVarChar,100),
                    new SqlParameter("@dizhi", SqlDbType.NVarChar,100),
                    new SqlParameter("@gongsi", SqlDbType.NVarChar,255),
                    new SqlParameter("@fuwuxiangju", SqlDbType.NVarChar,500),
                    new SqlParameter("@dianhua", SqlDbType.NVarChar,100),
                    new SqlParameter("@Areaid", SqlDbType.Int),
                    new SqlParameter("@lianxiren", SqlDbType.NVarChar,100),
                    new SqlParameter("@shangpinType", SqlDbType.NVarChar,100),
                    new SqlParameter("@xiajialiyou", SqlDbType.NVarChar,500),
                    new SqlParameter("@isFront", SqlDbType.TinyInt,1),
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
            parameters[14].Value = string.IsNullOrEmpty(model.Postid) ? "" : model.Postid;
            parameters[15].Value = model.Type == null ? 0 : model.Type;
            parameters[16].Value = model.AddType;
            parameters[17].Value = model.Status == null ? 0 : model.Status;

            parameters[18].Value = model.quyu;
            parameters[19].Value = model.jiaqianQJ;
            parameters[20].Value = model.mianji;
            parameters[21].Value = model.huxing;
            parameters[23].Value = model.fangshi;
            parameters[23].Value = model.xianlu;
            parameters[24].Value = model.yajin;
            parameters[25].Value = model.zuoxiang;
            parameters[26].Value = model.louceng;
            parameters[27].Value = model.xingneng;
            parameters[28].Value = model.yongtu;
            parameters[29].Value = model.chewei;
            parameters[30].Value = model.shequ;
            parameters[31].Value = model.dizhi;
            parameters[32].Value = model.gongsi;
            parameters[33].Value = model.fuwuxiangju;
            parameters[34].Value = model.dianhua;
            parameters[35].Value = model.Areaid;
            parameters[36].Value = model.lianxiren;
            parameters[37].Value = model.shangpinType;
            parameters[38].Value = model.xiajialiyou;
            parameters[39].Value = model.isFront;
            parameters[40].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //副表信息
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into dt_article_goods(");
            strSql2.Append("id,goods_no,stock_quantity,market_price,sell_price,single_price,point,is_msg,is_top,is_red,is_hot,is_slide)");
            strSql2.Append(" values (");
            strSql2.Append("@id,@goods_no,@stock_quantity,@market_price,@sell_price,@single_price,@point,@is_msg,@is_top,@is_red,@is_hot,@is_slide)");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@goods_no", SqlDbType.NVarChar,100),
					new SqlParameter("@stock_quantity", SqlDbType.Int,4),
					new SqlParameter("@market_price", SqlDbType.Decimal,8),
					new SqlParameter("@sell_price", SqlDbType.Decimal,8),
                    new SqlParameter("@single_price", SqlDbType.Decimal,8),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					new SqlParameter("@is_top", SqlDbType.TinyInt,1),
					new SqlParameter("@is_red", SqlDbType.TinyInt,1),
					new SqlParameter("@is_hot", SqlDbType.TinyInt,1),
					new SqlParameter("@is_slide", SqlDbType.TinyInt,1)};
            parameters2[0].Direction = ParameterDirection.InputOutput;
            parameters2[1].Value = model.goods_no;
            parameters2[2].Value = model.stock_quantity;
            parameters2[3].Value = model.market_price;
            parameters2[4].Value = model.sell_price;
            parameters2[5].Value = model.single_price;
            parameters2[6].Value = model.point;
            parameters2[7].Value = model.is_msg;
            parameters2[8].Value = model.is_top;
            parameters2[9].Value = model.is_red;
            parameters2[10].Value = model.is_hot;
            parameters2[11].Value = model.is_slide;
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

            //图片相册
            if (model.albums != null)
            {
                StringBuilder strSql4;
                foreach (Model.article_albums models in model.albums)
                {
                    strSql4 = new StringBuilder();
                    strSql4.Append("insert into dt_article_albums(");
                    strSql4.Append("article_id,big_img,small_img,remark)");
                    strSql4.Append(" values (");
                    strSql4.Append("@article_id,@big_img,@small_img,@remark)");
                    SqlParameter[] parameters4 = {
					        new SqlParameter("@article_id", SqlDbType.Int,4),
					        new SqlParameter("@big_img", SqlDbType.NVarChar,255),
					        new SqlParameter("@small_img", SqlDbType.NVarChar,255),
					        new SqlParameter("@remark", SqlDbType.NVarChar,500)};
                    parameters4[0].Direction = ParameterDirection.InputOutput;
                    parameters4[1].Value = models.big_img;
                    parameters4[2].Value = models.small_img;
                    parameters4[3].Value = models.remark;

                    cmd = new CommandInfo(strSql4.ToString(), parameters4);
                    sqllist.Add(cmd);
                }
            }
            //扩展属性
            if (model.attribute_values != null)
            {
                StringBuilder strSql5;
                foreach (Model.attribute_value models in model.attribute_values)
                {
                    strSql5 = new StringBuilder();
                    strSql5.Append("insert into dt_attribute_value(");
                    strSql5.Append("article_id,attribute_id,title,content)");
                    strSql5.Append(" values (");
                    strSql5.Append("@article_id,@attribute_id,@title,@content)");
                    SqlParameter[] parameters5 = {
					        new SqlParameter("@article_id", SqlDbType.Int,4),
					        new SqlParameter("@attribute_id", SqlDbType.Int,4),
					        new SqlParameter("@title", SqlDbType.NVarChar,100),
					        new SqlParameter("@content", SqlDbType.NText)};
                    parameters5[0].Direction = ParameterDirection.InputOutput;
                    parameters5[1].Value = models.attribute_id;
                    parameters5[2].Value = models.title;
                    parameters5[3].Value = models.content;
                    cmd = new CommandInfo(strSql5.ToString(), parameters5);
                    sqllist.Add(cmd);
                }
            }

            //用户组价格
            if (model.goods_group_prices != null)
            {
                StringBuilder strSql6;
                foreach (Model.goods_group_price models in model.goods_group_prices)
                {
                    strSql6 = new StringBuilder();
                    strSql6.Append("insert into dt_goods_group_price(");
                    strSql6.Append("article_id,group_id,price)");
                    strSql6.Append(" values (");
                    strSql6.Append("@article_id,@group_id,@price)");
                    SqlParameter[] parameters6 = {
						    new SqlParameter("@article_id", SqlDbType.Int,4),
					        new SqlParameter("@group_id", SqlDbType.Int,4),
					        new SqlParameter("@price", SqlDbType.Decimal,8)};
                    parameters6[0].Direction = ParameterDirection.InputOutput;
                    parameters6[1].Value = models.group_id;
                    parameters6[2].Value = models.price;
                    cmd = new CommandInfo(strSql6.ToString(), parameters6);
                    sqllist.Add(cmd);
                }
            }

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[40].Value;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_goods model)
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
                        strSql.Append("add_time=@add_time,");
                        strSql.Append("Postid=@Postid,");
                        strSql.Append("Type=@Type,");

                        strSql.Append("quyu=@quyu,");
                        strSql.Append("jiaqianQJ=@jiaqianQJ,");
                        strSql.Append("mianji=@mianji,");
                        strSql.Append("huxing=@huxing,");
                        strSql.Append("fangshi=@fangshi,");
                        strSql.Append("xianlu=@xianlu,");
                        strSql.Append("yajin=@yajin,");
                        strSql.Append("zuoxiang=@zuoxiang,");
                        strSql.Append("louceng=@louceng,");
                        strSql.Append("xingneng=@xingneng,");
                        strSql.Append("yongtu=@yongtu,");
                        strSql.Append("chewei=@chewei,");
                        strSql.Append("shequ=@shequ,");
                        strSql.Append("dizhi=@dizhi,");
                        strSql.Append("gongsi=@gongsi,");
                        strSql.Append("fuwuxiangju=@fuwuxiangju,");
                        strSql.Append("dianhua=@dianhua,");
                        strSql.Append("Areaid=@Areaid,");
                        strSql.Append("lianxiren=@lianxiren,");
                        strSql.Append("shangpinType=@shangpinType,");
                        strSql.Append("Status=@Status,");
                        strSql.Append("xiajialiyou=@xiajialiyou,");
                        strSql.Append("isFront=@isFront");
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
					            new SqlParameter("@user_id", SqlDbType.NVarChar,100),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
                                new SqlParameter("@Postid", SqlDbType.NVarChar,100),
                                new SqlParameter("@Type", SqlDbType.Int),

                                new SqlParameter("@quyu", SqlDbType.Int),
                                new SqlParameter("@jiaqianQJ", SqlDbType.Int),
                                new SqlParameter("@mianji", SqlDbType.Int),
                                new SqlParameter("@huxing", SqlDbType.Int),
                                new SqlParameter("@fangshi", SqlDbType.Int),
                                new SqlParameter("@xianlu", SqlDbType.Int),
                                new SqlParameter("@yajin", SqlDbType.NVarChar,100),
                    new SqlParameter("@zuoxiang", SqlDbType.NVarChar,100),
                    new SqlParameter("@louceng", SqlDbType.NVarChar,100),
                    new SqlParameter("@xingneng", SqlDbType.NVarChar,100),
                    new SqlParameter("@yongtu", SqlDbType.NVarChar,100),
                    new SqlParameter("@chewei", SqlDbType.NVarChar,100),
                    new SqlParameter("@shequ", SqlDbType.NVarChar,100),
                    new SqlParameter("@dizhi", SqlDbType.NVarChar,100),
                    new SqlParameter("@gongsi", SqlDbType.NVarChar,255),
                    new SqlParameter("@fuwuxiangju", SqlDbType.NVarChar,500),
                    new SqlParameter("@dianhua", SqlDbType.NVarChar,100),
                    new SqlParameter("@Areaid", SqlDbType.Int),
                    new SqlParameter("@lianxiren", SqlDbType.NVarChar,100),
                    new SqlParameter("@shangpinType", SqlDbType.NVarChar,100),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@xiajialiyou", SqlDbType.NVarChar,500),
                    new SqlParameter("@isFront", SqlDbType.TinyInt,1),
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
                        parameters[14].Value = model.Postid;
                        parameters[15].Value = model.Type;

                        parameters[16].Value = model.quyu;
                        parameters[17].Value = model.jiaqianQJ;
                        parameters[18].Value = model.mianji;
                        parameters[19].Value = model.huxing;
                        parameters[20].Value = model.fangshi;
                        parameters[21].Value = model.xianlu;
                        parameters[22].Value = model.yajin;
                        parameters[23].Value = model.zuoxiang;
                        parameters[24].Value = model.louceng;
                        parameters[25].Value = model.xingneng;
                        parameters[26].Value = model.yongtu;
                        parameters[27].Value = model.chewei;
                        parameters[28].Value = model.shequ;
                        parameters[29].Value = model.dizhi;
                        parameters[30].Value = model.gongsi;
                        parameters[31].Value = model.fuwuxiangju;
                        parameters[32].Value = model.dianhua;
                        parameters[33].Value = model.Areaid;
                        parameters[34].Value = model.lianxiren;
                        parameters[35].Value = model.shangpinType;
                        parameters[36].Value = model.Status;
                        parameters[37].Value = model.xiajialiyou;
                        parameters[38].Value = model.isFront;
                        parameters[39].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //修改副表
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update dt_article_goods set ");
                        strSql2.Append("id=@id,");
                        strSql2.Append("goods_no=@goods_no,");
                        strSql2.Append("stock_quantity=@stock_quantity,");
                        strSql2.Append("market_price=@market_price,");
                        strSql2.Append("sell_price=@sell_price,");
                        strSql2.Append("single_price=@single_price,");
                        strSql2.Append("point=@point,");
                        strSql2.Append("is_msg=@is_msg,");
                        strSql2.Append("is_top=@is_top,");
                        strSql2.Append("is_red=@is_red,");
                        strSql2.Append("is_hot=@is_hot,");
                        strSql2.Append("is_slide=@is_slide");
                        strSql2.Append(" where id=@id ");
                        SqlParameter[] parameters2 = {
					            new SqlParameter("@id", SqlDbType.Int,4),
					            new SqlParameter("@goods_no", SqlDbType.NVarChar,100),
					            new SqlParameter("@stock_quantity", SqlDbType.Int,4),
					            new SqlParameter("@market_price", SqlDbType.Decimal,5),
					            new SqlParameter("@sell_price", SqlDbType.Decimal,5),
                                new SqlParameter("@single_price", SqlDbType.Decimal,5),
					            new SqlParameter("@point", SqlDbType.Int,4),
					            new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_top", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_red", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_hot", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_slide", SqlDbType.TinyInt,1)};
                        parameters2[0].Value = model.id;
                        parameters2[1].Value = model.goods_no;
                        parameters2[2].Value = model.stock_quantity;
                        parameters2[3].Value = model.market_price;
                        parameters2[4].Value = model.sell_price;
                        parameters2[5].Value = model.single_price;
                        parameters2[6].Value = model.point;
                        parameters2[7].Value = model.is_msg;
                        parameters2[8].Value = model.is_top;
                        parameters2[9].Value = model.is_red;
                        parameters2[10].Value = model.is_hot;
                        parameters2[11].Value = model.is_slide;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);

                        //修改顶和踩
                        StringBuilder strSql3 = new StringBuilder();
                        strSql3.Append("update dt_article_diggs set ");
                        strSql3.Append("digg_good=@digg_good,");
                        strSql3.Append("digg_bad=@digg_bad");
                        strSql3.Append(" where id=@id ");
                        SqlParameter[] parameters3 = {
					            new SqlParameter("@digg_good", SqlDbType.Int,4),
					            new SqlParameter("@digg_bad", SqlDbType.Int,4),
                                new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters3[0].Value = model.digg_good;
                        parameters3[1].Value = model.digg_bad;
                        parameters3[2].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);

                        //删除已删除的图片
                        new article_albums().DeleteList(conn, trans, model.albums, model.id);
                        //添加/修改相册
                        if (model.albums != null)
                        {
                            StringBuilder strSql4;
                            foreach (Model.article_albums models in model.albums)
                            {
                                strSql4 = new StringBuilder();
                                if (models.id > 0)
                                {
                                    strSql4.Append("update dt_article_albums set ");
                                    strSql4.Append("article_id=@article_id,");
                                    strSql4.Append("big_img=@big_img,");
                                    strSql4.Append("small_img=@small_img,");
                                    strSql4.Append("remark=@remark");
                                    strSql4.Append(" where id=@id");
                                    SqlParameter[] parameters4 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@big_img", SqlDbType.NVarChar,255),
					                        new SqlParameter("@small_img", SqlDbType.NVarChar,255),
					                        new SqlParameter("@remark", SqlDbType.NVarChar,500),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters4[0].Value = models.article_id;
                                    parameters4[1].Value = models.big_img;
                                    parameters4[2].Value = models.small_img;
                                    parameters4[3].Value = models.remark;
                                    parameters4[4].Value = models.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql4.ToString(), parameters4);
                                }
                                else
                                {
                                    strSql4.Append("insert into dt_article_albums(");
                                    strSql4.Append("article_id,big_img,small_img,remark)");
                                    strSql4.Append(" values (");
                                    strSql4.Append("@article_id,@big_img,@small_img,@remark)");
                                    SqlParameter[] parameters4 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@big_img", SqlDbType.NVarChar,255),
					                        new SqlParameter("@small_img", SqlDbType.NVarChar,255),
					                        new SqlParameter("@remark", SqlDbType.NVarChar,500)};
                                    parameters4[0].Value = models.article_id;
                                    parameters4[1].Value = models.big_img;
                                    parameters4[2].Value = models.small_img;
                                    parameters4[3].Value = models.remark;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql4.ToString(), parameters4);
                                }
                            }
                        }

                        //添加/修改属性
                        if (model.attribute_values != null)
                        {
                            StringBuilder strSql5;
                            foreach (Model.attribute_value models in model.attribute_values)
                            {
                                strSql5 = new StringBuilder();
                                if (models.id > 0)
                                {
                                    strSql5.Append("update dt_attribute_value set ");
                                    strSql5.Append("article_id=@article_id,");
                                    strSql5.Append("attribute_id=@attribute_id,");
                                    strSql5.Append("title=@title,");
                                    strSql5.Append("content=@content");
                                    strSql5.Append(" where id=@id");
                                    SqlParameter[] parameters5 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@attribute_id", SqlDbType.Int,4),
					                        new SqlParameter("@title", SqlDbType.NVarChar,100),
					                        new SqlParameter("@content", SqlDbType.NText),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters5[0].Value = models.article_id;
                                    parameters5[1].Value = models.attribute_id;
                                    parameters5[2].Value = models.title;
                                    parameters5[3].Value = models.content;
                                    parameters5[4].Value = models.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql5.ToString(), parameters5);
                                }
                                else
                                {
                                    strSql5.Append("insert into dt_attribute_value(");
                                    strSql5.Append("article_id,attribute_id,title,content)");
                                    strSql5.Append(" values (");
                                    strSql5.Append("@article_id,@attribute_id,@title,@content)");
                                    SqlParameter[] parameters5 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@attribute_id", SqlDbType.Int,4),
					                        new SqlParameter("@title", SqlDbType.NVarChar,100),
					                        new SqlParameter("@content", SqlDbType.NText)};
                                    parameters5[0].Value = models.article_id;
                                    parameters5[1].Value = models.attribute_id;
                                    parameters5[2].Value = models.title;
                                    parameters5[3].Value = models.content;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql5.ToString(), parameters5);
                                }
                            }
                        }

                        //添加/修改用户组价格
                        if (model.goods_group_prices != null)
                        {
                            StringBuilder strSql6;
                            foreach (Model.goods_group_price models in model.goods_group_prices)
                            {
                                strSql6 = new StringBuilder();
                                if (models.id > 0)
                                {
                                    strSql6.Append("update dt_goods_group_price set ");
                                    strSql6.Append("article_id=@article_id,");
                                    strSql6.Append("group_id=@group_id,");
                                    strSql6.Append("price=@price");
                                    strSql6.Append(" where id=@id");
                                    SqlParameter[] parameters6 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@group_id", SqlDbType.Int,4),
					                        new SqlParameter("@price", SqlDbType.Decimal,8),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters6[0].Value = models.article_id;
                                    parameters6[1].Value = models.group_id;
                                    parameters6[2].Value = models.price;
                                    parameters6[3].Value = models.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql6.ToString(), parameters6);
                                }
                                else
                                {
                                    strSql6.Append("insert into dt_goods_group_price(");
                                    strSql6.Append("article_id,group_id,price)");
                                    strSql6.Append(" values (");
                                    strSql6.Append("@article_id,@group_id,@price)");
                                    SqlParameter[] parameters6 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@group_id", SqlDbType.Int,4),
					                        new SqlParameter("@price", SqlDbType.Decimal,8)};
                                    parameters6[0].Value = models.article_id;
                                    parameters6[1].Value = models.group_id;
                                    parameters6[2].Value = models.price;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql6.ToString(), parameters6);
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
        public Model.article_goods GetGoodsModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select top 1 id,channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time,Postid,Type,AddType,
Status,quyu,jiaqianQJ,mianji,huxing,fangshi,xianlu,yajin,zuoxiang,louceng,xingneng,yongtu,chewei,shequ,dizhi,gongsi,fuwuxiangju,dianhua,Areaid,lianxiren,shangpinType,xiajialiyou,goods_no,stock_quantity,market_price,sell_price,single_price,point,is_msg,is_top,is_red,is_hot,is_slide,digg_good,digg_bad,isFront from view_article_goods ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article_goods model = new Model.article_goods();
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
                if (ds.Tables[0].Rows[0]["goods_no"] != null && ds.Tables[0].Rows[0]["goods_no"].ToString() != "")
                {
                    model.goods_no = ds.Tables[0].Rows[0]["goods_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["stock_quantity"] != null && ds.Tables[0].Rows[0]["stock_quantity"].ToString() != "")
                {
                    model.stock_quantity = int.Parse(ds.Tables[0].Rows[0]["stock_quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["market_price"] != null && ds.Tables[0].Rows[0]["market_price"].ToString() != "")
                {
                    model.market_price = decimal.Parse(ds.Tables[0].Rows[0]["market_price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sell_price"] != null && ds.Tables[0].Rows[0]["sell_price"].ToString() != "")
                {
                    model.sell_price = decimal.Parse(ds.Tables[0].Rows[0]["sell_price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["single_price"] != null && ds.Tables[0].Rows[0]["single_price"].ToString() != "")
                {
                    model.single_price = decimal.Parse(ds.Tables[0].Rows[0]["single_price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"] != null && ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = Convert.ToInt32(ds.Tables[0].Rows[0]["point"]);
                }
                if (ds.Tables[0].Rows[0]["is_msg"] != null && ds.Tables[0].Rows[0]["is_msg"].ToString() != "")
                {
                    model.is_msg = int.Parse(ds.Tables[0].Rows[0]["is_msg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_top"] != null && ds.Tables[0].Rows[0]["is_top"].ToString() != "")
                {
                    model.is_top = int.Parse(ds.Tables[0].Rows[0]["is_top"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_red"] != null && ds.Tables[0].Rows[0]["is_red"].ToString() != "")
                {
                    model.is_red = int.Parse(ds.Tables[0].Rows[0]["is_red"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_hot"] != null && ds.Tables[0].Rows[0]["is_hot"].ToString() != "")
                {
                    model.is_hot = int.Parse(ds.Tables[0].Rows[0]["is_hot"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_slide"] != null && ds.Tables[0].Rows[0]["is_slide"].ToString() != "")
                {
                    model.is_slide = int.Parse(ds.Tables[0].Rows[0]["is_slide"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_good"] != null && ds.Tables[0].Rows[0]["digg_good"].ToString() != "")
                {
                    model.digg_good = int.Parse(ds.Tables[0].Rows[0]["digg_good"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_bad"] != null && ds.Tables[0].Rows[0]["digg_bad"].ToString() != "")
                {
                    model.digg_bad = int.Parse(ds.Tables[0].Rows[0]["digg_bad"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Postid"] != null)
                {
                    model.Postid = ds.Tables[0].Rows[0]["Postid"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddType"] != null && ds.Tables[0].Rows[0]["AddType"].ToString() != "")
                {
                    model.AddType = int.Parse(ds.Tables[0].Rows[0]["AddType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"] != null && ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (ds.Tables[0].Rows[0]["quyu"] != null && ds.Tables[0].Rows[0]["quyu"].ToString() != "")
                {
                    model.quyu = int.Parse(ds.Tables[0].Rows[0]["quyu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["jiaqianQJ"] != null && ds.Tables[0].Rows[0]["jiaqianQJ"].ToString() != "")
                {
                    model.jiaqianQJ = int.Parse(ds.Tables[0].Rows[0]["jiaqianQJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["mianji"] != null && ds.Tables[0].Rows[0]["mianji"].ToString() != "")
                {
                    model.mianji = int.Parse(ds.Tables[0].Rows[0]["mianji"].ToString());
                }
                if (ds.Tables[0].Rows[0]["huxing"] != null && ds.Tables[0].Rows[0]["huxing"].ToString() != "")
                {
                    model.huxing = int.Parse(ds.Tables[0].Rows[0]["huxing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fangshi"] != null && ds.Tables[0].Rows[0]["fangshi"].ToString() != "")
                {
                    model.fangshi = int.Parse(ds.Tables[0].Rows[0]["fangshi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["xianlu"] != null && ds.Tables[0].Rows[0]["xianlu"].ToString() != "")
                {
                    model.xianlu = int.Parse(ds.Tables[0].Rows[0]["xianlu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["yajin"] != null && ds.Tables[0].Rows[0]["yajin"].ToString() != "")
                {
                    model.yajin = int.Parse(ds.Tables[0].Rows[0]["yajin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["zuoxiang"] != null && ds.Tables[0].Rows[0]["zuoxiang"].ToString() != "")
                {
                    model.zuoxiang = ds.Tables[0].Rows[0]["zuoxiang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["louceng"] != null && ds.Tables[0].Rows[0]["louceng"].ToString() != "")
                {
                    model.louceng = ds.Tables[0].Rows[0]["louceng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["xingneng"] != null && ds.Tables[0].Rows[0]["xingneng"].ToString() != "")
                {
                    model.xingneng = ds.Tables[0].Rows[0]["xingneng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["yongtu"] != null && ds.Tables[0].Rows[0]["yongtu"].ToString() != "")
                {
                    model.yongtu = ds.Tables[0].Rows[0]["yongtu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["chewei"] != null && ds.Tables[0].Rows[0]["chewei"].ToString() != "")
                {
                    model.chewei = ds.Tables[0].Rows[0]["chewei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["shequ"] != null && ds.Tables[0].Rows[0]["shequ"].ToString() != "")
                {
                    model.shequ = ds.Tables[0].Rows[0]["shequ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["dizhi"] != null && ds.Tables[0].Rows[0]["dizhi"].ToString() != "")
                {
                    model.dizhi = ds.Tables[0].Rows[0]["dizhi"].ToString();
                }

                if (ds.Tables[0].Rows[0]["gongsi"] != null && ds.Tables[0].Rows[0]["gongsi"].ToString() != "")
                {
                    model.gongsi = ds.Tables[0].Rows[0]["gongsi"].ToString();
                }

                if (ds.Tables[0].Rows[0]["fuwuxiangju"] != null && ds.Tables[0].Rows[0]["fuwuxiangju"].ToString() != "")
                {
                    model.fuwuxiangju = ds.Tables[0].Rows[0]["fuwuxiangju"].ToString();
                }

                if (ds.Tables[0].Rows[0]["dianhua"] != null && ds.Tables[0].Rows[0]["dianhua"].ToString() != "")
                {
                    model.dianhua = ds.Tables[0].Rows[0]["dianhua"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Areaid"] != null && ds.Tables[0].Rows[0]["Areaid"].ToString() != "")
                {
                    model.Areaid = Convert.ToInt32(ds.Tables[0].Rows[0]["Areaid"]);
                }

                if (ds.Tables[0].Rows[0]["lianxiren"] != null && ds.Tables[0].Rows[0]["lianxiren"].ToString() != "")
                {
                    model.lianxiren = ds.Tables[0].Rows[0]["lianxiren"].ToString();
                }
                if (ds.Tables[0].Rows[0]["shangpinType"] != null && ds.Tables[0].Rows[0]["shangpinType"].ToString() != "")
                {
                    model.shangpinType = ds.Tables[0].Rows[0]["shangpinType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["xiajialiyou"] != null && ds.Tables[0].Rows[0]["xiajialiyou"].ToString() != "")
                {
                    model.xiajialiyou = ds.Tables[0].Rows[0]["xiajialiyou"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isFront"] != null && ds.Tables[0].Rows[0]["isFront"].ToString() != "")
                {
                    model.isFront = int.Parse(ds.Tables[0].Rows[0]["isFront"].ToString());
                }

                #endregion  父表信息end

                model.goods_group_prices = GetGoodsGroupPriceList(id); //获得用户组商品价格
                model.albums = new article_albums().GetList(id); //相册信息
                model.attribute_values = new attribute_value().GetList(id); //扩展属性
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
        public DataSet GetGoodsList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(@" id,channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time,Postid,Type,AddType,
Status,quyu,jiaqianQJ,mianji,huxing,fangshi,xianlu,yajin,zuoxiang,louceng,xingneng,yongtu,chewei,shequ,dizhi,Areaid,gongsi,fuwuxiangju,dianhua,lianxiren,shangpinType,xiajialiyou,goods_no,stock_quantity,market_price,sell_price,point,is_msg,is_top,is_red,is_hot,is_slide,digg_good,digg_bad ");
            strSql.Append(" FROM view_article_goods ");
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
        public DataSet GetGoodsList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_article_goods");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #region 分頁
        public DataTable list_pagesWhere(int page, int numPerPage, string whereStr, string orderStr)
        {
            string sql = "";
            try
            {
                sql = "select top " + numPerPage + " * from view_article_goods where 1=1";
                string sql1 = "";
                if (orderStr == "")
                {
                    orderStr = " order by sort_id asc";
                }
                sql += whereStr;
                sql1 += whereStr;
                if (page > 1)
                {
                    sql += " and id not in(select top " + (page - 1) * numPerPage + " id from view_article_goods where 1=1 " + sql1 + orderStr + ")";
                }
                sql += orderStr;

                string s = sql; ;
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #region 查詢前臺會員刊登物件列表
        public DataTable GetList(string whereStr, string orderStr)
        {
            string sql = "";
            try
            {
                sql = "select a.*,b.group_id from view_article_goods a,dt_users b where 1=1 and a.user_id=b.id ";
                if (orderStr == "")
                {
                    orderStr = " order by sort_id asc";
                }
                sql += whereStr;
                sql += orderStr;

                string s = sql; ;
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #region 獲取總數
        public int GetTatalNum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(id) FROM view_article_goods");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere + "");
            }
            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }
        #endregion

        #region 获得商品价格数据列表===================================
        /// <summary>
        /// 获得商品价格数据列表
        /// </summary>
        private List<Model.goods_group_price> GetGoodsGroupPriceList(int article_id)
        {
            List<Model.goods_group_price> modelList = new List<Model.goods_group_price>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,group_id,price from dt_goods_group_price ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.goods_group_price model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.goods_group_price();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["group_id"] != null && dt.Rows[n]["group_id"].ToString() != "")
                    {
                        model.group_id = int.Parse(dt.Rows[n]["group_id"].ToString());
                    }
                    if (dt.Rows[n]["price"] != null && dt.Rows[n]["price"].ToString() != "")
                    {
                        model.price = decimal.Parse(dt.Rows[n]["price"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion 获得商品价格数据列表

        #endregion  Method


    }
}

