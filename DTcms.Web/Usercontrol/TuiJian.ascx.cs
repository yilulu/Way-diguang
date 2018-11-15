using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using Common;
using System.Data;

namespace DTcms.Web
{
    public partial class TuiJian : System.Web.UI.UserControl
    {
        SQLHelper SQlHelper = new SQLHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            int typeid = string.IsNullOrEmpty(Request.QueryString["mid"]) ? 3 : Convert.ToInt32(Request.QueryString["mid"]);
            string strWhere = " and channel_id=" + typeid + "";
            //推荐商品
            //if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
            //{
            //    strWhere += " and category_id=" + int.Parse(Request.QueryString["cid"]);
            //}
            DAL.article dalArticle = new article();
            repdate2.DataSource = dalArticle.GetPageindexList("6", 10, "");
            repdate2.DataBind();
        }


        #region 获取區域/地標 显示名称
        public string GetTypleWhereTilte(int id, int? inherit_index = null)
        {
            try
            {
                string title = "";
                string sql = "select title from dt_sys_model where id=" + id;
                if (inherit_index != null)
                {
                    sql += " and inherit_index='" + inherit_index + "'";
                }
                var query = SQlHelper.ExecuteScalar(sql, CommandType.Text);
                title = query == null ? "" : query.ToString();
                return title;
            }
            catch (Exception)
            {
                return "";
            }

        }
        #endregion

        #region 截取字符串
        public string ToSubstring(string obj, int Length)
        {
            try
            {
                if (obj.Length > Length)
                {
                    return obj.Substring(0, Length) + "...";
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 獲取價格方式
        public string getSellOrHire(string price, string mid)
        {
            string ReturnHtml = string.Empty;
            if (!string.IsNullOrEmpty(mid))
            {
                if (mid == "2")
                {
                    ReturnHtml = "總價:" + price + "萬";
                }
                if (mid == "3")
                {
                    ReturnHtml = "租金:" + price + "元/月";
                }
            }

            return ReturnHtml;
        }
        #endregion
    }
}