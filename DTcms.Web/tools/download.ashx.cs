using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;

namespace DTcms.Web.tools
{
    /// <summary>
    /// 下載連結處理
    /// </summary>
    public class download : IHttpHandler, IRequiresSessionState
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
        public void ProcessRequest(HttpContext context)
        {
            int id = DTRequest.GetQueryInt("id");
            //獲得下載ID
            if (id < 1)
            {
                context.Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，參數傳值不正確！"));
                return;
            }
            //檢查下載記錄是否存在
            BLL.download_attach bll = new BLL.download_attach();
            if (!bll.Exists(id))
            {
                context.Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，您要下載的文件不存在或已被刪除！"));
                return;
            }
            Model.download_attach model = bll.GetModel(id);
            //檢查積分是否足夠
            if (model.point > 0)
            {
                //檢查用戶是否登錄
                Model.users userModel = new Web.UI.BasePage().GetUserInfo();
                if (userModel == null)
                {
                    //自動跳轉URL
                    HttpContext.Current.Response.Redirect(new Web.UI.BasePage().linkurl("login"));
                }
                //防止重複扣積分
                string cookie = Utils.GetCookie(DTKeys.COOKIE_DOWNLOAD_KEY, "attach_" + userModel.id.ToString());
                if (cookie != model.id.ToString())
                {
                    //檢查積分
                    if (model.point > userModel.point)
                    {
                        context.Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，您的積分不足支付本次下載！"));
                        return;
                    }
                    //扣取積分
                    new BLL.point_log().Add(userModel.id, userModel.user_name, model.point * -1, "下載附件：“" + model.title + "”，扣除積分");
                    //寫入Cookie
                    Utils.WriteCookie(DTKeys.COOKIE_DOWNLOAD_KEY, "attach_" + userModel.id.ToString(), model.id.ToString(), 8640);
                }
            }
            //下載次數+1
            bll.UpdateField(id, "down_num=down_num+1");
            //檢查檔本地還是遠端
            if (model.file_path.ToLower().StartsWith("http://"))
            {
                context.Response.Redirect(model.file_path);
                return;
            }
            else
            {
                //取得檔物理路徑
                string fullFileName = Utils.GetMapPath(model.file_path);
                if (!File.Exists(fullFileName))
                {
                    context.Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，您要下載的文件不存在或已被刪除！"));
                    return;
                }
                FileInfo file = new FileInfo(fullFileName);//路徑
                context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //解決中文亂碼
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(model.title)); //解決中文檔案名亂碼    
                context.Response.AddHeader("Content-length", file.Length.ToString());
                context.Response.ContentType = "application/pdf";
                context.Response.WriteFile(file.FullName);
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
} 
