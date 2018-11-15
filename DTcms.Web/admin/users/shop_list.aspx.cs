using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class shop_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int group_id=5;
        protected string keywords = string.Empty;
        protected string Url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Url = Request.Url.ToString();
            if (string.IsNullOrEmpty(Utils.GetCookie("UserCheckPwd")))
            {
                if (string.IsNullOrEmpty(DTRequest.GetQueryString("State")))
                {
                    Response.Redirect("RePw.aspx?Url=" + Url + "");
                }
            }

            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(15); //每頁數量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("users", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                //TreeBind("is_lock=0"); //綁定類別
                RptBind("id>0" + CombSqlTxt(this.group_id, this.keywords), "  order by exp asc");
            }
        }

        #region 綁定類別=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "id desc").Tables[0];

            this.ddlGroupId.Items.Clear();
            this.ddlGroupId.Items.Add(new ListItem("所有會員組", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 數據綁定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.group_id > 0)
            {
                this.ddlGroupId.SelectedValue = this.group_id.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            BLL.users bll = new BLL.users();
            this.rptList.DataSource = bll.list_pagesWhere(this.page, this.pageSize, " and group_id=5 and " + _strWhere, _orderby);
            this.totalCount = bll.GetTatalNum(_strWhere + " and group_id=5");
            this.rptList.DataBind();

            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("shop_list.aspx", "group_id={0}&keywords={1}&page={2}",
                this.group_id.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 組合SQL查詢語句==========================
        protected string CombSqlTxt(int _group_id, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_group_id > 0)
            {
                strTemp.Append(" and group_id=" + _group_id);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or email like '%" + _keywords + "%' or nick_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用戶每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("shop_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回使用者狀態===========================
        protected string GetUserStatus(int is_lock)
        {
            string result = string.Empty;
            switch (is_lock)
            {
                case 0:
                    result = "正常";
                    break;
                case 1:
                    result = "待驗證";
                    break;
                case 2:
                    result = "待審核";
                    break;
                case 3:
                    result = "已禁用";
                    break;
            }
            return result;
        }
        #endregion

        //關健字查詢
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("shop_list.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), txtKeywords.Text));
        }

        //篩選類別
        protected void ddlGroupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("shop_list.aspx", "group_id={0}&keywords={1}",
                ddlGroupId.SelectedValue, this.keywords));
        }

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("shop_list_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("shop_list.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), this.keywords));
        }

        //批次刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("users", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.users bll = new BLL.users();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功啦！", Utils.CombUrlTxt("shop_list.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), this.keywords), "Success");
        }

        #region 处理头像
        public string GetFace(string FaceUrl)
        {
            string Face = "../../images/default_user_avatar.gi";
            if (!string.IsNullOrEmpty(FaceUrl))
            {
                if (FaceUrl.IndexOf("upload") > 0)
                {
                    Face = "../.." + FaceUrl;
                }
                else
                {
                    Face = "../../upload/user/" + FaceUrl;
                }
            }
            return Face;
        }
        #endregion

        #region 儲存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BLL.users bll = new BLL.users();
            Repeater rptList = new Repeater();

            rptList = this.rptList;


            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "exp=" + sortId.ToString());
            }

            JscriptMsg("儲存排序成功！", "shop_list.aspx", "Success");
        }
        #endregion


    }
}
