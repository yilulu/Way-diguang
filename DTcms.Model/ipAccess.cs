using System;

namespace DTcms.Model
{
    /// <summary>
    /// 网站访问统计:实体类
    /// </summary>
    [Serializable]
    public partial class ipAccess
    {
        private string _iP_Address;
        private DateTime _iP_DateTime;


        /// <summary>
        /// 当前IP
        /// </summary>
        public string iP_Address
        {
            get { return _iP_Address; }
            set { _iP_Address = value; }
        }
        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime iP_DateTime
        {
            get { return _iP_DateTime; }
            set { _iP_DateTime = value; }
        }
    }
}