using System;
using System.Data;
using System.Collections.Generic;
using Ltf.Model;
namespace Ltf.BLL
{
    /// <summary>
    /// Freight
    /// </summary>
    public partial class Freight
    {
        private readonly Ltf.DAL.Freight dal = new Ltf.DAL.Freight();
        public Freight()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Ltf.Model.Freight model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Ltf.Model.Freight model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Ltf.Model.Freight GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

