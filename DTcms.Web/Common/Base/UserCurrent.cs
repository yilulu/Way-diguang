using System;
using System.Collections.Generic;
using System.Web;

namespace Common
{

    /// <summary>
    /// 前臺：當前登錄使用者資訊
    /// </summary>
    public static class WEBUserCurrent
    {
        /// <summary>
        /// 判斷是否登入
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBUSERID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBUSERID"].Value))
                {

                    return false;
                }
                return true;
            }
        }
        private static int userID;
        /// <summary>
        /// 用戶編號
        /// </summary>
        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBUSERID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBUSERID"].Value))
                {
                    return -1;
                }
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["WEBUSERID"].Value);
            }
        }

        private static string userName;
        /// <summary>
        /// 用戶帳號
        /// </summary>
        public static string UserName
        {
            get
            {
                //if (HttpContext.Current.Session["UserName"] == null)
                if (HttpContext.Current.Request.Cookies["WEBUserNamecook"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBUserNamecook"].Value))
                {
                    return "";
                }
                return HttpContext.Current.Request.Cookies["WEBUserNamecook"].Value.ToString();
            }
        }

        private static string userType;
        /// <summary>
        /// 用戶類型
        /// </summary>
        public static string UserType
        {
            get
            {
                //if (HttpContext.Current.Session["UserType"] == null)
                if (HttpContext.Current.Request.Cookies["WEBUserTypecook"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBUserTypecook"].Value))
                {
                    return "";
                }
                return HttpContext.Current.Request.Cookies["WEBUserTypecook"].Value.ToString();
            }
        }

        private static string realName;
        /// <summary>
        /// 姓名
        /// </summary>
        public static string RealName
        {
            get
            {
                //if (HttpContext.Current.Session["RealName"] == null)
                if (HttpContext.Current.Request.Cookies["WEBRealNamecook"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBRealNamecook"].Value))
                {
                    return "";
                }
                return HttpContext.Current.Request.Cookies["WEBRealNamecook"].Value.ToString();
            }
        }

        private static int area;
        /// <summary>
        /// 狀態
        /// </summary>
        public static int Area
        {
            get
            {
                //if (HttpContext.Current.Session["Status"] == null)
                if (HttpContext.Current.Request.Cookies["WEBAREAID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBAREAID"].Value))
                {
                    return -1;
                }
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["WEBAREAID"].Value);
            }
        }

        private static int classID;
        /// <summary>
        /// 狀態
        /// </summary>
        public static int ClassID
        {
            get
            {
                //if (HttpContext.Current.Session["Status"] == null)
                if (HttpContext.Current.Request.Cookies["WEBClassID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBClassID"].Value))
                {
                    return -1;
                }
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["WEBClassID"].Value);
            }
        }

        private static string imageStudent;
        /// <summary>
        /// 照片
        /// </summary>
        public static string ImageStudent
        {
            get
            {
                //if (HttpContext.Current.Session["Status"] == null)
                if (HttpContext.Current.Request.Cookies["WEBIMAGEStudent"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBIMAGEStudent"].Value))
                {
                    return "";
                }
                return HttpContext.Current.Request.Cookies["WEBIMAGEStudent"].Value;
            }
        }

    }

    /// <summary>
    /// 前台：當前登入鋪導員的資料
    /// </summary>
    public static class WEBTeacCurrent
    {
        /// <summary>
        /// 判斷是否登入
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBTEACID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBTEACID"].Value))
                {

                    return false;
                }
                return true;
            }
        }
        private static int userID;
        /// <summary>
        /// 用戶編號
        /// </summary>
        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBTEACID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBTEACID"].Value))
                {
                    return -1;
                }
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["WEBTEACID"].Value);
            }
        }

        private static string teacUserName;
        /// <summary>
        /// 用戶帳號
        /// </summary>
        public static string TeacUserName
        {
            get
            {
                //if (HttpContext.Current.Session["UserName"] == null)
                if (HttpContext.Current.Request.Cookies["WEBTeacUserNamecook"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBTeacUserNamecook"].Value))
                {
                    return "";
                }
                return HttpContext.Current.Request.Cookies["WEBTeacUserNamecook"].Value.ToString();
            }
        }

        private static string teacRealName;
        /// <summary>
        /// 姓名
        /// </summary>
        public static string TeacRealName
        {
            get
            {
                //if (HttpContext.Current.Session["RealName"] == null)
                if (HttpContext.Current.Request.Cookies["WEBTeacRealNamecook"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBTeacRealNamecook"].Value))
                {
                    return "";
                }
                return HttpContext.Current.Request.Cookies["WEBTeacRealNamecook"].Value.ToString();
            }
        }

        private static int teacarea;
        /// <summary>
        /// 狀態
        /// </summary>
        public static int TeacArea
        {
            get
            {
                //if (HttpContext.Current.Session["Status"] == null)
                if (HttpContext.Current.Request.Cookies["WEBTEACAREAID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBTEACAREAID"].Value))
                {
                    return -1;
                }
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["WEBTEACAREAID"].Value);
            }
        }

        private static int teacProjectid;
        /// <summary>
        /// 培訓專案ID
        /// </summary>
        public static int TeacProjectid
        {
            get
            {
                //if (HttpContext.Current.Session["Status"] == null)
                if (HttpContext.Current.Request.Cookies["WEBTEACProjectID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBTEACProjectID"].Value))
                {
                    return -1;
                }
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["WEBTEACProjectID"].Value);
            }
        }

        private static string image;
        /// <summary>
        /// 照片
        /// </summary>
        public static string Image
        {
            get
            {
                //if (HttpContext.Current.Session["Status"] == null)
                if (HttpContext.Current.Request.Cookies["WEBIMAGE"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBIMAGE"].Value))
                {
                    return "";
                }
                return HttpContext.Current.Request.Cookies["WEBIMAGE"].Value;
            }
        }

        private static int classID;
        /// <summary>
        /// 狀態
        /// </summary>
        public static int ClassID
        {
            get
            {
                //if (HttpContext.Current.Session["Status"] == null)
                if (HttpContext.Current.Request.Cookies["WEBTeaClassID"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["WEBTeaClassID"].Value))
                {
                    return -1;
                }
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["WEBTeaClassID"].Value);
            }
        }
    }
} 
