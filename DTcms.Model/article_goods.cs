using System;
using System.Collections.Generic;

namespace DTcms.Model
{
    /// <summary>
    /// 商品模型
    /// </summary>
    [Serializable]
    public partial class article_goods
    {
        public article_goods()
        { }
        #region Model
        private int _id;
        private int _channel_id = 0;
        private int _category_id = 0;
        private string _title = "";
        private string _goods_no = "";
        private int _stock_quantity = 0;
        private decimal _market_price = 0M;
        private decimal _sell_price = 0M;
        private decimal _single_price = 0M;
        private int _point = 0;
        private string _link_url = "";
        private string _img_url = "";
        private string _seo_title = "";
        private string _seo_keywords = "";
        private string _seo_description = "";
        private string _content = "";
        private int _sort_id = 99;
        private int _click = 0;
        private int _is_msg = 0;
        private int _is_top = 0;
        private int _is_red = 0;
        private int _is_hot = 0;
        private int _is_slide = 0;
        private int _is_lock = 0;
        private int _user_id = 0;
        private DateTime _add_time = DateTime.Now;
        private int _digg_good = 0;
        private int _digg_bad = 0;
        private int _isfront = 0;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 频道ID
        /// </summary>
        public int channel_id
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
        /// <summary>
        /// 类别ID
        /// </summary>
        public int category_id
        {
            set { _category_id = value; }
            get { return _category_id; }
        }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 商品货号
        /// </summary>
        public string goods_no
        {
            set { _goods_no = value; }
            get { return _goods_no; }
        }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int stock_quantity
        {
            set { _stock_quantity = value; }
            get { return _stock_quantity; }
        }
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal market_price
        {
            set { _market_price = value; }
            get { return _market_price; }
        }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal sell_price
        {
            set { _sell_price = value; }
            get { return _sell_price; }
        }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal single_price
        {
            set { _single_price = value; }
            get { return _single_price; }
        }
        /// <summary>
        /// 消费积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 外部链接
        /// </summary>
        public string link_url
        {
            set { _link_url = value; }
            get { return _link_url; }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string seo_title
        {
            set { _seo_title = value; }
            get { return _seo_title; }
        }
        /// <summary>
        /// SEO关健字
        /// </summary>
        public string seo_keywords
        {
            set { _seo_keywords = value; }
            get { return _seo_keywords; }
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string seo_description
        {
            set { _seo_description = value; }
            get { return _seo_description; }
        }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 人气
        /// </summary>
        public int click
        {
            set { _click = value; }
            get { return _click; }
        }
        /// <summary>
        /// 是否评论
        /// </summary>
        public int is_msg
        {
            set { _is_msg = value; }
            get { return _is_msg; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int is_top
        {
            set { _is_top = value; }
            get { return _is_top; }
        }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int is_red
        {
            set { _is_red = value; }
            get { return _is_red; }
        }
        /// <summary>
        /// 是否热门
        /// </summary>
        public int is_hot
        {
            set { _is_hot = value; }
            get { return _is_hot; }
        }
        /// <summary>
        /// 是否幻灯片
        /// </summary>
        public int is_slide
        {
            set { _is_slide = value; }
            get { return _is_slide; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 顶一下
        /// </summary>
        public int digg_good
        {
            set { _digg_good = value; }
            get { return _digg_good; }
        }
        /// <summary>
        /// 踩一下
        /// </summary>
        public int digg_bad
        {
            set { _digg_bad = value; }
            get { return _digg_bad; }
        }
        /// <summary>
        /// 是否前台数据
        /// </summary>
        public int isFront
        {
            set { _isfront = value; }
            get { return _isfront; }
        }
        #endregion Model

        private List<article_albums> _albums;
        /// <summary>
        /// 图片相册列表
        /// </summary>
        public List<article_albums> albums
        {
            set { _albums = value; }
            get { return _albums; }
        }

        private List<attribute_value> _attribute_values;
        /// <summary>
        /// 属性列表
        /// </summary>
        public List<attribute_value> attribute_values
        {
            set { _attribute_values = value; }
            get { return _attribute_values; }
        }

        private List<goods_group_price> _goods_group_prices;
        /// <summary>
        /// 会员组商品价格
        /// </summary>
        public List<goods_group_price> goods_group_prices
        {
            set { _goods_group_prices = value; }
            get { return _goods_group_prices; }
        }

        /// <summary>
        /// 首页摆放位置
        /// </summary>
        public string Postid { get; set; }

        /// <summary>
        ///  添加类型: 0 管理员添加   1前台会员发布
        /// </summary>
        public int AddType { get; set; }

        /// <summary>
        /// 状态:0 未审核 1：审核成功，2：深刻不通过，3成交完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 类型: 0 普通商品   1最新   2推荐   3特卖
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public int quyu { get; set; }
        /// <summary>
        /// 价格区间
        /// </summary>
        public int jiaqianQJ { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public float mianji { get; set; }
        /// <summary>
        /// 户型
        /// </summary>
        public int huxing { get; set; }
        /// <summary>
        /// 方式
        /// </summary>
        public int fangshi { get; set; }
        /// <summary>
        /// 地铁线路
        /// </summary>
        public int xianlu { get; set; }

        /// <summary>
        /// 押金
        /// </summary>
        public int yajin { get; set; }
        /// <summary>
        /// 座向
        /// </summary>
        public string zuoxiang { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string louceng { get; set; }
        /// <summary>
        /// 性能
        /// </summary>
        public string xingneng { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string yongtu { get; set; }
        /// <summary>
        /// 车位
        /// </summary>
        public string chewei { get; set; }
        /// <summary>
        /// 社区
        /// </summary>
        public string shequ { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string dizhi { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string gongsi { get; set; }
        /// <summary>
        /// 服务项目
        /// </summary>
        public string fuwuxiangju { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string dianhua { get; set; }

        /// <summary>
        /// 市区
        /// </summary>
        public int Areaid { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string lianxiren { get; set; }
        /// <summary>
        /// 新品 二手
        /// </summary>
        public string shangpinType { get; set; }
        /// <summary>
        /// 下架理由
        /// </summary>
        public string xiajialiyou { get; set; }
    }

    /// <summary>
    /// 购物车实体
    /// </summary>
    public class ShopItem
    {

        public Model.article_goods shopCart;
        public int Count;
        public ShopItem(Model.article_goods shop, int Count)
        {
            this.Count = Count;
            this.shopCart = shop;
        }
    }

 
}