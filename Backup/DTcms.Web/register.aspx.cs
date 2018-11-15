using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Model;
using DTcms.Common;
using System.Data;

namespace DTcms.Web
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        DTcms.DAL.users daluser = new DTcms.DAL.users();
        int type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            type = DTRequest.GetQueryInt("type");
            if (!IsPostBack)
            {
                TreeBind("is_lock=0"); //綁定類別
            }

            ddlGroup.SelectedValue = type.ToString();

            note.Value = "帝光會員優惠:";
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

        #region 確認註冊=================================
        protected void btnlogin_Click(object sender, ImageClickEventArgs e)
        {

            bool result = true;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();

            //model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.user_name = txtusername.Value.Trim();
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
            //model.sex = rblSex.SelectedValue;
            //DateTime _birthday;
            //if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            //{
            //    model.birthday = _birthday;
            //}
            model.mobile = txtphone.Value.Trim();
            //model.qq = "";
            model.address = txtAddress.Value.Trim();
            model.amount = 0;
            model.point = 0;
            switch (type)
            {
                case 1:
                    model.amount = 0;
                    break;
                case 2:
                    model.amount = 100;
                    break;
                case 3:
                    model.amount = 200;
                    break;
                case 4:
                    model.amount = 300;
                    break;
            }

            model.exp = 0;
            model.reg_time = DateTime.Now;
            model.reg_ip = DTRequest.GetIP();

            //if (ddlGroup.SelectedValue == "0")
            //{
            //    model.group_id = 1;
            //}
            //else
            //{
            model.group_id = Utils.StringToNum(ddlGroup.SelectedValue);
            model.dianming = txtIntroduce.Value;
            //model.dianmiaoshu = dianmiaoshu.Value;
            //model.congye = congye.Value;
            model.gongsi = gongsi.Value;
            model.fuwuquyu = CompanyName.Value;
            //model.fuwuquyu = fuwuquyu.Value;
            //model.shuxishequ = shuxishequ.Value;
            //model.fuwutechang = fuwutechang.Value;
            //model.jingli = jingli.Value;
            //model.zhengshu = zhengshu.Value;
            model.note = note.Value;
            model.is_lock = 1;
            //}
            if (bll.Add(model) < 1)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('網路異常，請重試')</script>");
            }
            else
            {
                login();
                //this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功,請登入');window.location.href='login.aspx'</script>");
            }

        }
        #endregion

        #region 登錄=====================================
        private void login()
        {
            BLL.users bll = new BLL.users();
            var model = bll.GetModel(txtusername.Value.Trim(), DESEncrypt.Encrypt(txtpassword.Value.Trim()), 0);
            if (model != null)
            {
                HttpCookie ccookie1 = new HttpCookie("WEBUSERID", model.id.ToString());
                HttpCookie ccookie2 = new HttpCookie("WEBUserNamecook", model.user_name.ToString());
                HttpCookie ccookie3 = new HttpCookie("WEBRealNamecook", model.nick_name.ToString());
                HttpCookie ccookie4 = new HttpCookie("WEBUserTypecook", model.group_id.ToString());
                Response.Cookies.Add(ccookie1);
                Response.Cookies.Add(ccookie2);
                Response.Cookies.Add(ccookie3);
                Response.Cookies.Add(ccookie4);
                if (model.group_id == 5)
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='userSJ.aspx'</script>");

                }
                else
                {
                    if (ddlGroup.SelectedValue == "1")
                    {
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('註冊成功');window.location.href='userinfo.aspx'</script>");
                    }
                    else
                    {
                        BLL.user_groups bllusergroup = new BLL.user_groups();
                        string typeName = string.Empty;
                        if (!string.IsNullOrEmpty(ddlGroup.SelectedValue))
                        {
                            typeName = bllusergroup.GetTitle(Utils.StringToNum(ddlGroup.SelectedValue));
                        }

                        string txt = "恭喜您註冊成功，您註冊的是" + typeName + "會員，若未交費，則您目前的會員級別仍為普通會員";
                        if (ddlGroup.SelectedValue == "1")
                        {
                            txt = "恭喜您註冊成功";
                        }
                        this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + txt + "');window.location.href='RegPay.aspx?paymenttype=" + ddlzhifu.SelectedValue + "'</script>");
                    }

                }
            }
        }
        #endregion

    }
}
