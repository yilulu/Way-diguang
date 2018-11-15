using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin
{
    public partial class vipfile_list : Web.UI.ManagePage
    {
        public string keywords = string.Empty;
        protected int totalCount;
        protected int page;
        protected int pageSize = 20;
        string where = "";
        public string pid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_model", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind("");
            }
        }

        #region 數據綁定
        private void RptBind(string _strWhere)
        {
            _strWhere = " Typeid=3";
            DAL.imagedal aredal = new DAL.imagedal();
            this.rptList.DataSource = aredal.GetDatalistpage(this.pageSize, this.page, _strWhere + where, " sort", out this.totalCount);
            this.rptList.DataBind();
        }
        #endregion

        //設置分頁數量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            RptBind("");
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

        //刪除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_model", DTEnums.ActionEnum.Delete.ToString()); //檢查許可權
            DAL.imagedal imagedal = new DAL.imagedal();
            //批次刪除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    imagedal.Delete(id);
                }
            }
            JscriptMsg("批次刪除成功啦！", Utils.CombUrlTxt("vipfile_list.aspx", "keywords={0}", ""), "Success", "parent.loadChannelTree");
        }

        //查詢操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RptBind("");
        }

    }
}
