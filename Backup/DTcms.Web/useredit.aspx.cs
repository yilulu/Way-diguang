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
    public partial class useredit : System.Web.UI.Page
    {
        DTcms.DAL.users daluser = new DTcms.DAL.users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            int id = WEBUserCurrent.UserID;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();
            model = bll.GetModel(id);
            if (model != null)
            {
                txtusername.Text = model.user_name;
                txtpassword.Value = DESEncrypt.Decrypt(model.password);
                txtpassword2.Value = DESEncrypt.Decrypt(model.password);
                txtemall.Value = model.email;
                txtName.Value = model.nick_name;

                ViewState["file"] = model.avatar;
                //model.sex = rblSex.SelectedValue;
                //DateTime _birthday;
                //if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
                //{
                //    model.birthday = _birthday;
                //}
                txtphone.Value = model.telphone;
                //model.qq = "";
                txtAddress.Value = model.address;



                if (model.group_id == 1)
                {
                    ddlGroup.SelectedValue = "0";
                    divsj.Attributes.Add("style", "display:none");
                }
                else
                {
                    divsj.Attributes.Add("style", "display:");
                    ddlGroup.SelectedValue = "1";
                    model.group_id = 5;
                    //dianming.Value = model.dianming;
                    //dianmiaoshu.Value = model.dianmiaoshu;
                    //congye.Value = model.congye;
                    gongsi.Value = model.gongsi;
                    //fuwuquyu.Value = model.fuwuquyu;
                    //shuxishequ.Value = model.shuxishequ;
                    //fuwutechang.Value = model.fuwutechang;
                    //jingli.Value = model.jingli;
                    //zhengshu.Value = model.zhengshu;
                    note.Value = model.note;
                }
                ddlGroup.Enabled = false;
            }
        }

        //確認註册
        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {

            bool result = true;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();
            model.id = WEBUserCurrent.UserID;
            //model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.user_name = txtusername.Text.Trim();
            model.password = DESEncrypt.Encrypt(txtpassword.Value);
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
                model.avatar = ViewState["file"] == null ? "" : ViewState["file"].ToString();
            }
            //DateTime _birthday;
            //if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            //{
            //    model.birthday = _birthday;
            //}
            model.telphone = txtphone.Value.Trim();
            //model.qq = "";
            model.address = txtAddress.Value.Trim();
            model.amount = 0;
            model.point = 0;
            model.exp = 0;
            model.reg_time = DateTime.Now;
            model.reg_ip = DTRequest.GetIP();

            if (ddlGroup.SelectedValue == "0")
            {
                model.group_id = 1;
            }
            else
            {
                model.group_id = 5;
                // model.dianming = dianming.Value;
                //model.dianmiaoshu = dianmiaoshu.Value;
                //model.congye = congye.Value;
                //model.gongsi = gongsi.Value;
                //model.fuwuquyu = fuwuquyu.Value;
                //model.shuxishequ = shuxishequ.Value;
                //model.fuwutechang = fuwutechang.Value;
                //model.jingli = jingli.Value;
                //model.zhengshu = zhengshu.Value;
                model.note = note.Value;
            }
            if (bll.Update(model))
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "<script>alert('網路異常，請重試')</script>", "");
            }
            else
            {
                if (model.group_id == 5)
                {
                    Response.Redirect("userSJ.aspx");
                }
                else
                {
                    Response.Redirect("user.aspx");
                }
            }
        }
    }
}