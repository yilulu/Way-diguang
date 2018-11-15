using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace DTcms.Common
{
    public class DTMail
    {
        #region 发送电子邮件
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="smtpserver">SMTP服务器</param>
        /// <param name="userName">登录帐号</param>
        /// <param name="pwd">登录密码</param>
        /// <param name="nickName">发件人昵称</param>
        /// <param name="strfrom">发件人</param>
        /// <param name="strto">收件人</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">内容</param>
        public static string sendMail(string smtpserver, int emailport, string userName, string pwd, string nickName, string strfrom, string strto, string subj, string bodys)
        {

            try
            {
                SmtpClient _smtpClient = new SmtpClient();
                _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
                _smtpClient.Host = smtpserver;//指定SMTP服务器
                _smtpClient.UseDefaultCredentials = true;
                _smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);//用户名和密码
                _smtpClient.EnableSsl = true;
                _smtpClient.Port = emailport;
                //MailMessage _mailMessage = new MailMessage(strfrom, strto);
                MailAddress _from = new MailAddress(strfrom, nickName, Encoding.GetEncoding("GB2312"));
                MailAddress _to = new MailAddress(strto);
                MailMessage _mailMessage = new MailMessage(_from, _to);
                _mailMessage.Subject = subj;//主题
                _mailMessage.Body = bodys;//内容
                _mailMessage.SubjectEncoding = Encoding.GetEncoding("GB2312");
                _mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
                _mailMessage.IsBodyHtml = true;//设置为HTML格式
                _mailMessage.Priority = MailPriority.Normal;//优先级

                _smtpClient.Send(_mailMessage);
                return "发送成功";
            }
            catch (Exception ex)
            {
                write(ex.Message.ToString(), ex.StackTrace.ToString());
                return "发送失敗";
            }
        }
        #endregion
        public static void write(string errorMsg, string strErr)
        {
            string str = AppDomain.CurrentDomain.BaseDirectory;
            str = str.Replace("\\", "/");
            str += "Logs/" + System.DateTime.Now.Year + "/" + System.DateTime.Now.Month.ToString("00");

            if (!File.Exists(str))
            {
                Directory.CreateDirectory(str);
            }

            StreamWriter sw = File.AppendText(str + "/" + System.DateTime.Now.Year + "-" + System.DateTime.Now.Month.ToString("00") + "-" + System.DateTime.Now.Day.ToString("00") + ".log");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine(errorMsg);
            sw.WriteLine(strErr);
            sw.Flush();
            sw.Close();
        }
        #region 发送邮件
        /// <param name="strHost"> 邮件服务器</param>
        /// <param name="strAccount">发件人帐号</param>
        /// <param name="strPassword">发件人密码</param>
        /// <param name="strName">发件人名称</param>
        /// <param name="strTo">收件人帐号</param>
        /// <param name="strSubject">邮件主题</param>
        /// <param name="strBody">邮件正文</param>
        public static void SendEmail(string strHost, string strAccount, string strPassword, string strName, string strTo, string strSubject, string strBody)
        {
            SmtpClient client = new SmtpClient();
            client.Host = strHost;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(strAccount, strPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailAddress addF = new MailAddress(strAccount, strName);
            MailAddress addT = new MailAddress(strTo);
            MailMessage message = new MailMessage(addF, addT);
            message.Subject = strSubject;
            message.Body = strBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            client.Send(message);
        }
        #endregion

    }
}
