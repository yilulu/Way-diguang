using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;
using Common;

namespace DTcms.Web
{
    public partial class Search : PageBase
    {
        protected string prolistview = string.Empty;
        int PartentID = 0, page = 1, pageSize, totalCount, cataID = 0; protected int channel_id;
        protected string property = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("mid");
            this.PartentID = DTRequest.GetQueryInt("Pcid");
            this.cataID = DTRequest.GetQueryInt("Catacid");
            this.page = DTRequest.GetQueryInt("page", 1);
            this.property = DTRequest.GetQueryString("property");

            this.pageSize = GetPageSize(20); //每頁數量
            this.prolistview = Utils.GetCookie("goods_list_view"); //顯示方式
            list();
            if (!IsPostBack)
            {
                Bind(page);
            }
        }

        #region 加载类别
        void list()
        {
            BLL.category bll = new BLL.category();
            string CatList = string.Empty;
            string FirstName = bll.GetTitle(PartentID);
            string keyWords = string.Empty;
            // CatList = "<h3><a href='Search.aspx?Pcid=" + PartentID + "&mid=" + channel_id + "&Catacid=" + PartentID + "'>" + FirstName + "</a></h3>";
            CatList = "<h3>" + FirstName + "</h3>";
            if (!string.IsNullOrEmpty(Request.QueryString["key"]) && Request.QueryString["key"] != "請輸入商品名稱")
            {
                keyWords = Request.QueryString["key"];
            }
            DataTable dt = bll.GetNextList(this.PartentID, channel_id, keyWords);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int pID = Utils.StringToNum(dt.Rows[i]["ID"].ToString());
                    string SecondName = dt.Rows[i]["title"].ToString();
                    CatList += "<h1><a href='Search.aspx?Pcid=" + PartentID + "&mid=" + channel_id + "&Catacid=" + pID + "'>" + SecondName + "</a></h1>";
                    DataTable dt1 = bll.GetList(pID, channel_id);
                    if (dt1 != null)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            pID = Utils.StringToNum(dt1.Rows[j]["ID"].ToString());
                            string ThreeName = dt1.Rows[j]["title"].ToString();
                            CatList += "<h2><a href='Search.aspx?Pcid=" + PartentID + "&mid=" + channel_id + "&Catacid=" + pID + "'>" + ThreeName + "</a></h2>";
                        }

                    }
                }
            }
            HtmlList.InnerHtml = CatList;
        }
        #endregion

        #region 加载数据
        private void Bind(int page)
        {
            BLL.article bll = new BLL.article();
            BLL.category bllCat = new BLL.category();
            int totalCount;
            int pageSize = 20;

            //最新商品
            string where = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Catacid"]) && DTRequest.GetQueryInt("Catacid") != 0)
            {
                where = " and category_id =" + Request.QueryString["Catacid"];
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Pcid"]) && DTRequest.GetQueryInt("Pcid") != 0)
                {
                    if (bllCat.GetNextList(this.PartentID, channel_id).Rows.Count > 0)
                    {
                        where = " and category_id in (select ID from dt_category where  parent_id= " + Request.QueryString["Pcid"] + ")";
                    }
                    else
                    {
                        where = " and category_id =" + Request.QueryString["Pcid"];
                    }

                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["key"]) && Request.QueryString["key"] != "請輸入商品名稱")
            {
                where += " and title like '%" + Request.QueryString["key"] + "%'";
            }
            this.rptList.DataSource = bll.list_pagesWhere(page, this.pageSize, " and  channel_id=" + channel_id + "  and Status=1" + where, "  order by add_time desc");
            this.rptList.DataBind();


            //綁定頁碼
            this.totalCount = bll.GetTatalNum(" channel_id=" + channel_id + "  and Status=1" + where);
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("Search.aspx", "mid={0}&Pcid={1}&keywords={2}&property={3}&Catacid={4}&page={5}",
                this.channel_id.ToString(), this.PartentID.ToString(), Request.QueryString["key"], this.property, this.cataID.ToString(), "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 返回圖文每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("goods_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("goods_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("Search.aspx", "mid={0}&Pcid={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.PartentID.ToString(), "", this.property));
        }
        #endregion

        //protected void aspPage_PageChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect("Search.aspx?Pcid=" + this.PartentID + "&mid=" + this.channel_id + "&page=" + aspPage.CurrentPageIndex);
        //    //Bind(aspPage.CurrentPageIndex);
        //}
    }
}