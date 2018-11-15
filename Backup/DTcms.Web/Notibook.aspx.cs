using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Text;
using System.Data;

namespace DTcms.Web
{
    public partial class Notibook : PageBase
    {
        protected int HtmlisLogin = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WEBUserCurrent.IsLogin)
            {
                HtmlisLogin = 0;
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('如果你已經本站會員，請先登入');window.location.href='login.aspx'</script>");
            }
            if (!IsPostBack)
            {
                int UID = WEBUserCurrent.UserID;
                if (UID != 0)
                {
                    BLL.users BLLUser = new BLL.users();
                    Model.users User = new Model.users();
                    User = BLLUser.GetModel(UID);
                    if (User != null)
                    {
                        txtEmail.Value = User.email;
                        txtPhone.Value = User.mobile;
                        txtTel.Value = User.telphone;
                        txtUserName.Value = User.user_name;
                    }
                }
            }

        }

        #region 添加留言
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtUserName.Value;
            string sex = "男";
            if (radio2.Checked == true)
            {
                sex = "女";
            }
            string mobile = txtPhone.Value;
            string telNuM = txtTel.Value;
            string EmailValue = txtEmail.Value;
            string NoteBookContent = txtContent.Value;
            string zhuzhi = Request.Form["chkZhuZhi"];
            string DiDian = Request.Form["dpProvince"] + "|";
            DiDian += Request.Form["dpCity"] + "|";
            DiDian += txtWrite.Value;
            string ClassName = Request.Form["chkCatagory"];
            string GongNeng = Request.Form["chkFunction"];
            string Money = txtOwer.Value;
            string MianJi = txtOwerTel.Value;

            Model.dt_feedback Model = new Model.dt_feedback();
            Model.UserID = WEBUserCurrent.UserID;
            Model.user_name = name;
            Model.user_qq = sex;
            Model.user_tel = mobile;
            Model.title = telNuM;
            Model.user_email = EmailValue;
            Model.content = NoteBookContent;
            Model.zhuzhi = zhuzhi;
            //if (Model.zhuzhi == "介紹房屋出售")
            //{
            //    BLL.users User = new BLL.users();
            //    User.UpPoint(WEBUserCurrent.UserID, 5000);
            //}
            Model.user_Address = DiDian;
            Model.user_Class = ClassName;
            Model.user_Function = GongNeng;
            Model.user_Money = Money;
            Model.user_MianJi = MianJi;
            BLL.dt_feedback bll = new BLL.dt_feedback();

            bll.Add(Model);

            this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('留言添加成功');window.location.href='index.aspx'</script>");
        }
        #endregion
    }
}