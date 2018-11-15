using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;

namespace DTcms.Web.tools
{
    /// <summary>
    /// 管理後臺AJAX處理頁
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得處事類型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "sys_channel_load": //載入頻道管理功能表
                    sys_channel_load(context);
                    break;
                case "plugins_nav_load": //載入外掛程式管理功能表
                    plugins_nav_load(context);
                    break;
                case "sys_channel_validate": //驗證頻道名稱是否重複
                    sys_channel_validate(context);
                    break;
                case "sys_urlrewrite_validate": //驗證URL重寫是否重複
                    sys_urlrewrite_validate(context);
                    break;
                case "validate_username": //驗證會員用戶名是否重複
                    validate_username(context);
                    break;
            }

        }

        #region 載入頻道管理功能表================================
        private void sys_channel_load(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.sys_channel bll = new BLL.sys_channel();
            DataTable dt = bll.GetList("").Tables[0];
            strTxt.Append("[");
            int i = 1;
            int jj = 0;
            foreach (DataRow dr in dt.Rows)
            {
                jj++;
                Model.manager admin_info = new ManagePage().GetAdminInfo();
                if (!new BLL.manager_role().Exists(admin_info.role_id, Convert.ToInt32(dr["id"]), DTEnums.ActionEnum.View.ToString()))
                {
                    continue;
                }
                BLL.sys_model bll2 = new BLL.sys_model();
                Model.sys_model model2 = bll2.GetModel(Convert.ToInt32(dr["model_id"]));
                if (jj == 1)
                {
                    strTxt.Append("{");
                    strTxt.Append("\"text\":\"基礎設置\",");
                    strTxt.Append("\"isexpand\":\"false\",");
                    strTxt.Append("\"children\":[");

                    strTxt.Append("{");
                    strTxt.Append("\"text\":\"房屋類型\",");
                    strTxt.Append("\"url\":\"settings/sys_model_list.aspx\""); //此處要優化，加上nav.nav_url網站目錄標籤替換
                    strTxt.Append("}");
                    strTxt.Append(",");

                    strTxt.Append("{");
                    strTxt.Append("\"text\":\"縣市鄉鎮\",");
                    strTxt.Append("\"url\":\"Area_list.aspx\""); //此處要優化，加上nav.nav_url網站目錄標籤替換
                    strTxt.Append("}");
                    strTxt.Append(",");

                    strTxt.Append("]");
                    strTxt.Append("}");
                    strTxt.Append(",");
                }
                strTxt.Append("{");
                strTxt.Append("\"text\":\"" + dr["title"] + "\",");
                strTxt.Append("\"isexpand\":\"false\",");
                strTxt.Append("\"children\":[");
                if (model2.sys_model_navs != null)
                {
                    int j = 1;
                    foreach (Model.sys_model_nav nav in model2.sys_model_navs)
                    {

                        strTxt.Append("{");
                        strTxt.Append("\"text\":\"" + nav.title + "\",");
                        switch (dr["name"].ToString())
                        {
                            case "kongjian": //空間規劃
                                if (!nav.title.Contains("類別"))
                                {
                                    nav.nav_url = "goods/list_kj.aspx";
                                }
                                break;
                            case "diguangjingpin": //帝光精品
                                if (!nav.title.Contains("類別"))
                                {
                                    nav.nav_url = "goods/list_dg.aspx";
                                }
                                break;
                            case "banjia": //搬家幫手
                                if (!nav.title.Contains("類別"))
                                {
                                    nav.nav_url = "goods/list_bj.aspx";
                                }
                                break;
                            case "VIP": //搬家幫手
                                if (!nav.title.Contains("類別"))
                                {
                                    nav.nav_url = "download/list.aspx";
                                }
                                break;
                            case "土地": //土地
                                if (!nav.title.Contains("類別"))
                                {
                                    nav.nav_url = "goods/list_td.aspx";
                                }
                                break;
                            default:
                                break;
                        }
                        strTxt.Append("\"url\":\"" + nav.nav_url + "?channel_id=" + dr["id"] + "\""); //此處要優化，加上nav.nav_url網站目錄標籤替換
                        strTxt.Append("}");
                        if (j < model2.sys_model_navs.Count)
                        {
                            strTxt.Append(",");
                        }
                        j++;
                    }
                }
                strTxt.Append("]");
                strTxt.Append("}");
                strTxt.Append(",");
                i++;
            }
            string newTxt = Utils.DelLastChar(strTxt.ToString(), ",") + "]";
            context.Response.Write(newTxt);
            return;
        }
        #endregion

        #region 載入外掛程式管理功能表================================
        private void plugins_nav_load(HttpContext context)
        {
            BLL.plugin bll = new BLL.plugin();
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath("../plugins/"));
            string strs = "";
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                Model.plugin aboutInfo = bll.GetInfo(dir.FullName + @"\");
                if (aboutInfo.isload == 1 && File.Exists(dir.FullName + @"\admin\index.aspx"))
                {

                    strs += "<li><a class=\"l-link\" href=\"javascript:f_addTab('plugin_" + dir.Name
                        + "','" + aboutInfo.name + "','../../plugins/" + dir.Name + "/admin/NoteBook.aspx')\">" + aboutInfo.name + "</a></li>\n";

                    //context.Response.Write("<li><a class=\"l-link\" href=\"javascript:f_addTab('plugin_" + dir.Name
                    //    + "','" + aboutInfo.name + "','../../plugins/" + dir.Name + "/admin/index.aspx')\">" + aboutInfo.name + "</a></li>\n");
                }
            }
            strs += "<li><a class=\"l-link\" href=\"javascript:f_addTab('sys_category','幫助中心類別','category/list.aspx?channel_id=100')\">幫助中心類別</a></li>\n";

            strs += "<li><a class=\"l-link\" href=\"javascript:f_addTab('sys_helper','幫助中心','helper.aspx?channel_id=100')\">幫助中心</a></li>\n";

            strs += "<li><a class=\"l-link\" href=\"javascript:f_addTab('sys_lunbo','輪播-廣告圖','lunbo_list.aspx')\">輪播-廣告圖</a></li>\n";

           // strs += "<li><a class=\"l-link\" href=\"javascript:f_addTab('sys_VipFile','VIP帳戶文件','vipfile_list.aspx')\">VIP帳戶文件</a></li>\n";
            strs += "<li><a class=\"l-link\" href=\"javascript:f_addTab('dt_user_need','用戶需求','UserNeedList.aspx')\">用戶需求</a></li>\n";
            strs += "<li><a class=\"l-link\" href=\"javascript:f_addTab('Freight','運費管理','Freight.aspx')\">運費管理</a></li>\n";
            context.Response.Write(strs);
            return;
        }
        #endregion

        #region 驗證頻道名稱是否重複============================
        private void sys_channel_validate(HttpContext context)
        {
            string channelname = DTRequest.GetFormString("channelname");
            string oldname = DTRequest.GetFormString("oldname");
            if (string.IsNullOrEmpty(channelname))
            {
                context.Response.Write("false");
                return;
            }
            //檢查是否與網站根目錄下的目錄同名
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(siteConfig.webpath));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                if (channelname.ToLower() == dir.Name)
                {
                    context.Response.Write("false");
                    return;
                }
            }
            //檢查是否修改操作
            if (channelname == oldname)
            {
                context.Response.Write("true");
                return;
            }
            //檢查Key是否與已存在
            BLL.sys_channel bll = new BLL.sys_channel();
            if (bll.Exists(channelname))
            {
                context.Response.Write("false");
                return;
            }
            context.Response.Write("true");
            return;
        }
        #endregion

        #region 驗證URL重寫是否重複=============================
        private void sys_urlrewrite_validate(HttpContext context)
        {
            string rewritekey = DTRequest.GetFormString("rewritekey");
            string oldkey = DTRequest.GetFormString("oldkey");
            if (string.IsNullOrEmpty(rewritekey))
            {
                context.Response.Write("false1");
                return;
            }
            //檢查是否修改操作
            if (rewritekey.ToLower() == oldkey.ToLower())
            {
                context.Response.Write("true");
                return;
            }
            //檢查站點URL設定檔節點是否重複
            List<Model.url_rewrite> ls = new BLL.url_rewrite().GetList("");
            foreach (Model.url_rewrite model in ls)
            {
                if (model.name.ToLower() == rewritekey.ToLower())
                {
                    context.Response.Write("false2");
                    return;
                }
            }
            context.Response.Write("true");
            return;
        }
        #endregion

        #region 驗證用戶名是否可用==============================
        private void validate_username(HttpContext context)
        {
            string username = DTRequest.GetFormString("username");
            string oldusername = DTRequest.GetFormString("oldusername");
            //如果為Null，退出
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("false");
                return;
            }
            Model.userconfig userConfig = new BLL.userconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_USER_XML_CONFING));
            //過濾註冊用戶名字元
            string[] strArray = userConfig.regkeywords.Split(',');
            foreach (string s in strArray)
            {
                if (s.ToLower() == username.ToLower())
                {
                    context.Response.Write("false");
                    return;
                }
            }
            //檢查是否修改操作
            if (username == oldusername)
            {
                context.Response.Write("true");
                return;
            }
            BLL.users bll = new BLL.users();
            //查詢資料庫
            if (bll.Exists(username.Trim()))
            {
                context.Response.Write("false");
                return;
            }
            context.Response.Write("true");
            return;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
