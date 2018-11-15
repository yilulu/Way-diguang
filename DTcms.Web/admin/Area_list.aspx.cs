using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Data;

namespace DTcms.Web.admin
{
    public partial class Area_list : Web.UI.ManagePage
    {
        public string keywords = string.Empty;
        protected int totalCount;
        protected int page;
        protected int pageSize = 20;
        string where = "";
        public string pid = "";
        protected string prolistview = string.Empty;
        DAL.AreaDal aredal = new DAL.AreaDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            this.pageSize = GetPageSize(20); //每頁數量
            this.prolistview = Utils.GetCookie("article_list_view"); //顯示方式
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                where = " and parent=" + Request.QueryString["pid"];
                pid = Request.QueryString["pid"];
            }
            else
            {
                where = " and parent=0";
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_model", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("id>0 " + CombSqlTxt(this.keywords));
            }
        }

        #region 資料綁定
        private void RptBind(string _strWhere)
        {
            BLL.AreaBll bllArea = new BLL.AreaBll();
            this.txtKeywords.Text = this.keywords;
            this.page = DTRequest.GetQueryInt("page", 1);
            string strWhere = "";
            strWhere = _strWhere + where;
            this.rptList.DataSource = bllArea.list_page(this.page, this.pageSize, " and " + strWhere, " ");
            this.rptList.DataBind();

            //綁定頁碼
            this.totalCount = bllArea.GetNewsTatalNum(strWhere);
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("Area_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("article_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("Area_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__"));
        }

        #region 組合SQL查詢語句
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 查詢階級
        protected string GetPath(string ID)
        {
            string HtmlName = "";
            //if (PartentID == "0")
            //{
            //    HtmlName = "進入區";
            //}
            //if (PartentID != "0")
            //{
            where = " parent=" + ID + "";
            DataTable dt = aredal.GetDatalistpage(this.pageSize, this.page, where, " sort", out this.totalCount).Tables[0];
            if (dt.Rows.Count > 0)
            {
                HtmlName = "進入下級";
            }
            // }
            return HtmlName;

        }
        #endregion

        //刪除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_model", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            DAL.AreaDal aredal = new DAL.AreaDal();
            //批次刪除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    aredal.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("Area_list.aspx", "keywords={0}", txtKeywords.Text.Trim()), "Success", "parent.loadChannelTree");
        }

        //查詢操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("Area_list.aspx?keywords=" + txtKeywords.Text);
        }


        #region 返回圖文每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("article_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion
    }
}
