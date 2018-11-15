using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DTcms.Web
{
    public partial class shengji : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TreeBind("is_lock=0"); //綁定類別
                Bindinfo();
            }
        }

        private void Bindinfo()
        {
            string GroupID = Request.QueryString["giD"];
            ddlGroup.SelectedValue = GroupID;
        }

        #region 綁定類別=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "grade asc,id asc").Tables[0];

            this.ddlGroup.Items.Clear();
            this.ddlGroup.Items.Add(new ListItem("請選擇組別...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroup.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {
            Session["GroupName"] = ddlGroup.SelectedValue;
            Response.Redirect("RegPay.aspx?paymenttype=" + ddlzhifu.SelectedValue + "");
        }
    }
}