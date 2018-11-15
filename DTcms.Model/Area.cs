using System;
using System.Collections.Generic;
using System.Text;

namespace DTcms.Model
{
    public class Area
    {
        public int id { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        public int parent { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string sort { get; set; }
    }
}
