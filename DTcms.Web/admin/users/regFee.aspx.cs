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
    public partial class regFee : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int group_id;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string Url = Request.Url.ToString();
            if (string.IsNullOrEmpty(Utils.GetCookie("UserCheckPwd")))
            {
                if (string.IsNullOrEmpty(DTRequest.GetQueryString("State")))
                {
                    Response.Redirect("RePw.aspx?Url=" + Url + "");
                }
            }
            this.group_id = DTRequest.GetQueryInt("group_id");
            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(15); //每頁數量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("users", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                //TreeBind("is_lock=0"); //綁定類別
                RptBind("id>0" + CombSqlTxt(this.group_id, this.keywords), "  order by reg_time desc,id desc");
            }
        }


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
            this.rptList.DataSource = bll.list_pagesWhere(this.page, this.pageSize, " and group_id<>5 and " + _strWhere, _orderby);
            this.totalCount = bll.GetTatalNum(_strWhere + " and group_id<>5");
            this.rptList.DataBind();

            //綁定頁碼
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("regFee.aspx", "group_id={0}&keywords={1}&page={2}",
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
            if (int.TryParse(Utils.GetCookie("regFee_page_size"), out _pagesize))
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
                    result = "未繳費";
                    break;
                case 1:
                    result = "已繳費";
                    break;

            }
            return result;
        }
        #endregion

        #region 關健字查詢
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("regFee.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), txtKeywords.Text));
        }
        #endregion

        #region 篩選類別
        protected void ddlGroupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("regFee.aspx", "isFee={0}&keywords={1}",
                ddlGroupId.SelectedValue, this.keywords));
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
                    Utils.WriteCookie("regFee_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("regFee.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), this.keywords));
        }
        #endregion

        #region 批次該更狀態
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int Uid = 0;
            ChkAdminLevel("users", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.users bll = new BLL.users();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    int n = bll.UpFee(id, 1);
                    if (n > 0)
                    {
                        Model.users mod = bll.GetModel(id);
                        if (mod != null)
                        {
                            string JiShaoRen = mod.dianming;
                            DataTable dtUser = bll.GetUser_Info(JiShaoRen);
                            if (dtUser != null)
                            {
                                int UserPoint = 0;
                                Uid = Utils.StringToNum(dtUser.Rows[0]["ID"].ToString());
                                int GroupID = Utils.StringToNum(dtUser.Rows[0]["group_id"].ToString());
                                switch (mod.group_id)
                                {
                                    case 1:
                                        UserPoint = 0;
                                        break;
                                    case 2:
                                        UserPoint = 50;
                                        break;
                                    case 3:
                                        UserPoint = 100;
                                        break;
                                    case 4:
                                        UserPoint = 150;
                                        break;
                                }
                                int bk = bll.UpPoint(Uid, UserPoint);
                                if (bk > 0)
                                {
                                    BLL.point_log points = new BLL.point_log();
                                    Model.point_log model = new Model.point_log();
                                    model.user_id = Uid;
                                    model.user_name = "";
                                    model.value = UserPoint;
                                    model.remark = "朋友成為會員回饋點數";
                                    model.add_time = DateTime.Now;
                                    model.type = 1;//2標誌點數是減少
                                    int m = points.Add(model);
                                }
                            }
                        }
                    }
                }
            }
            JscriptMsg("批次更改成功啦！", Utils.CombUrlTxt("regFee.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), this.keywords), "Success");
        }
        #endregion

        #region 取消朋友成為以下會員回饋點數
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int Uid = 0;
            ChkAdminLevel("users", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            BLL.users bll = new BLL.users();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.users mod = bll.GetModel(id);
                    if (mod != null)
                    {
                        string JiShaoRen = mod.dianming;
                        DataTable dtUser = bll.GetUser_Info(JiShaoRen);
                        if (dtUser != null)
                        {
                            int UserPoint = 0;
                            Uid = Utils.StringToNum(dtUser.Rows[0]["ID"].ToString());
                            int GroupID = Utils.StringToNum(dtUser.Rows[0]["group_id"].ToString());
                            switch (mod.group_id)
                            {
                                case 1:
                                    UserPoint = 0;
                                    break;
                                case 2:
                                    UserPoint = 50;
                                    break;
                                case 3:
                                    UserPoint = 100;
                                    break;
                                case 4:
                                    UserPoint = 150;
                                    break;
                            }
                            int bk = bll.UpJianPoint(Uid, UserPoint);
                            if (bk > 0)
                            {
                                BLL.point_log points = new BLL.point_log();
                                Model.point_log model = new Model.point_log();
                                model.user_id = Uid;
                                model.user_name = "";
                                model.value = UserPoint;
                                model.remark = "取消朋友成為會員回饋點數";
                                model.add_time = DateTime.Now;
                                model.type = 2;//2標誌點數是減少
                                int m = points.Add(model);
                            }
                        }
                    }
                }
            }
            JscriptMsg("批次更改成功啦！", Utils.CombUrlTxt("regFee.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), this.keywords), "Success");
        }
        #endregion

    }
}
