using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web;
using System.Text.RegularExpressions;
using DTcms.Common;
using DTcms.Web.UI;
using LitJson;

namespace DTcms.Web.tools
{
    /// <summary>
    /// 文件上傳處理頁
    /// </summary>
    public class upload_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得處事類型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "SingleFile": //單文件
                    SingleFile(context);
                    break;
                case "MultipleFile": //多文件
                    MultipleFile(context);
                    break;
                case "AttachFile": //附件
                    AttachFile(context);
                    break;
                case "EditorFile": //編輯器檔
                    EditorFile(context);
                    break;
                case "ManagerFile": //管理文件
                    ManagerFile(context);
                    break;
            }

        }

        #region 上傳單文件處理===================================
        private void SingleFile(HttpContext context)
        {
            string _refilepath = DTRequest.GetQueryString("ReFilePath"); //取得返回的對象名稱
            string _upfilepath = DTRequest.GetQueryString("UpFilePath"); //取得上傳的對象名稱
            string _delfile = DTRequest.GetString(_refilepath);
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默認不打浮水印
            bool _isthumbnail = false; //默認不生成縮略圖
            bool _isimage = false; //預設不限制圖片上傳

            if (DTRequest.GetQueryString("IsWater") == "1")
                _iswater = true;
            if (DTRequest.GetQueryString("IsThumbnail") == "1")
                _isthumbnail = true;
            if (DTRequest.GetQueryString("IsImage") == "1")
                _isimage = true;

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"請選擇要上傳文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, _isimage, false);
            //刪除已存在的舊檔
            Utils.DeleteUpFile(_delfile);
            //返回成功資訊
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 上傳多文件處理===================================
        private void MultipleFile(HttpContext context)
        {
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上傳的對象名稱
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默認不打浮水印
            bool _isthumbnail = false; //默認不生成縮略圖

            if (context.Request.QueryString["IsWater"] == "1")
                _iswater = true;
            if (context.Request.QueryString["IsThumbnail"] == "1")
                _isthumbnail = true;

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"請選擇要上傳文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, false);
            //返回成功資訊
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 上傳附件處理=====================================
        private void AttachFile(HttpContext context)
        {
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上傳的對象名稱
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默認不打浮水印
            bool _isthumbnail = false; //默認不生成縮略圖

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"請選擇要上傳文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, false, true, false);
            //返回成功資訊
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 編輯器上傳處理===================================
        private void EditorFile(HttpContext context)
        {
            bool _iswater = false; //默認不打浮水印
            if (context.Request.QueryString["IsWater"] == "1")
                _iswater = true;
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError(context, "請選擇要上傳文件！");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string remsg = upFiles.fileSaveAs(imgFile, false, _iswater, true);
            //string pattern = @"^{\s*msg:\s*(.*)\s*,\s*msgbox:\s*\""(.*)\""\s*}$"; //鍵名前和鍵值前後都允許出現空白字元
            //Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //規則運算式實例，不區分大小寫
            //Match m = r.Match(remsg); //搜索匹配項
            //string msg = m.Groups[1].Value; //msg的值，規則運算式中第1個圓括號捕獲的值
            //string msgbox = m.Groups[2].Value; //msgbox的值，規則運算式中第2個圓括號捕獲的值 
            JsonData jd = JsonMapper.ToObject(remsg);
            string msg = jd["msg"].ToString();
            string msgbox = jd["msgbox"].ToString();
            if (msg == "0")
            {
                showError(context, msgbox);
                return;
            }
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = msgbox;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }
        //顯示錯誤
        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }
        #endregion

        #region 流覽檔處理=====================================
        private void ManagerFile(HttpContext context)
        {
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath("Configpath"));
            //String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

            //根目錄路徑，相對路徑
            String rootPath = siteConfig.webpath + siteConfig.attachpath + "/"; //網站目錄+上傳目錄
            //根目錄URL，可以指定絕對路徑，比如 http://www.yoursite.com/attached/
            String rootUrl = siteConfig.webpath + siteConfig.attachpath + "/";
            //圖片副檔名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "";
            String moveupDirPath = "";

            String dirPath = Utils.GetMapPath(rootPath);
            String dirName = context.Request.QueryString["dir"];
            //if (!String.IsNullOrEmpty(dirName))
            //{
            //    if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
            //    {
            //        context.Response.Write("Invalid Directory name.");
            //        context.Response.End();
            //    }
            //    dirPath += dirName + "/";
            //    rootUrl += dirName + "/";
            //    if (!Directory.Exists(dirPath))
            //    {
            //        Directory.CreateDirectory(dirPath);
            //    }
            //}

            //根據path參數，設置各路徑和URL
            String path = context.Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = context.Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允許使用..移動到上一級目錄
            if (Regex.IsMatch(path, @"\.\."))
            {
                context.Response.Write("Access is not allowed.");
                context.Response.End();
            }
            //最後一個字元不是/
            if (path != "" && !path.EndsWith("/"))
            {
                context.Response.Write("Parameter is not valid.");
                context.Response.End();
            }
            //目錄不存在或不是目錄
            if (!Directory.Exists(currentPath))
            {
                context.Response.Write("Directory does not exist.");
                context.Response.End();
            }

            //遍歷目錄取得檔資訊
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(result));
            context.Response.End();
        }

        #region Helper
        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }
        #endregion
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
