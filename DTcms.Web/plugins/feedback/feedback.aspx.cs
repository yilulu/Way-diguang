using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;
using DTcms.Common;

namespace DTcms.Web.plugins.feedback.admin
{
    public partial class feedback : Web.UI.ManagePage
    {
        BLL.dt_feedback bllNot = new BLL.dt_feedback();
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
            {
                if (!IsPostBack)
                {
                    LoadNoteBook(int.Parse(Request.QueryString["id"].ToString()));
                }
            }
        }

        private void LoadNoteBook(int ID)
        {
            AreaDal dal = new AreaDal();
            Model.Area modelArea = new Model.Area();
            Model.dt_feedback Model = bllNot.GetModel(ID);
            chkPointValue.Value = Model.user_Function;
            if (Model.user_Function == "是")
            {
                chkPoint.Checked = true;
            }
            else
            {
                chkPoint.Checked = false;
            }
            lblName.Text = Model.user_name;
            lblsex.Text = Model.user_qq;

            lblPhone.Text = Model.user_tel;
            lbluser_tel.Text = Model.title;
            lblEmail.Text = Model.user_email;
            lblContent.Text = Model.content;
            lblZhuZhi.Text = Model.zhuzhi;
            string DiDian = Model.user_Address;
            string Address = string.Empty;
            if (!string.IsNullOrEmpty(DiDian))
            {
                if (DiDian.IndexOf('|') > 0)
                {
                    string[] addList = DiDian.Split('|');
                    for (int i = 0; i < addList.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(addList[i].ToString()))
                        {
                            int AreaID = int.Parse(addList[i].ToString());
                            modelArea = dal.GetModel(AreaID);
                            if (modelArea != null)
                            {
                                if (i > 0)
                                {
                                    Address += "-";
                                }
                                Address += modelArea.title;
                            }
                        }
                    }
                }
            }
            lblAdress.Text = Address;
            lblClassName.Text = Model.user_Class;
            //lblFunction.Text = Model.user_Function;
            lblMoney.Text = Model.user_Money.ToString();
            lblMianJi.Text = Model.user_MianJi.ToString();

            if (Model.zhuzhi == "介紹房屋出售")
            {
                p1.Visible = true;
            }
            else
            {
                p1.Visible = false;
            }
            txtReContent.Text = Model.reply_content;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(chkPointValue.Value) || chkPointValue.Value == "否")
            {
                if (chkPoint.Checked == true)
                {
                    //model.point = point + 5000;
                    BLL.users BLLUser = new BLL.users();
                    if (!string.IsNullOrEmpty(lblEmail.Text))
                    {
                        if (BLLUser.ExistsEmail(lblEmail.Text))
                        {
                            int UID = BLLUser.GetIDByExistsEmail(lblEmail.Text);
                            BLLUser.UpPoint(UID, 5000);

                            BLL.point_log point = new BLL.point_log();
                            Model.point_log model = new Model.point_log();
                            model.user_id = UID;
                            model.user_name = "";
                            model.value = 5000;
                            model.remark = "介紹房屋出售";
                            model.add_time = DateTime.Now;
                            model.type = 1;//2標誌點數是減少
                            int m = point.Add(model);
                        }
                    }
                }
            }
            else if (chkPointValue.Value == "是")
            {
                if (chkPoint.Checked == false)
                {
                    BLL.users BLLUser = new BLL.users();
                    if (!string.IsNullOrEmpty(lblEmail.Text))
                    {
                        if (BLLUser.ExistsEmail(lblEmail.Text))
                        {
                            int UID = BLLUser.GetIDByExistsEmail(lblEmail.Text);
                            BLLUser.UpJianPoint(UID, 5000);

                            BLL.point_log point = new BLL.point_log();
                            Model.point_log model = new Model.point_log();
                            model.user_id = UID;
                            model.user_name = "";
                            model.value = 5000;
                            model.remark = "取消介紹房屋出售點數";
                            model.type = 2;//2標誌點數是減少
                            model.add_time = DateTime.Now;

                            int m = point.Add(model);
                        }
                    }
                }
            }

            string replayContent = txtReContent.Text.Trim();
            Model.dt_feedback feedback = new Model.dt_feedback();
            if (chkPoint.Checked == true)
            {
                feedback.user_Function = "是";
            }
            else
            {
                feedback.user_Function = "否";
            }

            feedback.reply_content = replayContent;
            feedback.reply_time = DateTime.Now;
            feedback.id = int.Parse(Request.QueryString["id"].ToString());
            if (bllNot.Update(feedback))
            {
                setEmail();
                Response.Redirect("admin/NoteBook.aspx");
                JscriptMsg("留言回覆成功！", "admin/NoteBook.aspx.aspx", "Success");
            }
        }

        #region 發送郵件
        private void setEmail()
        {
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            DTMail.sendMail(model.emailstmp, model.emailport, model.emailfrom, model.emailpassword, model.emailusername, model.emailfrom, lblEmail.Text, "帝光房屋留言回覆", txtReContent.Text);
        }
        #endregion
    }
}