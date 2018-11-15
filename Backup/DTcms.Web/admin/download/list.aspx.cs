using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.download
{
    public partial class list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int category_id;
        protected string property = string.Empty;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.category_id = DTRequest.GetQueryInt("category_id");
            this.keywords = DTRequest.GetQueryString("keywords");
            this.property = DTRequest.GetQueryString("property");
            if (channel_id == 0)
            {
                JscriptMsg("頻道參數不正確！", "back", "Error");
                return;
            }
            this.pageSize = GetPageSize(15); //每頁數量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel(channel_id, DTEnums.ActionEnum.View.ToString()); //檢查許可權
                TreeBind(this.channel_id); //綁定類別
                RptBind("id>0" + CombSqlTxt(this.channel_id, this.category_id, this.keywords, this.property), " order by sort_id asc");
            }
        }

        #region 綁定類別=================================
        private void TreeBind(int _channel_id)
        {
            BLL.category bll = new BLL.category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("所有類別", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 數據綁定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.category_id > 0)
            {
                this.ddlCategoryId.SelectedValue = this.category_id.ToString();
            }
            this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;
            BLL.article bll = new BLL.article();
            this.rptList.DataSource = bll.listDown_page(this.page, this.pageSize, " and " + _strWhere, _orderby);
            this.totalCount = bll.GetDownTatalNum(_strWhere);
            this.rptList.DataBind();

            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 組合SQL查詢語句==========================
        protected string CombSqlTxt(int _channel_id, int _category_id, string _keywords, string _property)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_channel_id > 0)
            {
                strTemp.Append(" and channel_id=" + channel_id);
            }
            if (_category_id > 0)
            {
                strTemp.Append(" and category_id in(select id from dt_category where id=" + _category_id + " and class_list like '%," + _category_id + ",%')");
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        strTemp.Append(" and is_lock=1");
                        break;
                    case "unIsLock":
                        strTemp.Append(" and is_lock=0");
                        break;
                    case "isMsg":
                        strTemp.Append(" and is_msg=1");
                        break;
                    case "isRed":
                        strTemp.Append(" and is_red=1");
                        break;
                }
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回圖文每頁數量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("download_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //設置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ChkAdminLevel(channel_id, DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
            int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
            BLL.article bll = new BLL.article();
            Model.article_download model = bll.GetDownloadModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnmsg":
                    if (model.is_msg == 1)
                        bll.UpdateDownloadField(id, "is_msg=0");
                    else
                        bll.UpdateDownloadField(id, "is_msg=1");
                    break;
                case "ibtnred":
                    if (model.is_red == 1)
                        bll.UpdateDownloadField(id, "is_red=0");
                    else
                        bll.UpdateDownloadField(id, "is_red=1");
                    break;
            }
            this.RptBind("id>0" + CombSqlTxt(this.channel_id, this.category_id, this.keywords, this.property), "sort_id asc,add_time desc");
        }

        //關健字查詢
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), txtKeywords.Text, this.property));
        }

        //篩選類別
        protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), ddlCategoryId.SelectedValue, this.keywords, this.property));
        }

        //篩選屬性
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
               this.channel_id.ToString(), this.category_id.ToString(), this.keywords, ddlProperty.SelectedValue));
        }

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("download_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //儲存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BLL.article bll = new BLL.article();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            JscriptMsg("儲存排序成功！", Utils.CombUrlTxt("list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property), "Success");
        }

        //批次刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel(channel_id, DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.article bll = new BLL.article();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功！", Utils.CombUrlTxt("list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property), "Success");
        }

        #region 獲取用戶名
        public string GetUserName(string uID)
        {
            string UserName = string.Empty;
            if (!string.IsNullOrEmpty(uID))
            {
                int ID = Utils.StringToNum(uID);
                BLL.users blluser = new BLL.users();
                Model.users User = blluser.GetModel(ID);
                if (User != null)
                {
                    UserName = User.user_name;
                }
                else
                {
                    Model.manager admin_info = GetAdminInfo();
                    UserName = admin_info.user_name;
                }
            }
            return UserName;
        }
        #endregion

        #region 下载会员上传的档案
        public string DownFileByUser(string ID, string userID)
        {
            string HrefValue = "<a href=\"../../VipDown.aspx?ID=" + ID + "\">" + "下載" + "</a>";
            string ReturnValue = userID == "0" ? "" : HrefValue;
            return ReturnValue;
        }
        #endregion

    }
}
