using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DTcms.BLL
{
 public partial   class article_nav_BLL
    {
     private readonly DAL.article_nav_DAL dal = new DAL.article_nav_DAL();
     public article_nav_BLL() { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 返回记录总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(string n_title, string n_url, int n_state, int n_sequence, string n_desc)
        {
            Model.article_nav model = new Model.article_nav();
            model.n_title = n_title;
            model.n_url = n_url;
            model.n_state = n_state;
            model.n_sequence = n_sequence;
            model.n_desc = n_desc;
            return Add(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.article_nav model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.article_nav model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
      

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.article_nav GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }
}
