using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Model;
using System.Data;

namespace DTcms.BLL
{
    public partial class AreaBll
    {
        private readonly DAL.AreaDal cd = new DAL.AreaDal();
        public AreaBll()
        { }
        #region  Method
        public List<Model.Area> CityList(int code)
        {
            Model.Area model = new Model.Area();
            List<Model.Area> list = new List<Model.Area>();
            DataTable dt = cd.GetDatalistpageByParentID(code).Tables[0]; ;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Model.Area cm = new Model.Area();
                    cm.id = int.Parse(dt.Rows[i]["id"].ToString());
                    cm.title = dt.Rows[i]["title"].ToString();
                    cm.code = dt.Rows[i]["code"].ToString();
                    list.Add(cm);
                }
            }
            return list;
        }
        #endregion  Method

        #region 分頁
        public DataTable list_page(int page, int numPerPage, string whereStr, string orderStr)
        {
            return cd.list_page(page, numPerPage, whereStr, orderStr);
        }
        #endregion
        #region 獲取總數
        public int GetNewsTatalNum(string strWhere)
        {
            return cd.GetNewsTatalNum(strWhere);
        }
        #endregion  Method

    }
}
