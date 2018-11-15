using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class user_edit : Web.UI.ManagePage
    {
        protected string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改類型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("傳輸參數不正確！", "back", "Error");
                    return;
                }
                if (!new BLL.users().Exists(this.id))
                {
                    JscriptMsg("資料不存在或已被刪除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                TreeBind("is_lock=0"); //綁定類別
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }

            }
        }

        #region 綁定類別=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "grade asc,id asc").Tables[0];

            this.ddlGroupId.Items.Clear();
            this.ddlGroupId.Items.Add(new ListItem("請選擇組別...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(_id);
            txtPwd.Value = model.password;
            lblPwd.InnerText = "如果不修改密碼，請留空！";
            chkVip.Checked = false;
            if (model.isVip == 1)
            {
                chkVip.Checked = true;
            }
            llbInTroduce.Text = model.dianming;

            #region 处理点数 20140924
            txtSell.Value = model.isDonePoints.ToString();
            txtHire.Value = model.isHirePoints.ToString();
            txtAd.Value = model.isAd.ToString();
            txtMac.Value = model.isMac.ToString();

            txtSellHoursePoint.Text = model.isDonePoints.ToString();
            txtHireHoursePoint.Text = model.isHirePoints.ToString();
            txtHireAdPoint.Text = model.isAd.ToString();
            txtHireMacPoint.Text = model.isMac.ToString();
            ddlGroupId.SelectedValue = model.group_id.ToString();
            rblIsLock.SelectedValue = model.is_lock.ToString();
            #endregion

            txtUserName.Enabled = false;
            txtUserName.Text = model.user_name;
            hidUserName.Value = model.user_name;
            // txtPassword.Attributes["value"] = model.password;
            txtEmail.Text = model.email;
            txtNickName.Text = model.nick_name;
            txtAvatar.Text = model.avatar;
            rblSex.SelectedValue = model.sex;
            if (model.birthday != null)
            {
                txtBirthday.Text = model.birthday.GetValueOrDefault().ToString("yyyy-M-d");
            }
            txtTelphone.Text = model.telphone;
            txtMobile.Text = model.mobile;
            txtQQ.Text = model.qq;
            txtAddress.Text = model.address;
            txtAmount.Text = model.amount.ToString();
            txtPoint.Text = model.point.ToString();

            lblRegTime.Text = model.reg_time.ToString();
            lblRegIP.Text = model.reg_ip;
            //查找最近登錄資訊
            Model.user_login_log logModel = new BLL.user_login_log().GetLastModel(model.user_name);
            if (logModel != null)
            {
                lblLastTime.Text = logModel.login_time.ToString();
                lblLastIP.Text = logModel.login_ip;
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();

            model.group_id = Utils.StringToNum(ddlGroupId.SelectedValue);
            switch (model.group_id)
            {
                case 1:
                    model.point = 0;
                    break;
                case 2:
                    model.point = 50;
                    break;
                case 3:
                    model.point = 100;
                    break;
                case 4:
                    model.point = 150;
                    break;
            }
            model.isVip = 0;
            if (chkVip.Checked == true)
            {
                model.isVip = 1;
            }
            model.is_lock = Utils.StringToNum(rblIsLock.SelectedValue);
            model.user_name = txtUserName.Text.Trim();
            model.password = DESEncrypt.Encrypt(txtPassword.Text);
            model.email = txtEmail.Text;
            model.nick_name = txtNickName.Text;
            model.avatar = txtAvatar.Text;
            model.sex = rblSex.SelectedValue;
            DateTime _birthday;
            if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = txtTelphone.Text.Trim();
            model.mobile = txtMobile.Text.Trim();
            model.qq = txtQQ.Text;
            model.address = txtAddress.Text.Trim();
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            //model.exp = Utils.StringToNum(txtExp.Text.Trim());
            model.reg_time = DateTime.Now;
            model.reg_ip = DTRequest.GetIP();

            if (bll.Add(model) < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = true;
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();

            model.group_id = Utils.StringToNum(ddlGroupId.SelectedValue);
            int point = 0;
            switch (model.group_id)
            {
                case 1:
                    point = 0;
                    break;
                case 2:
                    point = 50;
                    break;
                case 3:
                    point = 100;
                    break;
                case 4:
                    point = 150;
                    break;
            }
            //model.point = point;
            model.isVip = 0;
            if (chkVip.Checked == true)
            {
                model.isVip = 1;
            }
            model.is_lock = Utils.StringToNum(rblIsLock.SelectedValue);
            if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim());
            }
            else
            {
                model.password = txtPwd.Value;
            }
            model.email = txtEmail.Text;
            model.dianming = llbInTroduce.Text;
            model.nick_name = txtNickName.Text;
            model.user_name = txtUserName.Text.Trim();
            model.avatar = txtAvatar.Text;
            model.sex = rblSex.SelectedValue;
            DateTime _birthday;
            if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = txtTelphone.Text.Trim();
            model.mobile = txtMobile.Text.Trim();
            model.qq = txtQQ.Text;
            model.address = txtAddress.Text.Trim();
            model.amount = decimal.Parse(txtAmount.Text.Trim());

            BLL.siteconfig bllConfig = new BLL.siteconfig();
            Model.siteconfig modelConfig = bllConfig.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));

            #region 介紹售屋物件
            if (modelConfig.weburl == DESEncrypt.Encrypt(txtManagePwd.Text))
            {
                if (!string.IsNullOrEmpty(txtSellHoursePoint.Text))
                {
                    BLL.users BLLUser = new BLL.users();
                    if (txtSell.Value == "0")
                    {
                        point = Utils.StringToNum(txtSellHoursePoint.Text) * 5000;
                        BLLUser.UpPoint(id, point);
                        BLL.point_log points = new BLL.point_log();
                        Model.point_log modelP = new Model.point_log();
                        modelP.user_id = _id;
                        modelP.user_name = txtNickName.Text;
                        modelP.value = point;
                        modelP.remark = "介紹售屋物件回饋點數";
                        modelP.add_time = DateTime.Now;
                        modelP.type = 1;//2會員介紹物件已成交
                        int m = points.Add(modelP);

                    }
                    else
                    {
                        int Nums = System.Math.Abs(Utils.StringToNum(txtSellHoursePoint.Text) - Utils.StringToNum(txtSell.Value));
                        if (Nums != 0)
                        {
                            int jianPoint = Nums * 5000;
                            BLL.point_log points = new BLL.point_log();
                            Model.point_log modelP = new Model.point_log();
                            modelP.user_id = _id;
                            modelP.user_name = txtNickName.Text;
                            modelP.value = jianPoint;
                            modelP.add_time = DateTime.Now;
                            if ((Utils.StringToNum(txtSellHoursePoint.Text) - Utils.StringToNum(txtSell.Value)) < 0)
                            {
                                modelP.remark = "減去介紹售屋物件回饋點數";
                                modelP.type = 2;//2會員介紹物件已成交
                                BLLUser.UpJianPoint(id, jianPoint);
                            }
                            else
                            {
                                modelP.remark = "介紹售屋物件回饋點數";
                                modelP.type = 1;//2會員介紹物件已成交
                                BLLUser.UpPoint(id, jianPoint);
                            }
                            int m = points.Add(modelP);

                        }
                    }
                    model.isDonePoints = Utils.StringToNum(txtSellHoursePoint.Text);
                }
            }
            else
            {
                model.isDonePoints = Utils.StringToNum(txtSell.Value);
            }
            #endregion

            #region 介紹租屋物件
            if (modelConfig.weburl == DESEncrypt.Encrypt(TextBox2.Text))
            {
                if (!string.IsNullOrEmpty(txtHireHoursePoint.Text))
                {
                    BLL.users BLLUser = new BLL.users();
                    if (txtHire.Value == "0")
                    {
                        point = Utils.StringToNum(txtHireHoursePoint.Text) * 200;
                        BLLUser.UpPoint(id, point);
                        BLL.point_log points = new BLL.point_log();
                        Model.point_log modelP = new Model.point_log();
                        modelP.user_id = _id;
                        modelP.user_name = txtNickName.Text;
                        modelP.value = point;
                        modelP.remark = "介紹租屋物件回饋點數";
                        modelP.add_time = DateTime.Now;
                        modelP.type = 1;//2會員介紹物件已成交
                        int m = points.Add(modelP);

                    }
                    else
                    {
                        int Nums = System.Math.Abs(Utils.StringToNum(txtHireHoursePoint.Text) - Utils.StringToNum(txtHire.Value));
                        if (Nums != 0)
                        {
                            int jianPoint = Nums * 200;
                            BLL.point_log points = new BLL.point_log();
                            Model.point_log modelP = new Model.point_log();
                            modelP.user_id = _id;
                            modelP.user_name = txtNickName.Text;
                            modelP.value = jianPoint;
                            modelP.add_time = DateTime.Now;
                            if ((Utils.StringToNum(txtHireHoursePoint.Text) - Utils.StringToNum(txtHire.Value)) < 0)
                            {
                                modelP.remark = "減去介紹租屋物件回饋點數";
                                modelP.type = 2;//2會員介紹物件已成交
                                BLLUser.UpJianPoint(id, jianPoint);
                            }
                            else
                            {
                                modelP.remark = "介紹租屋物件回饋點數";
                                modelP.type = 1;//2會員介紹物件已成交
                                BLLUser.UpPoint(id, jianPoint);
                            }
                            int m = points.Add(modelP);
                        }
                    }
                    model.isHirePoints = Utils.StringToNum(txtHireHoursePoint.Text);
                }
            }
            else
            {
                model.isHirePoints = Utils.StringToNum(txtHire.Value);
            }
            #endregion

            #region 介紹租賃廣告
            if (modelConfig.weburl == DESEncrypt.Encrypt(TextBox4.Text))
            {
                if (!string.IsNullOrEmpty(txtHireAdPoint.Text))
                {
                    BLL.users BLLUser = new BLL.users();
                    if (txtAd.Value == "0")
                    {
                        point = Utils.StringToNum(txtHireAdPoint.Text) * 300;
                        BLLUser.UpPoint(id, point);
                        BLL.point_log points = new BLL.point_log();
                        Model.point_log modelP = new Model.point_log();
                        modelP.user_id = _id;
                        modelP.user_name = txtNickName.Text;
                        modelP.value = point;
                        modelP.remark = "介紹租賃廣告回饋點數";
                        modelP.add_time = DateTime.Now;
                        modelP.type = 1;//2會員介紹物件已成交
                        int m = points.Add(modelP);

                    }
                    else
                    {
                        int Nums = System.Math.Abs(Utils.StringToNum(txtHireAdPoint.Text) - Utils.StringToNum(txtAd.Value));
                        if (Nums != 0)
                        {
                            int jianPoint = Nums * 300;

                            BLL.point_log points = new BLL.point_log();
                            Model.point_log modelP = new Model.point_log();
                            modelP.user_id = _id;
                            modelP.user_name = txtNickName.Text;
                            modelP.value = jianPoint;
                            modelP.add_time = DateTime.Now;
                            if ((Utils.StringToNum(txtHireAdPoint.Text) - Utils.StringToNum(txtAd.Value)) < 0)
                            {
                                modelP.type = 2;//2會員介紹物件已成交
                                modelP.remark = "減去介紹租賃廣告回饋點數";
                                BLLUser.UpJianPoint(id, jianPoint);
                            }
                            else
                            {
                                modelP.type = 1;//2會員介紹物件已成交
                                modelP.remark = "介紹租賃廣告回饋點數";
                                BLLUser.UpPoint(id, jianPoint);
                            }
                            int m = points.Add(modelP);
                        }
                    }
                    model.isAd = Utils.StringToNum(txtHireAdPoint.Text);
                }
            }
            else
            {
                model.isAd = Utils.StringToNum(txtAd.Value);
            }
            #endregion

            #region 介紹精品廠商
            if (modelConfig.weburl == DESEncrypt.Encrypt(TextBox6.Text))
            {
                if (!string.IsNullOrEmpty(txtHireMacPoint.Text))
                {
                    BLL.users BLLUser = new BLL.users();
                    if (txtMac.Value == "0")
                    {
                        point = Utils.StringToNum(txtHireMacPoint.Text) * 800;
                        BLLUser.UpPoint(id, point);
                        BLL.point_log points = new BLL.point_log();
                        Model.point_log modelP = new Model.point_log();
                        modelP.user_id = _id;
                        modelP.user_name = txtNickName.Text;
                        modelP.value = point;
                        modelP.remark = "介紹精品廠商回饋點數";
                        modelP.add_time = DateTime.Now;
                        modelP.type = 1;//2會員介紹物件已成交
                        int m = points.Add(modelP);

                    }
                    else
                    {
                        int Nums = System.Math.Abs(Utils.StringToNum(txtHireMacPoint.Text) - Utils.StringToNum(txtMac.Value));
                        if (Nums != 0)
                        {
                            int jianPoint = Nums * 800;

                            BLL.point_log points = new BLL.point_log();
                            Model.point_log modelP = new Model.point_log();
                            modelP.user_id = _id;
                            modelP.user_name = txtNickName.Text;
                            modelP.value = jianPoint;
                            modelP.add_time = DateTime.Now;
                            if ((Utils.StringToNum(txtHireMacPoint.Text) - Utils.StringToNum(txtMac.Value)) < 0)
                            {
                                modelP.remark = "減去介紹精品廠商回饋點數";
                                modelP.type = 2;//2會員介紹物件已成交
                                BLLUser.UpJianPoint(id, jianPoint);
                            }
                            else
                            {
                                modelP.remark = "介紹精品廠商回饋點數";
                                modelP.type = 1;//2會員介紹物件已成交
                                BLLUser.UpPoint(id, jianPoint);
                            }

                            int m = points.Add(modelP);
                        }
                    }
                    model.isMac = Utils.StringToNum(txtHireMacPoint.Text);
                }
            }
            else
            {
                model.isMac = Utils.StringToNum(txtMac.Value);
            }
            #endregion


            //model.exp = Utils.StringToNum(txtExp.Text.Trim());
            model.id = _id;
            if (!bll.Update(model))
            {
                result = false;
            }
            return result;
        }
        #endregion

        //儲存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("users", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改用戶成功！", "user_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("users", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (new BLL.users().Exists(txtUserName.Text.Trim()))
                {
                    JscriptMsg("用戶名已存在！", "", "Error");
                    return;
                }
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加用戶成功！", "user_list.aspx", "Success");
            }
        }

    }
}
