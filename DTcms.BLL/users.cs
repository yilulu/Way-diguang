using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public partial class users
    {
        private readonly DAL.users dal = new DAL.users();
        public users()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        public bool Exists(string user_name, int group_id, int bz)
        {
            return dal.Exists(user_name, group_id, bz);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }
        /// <summary>
        /// 检查Email是否存在并反悔用戶ID
        /// </summary>
        public int GetIDByExistsEmail(string email)
        {
            return dal.GetIDByExistsEmail(email);
        }
        /// <summary>
        /// 检查同一IP注册间隔(小时)内是否存在
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            return dal.Exists(reg_ip, regctrl);
        }

        /// <summary>
        /// 检查Email是否存在
        /// </summary>
        public bool ExistsEmail(string email)
        {
            return dal.ExistsEmail(email);
        }

        /// <summary>
        /// 返回一个随机用户名
        /// </summary>
        public string GetRandomName(int length)
        {
            string temp = Utils.Number(length, true);
            if (Exists(temp, 1))
            {
                return GetRandomName(length);
            }
            return temp;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.users model)
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
        /// 更新付费状态
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpFee(int UserID, int value)
        {
            return dal.UpFee(UserID, value);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.users GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name, string password, int emaillogin, string where = null)
        {
            return dal.GetModel(user_name, password, emaillogin, where);
        }

        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            return dal.GetModel(user_name);
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

        #region 分頁
        public DataTable list_pagesWhere(int page, int numPerPage, string whereStr, string orderStr)
        {
            return dal.list_pagesWhere(page, numPerPage, whereStr, orderStr);
        }
        #endregion

        #region 獲取總數
        public int GetTatalNum(string strWhere)
        {
            return dal.GetTatalNum(strWhere);
        }
        #endregion

        #region 扩展方法===================================
        /// <summary>
        /// 用户升级
        /// </summary>
        public bool Upgrade(int id)
        {
            if (!Exists(id))
            {
                return false;
            }
            Model.users model = GetModel(id);
            Model.user_groups groupModel = new user_groups().GetUpgrade(model.group_id, model.exp);
            if (groupModel == null)
            {
                return false;
            }
            int result = UpdateField(id, "group_id=" + groupModel.id);
            if (result > 0)
            {
                //增加积分
                if (groupModel.point > 0)
                {
                    new BLL.point_log().Add(model.id, model.user_name, groupModel.point, "升級獲得積分");
                }
                //增加金额
                if (groupModel.amount > 0)
                {
                    new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.SysGive.ToString(), groupModel.amount, "升級贈送金額", 1);
                }
            }
            return true;
        }
        #endregion

        #region 更新點數
        public int UpPoint(int Uid, int point)
        {
            return dal.UpPoint(Uid, point);
        }
        #endregion

        #region 根据用户名更新點數
        public int UpPoint(string UName, int point)
        {
            return dal.UpPoint(UName, point);
        }
        #endregion

        #region 减少點數
        public int UpJianPoint(int Uid, int point)
        {
            return dal.UpJianPoint(Uid, point);
        }
        #endregion

        #region 根据订单号减少點數
        public int UpJianPoint(string order_no, int point)
        {
            return dal.UpJianPoint(order_no, point);
        }
        #endregion

        #region 根據用戶名返回用戶信息
        public DataTable GetUser_Info(string UserName)
        {
            return dal.GetUser_Info(UserName);
        }
        #endregion

        #region 判断是否是VIP
        public bool isVip(int ID)
        {
            return dal.isVip(ID);
        }
        #endregion

        #region 规定时间未付款，降级为普通会员
        public int UpUserSetCommon(int UID)
        {
            return dal.UpUserSetCommon(UID);
        }
        #endregion

        #endregion  Method
    }
}