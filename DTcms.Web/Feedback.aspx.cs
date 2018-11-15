using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;

namespace DTcms.Web
{
    public partial class Feedback : System.Web.UI.Page
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
            lblReplayContent.Text = Model.reply_content;
        }

       
    }
}