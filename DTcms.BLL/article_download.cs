using System;
using System.Data;
using System.Collections.Generic;

namespace DTcms.BLL
{
    /// <summary>
    /// 下载模型
    /// </summary>
    public partial class article
    {
        #region  Method
        /// <summary>
        /// 修改下载副表一列数据
        /// </summary>
        public void UpdateDownloadField(int id, string strValue)
        {
            dal.UpdateDownloadField(id, strValue);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_download model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_download model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_download GetDownloadModel(int id)
        {
            return dal.GetDownloadModel(id);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetDownloadList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetDownloadList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetDownloadList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetDownloadList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #region 分頁
        public DataTable list_pagesWheres(int page, int numPerPage, string whereStr, string orderStr)
        {
            return dal.list_pagesWheres(page, numPerPage, whereStr, orderStr);
        }
        #endregion

        #region 獲取總數
        public int GetTatalNums(string strWhere)
        {
            return dal.GetTatalNums(strWhere);
        }
        #endregion


        #region 分頁
        public DataTable listDown_page(int page, int numPerPage, string whereStr, string orderStr)
        {
            return dal.listDown_page(page, numPerPage, whereStr, orderStr);
        }
        #endregion

        #region 獲取總數
        public int GetDownTatalNum(string strWhere)
        {
            return dal.GetDownTatalNum(strWhere);
        }
        #endregion

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_download_attach GetModelDown(int id)
        {

            return dal.GetModelDown(id);
        }

        #region 把上传文件指派给会员
        public int SetFileToMember(string IDList, int DownID)
        {
            return dal.SetFileToMember(IDList, DownID);
        }
        #endregion

        
        #region 得到集合
        public string GetUidList(int DownID)
        {
            return dal.GetUidList(DownID);
        }
        #endregion

        #endregion  Method

    }
}