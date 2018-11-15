using System;
using System.Collections.Generic;
using System.Text;

namespace DTcms.Model
{
    /// <summary>
    /// dt_feedback:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dt_feedback
    {
        public dt_feedback()
        { }
        #region Model
        private int _id;
        private string _title;
        private int _UserID;

      
        private string _content;
        private string _user_name;
        private string _user_tel;
        private string _user_qq;
        private string _user_email;
        private string _user_address;
        private string _user_money;
        private string _user_mianji;
        private string _user_class;
        private string _user_function;
        private DateTime _add_time = DateTime.Now;
        private string _reply_content = "";
        private DateTime _reply_time;
        private int _is_lock = 0;
        private string _zhuzhi;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_tel
        {
            set { _user_tel = value; }
            get { return _user_tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_qq
        {
            set { _user_qq = value; }
            get { return _user_qq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_email
        {
            set { _user_email = value; }
            get { return _user_email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_Address
        {
            set { _user_address = value; }
            get { return _user_address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_Money
        {
            set { _user_money = value; }
            get { return _user_money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_MianJi
        {
            set { _user_mianji = value; }
            get { return _user_mianji; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_Class
        {
            set { _user_class = value; }
            get { return _user_class; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_Function
        {
            set { _user_function = value; }
            get { return _user_function; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string reply_content
        {
            set { _reply_content = value; }
            get { return _reply_content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zhuzhi
        {
            set { _zhuzhi = value; }
            get { return _zhuzhi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime reply_time
        {
            set { _reply_time = value; }
            get { return _reply_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        #endregion Model

    }
}

