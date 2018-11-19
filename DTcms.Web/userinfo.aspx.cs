using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Model;
using DTcms.Common;
using Common;

namespace DTcms.Web
{
    public partial class userinfo : PageBase
    {
        DTcms.DAL.users daluser = new DTcms.DAL.users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WEBUserCurrent.IsLogin)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Bindinfo();
                }
            }
        }

        private void Bindinfo()
        {
            var usermodel = daluser.GetModel(WEBUserCurrent.UserID);
            lblUsername.Text = usermodel.user_name;
            txtpassword.Attributes.Add("Value", DESEncrypt.Decrypt(usermodel.password));
            txtpassword2.Attributes.Add("Value", DESEncrypt.Decrypt(usermodel.password));
            txtName.Value = usermodel.nick_name;
            txtemall.Value = usermodel.email;
            txtAddress.Value = usermodel.address;
            txtphone.Value = usermodel.telphone;
            txtMobile.Value = usermodel.mobile;
            txtQQ.Value = usermodel.qq;
            txtsafe_question.Value = usermodel.safe_question;
            txtsafe_answer.Value = usermodel.safe_answer;
            ViewState["Image"] = usermodel.avatar;
            txtsafe_question.Value = usermodel.safe_question;
            txtsafe_answer.Value = usermodel.safe_answer;
            lblPoint.Text = usermodel.point.ToString();
            BLL.user_groups bllusergroup = new BLL.user_groups();
            string typeName = "普通會員";
            if (!string.IsNullOrEmpty(usermodel.group_id.ToString()))
            {
                typeName = bllusergroup.GetTitle(usermodel.group_id);
            }
            lblGroupName.Text = typeName + "會員";
            if (usermodel.group_id == 1)
            {
                Session["Type"] = 0;
                UpUserGroup.Text = "您是" + new DAL.user_groups().GetTitle(usermodel.group_id) + "會員,點擊" + "<a href=\"shengji.aspx?giD=" + usermodel.group_id + "\" target=\"_blank\">升級</a>";
            }
            if (usermodel.group_id != 1 && usermodel.group_id != 5)
            {
                if (usermodel.endtime.AddDays(2).AddDays(-30) == DateTime.Now)
                {
                    Session["Type"] = 1;
                    UpUserGroup.Text = "您是" + new DAL.user_groups().GetTitle(usermodel.group_id) + "會員,將在30天後到期,點擊" + "<a href=\"shengji.aspx?giD=" + usermodel.group_id + "\" target=\"_blank\">續費</a>";
                }
            }

        }

        //确认
        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {
            Model.users model = daluser.GetModel(WEBUserCurrent.UserID);
            BLL.users bll = new BLL.users();
            model.user_name = lblUsername.Text.Trim();
            if (!string.IsNullOrEmpty(txtpassword.Text))
            {
                model.password = DESEncrypt.Encrypt(txtpassword.Text);
            }

            model.email = txtemall.Value;
            model.nick_name = txtName.Value;
            if (fileUpImage.HasFile)
            {
                string extendName = fileUpImage.FileName.Substring(fileUpImage.FileName.LastIndexOf('.'));
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extendName;
                if (!System.IO.Directory.Exists(Server.MapPath("upload/user/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("upload/user/"));
                }

                fileUpImage.SaveAs(Server.MapPath("upload/user/" + filename));
                model.avatar = filename;
            }
            else
            {
                model.avatar = ViewState["Image"].ToString();
            }
            model.sex = rblSex.SelectedValue;
            DateTime _birthday;
            //if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            //{
            //    model.birthday = _birthday;
            //}
            model.telphone = txtphone.Value.Trim();
            model.mobile = txtMobile.Value.Trim();
            model.qq = txtQQ.Value;
            model.address = txtAddress.Value.Trim();
            model.amount = 0;
            model.point = 0;
            model.exp = 0;
            //model.reg_time = DateTime.Now;
            model.reg_ip = DTRequest.GetIP();
            model.safe_question = txtsafe_question.Value;
            model.safe_answer = txtsafe_answer.Value;
            if (bll.Update(model) == false)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('網路異常，請重試!')</script>");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改資料成功!')</script>");
            }
        }
    }
}