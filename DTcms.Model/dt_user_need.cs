using System;
namespace Ltf.Model
{
    /// <summary>
    /// dt_user_need:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dt_user_need
    {
        public dt_user_need()
        { }
        #region Model
        private int _id;
        private string _typename;
        private string _content;
        private string _areaname;
        private string _cataname;
        private DateTime _addtime;
        private string _mianji;
        private int _userid;
        private string _spec;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string typeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaName
        {
            set { _areaname = value; }
            get { return _areaname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CataName
        {
            set { _cataname = value; }
            get { return _cataname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MianJi
        {
            set { _mianji = value; }
            get { return _mianji; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string spec
        {
            set { _spec = value; }
            get { return _spec; }
        }
        #endregion Model

    }
}

