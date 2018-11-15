using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace DTcms.BLL
{
    public class Function
    {
        private static Function _instance = new Function();
        public static Function Instance
        {
            get { return _instance; }
        }

        private SymmetricAlgorithm mobjCryptoService;
        private string Key;

        public Function()
        {
            //2013-8-19 C.RU注释以下两句，加密方法采用HQAes方法，不再采用以下方法
            //mobjCryptoService = new RijndaelManaged();
            //Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
        }

        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        /// <summary>
        /// 生成随机密码
        /// </summary>
        /// <param name="intLength">密码长度</param>
        /// <param name="rndType">密码组成类型[1、纯字母，2、纯数组，3、数字+字母]</param>
        /// <returns></returns>
        public string RndPassWord(int intLength, int rndType)
        {
            System.Threading.Thread.Sleep(1);
            long tick = DateTime.Now.Ticks;
            int seed = int.Parse(tick.ToString().Substring(9));
            Random rnd = new Random(seed);
            string baseLetters = "abcdefghigklmnopqrstuvwxyz";
            string baseNumbers = "0123456789";
            string basePass = string.Empty; //密码字符构成
            switch (rndType)
            {
                case 2:
                    basePass = baseNumbers;
                    break;
                case 3:
                    basePass = baseLetters + baseNumbers;
                    break;
                default:
                    basePass = baseLetters;
                    break;
            }
            StringBuilder rndPass = new StringBuilder();
            for (int i = 0; i < intLength; i++)
            {
                rndPass = rndPass.Append(basePass.Substring(rnd.Next(0, basePass.Length - 1), 1));
            }
            return rndPass.ToString();
        }

        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetSub(string length, int num)
        {
            if (length.Length > num)
            {
                return length.Substring(0, num) + "...";
            }
            else
            {
                return length;
            }
        }
        /// <summary>
        /// 正则判断是否为数字
        /// </summary>
        /// <param name="strNumber">字符</param>
        /// <returns></returns>
        public bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// MD5加密算法
        /// </summary>
        /// <param name="str">原始字符</param>
        /// <param name="len">密文长度</param>
        /// <returns></returns>
        public string MD5(string str, int len)
        {
            if (len == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            }
            else
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").Substring(8, 16);
            }
        }
        /// <summary>
        /// 获取客户端真实Ip
        /// </summary>
        /// <returns></returns>
        public string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>
        /// 获取实际的地区及地区所处级别
        /// </summary>
        /// <param name="provinceID">省</param>
        /// <param name="cityID">市</param>
        /// <param name="countryID">区</param>
        /// <returns></returns>
        public int[] getArea(int provinceID, int cityID, int countryID)
        {
            int areaType = 0;
            int areaID = 0;
            if (provinceID != 0 && cityID == 0)
            {
                areaType = 1;
                areaID = provinceID;
            }
            else if (cityID != 0 && countryID == 0)
            {
                areaType = 2;
                areaID = cityID;
            }
            else if (countryID != 0)
            {
                areaType = 3;
                areaID = countryID;
            }
            int[] areaArr = { areaType, areaID };
            return areaArr;
        }
        /// <summary>
        /// 获取生日选择下拉框内容
        /// </summary>
        /// <param name="dateType">[year,month,day]</param>
        /// <returns></returns>
        public int[] GetBirthDay(string dateType)
        {
            if (dateType == "month")
            {
                int[] monthList = new int[12];
                for (int i = 1; i <= 12; i++)
                {
                    monthList[i - 1] = i;
                }
                return monthList;
            }
            else if (dateType == "day")
            {
                int[] dayList = new int[31];
                for (int j = 1; j <= 31; j++)
                {
                    dayList[j - 1] = j;
                }
                return dayList;
            }
            else
            {
                int beginYear = 1900, endYear = DateTime.Now.Year + 1;
                int[] yearList = new int[endYear - beginYear];
                for (int k = 0; k < endYear - beginYear; k++)
                {
                    yearList[k] = beginYear + k;
                }
                return yearList;
            }
        }
        ///   <summary>   
        ///   返回两个日期之间的时间间隔（y：年份间隔、M：月份间隔、d：天数间隔、h：小时间隔、m：分钟间隔、s：秒钟间隔、ms：微秒间隔）   
        ///   </summary>   
        ///   <param   name="Date1">开始日期</param>   
        ///   <param   name="Date2">结束日期</param>   
        ///   <param   name="Interval">间隔标志</param>   
        ///   <returns>返回间隔标志指定的时间间隔</returns>   
        public int DateDiff(System.DateTime Date1, System.DateTime Date2, string Interval)
        {
            double dblYearLen = 365;//年的长度，365天   
            double dblMonthLen = (365 / 12);//每个月平均的天数   
            System.TimeSpan objT;
            objT = Date2.Subtract(Date1);
            switch (Interval)
            {
                case "y"://返回日期的年份间隔   
                    return System.Convert.ToInt32(objT.Days / dblYearLen);
                case "M"://返回日期的月份间隔   
                    return System.Convert.ToInt32(objT.Days / dblMonthLen);
                case "d"://返回日期的天数间隔   
                    return objT.Days;
                case "h"://返回日期的小时间隔   
                    return objT.Hours;
                case "m"://返回日期的分钟间隔   
                    return objT.Minutes;
                case "s"://返回日期的秒钟间隔   
                    return objT.Seconds;
                case "ms"://返回时间的微秒间隔   
                    return objT.Milliseconds;
                default:
                    break;
            }
            return 0;
        }
        /// <summary>
        /// 读取XML配置
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <param name="node">节点名称</param>
        public string GetConfig(string path, string nodeName)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);
            XmlElement root = xdoc.DocumentElement;
            XmlNode node = root.SelectSingleNode(nodeName);
            if (node != null)
            {
                return node.InnerXml;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 字符转数字，非数字类字符则转为0
        /// </summary>
        /// <param name="num">字符</param>
        /// <returns></returns>
        public int StringToNum(string num)
        {
            int printNum = 0;
            if (!string.IsNullOrEmpty(num))
            {
                if (IsNumber(num))
                {
                    printNum = int.Parse(num);
                }
            }
            return printNum;
        }

        /// <summary>
        /// 获取Obj对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetObj(object obj)
        {
            if (obj == null)
            {
                return "未知";
            }
            else
            {
                return obj.ToString();
            }
        }
        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="NoHTML">包括HTML的源码</param>
        ///   <returns>已经去除后的文字</returns>
        public string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
                RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
                RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
                RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// 转换字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string toUFT8(string str)
        {
            string enHtml = HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("UTF-8"));
            enHtml = enHtml.Replace("+", "%20");
            return enHtml;
        }

        /// <summary>
        /// 取得与当前时间的间隔(MM月dd日 刚刚)
        /// </summary>
        public string getTimeEXTSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else if (days == 1)
            {
                strTime = "昨天" + time1.ToString("MM:dd");
            }
            else if (days == 2)
            {
                strTime = "前天" + time1.ToString("MM:dd");
            }
            else
            {
                strTime = time1.ToString("yyyy年MM月dd日");
            }
            return strTime;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public String GetEnumDesc(Enum e)
        {
            FieldInfo EnumInfo = e.GetType().GetField(e.ToString());
            DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (EnumAttributes.Length > 0)
            {
                return EnumAttributes[0].Description;
            }
            return e.ToString();
        }

        /// <summary>
        /// 处理get post参数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetParam(string str)
        {
            string print = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                print = str.Replace("'", "");
            }
            return print;
        }


        /// <summary>
        /// 获取视频播放总时长
        /// </summary>
        /// <param name="timeStr">原始时间</param>
        /// <returns></returns>
        public string GetVideoTime(string timeStr)
        {
            timeStr = timeStr.Replace("：", ":");
            string vTime = string.Empty;
            if (timeStr.IndexOf(',') > -1)
            {
                string[] vTimeArr = timeStr.Split(',');
                try
                {
                    ///timeArr={h,m,s}
                    int[] timeArr = { 0, 0, 0 };
                    for (int i = 0; i < vTimeArr.Length; i++)
                    {
                        if (vTimeArr[i].IndexOf(':') > -1)
                        {
                            int mLen = vTimeArr[i].Length - (vTimeArr[i].Replace(":", "")).Length;
                            vTimeArr[i] = mLen == 1 ? "00:" + vTimeArr[i] : vTimeArr[i];
                            string[] vTimeArrTindex = vTimeArr[i].Split(':');
                            for (int j = 0; j < vTimeArrTindex.Length; j++)
                            {
                                string timeStruff = Regex.Replace(vTimeArrTindex[j], @"[^\d.\d]", "");
                                timeStruff = timeStruff.Substring(0, 1) == "0" ? timeStruff.Substring(1, 1) : timeStruff;
                                timeArr[j] += StringToNum(timeStruff);
                                if (j == 2)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            string timeStruffAlone = Regex.Replace(vTimeArr[i], @"[^\d.\d]", "");
                            timeStruffAlone = timeStruffAlone.Substring(0, 1) == "0" ? timeStruffAlone.Substring(1, 1) : timeStruffAlone;
                            timeArr[2] += StringToNum(timeStruffAlone);
                        }
                    }
                    if (timeArr[2] >= 60)
                    {
                        timeArr[1] += timeArr[2] / 60;
                        timeArr[2] = timeArr[2] % 60;
                    }
                    if (timeArr[1] >= 60)
                    {
                        timeArr[0] += timeArr[1] / 60;
                        timeArr[1] = timeArr[1] % 60;
                    }
                    vTime = timeArr[0].ToString() + ":" + timeArr[1].ToString() + ":" + timeArr[2].ToString() + "(" + vTimeArr.Length.ToString() + "讲)";
                }
                catch
                {
                    vTime = vTimeArr[0];
                }
            }
            else
            {
                vTime = timeStr;
            }
            return vTime;
        }
    }
}
