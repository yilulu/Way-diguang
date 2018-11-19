using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Data;
using DTcms.Common;
using System.IO;

namespace DTcms.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            #region 记录网站访问
            BLL.ipAccess bll_ipAccess = new BLL.ipAccess();
            Model.ipAccess entity = new Model.ipAccess();
            entity.iP_Address = DTRequest.GetIP();
            entity.iP_DateTime = DateTime.Now;
            if (!bll_ipAccess.Exists(entity.iP_Address))
            {
                bll_ipAccess.Add(entity);
            }
            #endregion
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //在新会话启动时运行的代码
            BLL.article bll = new BLL.article();
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
            //DateTime dtNow = DateTime.Now;

            //BLL.users bllUser = new BLL.users();
            //DataTable dtUserList = bllUser.GetList(1000000, "  group_id<>5 and is_lock=0", " reg_time desc").Tables[0];
            //if (dtUserList != null)
            //{
            //    for (int i = 0; i < dtUserList.Rows.Count; i++)
            //    {
            //        if (!string.IsNullOrEmpty(dtUserList.Rows[i]["reg_time"].ToString()))
            //        {
            //            DateTime UserRegTime = DateTime.Parse(dtUserList.Rows[i]["reg_time"].ToString()).AddYears(2);
            //            DateTime SetEmailUserRegTime = DateTime.Parse(dtUserList.Rows[i]["reg_time"].ToString()).AddDays(699);
            //            int isFee = int.Parse(dtUserList.Rows[i]["isFee"].ToString());
            //            int UserID = int.Parse(dtUserList.Rows[i]["ID"].ToString());
            //            //int length = ((TimeSpan)(dtNow - UserRegTime)).Days;
            //            string GroupID = dtUserList.Rows[i]["group_id"].ToString();
            //            string GroupName = "普通會員";
            //            switch (GroupID)
            //            {
            //                case "1":
            //                    GroupName = "普通會員";
            //                    break;
            //                case "2":
            //                    GroupName = "尊榮卡";
            //                    break;
            //                case "3":
            //                    GroupName = "柏金卡";
            //                    break;
            //                case "4":
            //                    GroupName = "御皇卡";
            //                    break;
            //            }
            //            if (isFee == 0 && dtNow == UserRegTime)
            //            {
            //                bllUser.UpUserSetCommon(UserID);
            //                int bk = bllUser.UpFee(UserID, 0);
            //            }
            //            else if (isFee == 0 && dtNow == SetEmailUserRegTime)
            //            {
            //                string UserName = dtUserList.Rows[i]["user_name"].ToString();
            //                string email = dtUserList.Rows[i]["email"].ToString();
            //                string Contnet = "親愛的" + UserName + ",你的" + GroupName + "會員將在" + DateTime.Now.AddDays(30).ToString() + "到期" + ",請您儘快續費";
            //                setEmail(email, "");
            //            }
            //        }
            //    }
            //}
            #endregion

            #region  前台会员刊登期限设定
            DataTable dt = bll.GetList(" and isFront=1", " order by sort_id asc");
            string IDlist = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime dtAddTime = Utils.StrToDateTime(dt.Rows[i]["add_time"].ToString(), DateTime.Now);
                    int Months = 2;
                    if (Utils.StrToInt(dt.Rows[i]["group_id"].ToString(), 0) == 10)
                    {
                        Months = 6;
                    }
                    if (dtAddTime.AddMonths(Months) == DateTime.Now)
                    {
                        int ID = Utils.StrToInt(dt.Rows[i]["id"].ToString(), 0);
                        IDlist += ID + ",";
                    }
                }
                IDlist = Utils.DelLastChar(IDlist, ",");// (IDlist + "end").Replace(",end", "");
                if (!string.IsNullOrEmpty(IDlist))
                {
                    bll.UpdateList(IDlist);
                }
            }
            #endregion

           

        }
        #region 發送郵件
        private void setEmail(string UserEmail, string Content)
        {
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            DTMail.sendMail(model.emailstmp, model.emailport, model.emailfrom, model.emailpassword, model.emailusername, model.emailfrom, UserEmail, "帝光房屋留言回覆", Content);
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
            //在出现未处理的错误时运行的代码
            try
            {
                //获取到HttpUnhandledException异常，这个异常包含一个实际出现的异常
                Exception ex = Server.GetLastError();

                string errorMsg = String.Empty;
                string particular = String.Empty;
                string applictionUser = String.Empty;

                try
                {
                    errorMsg = ex.InnerException.Message;
                    particular = ex.InnerException.StackTrace;

                }
                catch
                {
                    errorMsg = ex.Message;
                    particular = ex.StackTrace;
                }

                try
                {
                    applictionUser = "eshop";
                }
                catch
                {
                    applictionUser = "未登陆或未知用户";
                }

                if (string.IsNullOrEmpty(errorMsg))
                {
                    errorMsg = ex.Message;
                    particular = ex.StackTrace;
                }
                //保存错误日志 AppDomain.CurrentDomain.BaseDirectory

                string str = AppDomain.CurrentDomain.BaseDirectory;
                str = str.Replace("\\", "/");
                str += "Logs/" + System.DateTime.Now.Year + "/" + System.DateTime.Now.Month.ToString("00");

                if (!System.IO.File.Exists(str))
                {
                    System.IO.Directory.CreateDirectory(str);
                }

                System.IO.StreamWriter sw = System.IO.File.AppendText(str + "/" + System.DateTime.Now.Year + "-" + System.DateTime.Now.Month.ToString("00") + "-" + System.DateTime.Now.Day.ToString("00") + ".log");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine(applictionUser + "(IP:" + DTcms.Common.DTRequest.GetIP() + ")" + "\t" + System.DateTime.Now + "\t" + errorMsg);
                sw.WriteLine(particular);
                sw.Flush();
                sw.Close();
                //string rdtUrl = "http://" + Request.Url.Host;
                //if (Request.Url.Port != 80)
                //{
                //    rdtUrl += ":" + Request.Url.Port;
                //}
                //rdtUrl += "/500.htm";
                //HttpContext.Current.Response.Redirect(rdtUrl);
                string sysError = "很抱歉！当前页面不存在或发生错误";
                if (!string.IsNullOrEmpty(sysError))
                {
                    if (sysError.IndexOf("<![CDATA[") > -1)
                    {
                        sysError = sysError.Replace("<![CDATA[", "");
                    }
                    if (sysError.IndexOf("]]>") > -1)
                    {
                        sysError = sysError.Replace("]]>", "");
                    }
                }
                HttpContext.Current.Response.Write(sysError);
            }
            catch { }

            Server.ClearError();//处理完及时清理异常
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            /**********总访问量****************/
            string serverFile = Server.MapPath("~/xmlconfig/SystemVisitCount.config");
            int intStat = 0;
            intStat = (int)Application["counter"];
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            xmldoc.Load(serverFile);
            xmldoc.SelectSingleNode("Condition/Count").InnerText = intStat.ToString();
            xmldoc.Save(serverFile);
            /*********************************/

            /**********今日访问量****************/
            int Stat = 0;
            Stat = (int)Application["dayCounter"];
            string day0 = (string)Application["day"];         //保存日期
            string str = Stat.ToString() + "," + day0.ToString();
            string serverFileDay = Server.MapPath("~/xmlconfig/SystemVisitCount.config");
            System.Xml.XmlDocument xmldocDay = new System.Xml.XmlDocument();
            xmldocDay.Load(serverFileDay);
            xmldocDay.SelectSingleNode("Condition/DayCount").InnerText = str;
            xmldocDay.Save(serverFileDay);
            /*********************************/
        }
    }
}