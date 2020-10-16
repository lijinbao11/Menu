using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMenu.Model
{
    /// <summary>
    /// 分页帮助类 
    /// </summary>
    public class PageHelper
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Field { get; set; } = "Id";
        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; } = "asc";
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Count { get; set; }
    }
}
