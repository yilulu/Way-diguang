using System;
using System.Data;
using System.Collections.Generic;

namespace DTcms.BLL
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public partial class ipAccess
    {
        private readonly DAL.ipAccess dal = new DAL.ipAccess();
        public ipAccess()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string IP_Address)
        {
            return dal.Exists(IP_Address);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ipAccess model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 获得全部数据
        /// </summary>
        public int GetAllCount()
        {
            return dal.GetAllCount();
        }

        /// <summary>
        /// 获得当天数据
        /// </summary>
        public int GetTodayCount()
        {
            return dal.GetTodayCount();
        }

        #endregion  Method
    }
}