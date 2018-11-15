using System;
namespace Ltf.Model
{
    /// <summary>
    /// Freight:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Freight
    {
        public Freight()
        { }
        #region Model
        private int _id;
        private int _typid;
        private decimal _totalprice;
        private decimal _fee;
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
        public int typID
        {
            set { _typid = value; }
            get { return _typid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Fee
        {
            set { _fee = value; }
            get { return _fee; }
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

