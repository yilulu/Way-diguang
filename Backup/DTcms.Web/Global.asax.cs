using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Data;
using DTcms.Common;

namespace DTcms.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //在新会话启动时运行的代码

            #region 构建购物车内存表
            //构建购物车内存表
            DataTable dtShopCart = new DataTable();

            //添加产品编号字段
            DataColumn keyCol = new DataColumn("GoodsId", typeof(Int32));
            dtShopCart.Columns.Add(keyCol);

            //添加产品内容字段
            dtShopCart.Columns.Add(new DataColumn("GoodsName", typeof(string)));
            dtShopCart.Columns.Add(new DataColumn("GoodsImage", typeof(string)));
            dtShopCart.Columns.Add(new DataColumn("GoodsPrice", typeof(decimal)));
            dtShopCart.Columns.Add(new DataColumn("GoodsCount", typeof(int)));
            dtShopCart.Columns.Add(new DataColumn("GoodsTotal", typeof(decimal)));
            dtShopCart.Columns.Add(new DataColumn("GoodsActive", typeof(int)));

            //将产品编号设置为主键
            DataColumn[] primKey = { keyCol };
            dtShopCart.PrimaryKey = primKey;

            //将数据表结构写入Session
            Session["DGCart"] = dtShopCart;
            #endregion

            #region 规定时间未付款，降级为普通会员
            DateTime dtNow = DateTime.Now;

            BLL.users bllUser = new BLL.users();
            DataTable dtUserList = bllUser.GetList(1000000, "  group_id<>5 and is_lock=0", " reg_time desc").Tables[0];
            if (dtUserList != null)
            {
                for (int i = 0; i < dtUserList.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtUserList.Rows[i]["reg_time"].ToString()))
                    {
                        DateTime UserRegTime = DateTime.Parse(dtUserList.Rows[i]["reg_time"].ToString()).AddYears(2);
                        DateTime SetEmailUserRegTime = DateTime.Parse(dtUserList.Rows[i]["reg_time"].ToString()).AddDays(699);
                        int isFee = int.Parse(dtUserList.Rows[i]["isFee"].ToString());
                        int UserID = int.Parse(dtUserList.Rows[i]["ID"].ToString());
                        //int length = ((TimeSpan)(dtNow - UserRegTime)).Days;
                        string GroupID = dtUserList.Rows[i]["group_id"].ToString();
                        string GroupName = "普通會員";
                        switch (GroupID)
                        {
                            case "1":
                                GroupName = "普通會員";
                                break;
                            case "2":
                                GroupName = "尊榮卡";
                                break;
                            case "3":
                                GroupName = "柏金卡";
                                break;
                            case "4":
                                GroupName = "御皇卡";
                                break;
                        }
                        if (isFee == 0 && dtNow == UserRegTime)
                        {
                            bllUser.UpUserSetCommon(UserID);
                            int bk = bllUser.UpFee(UserID, 0);
                        }
                        else if (isFee == 0 && dtNow == SetEmailUserRegTime)
                        {
                            string UserName = dtUserList.Rows[i]["user_name"].ToString();
                            string email = dtUserList.Rows[i]["email"].ToString();
                            string Contnet = "親愛的" + UserName + ",你的" + GroupName + "會員將在" + DateTime.Now.AddDays(30).ToString() + "到期" + ",請您儘快續費";
                            setEmail(email, "");
                        }
                    }
                }
            }
            #endregion

        }
        #region 發送郵件
        private void setEmail(string UserEmail, string Content)
        {
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            DTMail.sendMail(model.emailstmp, model.emailfrom, model.emailpassword, model.emailusername, model.emailfrom, UserEmail, "帝光房屋留言回覆", Content);
        }
        #endregion

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}