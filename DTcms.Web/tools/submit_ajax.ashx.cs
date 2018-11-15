using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;
using LitJson;

namespace DTcms.Web.tools
{
    /// <summary>
    /// AJAX提交處理
    /// </summary>
    public class submit_ajax : IHttpHandler, IRequiresSessionState
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
        Model.userconfig userConfig = new BLL.userconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_USER_XML_CONFING));
        public void ProcessRequest(HttpContext context)
        {
            //取得處事類型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "digg_add": //頂踩
                    digg_add(context);
                    break;
                case "comment_add": //提交評論
                    comment_add(context);
                    break;
                case "comment_list": //評論列表
                    comment_list(context);
                    break;
                case "validate_username": //驗證用戶名
                    validate_username(context);
                    break;
                case "user_register": //用戶註冊
                    user_register(context);
                    break;
                case "user_invite_code": //申請邀請碼
                    user_invite_code(context);
                    break;
                case "user_verify_email": //發送註冊驗證郵件
                    user_verify_email(context);
                    break;
                case "user_login": //用戶登錄
                    user_login(context);
                    break;
                case "user_oauth_bind": //綁定協力廠商登錄帳戶
                    user_oauth_bind(context);
                    break;
                case "user_oauth_register": //註冊協力廠商登錄帳戶
                    user_oauth_register(context);
                    break;
                case "user_info_edit": //修改使用者資訊
                    user_info_edit(context);
                    break;
                case "user_avatar_crop": //確認裁剪用戶頭像
                    user_avatar_crop(context);
                    break;
                case "user_password_edit": //修改密碼
                    user_password_edit(context);
                    break;
                case "user_getpassword": //郵箱取回密碼
                    user_getpassword(context);
                    break;
                case "user_repassword": //郵箱重設密碼
                    user_repassword(context);
                    break;
                case "user_message_delete": //刪除短資訊
                    user_message_delete(context);
                    break;
                case "user_message_add": //發佈短資訊
                    user_message_add(context);
                    break;
                case "user_point_convert": //用戶兌換積分
                    user_point_convert(context);
                    break;
                case "user_point_delete": //刪除用戶積分明細
                    user_point_delete(context);
                    break;
                case "user_amount_recharge": //用戶線上充值
                    user_amount_recharge(context);
                    break;
                case "user_amount_delete": //刪除用戶收支明細
                    user_amount_delete(context);
                    break;
                case "cart_goods_add": //購物車加入商品
                    cart_goods_add(context);
                    break;
                case "cart_goods_update": //購物車修改商品
                    cart_goods_update(context);
                    break;
                case "cart_goods_delete": //購物車刪除商品
                    cart_goods_delete(context);
                    break;
                case "order_save": //保存訂單
                    order_save(context);
                    break;
                case "order_cancel": //使用者取消訂單
                    order_cancel(context);
                    break;

            }
        }

        #region 用户在刊登前提示用户是否已经刊登过商品，如果有则提示用户第2次刊登将收费

        #endregion

        #region 頂和踩的處理方法OK==============================
        private void digg_add(HttpContext context)
        {
            string digg_type = DTRequest.GetFormString("digg_type");
            int article_id = DTRequest.GetFormInt("article_id");
            //檢查是否重複點擊
            if (article_id > 0)
            {
                string cookie = Utils.GetCookie(DTKeys.COOKIE_DIGG_KEY, "diggs_" + article_id.ToString());
                if (cookie == article_id.ToString())
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"您剛剛提交過，休息一會吧！\"}");
                    return;
                }
            }
            BLL.article_diggs bll = new BLL.article_diggs();
            if (!bll.Exists(article_id))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"資訊不存在或已被刪除！\"}");
                return;
            }
            //自動+1
            if (digg_type == "good")
            {
                bll.UpdateField(article_id, "digg_good=digg_good+1");
            }
            else
            {
                bll.UpdateField(article_id, "digg_bad=digg_bad+1");
            }
            //返回成功
            Model.article_diggs model = bll.GetModel(article_id);
            context.Response.Write("{\"msg\":1, \"digggood\":" + model.digg_good + ", \"diggbad\":" + model.digg_bad + ", \"msgbox\":\"成功頂或踩了一下！\"}");
            Utils.WriteCookie(DTKeys.COOKIE_DIGG_KEY, "diggs_" + article_id.ToString(), article_id.ToString(), 8640);
            return;

        }
        #endregion

        #region 提交評論的處理方法OK============================
        private void comment_add(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.article_comment bll = new BLL.article_comment();
            Model.article_comment model = new Model.article_comment();

            string code = DTRequest.GetFormString("txtCode");
            int article_id = DTRequest.GetQueryInt("article_id");
            string _content = DTRequest.GetFormString("txtContent");
            //校檢驗證碼
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            if (article_id == 0)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"對不起，參數傳輸有誤！\"}");
                return;
            }
            if (string.IsNullOrEmpty(_content))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"對不起，請輸入評論內容！\"}");
                return;
            }
            //檢查用戶是否登錄
            int user_id = 0;
            string user_name = "匿名用戶";
            Model.users userModel = new Web.UI.BasePage().GetUserInfo();
            if (userModel != null)
            {
                user_id = userModel.id;
                user_name = userModel.user_name;
            }
            model.article_id = article_id;
            model.content = Utils.ToHtml(_content);
            model.user_id = user_id;
            model.user_name = user_name;
            model.user_ip = DTRequest.GetIP();
            model.is_lock = siteConfig.commentstatus; //審核開關
            model.add_time = DateTime.Now;
            model.is_reply = 0;
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"msg\": 1, \"msgbox\": \"恭喜您，留言提交成功啦！\"}");
                return;
            }
            context.Response.Write("{\"msg\": 0, \"msgbox\": \"對不起，保存過程中發生錯誤啦！\"}");
            return;
        }
        #endregion

        #region 取得評論列表方法OK==============================
        private void comment_list(HttpContext context)
        {
            int article_id = DTRequest.GetQueryInt("article_id");
            int page_index = DTRequest.GetQueryInt("page_index");
            int page_size = DTRequest.GetQueryInt("page_size");
            int totalcount;
            StringBuilder strTxt = new StringBuilder();

            if (article_id == 0 || page_size == 0)
            {
                context.Response.Write("獲取失敗，傳輸參數有誤！");
                return;
            }

            BLL.article_comment bll = new BLL.article_comment();
            DataSet ds = bll.GetList(page_size, page_index, string.Format("is_lock=0 and article_id={0}", article_id.ToString()), "add_time asc", out totalcount);
            //如果記錄存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                strTxt.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    //strTxt.Append("<li>\n");
                    //strTxt.Append("<div class=\"title\"><span>" + dr["add_time"] + "</span>" + dr["user_name"] + "</div>");
                    //strTxt.Append("<div class=\"box\">" + dr["content"] + "</div>");
                    //if (Convert.ToInt32(dr["is_reply"]) == 1)
                    //{
                    //    strTxt.Append("<div class=\"reply\">");
                    //    strTxt.Append("<strong>管理員回復：</strong>" + dr["reply_content"].ToString());
                    //    strTxt.Append("<span class=\"time\">" + dr["reply_time"].ToString() + "</span>");
                    //    strTxt.Append("</div>");
                    //}
                    //strTxt.Append("</li>\n");

                    strTxt.Append("{");
                    strTxt.Append("\"user_id\":" + dr["user_id"]);
                    strTxt.Append(",\"user_name\":\"" + dr["user_name"] + "\"");
                    if (Convert.ToInt32(dr["user_id"]) > 0)
                    {
                        Model.users userModel = new BLL.users().GetModel(Convert.ToInt32(dr["user_id"]));
                        if (userModel != null)
                        {
                            strTxt.Append(",\"avatar\":\"" + userModel.avatar + "\"");
                        }
                    }
                    strTxt.Append("");
                    strTxt.Append(",\"content\":\"" + Microsoft.JScript.GlobalObject.escape(dr["content"]) + "\"");
                    strTxt.Append(",\"add_time\":\"" + dr["add_time"] + "\"");
                    strTxt.Append(",\"is_reply\":" + dr["is_reply"]);
                    if (Convert.ToInt32(dr["is_reply"]) == 1)
                    {
                        strTxt.Append(",\"reply_content\":\"" + Microsoft.JScript.GlobalObject.escape(dr["reply_content"]) + "\"");
                        strTxt.Append(",\"reply_time\":\"" + dr["reply_time"] + "\"");
                    }
                    strTxt.Append("}");
                    //是否加逗號
                    if (i < ds.Tables[0].Rows.Count - 1)
                    {
                        strTxt.Append(",");
                    }

                }
                strTxt.Append("]");
            }
            //else
            //{
            //    strTxt.Append("<p>暫無評論，快來搶沙發吧！</p>");
            //}
            context.Response.Write(strTxt.ToString());
        }
        #endregion

        #region 驗證用戶名是否可用OK============================
        private void validate_username(HttpContext context)
        {
            string username = DTRequest.GetString("username");
            //如果為Null，退出
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("null");
                return;
            }
            //過濾註冊用戶名字元
            string[] strArray = userConfig.regkeywords.Split(',');
            foreach (string s in strArray)
            {
                if (s.ToLower() == username.ToLower())
                {
                    context.Response.Write("lock");
                    return;
                }
            }
            BLL.users bll = new BLL.users();
            //查詢資料庫
            if (!bll.Exists(username.Trim()))
            {
                context.Response.Write("true");
                return;
            }
            context.Response.Write("false");
            return;
        }
        #endregion

        #region 用戶註冊OK=====================================
        private void user_register(HttpContext context)
        {
            string code = DTRequest.GetFormString("txtCode").Trim();
            string invitecode = DTRequest.GetFormString("txtInviteCode").Trim();
            string username = DTRequest.GetFormString("txtUserName").Trim();
            string password = DTRequest.GetFormString("txtPassword").Trim();
            string email = DTRequest.GetFormString("txtEmail").Trim();
            string userip = DTRequest.GetIP();

            #region 檢查各項並提示
            //檢查是否開啟會員功能
            if (siteConfig.memberstatus == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，會員功能已被關閉，無法註冊新會員！\"}");
                return;
            }
            if (userConfig.regstatus == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，系統戰不允許註冊新用戶！\"}");
                return;
            }
            //校檢驗證碼
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //檢查使用者輸入資訊是否為空
            if (username == "" || password == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"用戶名和密碼不能為空！\"}");
                return;
            }
            if (email == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"電子郵箱不能為空！\"}");
                return;
            }

            //檢查用戶名
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();
            if (bll.Exists(username))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"該用戶名已經存在！\"}");
                return;
            }
            //檢查同一IP註冊時隔
            if (userConfig.regctrl > 0)
            {
                if (bll.Exists(userip, userConfig.regctrl))
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，同一IP在" + userConfig.regctrl + "小時內不能註冊多個用戶！\"}");
                    return;
                }
            }
            //不允許同一Email註冊不同用戶
            if (userConfig.regemailditto == 0)
            {
                if (bll.ExistsEmail(email))
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"Email不允許重複註冊，如果你忘記用戶名，請找回密碼！\"}");
                    return;
                }
            }
            //檢查默認組別是否存在
            Model.user_groups modelGroup = new BLL.user_groups().GetDefault();
            if (modelGroup == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系統尚未分組，請聯繫管理員設置會員分組！\"}");
                return;
            }
            //檢查是否通過邀請碼註冊
            if (userConfig.regstatus == 2)
            {
                string result1 = verify_invite_reg(username, invitecode);
                if (result1 != "success")
                {
                    context.Response.Write(result1);
                    return;
                }
            }
            #endregion

            //保存註冊資訊
            model.group_id = modelGroup.id;
            model.user_name = username;
            model.password = DESEncrypt.Encrypt(password);
            model.email = email;
            model.reg_ip = userip;
            model.reg_time = DateTime.Now;
            model.is_lock = userConfig.regverify; //設置為對應狀態
            int newId = bll.Add(model);
            if (newId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系統故障，註冊失敗，請聯繫網站管理員！\"}");
                return;
            }
            model = bll.GetModel(newId);
            //贈送積分金額
            if (modelGroup.point > 0)
            {
                new BLL.point_log().Add(model.id, model.user_name, modelGroup.point, "註冊贈送積分");
            }
            if (modelGroup.amount > 0)
            {
                new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.SysGive.ToString(), modelGroup.amount, "註冊贈送金額", 1);
            }
            //判斷是否發送站內短消息
            if (userConfig.regmsgstatus == 1)
            {
                new BLL.user_message().Add(1, "", model.user_name, "歡迎您成為本站會員", userConfig.regmsgtxt);
            }
            //需要Email驗證
            if (userConfig.regverify == 1)
            {
                string result2 = verify_email(model);
                if (result2 != "success")
                {
                    context.Response.Write(result2);
                    return;
                }
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=sendmail&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"註冊成功，請進入郵箱驗證啟動帳戶！\"}");
            }
            //需要人工審核
            else if (userConfig.regverify == 2)
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=verify&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"註冊成功，請等待審核通過！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=succeed&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"恭喜您，註冊成功啦！\"}");
            }
            return;
        }

        #region 邀請註冊處理方法OK==============================
        private string verify_invite_reg(string user_name, string invite_code)
        {
            if (string.IsNullOrEmpty(invite_code))
            {
                return "{\"msg\":0, \"msgbox\":\"邀請碼不能為空！\"}";
            }
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(invite_code);
            if (codeModel == null)
            {
                return "{\"msg\":0, \"msgbox\":\"邀請碼不正確或已過期啦！\"}";
            }
            if (userConfig.invitecodecount > 0)
            {
                if (codeModel.count >= userConfig.invitecodecount)
                {
                    codeModel.status = 1;
                    return "{\"msg\":0, \"msgbox\":\"該邀請碼已經被使用啦！\"}";
                }
            }
            //檢查是否給邀請人增加積分
            if (userConfig.pointinvitenum > 0)
            {
                new BLL.point_log().Add(codeModel.user_id, codeModel.user_name, userConfig.pointinvitenum, "邀請用戶【" + user_name + "】註冊獲得積分");
            }
            //更改邀請碼狀態
            codeModel.count += 1;
            codeBll.Update(codeModel);
            return "success";
        }
        #endregion

        #region Email驗證發送郵件OK=============================
        private string verify_email(Model.users userModel)
        {
            //生成隨機碼
            string strcode = Utils.GetCheckCode(20);
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            //檢查是否重複提交
            codeModel = codeBll.GetModel(userModel.user_name, DTEnums.CodeEnum.RegVerify.ToString());
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                codeModel.user_id = userModel.id;
                codeModel.user_name = userModel.user_name;
                codeModel.type = DTEnums.CodeEnum.RegVerify.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.regemailexpired);
                codeModel.add_time = DateTime.Now;
                new BLL.user_code().Add(codeModel);
            }
            //獲得郵件內容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("regverify");
            if (mailModel == null)
            {
                return "{\"msg\":0, \"msgbox\":\"郵件發送失敗，郵件範本內容不存在！\"}";
            }
            //替換範本內容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", userModel.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{username}", userModel.user_name);
            bodytxt = bodytxt.Replace("{linkurl}", Utils.DelLastChar(siteConfig.weburl, "/") + new Web.UI.BasePage().linkurl("register")+"?action=checkmail&strcode=" + codeModel.str_code);
            //發送郵件
            try
            {
                DTMail.sendMail(siteConfig.emailstmp, siteConfig.emailport,
                    siteConfig.emailusername,
                    DESEncrypt.Decrypt(siteConfig.emailpassword), 
                    siteConfig.emailnickname, 
                    siteConfig.emailfrom, 
                    userModel.email, 
                    titletxt, bodytxt);
            }
            catch
            {
                return "{\"msg\":0, \"msgbox\":\"郵件發送失敗，請聯繫本站客服或管理人員！\"}";
            }
            return "success";
        }

        #endregion

        #endregion

        #region 申請邀請碼OK===================================
        private void user_invite_code(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            //檢查是否開啟邀請註冊
            if (userConfig.regstatus != 2)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，系統不允許通過邀請註冊！\"}");
                return;
            }
            BLL.user_code codeBll = new BLL.user_code();
            //檢查申請是否超過限制
            if (userConfig.invitecodenum > 0)
            {
                int result = codeBll.GetCount("user_name='" + model.user_name + "' and type='" + DTEnums.CodeEnum.Register.ToString() + "' and datediff(d,add_time,getdate())=0");
                if (result >= userConfig.invitecodenum)
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您申請的邀請碼數量已超過每天的限制！\"}");
                    return;
                }
            }
            //刪除過期的邀請碼
            codeBll.Delete("type='" + DTEnums.CodeEnum.Register.ToString() + "' and status=1 or datediff(d,eff_time,getdate())>0");
            //隨機取得邀請碼
            string str_code = Utils.GetCheckCode(8);
            Model.user_code codeModel = new Model.user_code();
            codeModel.user_id = model.id;
            codeModel.user_name = model.user_name;
            codeModel.type = DTEnums.CodeEnum.Register.ToString();
            codeModel.str_code = str_code;
            if (userConfig.invitecodeexpired > 0)
            {
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.invitecodeexpired);
            }
            codeBll.Add(codeModel);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"恭喜您，申請邀請碼已成功！\"}");
            return;
        }
        #endregion

        #region 發送註冊驗證郵件OK=============================
        private void user_verify_email(HttpContext context)
        {
            string username = DTRequest.GetFormString("username");
            //檢查是否過快
            string cookie = Utils.GetCookie("user_reg_email");
            if (cookie == username)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"發送郵件間隔為20分鐘，您剛才已經提交過啦，休息一會再來吧！\"}");
                return;
            }
            Model.users model = new BLL.users().GetModel(username);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"該用戶不存在或已刪除！\"}");
                return;
            }
            if (model.is_lock != 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"該用戶無法進行郵箱驗證！\"}");
                return;
            }
            string result = verify_email(model);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"郵件已經發送成功！\"}");
            Utils.WriteCookie("user_reg_email", username, 20); //20分鐘內無重複發送
            return;
        }
        #endregion

        #region 用戶登錄OK=====================================
        private void user_login(HttpContext context)
        {
            string username = DTRequest.GetFormString("txtUserName");
            string password = DTRequest.GetFormString("txtPassword");
            string remember = DTRequest.GetFormString("chkRemember");
            //檢查用戶名密碼
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"溫馨提示：請輸入用戶名或密碼！\"}");
                return;
            }

            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username, DESEncrypt.Encrypt(password), userConfig.emaillogin);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"錯誤提示：用戶名或密碼錯誤，請重試！\"}");
                return;
            }
            //檢查用戶是否通過驗證
            if (model.is_lock == 1) //待驗證
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=sendmail&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"會員尚未通過驗證！\"}");
                return;
            }
            else if (model.is_lock == 2) //待審核
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=verify&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"會員尚未通過審核！\"}");
                return;
            }
            context.Session[DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //記住登錄狀態下次自動登錄
            if (remember.ToLower() == "true")
            {
                Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name, 43200);
                Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password, 43200);
            }
            else
            {
                //防止Session提前過期
                Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
                Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
            }

            //寫入登錄日誌
            new BLL.user_login_log().Add(model.id, model.user_name, "會員登入", DTRequest.GetIP());
            //返回URL
            context.Response.Write("{\"msg\":1, \"msgbox\":\"會員登入成功！\"}");
            return;
        }
        #endregion

        #region 綁定協力廠商登錄帳戶OK============================
        private void user_oauth_bind(HttpContext context)
        {
            //檢查URL參數
            if (context.Session["oauth_name"] == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"錯誤提示：授權參數不正確！\"}");
                return;
            }
            //獲取授權資訊
            string result = Utils.UrlExecute(siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
            if (result.Contains("error"))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"錯誤提示：請檢查URL是否正確！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"錯誤代碼：" + dic["ret"] + "，描述：" + dic["msg"] + "\"}");
                return;
            }

            //檢查用戶名密碼
            string username = DTRequest.GetString("txtUserName");
            string password = DTRequest.GetString("txtPassword");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"溫馨提示：請輸入用戶名或密碼！\"}");
                return;
            }
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username, DESEncrypt.Encrypt(password), userConfig.emaillogin);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"錯誤提示：用戶名或密碼錯誤，請重試！\"}");
                return;
            }
            //開始綁定
            Model.user_oauth oauthModel = new Model.user_oauth();
            oauthModel.oauth_name = dic["oauth_name"].ToString();
            oauthModel.user_id = model.id;
            oauthModel.user_name = model.user_name;
            oauthModel.oauth_access_token = dic["oauth_access_token"].ToString();
            oauthModel.oauth_openid = dic["oauth_openid"].ToString();
            int newId = new BLL.user_oauth().Add(oauthModel);
            if (newId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"錯誤提示：綁定過程中出現錯誤，請重新登入授權！\"}");
                return;
            }
            context.Session[DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //記住登錄狀態，防止Session提前過期
            Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
            Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
            //寫入登錄日誌
            new BLL.user_login_log().Add(model.id, model.user_name, "會員登入", DTRequest.GetIP());
            //返回URL
            context.Response.Write("{\"msg\":1, \"msgbox\":\"會員登入成功！\"}");
            return;
        }
        #endregion

        #region 註冊協力廠商登錄帳戶OK============================
        private void user_oauth_register(HttpContext context)
        {
            //檢查URL參數
            if (context.Session["oauth_name"] == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"錯誤信息：授權參數不正確！\"}");
                return;
            }
            //獲取授權資訊
            string result = Utils.UrlExecute(siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
            if (result.Contains("error"))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"錯誤提示：請檢查URL是否正確！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"錯誤代碼：" + dic["ret"] + "，" + dic["msg"] + "\"}");
                return;
            }

            string password = DTRequest.GetFormString("txtPassword").Trim();
            string email = DTRequest.GetFormString("txtEmail").Trim();
            string userip = DTRequest.GetIP();
            //檢查用戶名
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();
            //檢查默認組別是否存在
            Model.user_groups modelGroup = new BLL.user_groups().GetDefault();
            if (modelGroup == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系統尚未分組，請聯繫管理員設置會員分組！\"}");
                return;
            }
            //保存註冊資訊
            model.group_id = modelGroup.id;
            model.user_name = bll.GetRandomName(10);
            model.password = DESEncrypt.Encrypt(password);
            model.email = email;
            if (!string.IsNullOrEmpty(dic["nick"].ToString()))
            {
                model.nick_name = dic["nick"].ToString();
            }
            if (dic["avatar"].ToString().StartsWith("http://"))
            {
                model.avatar = dic["avatar"].ToString();
            }
            if (!string.IsNullOrEmpty(dic["sex"].ToString()))
            {
                model.sex = dic["sex"].ToString();
            }
            if (!string.IsNullOrEmpty(dic["birthday"].ToString()))
            {
                model.birthday = DateTime.Parse(dic["birthday"].ToString());
            }
            model.reg_ip = userip;
            model.reg_time = DateTime.Now;
            model.is_lock = 0; //設置為對應狀態
            int newId = bll.Add(model);
            if (newId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系統故障，註冊失敗，請聯繫網站管理員！\"}");
                return;
            }
            model = bll.GetModel(newId);
            //贈送積分金額
            if (modelGroup.point > 0)
            {
                new BLL.point_log().Add(model.id, model.user_name, modelGroup.point, "註冊贈送積分");
            }
            if (modelGroup.amount > 0)
            {
                new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.SysGive.ToString(), modelGroup.amount, "註冊贈送金額", 1);
            }
            //判斷是否發送站內短消息
            if (userConfig.regmsgstatus == 1)
            {
                new BLL.user_message().Add(1, "", model.user_name, "歡迎您成為本站會員", userConfig.regmsgtxt);
            }
            //綁定到對應的授權類型
            Model.user_oauth oauthModel = new Model.user_oauth();
            oauthModel.oauth_name = dic["oauth_name"].ToString();
            oauthModel.user_id = model.id;
            oauthModel.user_name = model.user_name;
            oauthModel.oauth_access_token = dic["oauth_access_token"].ToString();
            oauthModel.oauth_openid = dic["oauth_openid"].ToString();
            new BLL.user_oauth().Add(oauthModel);

            context.Session[DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //記住登錄狀態，防止Session提前過期
            Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
            Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
            //寫入登錄日誌
            new BLL.user_login_log().Add(model.id, model.user_name, "會員登入", DTRequest.GetIP());
            //返回URL
            context.Response.Write("{\"msg\":1, \"msgbox\":\"會員登入成功！\"}");
            return;
        }
        #endregion

        #region 修改使用者資訊OK=================================
        private void user_info_edit(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            string email = DTRequest.GetFormString("txtEmail");
            string nick_name = DTRequest.GetFormString("txtNickName");
            string sex = DTRequest.GetFormString("rblSex");
            string birthday = DTRequest.GetFormString("txtBirthday");
            string telphone = DTRequest.GetFormString("txtTelphone");
            string mobile = DTRequest.GetFormString("txtMobile");
            string qq = DTRequest.GetFormString("txtQQ");
            string address = context.Request.Form["txtAddress"];
            string safe_question = context.Request.Form["txtSafeQuestion"];
            string safe_answer = context.Request.Form["txtSafeAnswer"];
            //檢查郵箱
            if (nick_name == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入您的姓名昵稱！\"}");
                return;
            }
            //檢查郵箱
            if (email == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入您郵箱帳號！\"}");
                return;
            }

            //開始寫入資料庫
            model.email = email;
            model.nick_name = nick_name;
            model.sex = sex;
            DateTime _birthday;
            if (DateTime.TryParse(birthday, out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = telphone;
            model.mobile = mobile;
            model.qq = qq;
            model.address = address;
            model.safe_question = safe_question;
            model.safe_answer = safe_answer;


            new BLL.users().Update(model);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"您的帳戶資料已修改成功啦！\"}");
            return;
        }
        #endregion

        #region 確認裁剪用戶頭像OK=============================
        private void user_avatar_crop(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            string fileName = DTRequest.GetFormString("hideFileName");
            int x1 = DTRequest.GetFormInt("hideX1");
            int y1 = DTRequest.GetFormInt("hideY1");
            int w = DTRequest.GetFormInt("hideWidth");
            int h = DTRequest.GetFormInt("hideHeight");
            //檢查是否圖片

            //檢查參數
            if (!Utils.FileExists(fileName) || w == 0 || h == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請先上傳一張圖片！\"}");
                return;
            }
            //取得保存的新檔案名
            UpLoad upFiles = new UpLoad();
            bool result = upFiles.cropSaveAs(fileName, fileName, 180, 180, w, h, x1, y1);
            if (!result)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"圖片裁剪過程中發生意外錯誤！\"}");
                return;
            }
            //刪除原用戶頭像
            Utils.DeleteFile(model.avatar);
            model.avatar = fileName;
            //修改用戶頭像
            new BLL.users().UpdateField(model.id, "avatar='" + model.avatar + "'");
            context.Response.Write("{\"msg\": 1, \"msgbox\": \"" + model.avatar + "\"}");
            return;
        }
        #endregion

        #region 修改登錄密碼OK=================================
        private void user_password_edit(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            int user_id = model.id;
            string oldpassword = DTRequest.GetFormString("txtOldPassword");
            string password = DTRequest.GetFormString("txtPassword");
            //檢查輸入的舊密碼
            if (string.IsNullOrEmpty(oldpassword))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"請輸入您的舊登入密碼！\"}");
                return;
            }
            //檢查輸入的新密碼
            if (string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"請輸入您的新登入密碼！\"}");
                return;
            }
            //舊密碼是否正確
            if (model.password != DESEncrypt.Encrypt(oldpassword))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您輸入的舊密碼不正確！\"}");
                return;
            }
            //執行修改操作
            model.password = DESEncrypt.Encrypt(password);
            new BLL.users().Update(model);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"您的密碼已修改成功，請記住新密碼！\"}");
            return;
        }
        #endregion

        #region 郵箱取回密碼OK=================================
        private void user_getpassword(HttpContext context)
        {
            string code = DTRequest.GetFormString("txtCode");
            string username = DTRequest.GetFormString("txtUserName").Trim();
            //檢查用戶名是否正確
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶名不能為空！\"}");
                return;
            }
            //校檢驗證碼
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //檢查使用者資訊
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您輸入的用戶名不存在！\"}");
                return;
            }
            if (string.IsNullOrEmpty(model.email))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您尚未設置郵箱位址，無法使用取回密碼功能！\"}");
                return;
            }
            //生成隨機碼
            string strcode = Utils.GetCheckCode(20);
            //獲得郵件內容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("getpassword");
            if (mailModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"郵件發送失敗，郵件範本內容不存在！\"}");
                return;
            }
            //檢查是否重複提交
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            codeModel = codeBll.GetModel(username, DTEnums.CodeEnum.RegVerify.ToString());
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                //寫入資料庫
                codeModel.user_id = model.id;
                codeModel.user_name = model.user_name;
                codeModel.type = DTEnums.CodeEnum.Password.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddDays(1);
                codeModel.add_time = DateTime.Now;
                codeBll.Add(codeModel);
            }
            //替換範本內容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{linkurl}", Utils.DelLastChar(siteConfig.weburl, "/") + new BasePage().linkurl("repassword1", "reset", strcode)); //此處需要修改
            //發送郵件
            try
            {
                DTMail.sendMail(siteConfig.emailstmp, siteConfig.emailport,
                    siteConfig.emailusername,
                    DESEncrypt.Decrypt(siteConfig.emailpassword),
                    siteConfig.emailnickname,
                    siteConfig.emailfrom,
                    model.email,
                    titletxt, bodytxt);
            }
            catch
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"郵件發送失敗，請聯繫本站管理員！\"}");
                return;
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"郵件發送成功，請登入您的郵箱找回登入密碼！\"}");
            return;
        }
        #endregion

        #region 郵箱重設密碼OK=================================
        private void user_repassword(HttpContext context)
        {
            string code = context.Request.Form["txtCode"];
            string strcode = context.Request.Form["hideCode"];
            string password = context.Request.Form["txtPassword"];

            //校檢驗證碼
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //檢查驗證字串
            if (string.IsNullOrEmpty(strcode))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系統找不到郵件驗證的字串！\"}");
                return;
            }
            //檢查輸入的新密碼
            if (string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"請輸入您的新密碼！\"}");
                return;
            }

            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(strcode);
            if (codeModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"郵件驗證的字串不存在或已過期！\"}");
                return;
            }
            //驗證用戶是否存在
            BLL.users userBll = new BLL.users();
            if (!userBll.Exists(codeModel.user_id))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"該用戶不存在或已刪除！\"}");
                return;
            }
            Model.users userModel = userBll.GetModel(codeModel.user_id);
            //執行修改操作
            userModel.password = DESEncrypt.Encrypt(password);
            userBll.Update(userModel);
            //更改驗證字串狀態
            codeModel.count = 1;
            codeModel.status = 1;
            codeBll.Update(codeModel);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"修改密碼成功，請記住您的新密碼！\"}");
            return;
        }
        #endregion

        #region 刪除短資訊OK===================================
        private void user_message_delete(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            string check_id = DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"刪除失敗，請檢查傳輸參數！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.user_message().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"刪除短資訊成功啦！\"}");
            return;
        }
        #endregion

        #region 發佈短資訊OK===================================
        private void user_message_add(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            string code = context.Request.Form["txtCode"];
            string send_save = DTRequest.GetFormString("sendSave");
            string user_name = DTRequest.GetFormString("txtUserName");
            string title = DTRequest.GetFormString("txtTitle");
            string content = DTRequest.GetFormString("txtContent");
            //校檢驗證碼
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //檢查用戶名
            if (user_name == "" || !new BLL.users().Exists(user_name))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，該用戶名不存在或已經被刪除啦！\"}");
                return;
            }
            //檢查標題
            if (title == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入短消息標題！\"}");
                return;
            }
            //檢查內容
            if (content == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入短消息內容！\"}");
                return;
            }
            //保存資料
            Model.user_message modelMessage = new Model.user_message();
            modelMessage.type = 2;
            modelMessage.post_user_name = model.user_name;
            modelMessage.accept_user_name = user_name;
            modelMessage.title = title;
            modelMessage.content = Utils.ToHtml(content);
            new BLL.user_message().Add(modelMessage);
            if (send_save == "true") //保存到收件箱
            {
                modelMessage.type = 3;
                new BLL.user_message().Add(modelMessage);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"發佈短資訊成功啦！\"}");
            return;
        }
        #endregion

        #region 用戶兌換積分OK=================================
        private void user_point_convert(HttpContext context)
        {
            //檢查系統是否啟用兌換積分功能
            if (userConfig.pointcashrate == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，網站已關閉兌換積分功能！\"}");
                return;
            }
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時！\"}");
                return;
            }
            int amout = DTRequest.GetFormInt("txtAmount");
            string password = DTRequest.GetFormString("txtPassword");
            if (model.amount < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您的帳戶餘額不足！\"}");
                return;
            }
            if (amout < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，最小兌換金額為1元！\"}");
                return;
            }
            if (amout > model.amount)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您兌換的金額大於帳戶餘額！\"}");
                return;
            }
            if (password == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入您帳戶的密碼！\"}");
                return;
            }
            //驗證密碼
            if (DESEncrypt.Encrypt(password) != model.password)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您輸入的密碼不正確！\"}");
                return;
            }
            //計算兌換後的積分值
            int convertPoint = (int)(Convert.ToDecimal(amout) * userConfig.pointcashrate);
            //扣除金額
            int amountNewId = new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.Convert.ToString(), amout * -1, "用戶兌換積分", 1);
            //增加積分
            if (amountNewId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"轉換過程中發生錯誤，請重新提交！\"}");
                return;
            }
            int pointNewId = new BLL.point_log().Add(model.id, model.user_name, convertPoint, "用戶兌換積分");
            if (pointNewId < 1)
            {
                //返還金額
                new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.Convert.ToString(), amout, "用戶兌換積分失敗，返還金額", 1);
                context.Response.Write("{\"msg\":0, \"msgbox\":\"轉換過程中發生錯誤，請重新提交！\"}");
                return;
            }

            context.Response.Write("{\"msg\":1, \"msgbox\":\"恭喜您，積分兌換成功！\"}");
            return;
        }
        #endregion

        #region 刪除用戶積分明細OK=============================
        private void user_point_delete(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            string check_id = DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"刪除失敗，請檢查傳輸參數！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.point_log().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"積分明細刪除成功！\"}");
            return;
        }
        #endregion

        #region 用戶線上充值OK=================================
        private void user_amount_recharge(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            decimal amount = DTRequest.GetFormDecimal("order_amount", 0);
            int payment_id = DTRequest.GetFormInt("payment_id");
            if (amount == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入正確的充值金額！\"}");
                return;
            }
            if (payment_id == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請選擇正確的支付方式！\"}");
                return;
            }
            if (!new BLL.payment().Exists(payment_id))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您選擇的支付方式不存在或已刪除！\"}");
                return;
            }
            //生成訂單號
            string order_no = Utils.GetOrderNumber(); //訂單號
            new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.Recharge.ToString(), order_no, payment_id, amount,
                "帳戶充值(" + new BLL.payment().GetModel(payment_id).title + ")", 0);
            //保存成功後返回訂單號
            context.Response.Write("{\"msg\":1, \"msgbox\":\"訂單保存成功！\", \"url\":\"" + new Web.UI.BasePage().linkurl("payment1", "confirm", DTEnums.AmountTypeEnum.Recharge.ToString(), order_no) + "\"}");
            return;

        }
        #endregion

        #region 刪除用戶收支明細OK=============================
        private void user_amount_delete(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            string check_id = DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"刪除失敗，請檢查傳輸參數！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.amount_log().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"收支明細刪除成功！\"}");
            return;
        }
        #endregion

        #region 購物車加入商品OK===============================
        private void cart_goods_add(HttpContext context)
        {
            string goods_id = DTRequest.GetFormString("goods_id");
            int goods_quantity = DTRequest.GetFormInt("goods_quantity", 1);
            if (goods_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您提交的商品參數有誤！\"}");
                return;
            }
            //查找會員組
            int group_id = 0;
            Model.users groupModel = new Web.UI.BasePage().GetUserInfo();
            if (groupModel != null)
            {
                group_id = groupModel.group_id;
            }
            //統計購物車
            Web.UI.ShopCart.Add(goods_id, goods_quantity);
            Model.cart_total cartModel = Web.UI.ShopCart.GetTotal(group_id);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"商品已成功添加到購物車！\", \"quantity\":" + cartModel.total_quantity + ", \"amount\":" + cartModel.real_amount + "}");
            return;
        }
        #endregion

        #region 修改購物車商品OK===============================
        private void cart_goods_update(HttpContext context)
        {
            string goods_id = DTRequest.GetFormString("goods_id");
            int goods_quantity = DTRequest.GetFormInt("goods_quantity");
            if (goods_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您提交的商品參數有誤！\"}");
                return;
            }

            if (Web.UI.ShopCart.Update(goods_id, goods_quantity))
            {
                context.Response.Write("{\"msg\":1, \"msgbox\":\"商品數量修改成功！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"商品數量更改失敗，請檢查操作是否有誤！\"}");
            }
            return;
        }
        #endregion

        #region 刪除購物車商品OK===============================
        private void cart_goods_delete(HttpContext context)
        {
            string goods_id = DTRequest.GetFormString("goods_id");
            if (goods_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您提交的商品參數有誤！\"}");
                return;
            }
            Web.UI.ShopCart.Clear(goods_id);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"商品移除成功！\"}");
            return;
        }
        #endregion

        #region 保存使用者訂單OK=================================
        private void order_save(HttpContext context)
        {
            int payment_id = DTRequest.GetFormInt("payment_id");
            int distribution_id = DTRequest.GetFormInt("distribution_id");
            string accept_name = DTRequest.GetFormString("accept_name");
            string post_code = DTRequest.GetFormString("post_code");
            string telphone = DTRequest.GetFormString("telphone");
            string mobile = DTRequest.GetFormString("mobile");
            string address = DTRequest.GetFormString("address");
            string message = DTRequest.GetFormString("message");
            //檢查配送方式
            if (distribution_id == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請選擇配送方式！\"}");
                return;
            }
            Model.distribution disModel = new BLL.distribution().GetModel(distribution_id);
            if (disModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您選擇的配送方式不存在或已刪除！\"}");
                return;
            }
            //檢查支付方式
            if (payment_id == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請選擇支付方式！\"}");
                return;
            }
            Model.payment payModel = new BLL.payment().GetModel(payment_id);
            if (payModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，您選擇的支付方式不存在或已刪除！\"}");
                return;
            }
            //檢查收貨人
            if (string.IsNullOrEmpty(accept_name))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入收貨人姓名！\"}");
                return;
            }
            //檢查手機和電話
            if (string.IsNullOrEmpty(telphone) && string.IsNullOrEmpty(mobile))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入收貨人聯繫電話或手機！\"}");
                return;
            }
            //檢查地址
            if (string.IsNullOrEmpty(address))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，請輸入詳細的收貨地址！\"}");
                return;
            }
            //檢查用戶是否登錄
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            //檢查購物車商品
            IList<Model.cart_items> iList = DTcms.Web.UI.ShopCart.GetList(userModel.group_id);
            if (iList == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，購物車為空，無法結算！\"}");
                return;
            }
            //統計購物車
            Model.cart_total cartModel = DTcms.Web.UI.ShopCart.GetTotal(userModel.group_id);
            //保存訂單=======================================================================
            Model.orders model = new Model.orders();
            model.order_no = Utils.GetOrderNumber(); //訂單號
            model.user_id = userModel.id;
            model.user_name = userModel.user_name;
            model.payment_id = payment_id;
            model.distribution_id = distribution_id;
            model.accept_name = accept_name;
            model.post_code = post_code;
            model.telphone = telphone;
            model.mobile = mobile;
            model.address = address;
            model.message = message;
            model.payable_amount = cartModel.payable_amount;
            model.real_amount = cartModel.real_amount;
            model.payable_freight = disModel.amount; //應付運費
            model.real_freight = disModel.amount; //實付運費
            //如果是先款後貨的話
            if (payModel.type == 1)
            {
                if (payModel.poundage_type == 1) //百分比
                {
                    model.payment_fee = model.real_amount * payModel.poundage_amount / 100;
                }
                else //固定金額
                {
                    model.payment_fee = payModel.poundage_amount;
                }
            }
            //訂單總金額=實付商品金額+運費+支付手續費
            model.order_amount = model.real_amount + model.real_freight + model.payment_fee;
            //購物積分,可為負數
            model.point = cartModel.total_point;
            model.add_time = DateTime.Now;
            //商品詳細列表
            List<Model.order_goods> gls = new List<Model.order_goods>();
            foreach (Model.cart_items item in iList)
            {
                gls.Add(new Model.order_goods { goods_id = item.id, goods_name = item.title, goods_price = item.price, real_price = item.user_price, quantity = item.quantity, point = item.point });
            }
            model.order_goods = gls;
            int result = new BLL.orders().Add(model);
            if (result < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"訂單保存過程中發生錯誤，請重新提交！\"}");
                return;
            }
            //扣除積分
            if (model.point < 0)
            {
                new BLL.point_log().Add(model.user_id, model.user_name, model.point, "積分換購，訂單號：" + model.order_no);
            }
            //清空購物車
            DTcms.Web.UI.ShopCart.Clear("0");
            //提交成功，返回URL
            context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("payment1", "confirm", DTEnums.AmountTypeEnum.BuyGoods.ToString(), model.order_no) + "\", \"msgbox\":\"恭喜您，訂單已成功提交！\"}");
            return;
        }
        #endregion

        #region 使用者取消訂單OK=================================
        private void order_cancel(HttpContext context)
        {
            //檢查用戶是否登錄
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，用戶沒有登入或登入超時啦！\"}");
                return;
            }
            //檢查訂單是否存在
            string order_no = DTRequest.GetQueryString("order_no");
            Model.orders orderModel = new BLL.orders().GetModel(order_no);
            if (order_no == "" || orderModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，該訂單號不存在！\"}");
                return;
            }
            //檢查是否自己的訂單
            if (userModel.id != orderModel.user_id)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，不能取消別人的訂單狀態！\"}");
                return;
            }
            //檢查訂單狀態
            if (orderModel.status > 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，該訂單不是生成狀態，不能取消！\"}");
                return;
            }
            bool result = new BLL.orders().UpdateField(order_no, "status=4");
            if (!result)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"對不起，操作過程中發生不可預知的錯誤！\"}");
                return;
            }
            //如果是積分換購則返還積分
            if (orderModel.point < 0)
            {
                new BLL.point_log().Add(orderModel.user_id, orderModel.user_name, -1 * orderModel.point, "取消訂單，返還換購積分，訂單號：" + orderModel.order_no);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"取消訂單成功啦！\"}");
            return;
        }
        #endregion

        #region 通用外理方法OK=================================
        //校檢驗證碼
        private string verify_code(HttpContext context, string strcode)
        {
            if (string.IsNullOrEmpty(strcode))
            {
                return "{\"msg\":0, \"msgbox\":\"對不起，請輸入驗證碼！\"}";
            }
            if (context.Session[DTKeys.SESSION_CODE] == null)
            {
                return "{\"msg\":0, \"msgbox\":\"對不起，驗證碼超時或已過期！\"}";
            }
            if (strcode.ToLower() != (context.Session[DTKeys.SESSION_CODE].ToString()).ToLower())
            {
                return "{\"msg\":0, \"msgbox\":\"您輸入的驗證碼與系統的不一致！\"}";
            }
            context.Session[DTKeys.SESSION_CODE] = null;
            return "success";
        }
        #endregion END通用方法=================================================

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
