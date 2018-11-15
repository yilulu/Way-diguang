using System;
using System.Collections.Generic;
using System.Text;

namespace DTcms.Model
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Shopcart
    {
        /// <summary>
        /// 购物车编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int ShopNumber { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductNo { get; set; }

        /// <summary>
        /// 图片名字
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
    }
}
