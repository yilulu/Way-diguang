using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Common;
using System.Text;
using System.Data;
using Common;

namespace DTcms.Web
{
    public partial class productTdList : PageBase
    {
        protected int channel_id; int CatagoryID = 328;
        BLL.article bll = new BLL.article();
        string sqlwhere = ""; protected string title = "";
        protected string Cid = "51";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("mid");
            switch (channel_id)
            {
                case 2:

                    CatagoryID = 303;
                    break;
                case 3:

                    CatagoryID = 302;
                    break;

            }
            Cid = DTRequest.GetQueryString("cid");
            if (!IsPostBack)
            {
                Bind(1);
                RptBind();
            }
        }

        #region 獲取標題
        public string GetTitle()
        {

            switch (channel_id)
            {
                case 2:
                    title = "<a href=\"productlist.aspx?mid=" + channel_id + "\">" + "土地出售" + "</a>";
                    break;
                case 3:
                    title = "<a href=\"productlist.aspx?mid=" + channel_id + "\">" + "土地出租" + "</a>";
                    break;

            }
            return title;
        }
        #endregion

        #region 加載數據
        private void Bind(int page)
        {
            int totalCount;
            int pageSize = 5;
            //最新商品
            this.repdate1.DataSource = bll.list_pagesWhere(page, pageSize, " and channel_id=10 and category_id=" + CatagoryID + "" + GetWhere() + " and Status=1", " order by sort_id asc");
            this.repdate1.DataBind();
            aspPage.PageSize = pageSize;
            aspPage.RecordCount = bll.GetTatalNum(" channel_id=10 and category_id=" + CatagoryID + "" + GetWhere() + " and Status=1");

        }
        #endregion

        #region 加載條件
        private string GetWhere()
        {
            StringBuilder str = new StringBuilder("");
            if (Request.QueryString["czid"] != null && Request.QueryString["czid"].ToString() != "0")
            {
                str.Append(" and Areaid=" + Request.QueryString["czid"]);
            }
            if (Request.QueryString["qyid"] != null && Request.QueryString["qyid"].ToString() != "0")
            {
                str.Append(" and quyu=" + Request.QueryString["qyid"]);
            }

            if (Request.QueryString["mjid"] != null && Request.QueryString["mjid"].ToString() != "0")
            {
                str.Append(" and mianji=" + Request.QueryString["mjid"]);
            }
            if (Request.QueryString["hxid"] != null && Request.QueryString["hxid"].ToString() != "0")
            {
                str.Append(" and huxing=" + Request.QueryString["hxid"]);
            }

            if (Request.QueryString["fsid"] != null && Request.QueryString["fsid"].ToString() != "0")
            {
                str.Append(" and fangshi=" + Request.QueryString["fsid"]);
            }
            if (Request.QueryString["dtid"] != null && Request.QueryString["dtid"].ToString() != "0")
            {
                str.Append(" and xianlu=" + Request.QueryString["dtid"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                str.Append(" and category_id=" + int.Parse(Request.QueryString["cid"]));
            }
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                int CataID = DTRequest.GetQueryInt("mid");
                switch (CataID)
                {
                    case 2:
                        str.Append(" and (category_id=303 or category_id=328)");
                        break;
                    case 3:
                        str.Append(" and (category_id=302 or category_id=328)");
                        break;
                }
            }
            return str.ToString();
        }
        #endregion

        #region 搜尋
        protected void aspPage_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Bind(e.NewPageIndex);
        }
        #endregion
        SQLHelper SQlHelper = new SQLHelper();

        #region 獲取區域 獲取區域/地標 顯示名稱
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

        #region 處理價格
        public string getSellOrHire(string ID)
        {
            string ReturnHtml = string.Empty;
            if (!string.IsNullOrEmpty(ID))
            {
                int proID = Utils.StringToNum(ID);
                BLL.article bll = new BLL.article();
                Model.article_goods model = bll.GetGoodsModel(proID);
                if (model != null)
                {
                    switch (model.category_id)
                    {
                        case 303:
                            ReturnHtml = "單價:" + model.single_price + "元/坪";
                            break;
                        case 302:
                            ReturnHtml = "租金:" + model.market_price + "元/月";
                            break;
                        case 328:
                            ReturnHtml = "總價:" + model.sell_price + "萬<br/>" + "租金:" + model.market_price + "元/月";
                            break;
                    }
                    //ReturnHtml = "總價:" + model.sell_price + "萬/坪";
                }
            }
            return ReturnHtml;
        }
        #endregion

        #region 獲取區域
        public string GetA(string id)
        {
            string AreaName = string.Empty;
            string cityName = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                AreaDal dal = new AreaDal();
                Model.Area model = new Model.Area();
                model = dal.GetModel(int.Parse(id));
                if (model != null)
                {
                    AreaName = model.title;
                    int partentID = model.parent;
                    Model.Area City = dal.GetModel(partentID);
                    if (City != null)
                    {
                        cityName = City.title;
                    }
                }
            }

            return cityName + "-" + AreaName;
        }
        #endregion

        #region 數據綁定
        private void RptBind()
        {
            BLL.category bll = new BLL.category();
            int partentID = 22;
            if (this.channel_id == 3)
            {
                partentID = 55;
            }

            DataTable dt = bll.GetList(partentID, this.channel_id);
            if (dt.Rows.Count > 0)
            {
                rptList.DataSource = dt.DefaultView;
                rptList.DataBind();
            }

        }
        #endregion

        #region  獲取分區
        public string GetCategory(string CID)
        {
            string cataName = string.Empty;
            if (!string.IsNullOrEmpty(CID))
            {
                int ID = Utils.StringToNum(CID);
                BLL.category bllcat = new BLL.category();
                cataName = bllcat.GetTitle(ID);
            }
            return cataName;
        }
        #endregion
    }
}